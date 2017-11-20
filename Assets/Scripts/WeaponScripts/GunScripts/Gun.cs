﻿using Assets.Scripts.HelperClasses;
using Assets.Scripts.WeaponScripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
	public class Gun : Weapon
	{
		public float Duration = 2.0f;
		public float Accuracy = 90.0f;
		public float ReloadTimeInMilliseconds = 1000;
		public int VelocityMultiplier = 20;
		public int BulletCount = 1;
		public int AmmoPerShot = 1;
		public int MagazineSize = 10;
		public int CurrentMagazineSize { get; private set; }
		public GameObject BulletType;
		public Transform BulletSpawn;
		public Material BulletSkin;
		public Vector3 BulletScale = new Vector3(.125f, .125f, .125f);

		private Text _CurrentAmmoText;
		private Text _MagazineSizeText;

		private void Awake()
		{
			this.CurrentMagazineSize = this.MagazineSize;
		}
		private void Start()
		{
			this._CurrentAmmoText = GetObjectHelper.FindGameObjectsWithTag(Tags.Ammo)[0].GetComponent<Text>();
			this._MagazineSizeText = GetObjectHelper.FindGameObjectsWithTag(Tags.Magazine)[0].GetComponent<Text>();
		}

		protected override void UseWeapon()
		{
			if (this.CurrentMagazineSize <= 0)
			{
				ReloadWeapon();
			}
			else if (Input.GetKey(KeyCode.Mouse0))
			{
				FireBullets();
			}
		}
		protected override void UpdateUI()
		{
			this._MagazineSizeText.text = this.MagazineSize.ToString();
			this._CurrentAmmoText.text = this.CurrentMagazineSize.ToString();
			this._CurrentAmmoText.color = Color.gray;
		}
		private void ReloadWeapon()
		{
			this.NextAllowedToAttack = Time.time + this.ReloadTimeInMilliseconds / 1000.0f;
			this._CurrentAmmoText.color = Color.red;
			this.CurrentMagazineSize = this.MagazineSize;
		}
		private void FireBullets()
		{
			this.NextAllowedToAttack = Time.time + this.AttackRateInMilliseconds / 1000.0f;
			this.CurrentMagazineSize -= this.AmmoPerShot;
			for (int i = 0; i < this.BulletCount; ++i)
			{
				BulletHelper.CreateBullet(this);
			}
		}
	}
}