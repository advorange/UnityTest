using Assets.Scripts.HelperClasses;
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

		private float _NextAllowedToFire;
		private Text _CurrentAmmoText;
		private Text _MagazineSizeText;

		protected virtual void Awake()
		{
			CurrentMagazineSize = MagazineSize;
		}
		protected virtual void Start()
		{
			_CurrentAmmoText = GetObjectHelper.FindGameObjectWithTag(Constants.AMMO_TAG).GetComponent<Text>();
			_MagazineSizeText = GetObjectHelper.FindGameObjectWithTag(Constants.MAGAZINE_TAG).GetComponent<Text>();
		}
		protected virtual void Update()
		{
			//Have a fire rate so all guns don't trigger stupidly fast
			if (_NextAllowedToFire > Time.time)
			{
				return;
			}

			_MagazineSizeText.text = MagazineSize.ToString();
			_CurrentAmmoText.text = CurrentMagazineSize.ToString();
			_CurrentAmmoText.color = Color.gray;
			if (CurrentMagazineSize <= 0)
			{
				ReloadWeapon();
			}
			else if (Input.GetKey(KeyCode.Mouse0))
			{
				TriggerWeapon();
			}
		}

		protected override void TriggerWeapon()
		{
			_NextAllowedToFire = Time.time + AttackRateInMilliseconds / 1000.0f;
			CurrentMagazineSize -= AmmoPerShot;
			for (int i = 0; i < BulletCount; ++i)
			{
				//Create bullet starting at the spawning bullet's position and rotation, give it a scale, then velocity, then destroy it
				var bullet = Instantiate(BulletType, BulletSpawn.position, this.transform.parent.transform.rotation);
				bullet.SetBulletVelocity(Accuracy, VelocityMultiplier);
				bullet.transform.localScale = BulletScale;

				Destroy(bullet, Duration);
			}
		}
		protected virtual void ReloadWeapon()
		{
			_NextAllowedToFire = Time.time + ReloadTimeInMilliseconds / 1000.0f;
			CurrentMagazineSize = MagazineSize;
			_CurrentAmmoText.color = Color.red;
		}
		protected override void UpdateWithRuntimeMetadata(WeaponRuntimeMetadata metadata)
		{
			CurrentMagazineSize = metadata.CurrentMagazineAmmo;
		}
	}
}