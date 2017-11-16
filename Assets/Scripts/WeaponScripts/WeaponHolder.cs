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
			if (Weapons.Length != SIZE)
			{
				Debug.LogWarning($"{nameof(Weapons)} should be kept with a length of {SIZE}.");
				Array.Resize(ref Weapons, SIZE);
			}
		}
		private void Start()
		{
			AttachWeaponToPlayer(0);
		}
		private void Update()
		{
			var scroll = InputHelper.MouseScrollWheel;
			if (!scroll.Equals(0.0f))
			{
				AttachWeaponToPlayer(GetNextValidWeaponIndex(_CurIndex, scroll < 0.0f));
			}
		}

		internal void ReplaceWeapon(Weapon weapon, int index)
		{
			Weapons[index] = weapon.gameObject;
			//If current index reattach the weapon
			if (_CurIndex == index)
			{
				AttachWeaponToPlayer(_CurIndex);
			}
		}
		public Weapon GetCurrentlyHeldWeapon()
		{
			//return RuntimeWeapons[_CurIndex]?.GetComponent<Weapon>(); Which one to use?
			return this.gameObject.GetComponentInChildren<Weapon>();
		}
		private int GetNextValidWeaponIndex(int index, bool down)
		{
			if (Weapons.All(x => x == null))
			{
				return -1;
			}

			var newIndex = down ? index - 1 : index + 1;
			if (newIndex >= Weapons.Length)
			{
				newIndex = 0;
			}
			else if (newIndex < 0)
			{
				newIndex = Weapons.Count(x => x != null) - 1;
			}
			return Weapons[newIndex] == null ? GetNextValidWeaponIndex(newIndex, down) : newIndex;
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
				Debug.Log($"Hid a weapon while trying to bring {_CurIndex} up");
			}

			_CurIndex = index;
			var curWep = Weapons[_CurIndex]?.GetComponent<Weapon>();
			if (!curWep)
			{
				Debug.Log($"Attempted to access a weapon which did not exist at {_CurIndex}");
				return;
			}

			var curRuntimeWep = RuntimeWeapons[_CurIndex]?.GetComponent<Weapon>();
			GameObject child;
			if (curRuntimeWep?.RuntimeGuid != curWep?.RuntimeGuid && curWep.TryCreate(this.transform, out child))
			{
				RuntimeWeapons[_CurIndex] = child;
				Debug.Log($"Created weapon at {_CurIndex}");
			}
			else
			{
				curRuntimeWep.UnhideWeapon();
				Debug.Log($"Unhid weapon at {_CurIndex}");
			}
		}
	}
}
