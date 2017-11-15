using UnityEngine;

namespace Assets.Scripts.HelperClasses
{
	public static class InputHelper
	{
		public static float Horizontal => Input.GetAxis("Horizontal");
		public static float Vertical => Input.GetAxis("Vertical");
		public static float MouseScrollWheel => Input.GetAxis("Mouse ScrollWheel");

		public static bool Jump => Input.GetButton("Jump");
	}
}
