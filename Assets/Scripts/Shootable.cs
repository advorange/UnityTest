using Assets.Scripts.HelperClasses;
using Assets.Scripts.WeaponScripts.GunScripts.BulletScripts;
using UnityEngine;

namespace Assets.Scripts
{
	public class Shootable : MonoBehaviour
	{
		public int StartingLife = 100;
		private int _CurrentLife;

		protected virtual void Start()
		{
			_CurrentLife = StartingLife;
		}
		public void OnShot(BulletCollision bullet)
		{
			var gun = GetObjectHelper.FindGameObjectWithTag(Constants.GUN_TAG).GetComponent<Gun>();
			_CurrentLife -= gun.Damage;
			//TODO: implement life going down
		}
	}
}
