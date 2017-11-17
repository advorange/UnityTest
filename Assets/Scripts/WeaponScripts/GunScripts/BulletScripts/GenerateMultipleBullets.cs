﻿using Assets.Scripts.HelperClasses;
using System;
using UnityEngine;

namespace Assets.Scripts.WeaponScripts.GunScripts.BulletScripts
{
	public class GenerateMultipleBullets : WeaponCollisionEffect
	{
		public float Duration = 1.0f;
		public float Accuracy = 50.0f;
		public float VelocityMultiplier = .5f;
		public int ExtraBulletsCount = 1;
		public GameObject BulletType;
		public Vector3 BulletScale = new Vector3(.1f, .1f, .1f);
		public Collision Collision { get; private set; }

		protected virtual void Start()
		{
			if (this.BulletType.GetComponent<GenerateMultipleBullets>())
			{
				UnityEditor.EditorApplication.isPlaying = false;
				throw new ArgumentException($"Don't use the a {nameof(GenerateMultipleBullets)} for {nameof(this.BulletType)}.");
			}
		}

		public override void InvokeEffects(Collision collision)
		{
			if (this.Collision != null)
			{
				return;
			}

			this.Collision = collision;
			for (int i = 0; i < this.ExtraBulletsCount; ++i)
			{
				BulletHelper.CreateBullet(this);
			}
		}
	}
}