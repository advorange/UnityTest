using UnityEngine;

namespace UnityTest.Assets
{
	public abstract class Weapon : MonoBehaviour
	{
		public string Name;
		public float Damage = 10.0f;
		public float AttackRateInMilliseconds = 100;
		public Transform Player;

		protected abstract void TriggerWeapon();
	}
}