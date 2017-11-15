using UnityEngine;

namespace Assets.Scripts
{
	internal static class Constants
	{
		public static readonly KeyCode FORWARDS_KEY = KeyCode.W;
		public static readonly KeyCode BACKWARDS_KEY = KeyCode.S;
		public static readonly KeyCode RIGHT_KEY = KeyCode.D;
		public static readonly KeyCode LEFT_KEY = KeyCode.A;

		public static readonly string AMMO_TAG = "CurrentAmmo";
		public static readonly string MAGAZINE_TAG = "MagazineSize";
		public static readonly string BULLET_TAG = "Bullet";
		public static readonly string GUN_TAG = "Gun";
	}
}
