using UnityEngine;

namespace Assets.Scripts.WeaponScripts
{
	public class Damager : MonoBehaviour
	{
		public float Damage { get; private set; }
		private bool _Set;

		public void SetDamageValue(float damage)
		{
			if (!this._Set)
			{
				this.Damage = damage;
				this._Set = true;
			}
		}
	}
}
