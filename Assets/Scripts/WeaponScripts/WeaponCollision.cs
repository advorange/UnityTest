using Assets.Scripts.HelperClasses;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.WeaponScripts
{
	public class WeaponCollision : MonoBehaviour
	{
		public string[] IgnoredTags;
		private WeaponCollisionEffect[] _Effects;
		private Collider _Collider;

		private void OnValidate()
		{
			foreach (var tag in IgnoredTags)
			{
				if (!String.IsNullOrWhiteSpace(tag) && !GetObjectHelper.DoesTagExist(tag))
				{
					Debug.Log($"The tag {tag} is not valid to ignore.");
				}
			}
		}

		protected virtual void Start()
		{
			_Effects = this.GetComponents<WeaponCollisionEffect>();
			_Collider = this.GetComponent<Collider>();
		}
		protected virtual void OnCollisionEnter(Collision collision)
		{
			if (IgnoredTags.Contains(collision.gameObject.tag))
			{
				Physics.IgnoreCollision(_Collider, collision.gameObject.GetComponent<Collider>());
				return;
			}

			//An interaction of weapon collision must exist before a weapon collision will do anything
			foreach (var weaponCollidable in collision.gameObject.GetComponents<EntityWeaponCollisionInteraction>())
			{
				weaponCollidable.Interact(collision, _Effects);
			}
		}
	}
}
