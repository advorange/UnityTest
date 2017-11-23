using Assets.Scripts.HelperClasses;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
	public abstract class Weapon : MonoBehaviour
	{
		public string RuntimeGuid { get; protected set; } = Guid.NewGuid().ToString();
		public string Name;
		[Header("Stats")]
		public float Damage = 10.0f;
		public float Knockback = 1.0f;
		public float AttackRateInMilliseconds = 100.0f;

		public GameObject Player { get; protected set; }
		public float NextAllowedToAttack { get; protected set; }
		public bool IsVisible { get; protected set; } = true;
		public bool AllowedToUse => this.IsVisible && this.NextAllowedToAttack <= Time.time;

		protected virtual void Start()
		{
			this.Player = this.transform.parent.gameObject;
		}
		/// <summary>
		/// Base implementation calls <see cref="UpdateUI"/> and <see cref="UseWeapon"/> if <see cref="AllowedToUse"/> is true.
		/// </summary>
		protected virtual void Update()
		{
			if (this.AllowedToUse)
			{
				UpdateUI();
				UseWeapon();
			}
		}
		protected abstract void UpdateUI();
		protected abstract void UseWeapon();
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
		public void HideWeapon() => ToggleComponents(false);
		/// <summary>
		/// Disables every type passed in which inherits from <see cref="Renderer"/>.
		/// </summary>
		/// <param name="componentsToTurnOff"></param>
		public void UnhideWeapon() => ToggleComponents(true);
		private void ToggleComponents(bool value)
		{
			this.IsVisible = value;
			GetObjectHelper.ToggleComponents(this.gameObject, new[] { typeof(MeshRenderer) }, value);
		}
	}
}