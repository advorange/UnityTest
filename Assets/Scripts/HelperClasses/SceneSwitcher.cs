using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace Assets.Scripts.HelperClasses
{
	public class SceneSwitcher : MonoBehaviour
	{
		public string SceneName;
		public Vector3 Spawn;

		private static Scene _LoadingScene;
		private static Text _PercentLoadedText;

		private float _PercentLoaded;
		private float _PercentUnloaded;
		private bool _BusyLoading;
		private bool _BusyUnloading;
		private bool _CurrentlySwitching => this._BusyLoading || this._BusyUnloading;

		private void OnValidate()
		{
			if (!Scenes.DoesSceneExist(this.SceneName))
			{
				Debug.LogWarning($"{this.SceneName} is not a valid scene name.");
			}
		}
		private void Start()
		{
			if (_LoadingScene == default(Scene))
			{
				//StartCoroutine(LoadLoadingScene());
			}
		}
		private void Update()
		{
			if (this._CurrentlySwitching)
			{
				var minPercent = Mathf.Min(this._PercentLoaded, this._PercentUnloaded);
				_PercentLoadedText.text = $"{minPercent}% LOADED";
			}
			else if (this._PercentLoaded >= 100.0f && this._PercentUnloaded >= 100.0f)
			{
				SceneManager.SetActiveScene(Scenes.GetSceneByName(this.SceneName));
				this._PercentLoaded = 0.0f;
				this._PercentUnloaded = 0.0f;
			}
		}
		private void OnCollisionEnter(Collision collision)
		{
			if (!this._CurrentlySwitching/* && collision.gameObject.tag == Tags.Player*/)
			{
				StartCoroutine(LoadLoadingScene());
				StartCoroutine(LoadScene(this.SceneName));
				StartCoroutine(UnloadCurrentScene(SceneManager.GetActiveScene().path));
				SceneManager.SetActiveScene(_LoadingScene);
			}
		}
		private IEnumerator LoadScene(string path)
		{
			this._BusyLoading = true;
			var asyncOp = SceneManager.LoadSceneAsync(path);
			while (!asyncOp.isDone)
			{
				this._PercentLoaded = asyncOp.progress * 100.0f;
				yield return null;
			}
			this._BusyLoading = false;
			this._PercentLoaded = 100.0f;
		}
		private IEnumerator UnloadCurrentScene(string path)
		{
			this._BusyUnloading = true;
			var asyncOp = SceneManager.UnloadSceneAsync(path);
			while (!asyncOp.isDone)
			{
				this._PercentUnloaded = asyncOp.progress * 100.0f;
				yield return null;
			}
			this._BusyUnloading = false;
			this._PercentUnloaded = 100.0f;
		}
		private IEnumerator LoadLoadingScene()
		{
			var loading = SceneManager.LoadSceneAsync(Scenes.Loading, LoadSceneMode.Additive);
			yield return loading;
			_LoadingScene = Scenes.GetSceneByName(Scenes.Loading);
			_PercentLoadedText = Tags.FindGameObjectsWithTag(Tags.PercentLoaded)[0].GetComponent<Text>();
			//SceneManager.SetActiveScene(Scenes.GetSceneByName(Scenes.Test));
		}
	}
}
