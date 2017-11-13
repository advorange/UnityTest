using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Gun : Weapon
{
	public float Duration = 2.0f;
	public float Accuracy = 90.0f;
	public float ReloadTimeInMilliseconds = 1000;
	public int VelocityMultiplier = 20;
	public int BulletCount = 1;
	public int AmmoPerShot = 1;
	public int MagazineSize = 10;
	public GameObject BulletSpawn;
	public Material BulletSkin;
	public Vector3 BulletScale = new Vector3(.125f, .125f, .125f);

	private float NextAllowedToFire;
	private int CurrentMagazineAmmo;
	private GameObject Bullet;

	private static Text CurrentAmmoText;
	private static Text MagazineSizeText;
	private static bool UILoaded;

	protected virtual void Start()
	{
		CurrentMagazineAmmo = MagazineSize;
		Bullet = Resources.Load<GameObject>("Bullet Prefab");
		if (!UILoaded)
		{
			CurrentAmmoText = GameObject.FindGameObjectWithTag("CurrentAmmo").GetComponent<Text>();
			MagazineSizeText = GameObject.FindGameObjectWithTag("MagazineSize").GetComponent<Text>();
			UILoaded = true;
		}
	}

	protected virtual void Update()
	{
		//Have a fire rate so all guns don't trigger stupidly fast
		if (NextAllowedToFire > Time.time)
		{
			return;
		}

		MagazineSizeText.text = MagazineSize.ToString();
		CurrentAmmoText.text = CurrentMagazineAmmo.ToString();
		if (CurrentMagazineAmmo <= 0)
		{
			NextAllowedToFire = Time.time + ReloadTimeInMilliseconds / 1000.0f;
			ReloadWeapon();
		}
		else if (Input.GetKey(KeyCode.Mouse0))
		{
			CurrentMagazineAmmo -= AmmoPerShot;
			NextAllowedToFire = Time.time + AttackRateInMilliseconds / 1000.0f;
			TriggerWeapon();
		}
	}
	protected override void TriggerWeapon()
	{
		for (int i = 0; i < BulletCount; ++i)
		{
			//Create bullet starting at spawn point and with player's rotation
			var bullet = Instantiate(Bullet, BulletSpawn.transform.position, Player.transform.rotation);
			bullet.GetComponent<Transform>().localScale = BulletScale;

			//Give it a velocity to make it go forward with some variation
			var calcVelo = (bullet.transform.forward + GenerateVelocityOffset()).normalized;
			bullet.GetComponent<Rigidbody>().velocity = calcVelo * VelocityMultiplier;

			//Get rid of the bullet eventually
			Destroy(bullet, Duration);
		}
	}
	protected virtual void ReloadWeapon()
	{
		CurrentMagazineAmmo = MagazineSize;
		Debug.Log("Reloading Weapon");
	}
	protected Vector3 GenerateVelocityOffset()
	{
		//Only allow values between 0 and 100
		var accuracy = Math.Min(100.0f, Math.Max(0.0f, Accuracy));
		//Generate a spread from accuracy, e.g. if accuracy is 85.0f then margin is .15
		var largeSpread = (100.0f - accuracy) / 100.0f;
		//Make the spread smaller (otherwise even 90 accuracy looks very inaccurate)
		var spread = largeSpread * .2f;

		//Only affect the x and y since this gets applied to the forward transform
		//and guns generally don't shoot backwards.
		return new Vector3(GenerateOffsetPoint(spread), GenerateOffsetPoint(spread));
	}
	protected float GenerateOffsetPoint(float spread)
	{
		return spread - UnityEngine.Random.Range(0, 2 * spread);
	}
}