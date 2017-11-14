using System.Linq;
using UnityEngine;

namespace Assets.WeaponScripts.GunScripts.BulletScripts
{
	public class BulletCollision : MonoBehaviour
	{
		private Rigidbody _RigidBody;
		private Vector3 _LastFrameVelocity;
		private BulletCollisionEffect[] _CollisionEffects;

		private void Start()
		{
			_RigidBody = this.GetComponent<Rigidbody>();
			_CollisionEffects = this.GetComponents<BulletCollisionEffect>();
		}

		private void Update()
		{
			_LastFrameVelocity = _RigidBody.velocity;
		}

		private void OnCollisionEnter(Collision collision)
		{
			var shootable = collision.gameObject.GetComponent<Shootable>();
			if (shootable)
			{
				foreach (var bce in _CollisionEffects.Where(x => x.Targets.HasFlag(BulletEffectTargets.Shootable)))
				{
					bce.InvokeEffects(collision);
				}
			}

			var reflectable = collision.gameObject.GetComponent<Reflectable>();
			if (reflectable)
			{
				//Get the angle to bounce off at (basically supplementary angle) from the contact point
				var dir = Vector3.Reflect(_LastFrameVelocity.normalized, collision.contacts[0].normal);
				//Shoot the bullet back out in that direction with its same magnitude
				_RigidBody.velocity = dir * _LastFrameVelocity.magnitude;

				foreach (var bce in _CollisionEffects.Where(x => x.Targets.HasFlag(BulletEffectTargets.Reflectable)))
				{
					bce.InvokeEffects(collision);
				}
			}

			//Anything
			foreach (var bce in _CollisionEffects.Where(x => x.Targets == default(BulletEffectTargets)))
			{
				bce.InvokeEffects(collision);
			}
			Destroy(this);
		}
	}
}
