using System;
using UnityEngine;

public class Gun : Weapon
{
	public float Duration = 2.0f;
	public float Accuracy = 90.0f;
	public int VelocityMultiplier = 20;
	public int BulletCount = 1;
	public GameObject BulletSpawn;
	private GameObject Bullet;
	public Material BulletSkin;
	public Vector3 BulletScale = new Vector3(.125f, .125f, .125f);

	private int FramesSinceLastFired;
	private bool WeaponHasBeenTriggered;

	protected void Start()
	{
		Bullet = Resources.Load<GameObject>("Bullet Prefab");
	}

	protected void Update()
	{
		//Have a fire rate so all guns don't trigger stupidly fast
		if (WeaponHasBeenTriggered)
		{
			++FramesSinceLastFired;
			if (FramesSinceLastFired >= FramesPerAttack)
			{
				FramesSinceLastFired = 0;
				WeaponHasBeenTriggered = false;
			}
		}
		else if (Input.GetKey(KeyCode.Mouse0))
		{
			TriggerWeapon();
			WeaponHasBeenTriggered = true;
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
