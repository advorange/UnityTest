using Assets.HelperClasses;
using System;
using UnityEngine;

namespace Assets.WeaponScripts.GunScripts.BulletScripts
{
	public class GenerateMultipleBullets : BulletCollisionEffect
	{
		public float Duration = 1.0f;
		public float Accuracy = 50.0f;
		public int ExtraBulletsCount = 1;
		public GameObject BulletType;
		public Vector3 BulletScale = new Vector3(.125f, .125f, .125f);

		private Rigidbody _RigidBody;

		protected virtual void Start()
		{
			_RigidBody = this.GetComponent<Rigidbody>();
			if (BulletType.GetComponent<GenerateMultipleBullets>())
			{
				UnityEditor.EditorApplication.isPlaying = false;
				throw new ArgumentException($"Don't use the a {nameof(GenerateMultipleBullets)} for {nameof(BulletType)}.");
			}
		}

		public override void InvokeEffects(Collision collision)
		{
			for (int i = 0; i < ExtraBulletsCount; ++i)
			{
				//Create bullet starting at spawn point and with player's rotation
				var bullet = Instantiate(BulletType, _RigidBody.position, _RigidBody.rotation);
				bullet.GetComponent<Transform>().localScale = BulletScale;

				//Give it a velocity to make it go forward with some variation
				var calcVelo = (bullet.transform.forward + BulletHelper.GenerateVelocityOffset(Accuracy)).normalized;
				bullet.GetComponent<Rigidbody>().velocity = calcVelo;

				//Get rid of the bullet eventually
				Destroy(bullet, Duration);
			}
		}
	}
}