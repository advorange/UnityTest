using Assets.Scripts.EditorBitMask;
using UnityEngine;

namespace Assets.Scripts.WeaponScripts
{
	/// <summary>
	/// Describes what an object should do when hit by a weapon.
	/// </summary>
	public abstract class EntityWeaponCollisionInteraction : MonoBehaviour
	{
		/// <summary>
		/// Used to determine what <see cref="WeaponCollisionEffect"/> get activated on a collision.
		/// </summary>
		[BitMask(typeof(WeaponEffectTargets))]
		public WeaponEffectTargets Targets;

		/// <summary>
		/// Utilizes the passed in arguments to affect the <see cref="GameObject"/> this script is applied to.
		/// </summary>
		/// <param name="collision"></param>
		/// <param name="effects"></param>
		public abstract void Interact(Collision collision, WeaponCollisionEffect[] effects);
	}
}
