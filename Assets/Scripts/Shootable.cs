using Assets.WeaponScripts.GunScripts.BulletScripts;
using UnityEngine;

namespace Assets
{
	public class Shootable : MonoBehaviour
	{
		public int StartingLife = 100;
		private int _CurrentLife;

		public void OnShot(BulletCollision bullet)
		{
			//TODO: implement life going down
		}
	}
}
