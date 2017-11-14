using Assets.HelperClasses;
using UnityEngine;
using UnityEngine.UI;

namespace UnityTest.Assets
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
		public GameObject BulletType;
		public Transform BulletSpawn;
		public Material BulletSkin;
		public Vector3 BulletScale = new Vector3(.125f, .125f, .125f);

		private float _NextAllowedToFire;
		private int _CurrentMagazineAmmo;
		private Text _CurrentAmmoText;
		private Text _MagazineSizeText;

		protected virtual void Start()
		{
			_CurrentMagazineAmmo = MagazineSize;
			_CurrentAmmoText = GameObject.FindGameObjectWithTag("CurrentAmmo").GetComponent<Text>();
			_MagazineSizeText = GameObject.FindGameObjectWithTag("MagazineSize").GetComponent<Text>();
		}

		protected virtual void Update()
		{
			//Have a fire rate so all guns don't trigger stupidly fast
			if (_NextAllowedToFire > Time.time)
			{
				return;
			}

			_MagazineSizeText.text = MagazineSize.ToString();
			_CurrentAmmoText.text = _CurrentMagazineAmmo.ToString();
			if (_CurrentMagazineAmmo <= 0)
			{
				_NextAllowedToFire = Time.time + ReloadTimeInMilliseconds / 1000.0f;
				ReloadWeapon();
			}
			else if (Input.GetKey(KeyCode.Mouse0))
			{
				_CurrentMagazineAmmo -= AmmoPerShot;
				_NextAllowedToFire = Time.time + AttackRateInMilliseconds / 1000.0f;
				TriggerWeapon();
			}
		}
		protected override void TriggerWeapon()
		{
			for (int i = 0; i < BulletCount; ++i)
			{
				//TODO: make bullets ignore collisions with other bullets
				//Create bullet starting at the spawning bullet's position and rotation, give it a scale, then velocity, then destroy it
				var bullet = Instantiate(BulletType, BulletSpawn.position, Player.rotation, this.transform);
				bullet.SetBulletScale(BulletScale);
				bullet.SetBulletVelocity(Accuracy, VelocityMultiplier);
				Destroy(bullet, Duration);
			}
		}
		protected virtual void ReloadWeapon()
		{
			_CurrentMagazineAmmo = MagazineSize;
			Debug.Log("Reloading Weapon");
		}
	}
}