using Assets.Scripts.EditorBitMask;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.WeaponScripts.CollisionInteractionScripts
{
	/// <summary>
	/// Describes what an object should do when hit by a weapon.
	/// </summary>
	public abstract class EntityWeaponCollisionInteraction : MonoBehaviour
	{
		private static Dictionary<WeaponEffectTargets, Type> _TargetType = new Dictionary<WeaponEffectTargets, Type>
		{
			{ WeaponEffectTargets.Reflectable, typeof(Reflectable) },
			{ WeaponEffectTargets.Shootable, typeof(Shootable) },
		};

		/// <summary>
		/// Utilizes the passed in arguments to affect the <see cref="GameObject"/> this script is applied to.
		/// </summary>
		/// <param name="collision"></param>
		/// <param name="effects"></param>
		public void Interact(GameObject weapon, Collision collision, WeaponCollisionEffect[] effects)
		{
			BeforeEffects(weapon, collision);
			foreach (var effect in effects.Where(x => EffectWorksOnThisType(x)))
			{
				effect.InvokeEffects(collision);
			}
			AfterEffects(weapon, collision);
		}
		private bool EffectWorksOnThisType(WeaponCollisionEffect effect)
		{
			foreach (WeaponEffectTargets target in Enum.GetValues(typeof(WeaponEffectTargets)))
			{
				//If flag isn't set don't bother checking
				if (!effect.Targets.HasFlag(target))
				{
					continue;
				}
				//If this is derived from a set flag type then can return true
				else if (_TargetType[target].IsAssignableFrom(this.GetType()))
				{
					return true;
				}
			}
			//Return false if no flags are set or the implementation of this is not a set flag
			return false;
		}
		//Allow overriding but don't make it mandatory
		/// <summary>
		/// This method is called before any effects are used in <see cref="Interact(Collision, WeaponCollisionEffect[])"/>.
		/// </summary>
		protected virtual void BeforeEffects(GameObject source, Collision collision) { }
		/// <summary>
		/// This method is called after all effects are used in <see cref="Interact(Collision, WeaponCollisionEffect[])"/>.
		/// </summary>
		protected virtual void AfterEffects(GameObject source, Collision collision) { }
	}
}
