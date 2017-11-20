using Assets.Scripts.HelperClasses;
using UnityEngine;

namespace Assets.Scripts.WeaponScripts.GunScripts.BulletScripts
{
	public class ReflectBullet : WeaponCollisionEffect
	{
		public float VelocityMultiplier = .5f;

		private Rigidbody _RigidBody;
		private Vector3 _LastFrameVelocity;

		protected virtual void Start()
		{
			this._RigidBody = this.GetComponent<Rigidbody>();
		}
		protected virtual void Update()
		{
			this._LastFrameVelocity = this._RigidBody.velocity;
		}

		public override void InvokeEffects(Collision collision)
		{
			//Shoot the bullet back out in that direction with an updated magnitude
			this._RigidBody.velocity = this._LastFrameVelocity.Reflect(collision) * this.VelocityMultiplier;
		}
	}
}
