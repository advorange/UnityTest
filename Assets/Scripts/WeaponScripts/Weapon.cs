using UnityEngine;

namespace Assets.Scripts
{
	public abstract class Weapon : MonoBehaviour
	{
		public string Name;
		public int Damage = 10;
		public float Knockback = 1.0f;
		public float AttackRateInMilliseconds = 100;

		protected abstract void TriggerWeapon();
	}
}