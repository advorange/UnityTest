using System.Linq;
using UnityEngine;

namespace Assets.Scripts.WeaponScripts.GunScripts.BulletScripts
{
	public class BulletCollision : MonoBehaviour
	{
		private Collider _Collider;
		private BulletCollisionEffect[] _CollisionEffects;

		protected virtual void Start()
		{
			_Collider = this.GetComponent<Collider>();
			_CollisionEffects = this.GetComponentsInParent<BulletCollisionEffect>();
		}
		protected virtual void OnCollisionEnter(Collision collision)
		{
			if (collision.gameObject.tag == Constants.BULLET_TAG)
			{
				Physics.IgnoreCollision(_Collider, collision.gameObject.GetComponent<Collider>());
				return;
			}

			//Shootable
			var shootable = collision.gameObject.GetComponent<Shootable>();
			if (shootable)
			{
				shootable.OnShot(this);
				foreach (var bce in _CollisionEffects.Where(x => x.Targets.HasFlag(BulletEffectTargets.Shootable)))
				{
					bce.InvokeEffects(collision);
				}
			}

			//Reflectable
			var reflectable = collision.gameObject.GetComponent<Reflectable>();
			if (reflectable)
			{
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
		}
	}
}
