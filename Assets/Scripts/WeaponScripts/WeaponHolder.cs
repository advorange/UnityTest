using Assets.Scripts.HelperClasses;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.WeaponScripts
{
	public class WeaponHolder : MonoBehaviour
	{
		private const int SIZE = 4;
		public GameObject[] Weapons = new GameObject[SIZE];
		private int _CurIndex;

		protected virtual void OnValidate()
		{
			if (Weapons.Length != SIZE)
			{
				Debug.LogWarning($"{nameof(Weapons)} should be kept with a length of {SIZE}.");
				Array.Resize(ref Weapons, SIZE);
			}
		}
		protected virtual void Start()
		{
			AttachWeaponToPlayer();
		}
		protected virtual void Update()
		{
			var scroll = InputHelper.MouseScrollWheel;
			if (!scroll.Equals(default(float)))
			{
				AttachWeaponToPlayer(GetNextWeapon(_CurIndex, scroll < 0.0f));
			}
		}

		private int GetNextWeapon(int curIndex, bool down)
		{
			if (Weapons.All(x => x == null))
			{
				return -1;
			}

			var newIndex = down ? curIndex - 1 : curIndex + 1;
			if (newIndex >= Weapons.Length)
			{
				newIndex = 0;
			}
			else if (newIndex < 0)
			{
				newIndex = Weapons.Count(x => x != null) - 1;
			}
			return Weapons[newIndex] == null ? GetNextWeapon(newIndex, down) : newIndex;
		}
		private void AttachWeaponToPlayer(int index = 0)
		{
			if (index == -1)
			{
				return;
			}

			//Get rid of all other visible weapons on the player
			for (int i = 0; i < this.transform.childCount; ++i)
			{
				var childWeapon = this.transform.GetChild(i)?.GetComponent<Weapon>();
				if (childWeapon)
				{
					Destroy(childWeapon.gameObject);
				}
			}

			_CurIndex = index;
			var curWep = Weapons[_CurIndex];
			var newWep = Instantiate(curWep, this.transform, false);
			newWep.IgnoreParentScaling();
		}
	}
}
