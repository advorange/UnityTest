using UnityEngine;

namespace Assets.Scripts.WeaponScripts.GunScripts.BulletScripts
{
	public class RemoveBullet : BulletCollisionEffect
	{
		public override void InvokeEffects(Collision collision)
		{
			Destroy(this.gameObject);
		}
	}
}
