using Assets.Scripts.HelperClasses;
using Assets.Scripts.WeaponScripts;
using UnityEngine;

namespace Assets.Scripts
{
	public class Shootable : Damagable
	{
		public override void Interact(Collision collision, WeaponCollisionEffect[] effects)
		{
			var weapon = GetObjectHelper.GetPlayer().GetComponent<WeaponHolder>().GetCurrentlyHeldWeapon();
			_CurrentLife -= weapon.Damage;
			foreach (var collisionEffect in effects.GetCollisionEffects(Targets))
			{
				collisionEffect.InvokeEffects(collision);
			}
		}
	}
}
