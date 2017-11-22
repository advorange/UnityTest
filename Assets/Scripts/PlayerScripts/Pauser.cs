using Assets.Scripts.HelperClasses;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
	public class Pauser : MonoBehaviour
	{
		public Canvas PauseCanvas;

		private void Awake()
		{
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
		private void Start()
		{
			foreach (var obj in Tags.FindChildrenWithTag(this.gameObject, Tags.Pause))
			{
				var canvas = obj.GetComponent<Canvas>();
				if (canvas)
				{
					this.PauseCanvas = canvas;
					break;
				}
			}
		}
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				//Actually freeze the game
				if (GetObjectHelper.GetPlayers().Length == 1)
				{
					Time.timeScale = Time.timeScale.Equals(1.0f) ? 0.0f : 1.0f;
				}

				//Let the mouse do whatever now
				Cursor.visible = !Cursor.visible;
				Cursor.lockState = Cursor.visible ? CursorLockMode.None : CursorLockMode.Locked;

				//Bring up the pause menu
				this.PauseCanvas.enabled = !this.PauseCanvas.enabled;
			}
		}
	}
}