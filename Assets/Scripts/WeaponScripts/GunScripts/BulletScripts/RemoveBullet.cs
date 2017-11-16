using UnityEngine;

namespace Assets.Scripts.WeaponScripts.GunScripts.BulletScripts
{
	public class RemoveBullet : WeaponCollisionEffect
	{
		public override void InvokeEffects(Collision collision)
		{
			Destroy(this.gameObject);
		}
	}
}
