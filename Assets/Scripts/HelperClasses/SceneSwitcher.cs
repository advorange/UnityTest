using Assets.Scripts.PlayerScripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.HelperClasses
{
	public class SceneSwitcher : Interactable
	{
		public string SceneName;

		private bool _BusyLoading;

		private void OnValidate()
		{
			this.Text = $"go to {this.SceneName}";
			if (!Scenes.DoesSceneExist(this.SceneName))
			{
				Debug.LogWarning($"{this.SceneName} is not a valid scene name.");
			}
		}
		public override void Interact()
		{
			if (!this._BusyLoading)
			{
				StartCoroutine(LoadScene(this.SceneName));
			}
		}
		private IEnumerator LoadScene(string path)
		{
			this._BusyLoading = true;

			//Start displaying the loading scene
			//Additive because only gets shown on top and the new scene deletes everything anyways
			var loadingSceneOp = SceneManager.LoadSceneAsync(Scenes.Loading, LoadSceneMode.Additive);
			while (!loadingSceneOp.isDone)
			{
				yield return null;
			}
			//yield return null here so that the scene is loaded otherwise getting objects returns null
			var loading = GameObject.FindGameObjectWithTag(Tags.Loading);
			var slider = loading.GetComponent<Slider>();
			var text = loading.GetComponentInChildren<Text>();

			//Single because the previous scenes are unimportant
			//I guess it would be nice to maybe cache the loading scene, but I don't know how
			var newSceneOp = SceneManager.LoadSceneAsync(path, LoadSceneMode.Single);
			//Don't want it to be shown too quickly
			newSceneOp.allowSceneActivation = false;
			yield return new WaitForSeconds(.1f);

			//Update until the scene is done loading
			while (!newSceneOp.isDone)
			{
				var progress = Mathf.Clamp01(newSceneOp.progress / 0.9f);
				slider.value = progress;
				text.text = $"{progress * 100}%";

				//After all the initial loading has been done for the scene allow it to delete the old scenes
				if (progress.Equals(1.0f))
				{
					this._BusyLoading = false;
					newSceneOp.allowSceneActivation = true;
				}
				yield return new WaitForSeconds(.1f);
			}
		}
	}
}
