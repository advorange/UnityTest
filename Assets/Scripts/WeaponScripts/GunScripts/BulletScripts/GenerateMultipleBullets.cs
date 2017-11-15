﻿using Assets.Scripts.HelperClasses;
using System;
using UnityEngine;

namespace Assets.Scripts.WeaponScripts.GunScripts.BulletScripts
{
	public class GenerateMultipleBullets : BulletCollisionEffect
	{
		public float Duration = 1.0f;
		public float Accuracy = 50.0f;
		public float VelocityMultiplier = .5f;
		public int ExtraBulletsCount = 1;
		public GameObject BulletType;
		public Vector3 BulletScale = new Vector3(.1f, .1f, .1f);

		private Rigidbody _RigidBody;
		private bool _AlreadyUsed;

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
			if (_AlreadyUsed)
			{
				return;
			}

			_AlreadyUsed = true;
			for (int i = 0; i < ExtraBulletsCount; ++i)
			{
				//Create bullet starting at the spawning bullet's position and rotation, give it a scale, then velocity, then destroy it
				var bullet = Instantiate(BulletType, _RigidBody.position, Quaternion.LookRotation(collision.contacts[0].normal));
				bullet.transform.localScale = BulletScale;
				bullet.SetBulletVelocity(Accuracy, VelocityMultiplier, _RigidBody.velocity.magnitude);
				Destroy(bullet, Duration);
			}
		}
	}
}