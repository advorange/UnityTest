using Assets.Scripts.HelperClasses;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
	public class CameraMovement : MonoBehaviour
	{
		public float MoveSpeed = 10.0f;
		public Vector3 Offset = new Vector3(0, 1, -5);
		public Transform Player;

		private void Awake()
		{
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				Cursor.visible = !Cursor.visible;
				Cursor.lockState = Cursor.visible ? CursorLockMode.None : CursorLockMode.Locked;
			}
			if (!InputHelper.MouseX().Equals(0.0f))
			{
				var x = InputHelper.MouseX() * Time.deltaTime * this.MoveSpeed * (Screen.currentResolution.width / 500.0f);
				this.Player.rotation = CreateNewRotation(this.Player.rotation.eulerAngles, x, 1);
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
		private Quaternion CreateNewRotation(Vector3 euler, float val, int index)
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