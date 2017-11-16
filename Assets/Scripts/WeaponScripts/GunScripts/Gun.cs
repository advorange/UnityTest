using Assets.Scripts.HelperClasses;
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
			CurrentMagazineSize = MagazineSize;
		}
		private void Start()
		{
			_CurrentAmmoText = GetObjectHelper.FindGameObjectWithTag(Tags.Ammo).GetComponent<Text>();
			_MagazineSizeText = GetObjectHelper.FindGameObjectWithTag(Tags.Magazine).GetComponent<Text>();
		}

		protected override void UseWeapon()
		{
			if (CurrentMagazineSize <= 0)
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
			_MagazineSizeText.text = MagazineSize.ToString();
			_CurrentAmmoText.text = CurrentMagazineSize.ToString();
			_CurrentAmmoText.color = Color.gray;
		}
		private void ReloadWeapon()
		{
			NextAllowedToAttack = Time.time + ReloadTimeInMilliseconds / 1000.0f;
			_CurrentAmmoText.color = Color.red;
			CurrentMagazineSize = MagazineSize;
		}
		private void FireBullets()
		{
			NextAllowedToAttack = Time.time + AttackRateInMilliseconds / 1000.0f;
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
	}
}