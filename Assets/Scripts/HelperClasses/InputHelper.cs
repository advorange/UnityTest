using UnityEngine;

namespace Assets.Scripts.HelperClasses
{
	public static class InputHelper
	{
		public static float Horizontal(bool raw = false) => GetAxis("Horizontal", raw);
		public static float Vertical(bool raw = false) => GetAxis("Vertical", raw);
		public static float MouseScrollWheel(bool raw = false) => GetAxis("Mouse ScrollWheel", raw);
		public static float MouseX(bool raw = false) => GetAxis("Mouse X", raw);
		public static float MouseY(bool raw = false) => GetAxis("Mouse Y", raw);

		public static bool Jump => Input.GetButton("Jump");

		private static float GetAxis(string name, bool raw)
		{
			return raw ? Input.GetAxisRaw(name) : Input.GetAxis(name);
		}
	}
}
