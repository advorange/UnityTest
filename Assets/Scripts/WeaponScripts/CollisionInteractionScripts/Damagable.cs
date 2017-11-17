using Assets.Scripts.WeaponScripts.DeathSequenceScripts;
using UnityEngine;

namespace Assets.Scripts.WeaponScripts.CollisionInteractionScripts
{
	public abstract class Damagable : EntityWeaponCollisionInteraction
	{
		public float StartingLife = 100.0f;
		public float CurrentLife { get; protected set; }
		public DeathSequence DestroySequence;

		protected virtual void Awake()
		{
			this.CurrentLife = this.StartingLife;
		}
		protected override void BeforeEffects(GameObject source, Collision collision)
		{
			var damager = source.GetComponent<Damager>();
			if (!damager)
			{
				return;
			}

			this.CurrentLife -= damager.Damage;
			if (this.CurrentLife <= 0.0f)
			{
				this.DestroySequence.Kill(source, collision);
			}
		}
	}
}
