using Assets.Scripts.HelperClasses;
using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
	public class CameraMovement : MonoBehaviour
	{
		public float MoveSpeed = 10.0f;
		public Vector3 Offset = new Vector3(0, 1, -5);
		public Transform Player;

		private void Start()
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
			}
			if (!InputHelper.MouseX().Equals(0.0f))
			{
				var x = InputHelper.MouseX() * Time.deltaTime * this.MoveSpeed;
				var curAngles = this.Player.rotation.eulerAngles;
				this.Player.rotation = Quaternion.Euler(curAngles.x, curAngles.y + x, curAngles.z);
			}
			/*if (!InputHelper.MouseY().Equals(0.0f))
			{
				var y = InputHelper.MouseY() * Time.deltaTime * this.MoveSpeed;
				var curAngles = this.Player.rotation.eulerAngles;
				this.Player.rotation = Quaternion.Euler(curAngles.x + y, curAngles.y, curAngles.z);
			}*/

			this.transform.position = this.Player.position + this.Player.rotation * this.Offset;
			this.transform.rotation = this.Player.rotation;
		}
	}
}