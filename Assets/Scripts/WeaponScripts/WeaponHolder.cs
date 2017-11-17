using Assets.Scripts.HelperClasses;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.WeaponScripts
{
	public sealed class WeaponHolder : MonoBehaviour
	{
		private const int SIZE = 4;
		public GameObject[] Weapons = new GameObject[SIZE];

		private GameObject[] RuntimeWeapons = new GameObject[SIZE];
		private int _CurIndex;

		private void OnValidate()
		{
			if (this.Weapons.Length != SIZE)
			{
				Debug.LogWarning($"{nameof(this.Weapons)} should be kept with a length of {SIZE}.");
				Array.Resize(ref this.Weapons, SIZE);
			}
		}
		private void Start()
		{
			AttachWeaponToPlayer(0);
		}
		private void Update()
		{
			var scroll = InputHelper.MouseScrollWheel();
			if (!scroll.Equals(0.0f))
			{
				AttachWeaponToPlayer(GetNextValidWeaponIndex(this._CurIndex, scroll < 0.0f));
			}
		}

		internal void ReplaceWeapon(Weapon weapon, int index)
		{
			this.Weapons[index] = weapon.gameObject;
			//If current index reattach the weapon
			if (this._CurIndex == index)
			{
				AttachWeaponToPlayer(this._CurIndex);
			}
		}
		public Weapon GetCurrentlyHeldWeapon()
		{
			//return RuntimeWeapons[_CurIndex]?.GetComponent<Weapon>(); Which one to use?
			return this.gameObject.GetComponentInChildren<Weapon>();
		}
		private int GetNextValidWeaponIndex(int index, bool down)
		{
			if (this.Weapons.All(x => x == null))
			{
				return -1;
			}

			var newIndex = down ? index - 1 : index + 1;
			if (newIndex >= this.Weapons.Length)
			{
				newIndex = 0;
			}
			else if (newIndex < 0)
			{
				newIndex = this.Weapons.Count(x => x != null) - 1;
			}
			return this.Weapons[newIndex] == null ? GetNextValidWeaponIndex(newIndex, down) : newIndex;
		}
		private void AttachWeaponToPlayer(int index)
		{
			if (index == -1)
			{
				return;
			}

			//Hide all other children
			for (int i = 0; i < this.transform.childCount; ++i)
			{
				this.transform.GetChild(i)?.GetComponent<Weapon>()?.HideWeapon();
			}

			this._CurIndex = index;
			var curWep = this.Weapons[this._CurIndex]?.GetComponent<Weapon>();
			if (!curWep)
			{
				Debug.LogWarning($"Attempted to access a weapon which did not exist at {this._CurIndex}");
				return;
			}

			var curRuntimeWep = this.RuntimeWeapons[this._CurIndex]?.GetComponent<Weapon>();
			GameObject child;
			if (curRuntimeWep?.RuntimeGuid != curWep?.RuntimeGuid && curWep.TryCreate(this.transform, out child))
			{
				this.RuntimeWeapons[this._CurIndex] = child;
				Debug.Log($"Created weapon at {this._CurIndex}");
			}
			else
			{
				curRuntimeWep.UnhideWeapon();
			}
		}
	}
}
