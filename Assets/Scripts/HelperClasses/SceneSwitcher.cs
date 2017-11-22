using Assets.Scripts.PlayerScripts;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.HelperClasses
{
	public class SceneSwitcher : Interactable
	{
		public string SceneName;

		private float _PercentLoaded;
		private bool _LoadFinished;
		private bool _BusyLoading;

		private void OnValidate()
		{
			this.Text = $"go to {this.SceneName}";
			if (!Scenes.DoesSceneExist(this.SceneName))
			{
				Debug.LogWarning($"{this.SceneName} is not a valid scene name.");
			}
		}
		private void Update()
		{
			if (this._BusyLoading)
			{
				Debug.Log($"{this._PercentLoaded}% LOADED");
			}
			else if (this._LoadFinished)
			{
				SceneManager.SetActiveScene(Scenes.GetSceneByName(this.SceneName));
				this._PercentLoaded = 0.0f;
				this._LoadFinished = false;
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
			var asyncOp = SceneManager.LoadSceneAsync(path, LoadSceneMode.Single);
			while (!asyncOp.isDone)
			{
				this._PercentLoaded = asyncOp.progress * 100.0f;
				yield return null;
			}
			this._BusyLoading = false;
			this._LoadFinished = true;
			this._PercentLoaded = 100.0f;
		}
	}
}
