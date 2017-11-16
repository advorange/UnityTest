using Assets.Scripts.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
	public abstract class Weapon : MonoBehaviour
	{
		public string RuntimeGuid { get; protected set; } = Guid.NewGuid().ToString();
		public string Name;
		public int Damage = 10;
		public float Knockback = 1.0f;
		public float AttackRateInMilliseconds = 100;

		private static Type[] _DefaultComponentsToToggle = new[] { typeof(MeshRenderer) };
		public float NextAllowedToAttack { get; protected set; }
		public bool IsVisible { get; protected set; } = true;
		public bool AllowedToUse => IsVisible && NextAllowedToAttack <= Time.time;

		/// <summary>
		/// Base implementation calls <see cref="UpdateUI"/> and <see cref="UseWeapon"/> if <see cref="AllowedToUse"/> is true.
		/// </summary>
		protected virtual void Update()
		{
			if (AllowedToUse)
			{
				UpdateUI();
				UseWeapon();
			}
		}
		protected abstract void UseWeapon();
		protected abstract void UpdateUI();
		/// <summary>
		/// Creates a new <see cref="GameObject"/> from this and attaches it to <paramref name="parent"/>.
		/// </summary>
		/// <param name="parent"></param>
		/// <returns></returns>
		public bool TryCreate(Transform parent, out GameObject child)
		{
			//Don't allow a weapon to be duplicated
			if (parent.transform.GetComponents<Weapon>().Any(x => x.RuntimeGuid == this.RuntimeGuid))
			{
				child = null;
				return false;
			}

			var newWep = Instantiate(this.gameObject, parent.transform, false);
			newWep.IgnoreParentScaling();
			newWep.GetComponent<Weapon>().RuntimeGuid = this.RuntimeGuid;
			child = newWep;
			return true;
		}
		/// <summary>
		/// Disables every type passed in which inherits from <see cref="Renderer"/>.
		/// </summary>
		/// <param name="componentsToTurnOff"></param>
		public void HideWeapon(Type[] componentsToTurnOff = null) => ToggleComponents(componentsToTurnOff, false);
		/// <summary>
		/// Disables every type passed in which inherits from <see cref="Renderer"/>.
		/// </summary>
		/// <param name="componentsToTurnOff"></param>
		public void UnhideWeapon(Type[] componentsToTurnOff = null) => ToggleComponents(componentsToTurnOff, true);
		private void ToggleComponents(Type[] components, bool value)
		{
			IsVisible = value;
			foreach (var renderType in ReturnRendererTypes(components))
			{
				var component = this.GetComponent(renderType) as Renderer;
				if (component)
				{
					component.enabled = value;
				}
			}
		}
		private IEnumerable<Type> ReturnRendererTypes(IEnumerable<Type> inputTypes)
		{
			var types = inputTypes ?? _DefaultComponentsToToggle;
			if (!types.Any())
			{
				Debug.Log("An empty array was passed in to hide or unhide a weapon. This has no effect; was this intentional?");
			}

			foreach (var type in types)
			{
				if (typeof(Renderer).IsAssignableFrom(type))
				{
					yield return type;
				}
				else
				{
					Debug.Log($"{type.Name} is not a {nameof(Renderer)} type and cannot be used to hide or unhide a weapon.");
				}
			}
		}
	}
}