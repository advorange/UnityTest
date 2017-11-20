using Assets.Scripts.EditorBitMask;
using UnityEngine;

namespace Assets.Scripts.WeaponScripts
{
	public abstract class WeaponCollisionEffect : MonoBehaviour
	{
		/// <summary>
		/// When this bullet hits an object of a given type <see cref="InvokeEffects(Collision)"/> will occur.
		/// </summary>
		[BitMask(typeof(WeaponEffectTargets))]
		public WeaponEffectTargets Targets;

		/// <summary>
		/// Runs the effect, which can be something such as generating more bullets.
		/// </summary>
		/// <param name="collision"></param>
		public abstract void InvokeEffects(Collision collision);
	}
}
