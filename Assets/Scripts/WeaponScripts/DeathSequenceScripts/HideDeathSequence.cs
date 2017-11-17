using Assets.Scripts.HelperClasses;
using UnityEngine;

namespace Assets.Scripts.WeaponScripts.DeathSequenceScripts
{
	/// <summary>
	/// Simple death sequence which hides the item it is attached to when its life reaches zero.
	/// </summary>
	public class HideDeathSequence : DeathSequence
	{
		public override void Kill(GameObject source, Collision collision)
		{
			GetObjectHelper.ToggleComponents(this.gameObject, new[] { typeof(Renderer), typeof(Collider) }, false);
		}
	}
}