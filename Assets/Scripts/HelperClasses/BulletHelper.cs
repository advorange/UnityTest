using System;
using UnityEngine;

namespace Assets.HelperClasses
{
	public static class BulletHelper
	{
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
