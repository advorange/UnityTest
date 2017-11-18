using UnityEngine;

namespace Assets.Scripts.HelperClasses
{
	public static class ScaleHelper
	{
		/// <summary>
		/// It's possible to just unattach the child then reattach after scaling,
		/// but I'd rather do this the non cheaty way.
		/// </summary>
		/// <param name="gameObject"></param>
		public static void IgnoreParentScaling(this GameObject gameObject)
		{
			var obj = gameObject.transform;
			//Got this through trial and error so don't expect any useful comments
			var rotation = Quaternion.Inverse(obj.parent.rotation) * obj.rotation;
			var rotatedScale = rotation * obj.parent.transform.localScale;
			//Divide the child local scalings by the parent's to get rid of them
			//Abs so nothing fires from the back of the gun
			obj.localScale = obj.localScale.DivideBy(rotatedScale).AbsValue();
		}
		/// <summary>
		/// Divides every component in the first vector by the same component in the second vector.
		/// If the vectors do not have the same rotation this may send back unwanted values.
		/// </summary>
		/// <param name="numerator"></param>
		/// <param name="denominator"></param>
		/// <returns></returns>
		public static Vector3 DivideBy(this Vector3 numerator, Vector3 denominator)
		{
			return new Vector3
			{
				x = numerator.x / denominator.x,
				y = numerator.y / denominator.y,
				z = numerator.z / denominator.z,
			};
		}
		/// <summary>
		/// Returns every component in the vector as positive.
		/// </summary>
		/// <param name="v"></param>
		/// <returns></returns>
		public static Vector3 AbsValue(this Vector3 v)
		{
			return new Vector3
			{
				x = Mathf.Abs(v.x),
				y = Mathf.Abs(v.y),
				z = Mathf.Abs(v.z),
			};
		}
	}
}