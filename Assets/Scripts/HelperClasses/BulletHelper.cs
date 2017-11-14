using System;
using System.Linq;
using UnityEngine;

namespace Assets.HelperClasses
{
	public static class BulletHelper
	{
		public static void SetBulletScale(this GameObject bullet, Vector3 scale)
		{
			//Scale up the vector by the inverse of each parent's size so when local scale scales it down it is the correct size
			var parentScale = bullet.GetComponentInParent<Transform>().lossyScale;
			bullet.GetComponent<Transform>().localScale = new Vector3
			{
				x = scale.x * (1 / parentScale.x),
				y = scale.y * (1 / parentScale.y),
				z = scale.z * (1 / parentScale.z),
			};
		}
		public static void SetBulletVelocity(this GameObject bullet, float accuracy, params float[] velocityMultipliers)
		{
			//Give it a velocity to make it go outwards with some variation
			var calcVelo = (bullet.transform.forward + GenerateVelocityOffset(accuracy)).normalized;
			//Add in all the mulitpliers
			velocityMultipliers.ToList().ForEach(x => calcVelo *= x);
			bullet.GetComponent<Rigidbody>().velocity = calcVelo;
		}
		public static Vector3 GenerateVelocityOffset(float accuracy)
		{
			//Only allow values between 0 and 100
			accuracy = Math.Min(100.0f, Math.Max(0.0f, accuracy));
			//Generate a spread from accuracy, e.g. if accuracy is 85.0f then margin is .15
			var largeSpread = (100.0f - accuracy) / 100.0f;
			//Make the spread smaller (otherwise even 90 accuracy looks very inaccurate)
			var spread = largeSpread * .2f;

			//Only affect the x and y since this gets applied to the forward transform and guns generally don't shoot backwards.
			var offsetPoint = GenerateOffsetPoint(spread);
			return new Vector3(offsetPoint.x, offsetPoint.y);
		}
		public static Vector2 GenerateOffsetPoint(float spread)
		{
//Circle
#if true
			return UnityEngine.Random.insideUnitCircle * spread;
//Square
#else
			return new Vector2
			{
				x = spread - UnityEngine.Random.Range(0, 2 * spread),
				y = spread - UnityEngine.Random.Range(0, 2 * spread),
			};
#endif
		}
	}
}
