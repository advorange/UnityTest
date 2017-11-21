using Assets.Scripts.WeaponScripts;
using Assets.Scripts.WeaponScripts.GunScripts.BulletScripts;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.HelperClasses
{
	public static class BulletHelper
	{
		public static GameObject CreateBullet(Gun gun)
		{
			var gunRotation = gun.transform.parent.transform.rotation;
			return CreateBullet
			(
				gun.Player,
				gun.BulletType, 
				gun.BulletSpawn.position,
				gunRotation, 
				gun.BulletScale,
				gun.Accuracy, 
				gun.Damage, 
				gun.Duration, 
				gun.VelocityMultiplier
			);
		}
		public static GameObject CreateBullet(GenerateBullets bullet)
		{
			//TODO: see why not generating extra bullets
			var bulletRB = bullet.GetComponent<Rigidbody>();
			var bulletDamager = bullet.GetComponent<Damager>();
			var bulletRotation = Quaternion.LookRotation(bullet.Collision.contacts[0].normal);
			return CreateBullet
			(
				bulletDamager.Player,
				bullet.BulletType,
				bulletRB.position,
				bulletRotation,
				bullet.BulletScale,
				bullet.Accuracy,
				bulletDamager.Damage,
				bullet.Duration,
				bullet.VelocityMultiplier, bulletRB.velocity.magnitude
			);
		}
		private static GameObject CreateBullet(GameObject player, GameObject bulletType, Vector3 spawnPosition, Quaternion rotation, Vector3 bulletScale,
			float accuracy, float damage, float duration, params float[] velocityMultipliers)
		{
			//Create at position w/ rotation
			var bullet = UnityEngine.Object.Instantiate(bulletType, spawnPosition, rotation);
			//Give it speed
			bullet.SetBulletVelocity(accuracy, velocityMultipliers);
			//Give it the correct size
			bullet.transform.localScale = bulletScale;
			//Give it the damager component so when it strikes things it can pass damage
			var damager = bullet.AddComponent<Damager>();
			damager.Damage = damage;
			damager.Player = player;
			//Destroy after the passed in time
			UnityEngine.Object.Destroy(bullet, duration);
			return bullet;
		}
		/// <summary>
		/// Sets the <see cref="Rigidbody.velocity"/> of <paramref name="bullet"/> 
		/// </summary>
		/// <param name="bullet"></param>
		/// <param name="accuracy"></param>
		/// <param name="velocityMultipliers"></param>
		public static void SetBulletVelocity(this GameObject bullet, float accuracy, params float[] velocityMultipliers)
		{
			//Give it a velocity to make it go outwards with some variation
			var calcVelo = (bullet.transform.forward + GenerateVectorOffset(accuracy)).normalized;
			var multipliers = velocityMultipliers.Aggregate(1.0f, (a, b) => a * b);
			bullet.GetComponent<Rigidbody>().velocity = calcVelo * multipliers;
		}
		/// <summary>
		/// Reflects the velocity off of the normal of the first contact point.
		/// </summary>
		/// <param name="velocity"></param>
		/// <param name="collision"></param>
		/// <returns></returns>
		public static Vector3 Reflect(this Vector3 velocity, Collision collision)
		{
			//Get the angle to bounce off at (basically supplementary angle) from the contact point
			return Vector3.Reflect(velocity, collision.contacts[0].normal);
		}
		/// <summary>
		/// Generates a random offset for bullets to appear from.
		/// </summary>
		/// <param name="accuracy"></param>
		/// <returns></returns>
		public static Vector3 GenerateVectorOffset(float accuracy)
		{
			//Only allow values between 0 and 100
			accuracy = Math.Min(100.0f, Math.Max(0.0f, accuracy));
			//Generate a spread from accuracy, e.g. if accuracy is 85.0f then margin is .15
			var largeSpread = (100.0f - accuracy) / 100.0f;
			//Make the spread smaller (otherwise even 90 accuracy looks very inaccurate)
			var spread = largeSpread * .2f;

			//Only affect the x and y since this gets applied to the forward transform and guns generally don't shoot backwards.
			var offsetCoord = GenerateOffsetCoordinate(spread);
			return new Vector3(offsetCoord.x, offsetCoord.y);
		}
		/// <summary>
		/// Multiplied a <see cref="Vector2"/> by the spread.
		/// If <paramref name="circle"/> is true, the used vector is a random value from inside a circle.
		/// If <paramref name="circle"/> is false, the used vector is a random value from inside a square.
		/// </summary>
		/// <param name="spread"></param>
		/// <param name="circle"></param>
		/// <returns></returns>
		public static Vector2 GenerateOffsetCoordinate(float spread, bool circle = true)
		{
			return circle ? (UnityEngine.Random.insideUnitCircle * spread) : new Vector2
			{
				x = spread - UnityEngine.Random.Range(0, 2 * spread),
				y = spread - UnityEngine.Random.Range(0, 2 * spread),
			};
		}
	}
}
