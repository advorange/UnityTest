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
				var x = InputHelper.MouseX() * Time.deltaTime * this.MoveSpeed * (Screen.currentResolution.width / 500.0f);
				this.Player.rotation = CreateNewRotation(this.Player.rotation.eulerAngles, x);
			}
			/*if (!InputHelper.MouseY().Equals(0.0f))
			{
				var y = InputHelper.MouseY() * Time.deltaTime * this.MoveSpeed;
				var curAngles = this.Player.rotation.eulerAngles;
				this.Player.rotation = Quaternion.Euler(curAngles.x + y, curAngles.y, curAngles.z);
			}*/

			this.transform.position = this.Player.position + (this.Player.rotation * this.Offset);
			this.transform.rotation = this.Player.rotation;
		}
		private Quaternion CreateNewRotation(Vector3 euler, float val, int index = 1)
		{
			var v = new Vector3();
			for (int i = 0; i < 3; ++i)
			{
				v[i] = euler[i];
				if (i == index)
				{
					v[i] += val;
				}
			}
			return Quaternion.Euler(v);
		}
	}
}