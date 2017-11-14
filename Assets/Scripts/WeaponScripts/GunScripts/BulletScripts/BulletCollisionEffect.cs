using Assets.EditorBitMask;
using UnityEngine;

namespace Assets.WeaponScripts.GunScripts.BulletScripts
{
	public abstract class BulletCollisionEffect : MonoBehaviour
	{
		/// <summary>
		/// When this bullet hits an object of a given type <see cref="InvokeEffects(Collision)"/> will occur.
		/// </summary>
		[BitMask(typeof(BulletEffectTargets))]
		public BulletEffectTargets Targets;

		/// <summary>
		/// Runs the effect, which can be something such as generating more bullets.
		/// </summary>
		/// <param name="collision"></param>
		public abstract void InvokeEffects(Collision collision);
	}
}