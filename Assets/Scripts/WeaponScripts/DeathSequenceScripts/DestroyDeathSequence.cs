using UnityEngine;

namespace Assets.Scripts.WeaponScripts.DeathSequenceScripts
{
	/// <summary>
	/// Simple death sequence which deletes the item it is attached to when its life reaches zero.
	/// </summary>
	public class DestroyDeathSequence : DeathSequence
	{
		public override void Kill(GameObject source, Collision collision)
		{
			Destroy(this.gameObject);
		}
	}
}