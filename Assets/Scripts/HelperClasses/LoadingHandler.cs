using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.HelperClasses
{
	public class LoadingHandler : MonoBehaviour
	{
		private Canvas _LoadingCanvas;

		private void Start()
		{
			this._LoadingCanvas = this.GetComponent<Canvas>();
			SceneManager.sceneUnloaded += this.SceneUnloaded;
		}

		private void SceneUnloaded(Scene arg0)
		{
			this._LoadingCanvas.enabled = false;
		}
	}
}
