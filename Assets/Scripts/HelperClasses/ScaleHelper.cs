using UnityEngine;

namespace Assets.Scripts.HelperClasses
{
	public static class ScaleHelper
	{
		public static void IgnoreParentScaling(this GameObject gameObject)
		{
			var obj = gameObject.transform;
			//Make the parent's local scale has the same angle as the child's rotation
			var parentScale = obj.rotation * obj.parent.transform.localScale;
			//Add another 180 onto those values so they are facing the correct way
			var reflected = Vector3.Reflect(parentScale, Vector3.up);
			//Divide the child local scalings by the parent's to get rid of them
			//Abs so nothing fires from the back of the gun
			obj.localScale = new Vector3
			{
				x = Mathf.Abs(obj.localScale.x / reflected.x),
				y = Mathf.Abs(obj.localScale.y / reflected.y),
				z = Mathf.Abs(obj.localScale.z / reflected.z),
			};
		}
	}
}
