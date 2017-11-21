using Assets.Scripts.HelperClasses;
using Assets.Scripts.WeaponScripts.CollisionInteractionScripts;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.WeaponScripts
{
	public class WeaponCollision : MonoBehaviour
	{
		private static string[] _GlobalIgnoredTags = new[] { Tags.Bullet };
		public string[] IgnoredTags;
		private WeaponCollisionEffect[] _Effects;
		private Collider _Collider;

		private void OnValidate()
		{
			foreach (var tag in this.IgnoredTags)
			{
				if (!String.IsNullOrWhiteSpace(tag) && !Tags.DoesTagExist(tag))
				{
					Debug.LogWarning($"The tag {tag} is not valid to ignore since it is not explicitly stated in the {nameof(Tags)} class.");
				}
			}
		}

		protected virtual void Start()
		{
			this._Effects = this.GetComponents<WeaponCollisionEffect>();
			this._Collider = this.GetComponent<Collider>();
		}
		protected virtual void OnCollisionEnter(Collision collision)
		{
			if (_GlobalIgnoredTags.Contains(collision.gameObject.tag) || this.IgnoredTags.Contains(collision.gameObject.tag))
			{
				Physics.IgnoreCollision(this._Collider, collision.gameObject.GetComponent<Collider>());
				return;
			}

			//An interaction of weapon collision must exist before a weapon collision will do anything
			//Can be bouncing, disappearing, exploding, etc.
			foreach (var interaction in collision.gameObject.GetComponents<EntityWeaponCollisionInteraction>())
			{
				interaction.Interact(this.gameObject, collision, this._Effects);
			}
		}
	}
}
