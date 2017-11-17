using UnityEngine;

namespace Assets.Scripts.WeaponScripts.DeathSequenceScripts
{
	/// <summary>
	/// Instructions for what to do after a some object's life becomes equal to or less than zero.
	/// </summary>
	public abstract class DeathSequence : MonoBehaviour
	{
		public abstract void Kill(GameObject source, Collision collision);
	}
}
