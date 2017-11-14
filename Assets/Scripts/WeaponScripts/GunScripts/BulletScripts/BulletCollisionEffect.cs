using Assets.EditorBitMask;
using UnityEngine;

namespace Assets.WeaponScripts.GunScripts.BulletScripts
{
	public abstract class BulletCollisionEffect : MonoBehaviour
	{
		[BitMask(typeof(BulletEffectTargets))]
		public BulletEffectTargets Targets;

		public abstract void InvokeEffects(Collision collision);
	}
}