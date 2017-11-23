using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.HelperClasses
{
	public class SceneSwitcher : Interactable
	{
		[ReadOnly]
		public string SceneName;
		public bool UnloadThisScene = true;
		public bool TieTextToSceneName = true;

		private bool _BusyLoading;
		private Scene _LoadingScene;
		private Canvas _LoadingCanvas;
		private Slider _LoadingSlider;
		private Text _LoadingText;

		private void Awake()
		{
			StartCoroutine(GetOrCreateLoadingScene());
		}

		public override void Interact()
		{
			if (!this._BusyLoading)
			{
				StartCoroutine(LoadScene(this.SceneName));
			}
		}
		private IEnumerator GetOrCreateLoadingScene()
		{
			this._BusyLoading = true;
			this._LoadingScene = SceneManager.GetSceneByName(Scenes.Loading);
			if (this._LoadingScene == default(Scene))
			{
				//Additive so it doesn't delete anything
				var loadingSceneOp = SceneManager.LoadSceneAsync(Scenes.Loading, LoadSceneMode.Additive);
				while (!loadingSceneOp.isDone)
				{
					yield return null;
				}
				this._LoadingScene = SceneManager.GetSceneByName(Scenes.Loading);
			}
			var loading = GameObject.FindGameObjectWithTag(Tags.Loading);
			this._LoadingCanvas = loading.GetComponentInChildren<Canvas>();
			this._LoadingSlider = loading.GetComponentInChildren<Slider>();
			this._LoadingText = loading.GetComponentInChildren<Text>();

			//Hide the scene
			this._LoadingCanvas.enabled = false;
			this._BusyLoading = false;
		}
		private IEnumerator LoadScene(string path)
		{
			//Start displaying the loading scene
			this._BusyLoading = true;
			this._LoadingCanvas.enabled = true;

			//Don't want the new scene to be shown too quickly
			//Additive in case the old scene needs to be kept around
			//Also so loading scene doesn't get deleted each time
			var newSceneOp = SceneManager.LoadSceneAsync(path, LoadSceneMode.Additive);
			newSceneOp.allowSceneActivation = false;
			yield return new WaitForSeconds(.1f);

			//Update loading scene until the new scene is done loading
			while (!newSceneOp.isDone)
			{
				var progress = Mathf.Clamp01(newSceneOp.progress / 0.9f);
				this._LoadingSlider.value = progress;
				this._LoadingText.text = $"{progress * 100}%";

				//After all the initial loading has been done for the new scene allow it to exit this loop
				yield return new WaitForSeconds(.1f);
				if (progress.Equals(1.0f))
				{
					this._BusyLoading = false;
					newSceneOp.allowSceneActivation = true;
					break;
				}
			}

			//Only unload this scene if it's unwanted
			if (this.UnloadThisScene)
			{
				var unloadOp = SceneManager.UnloadSceneAsync(this.gameObject.scene);
				while (!unloadOp.isDone)
				{
					yield return null;
				}
			}
			//If the scene is not unloaded then the loading canvas has to be disabled manually
			else
			{
				this._LoadingCanvas.enabled = false;
			}
		}
	}
}