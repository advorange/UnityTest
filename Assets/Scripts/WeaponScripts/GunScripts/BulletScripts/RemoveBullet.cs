using Assets.Scripts.HelperClasses;
using UnityEngine;

namespace Assets.Scripts.WeaponScripts.GunScripts.BulletScripts
{
	public class RemoveBullet : BulletCollisionEffect
	{
		public float VelocityMultiplier = .5f;

		private Rigidbody _RigidBody;
		private Vector3 _LastFrameVelocity;

		protected virtual void Start()
		{
			_RigidBody = this.GetComponent<Rigidbody>();
		}
		protected virtual void Update()
		{
			_LastFrameVelocity = _RigidBody.velocity;
		}

		public override void InvokeEffects(Collision collision)
		{
			//Shoot the bullet back out in that direction with an updated magnitude
			_RigidBody.velocity = _LastFrameVelocity.Reflect(collision) * VelocityMultiplier;
		}
	}
}
