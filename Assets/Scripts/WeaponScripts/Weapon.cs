using Assets.Scripts.HelperClasses;
using Assets.Scripts.WeaponScripts;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	public abstract class Weapon : MonoBehaviour
	{
		private static Dictionary<string, WeaponRuntimeMetadata> _WeaponMetadata = new Dictionary<string, WeaponRuntimeMetadata>();
		public string RuntimeGuid { get; private set; } = Guid.NewGuid().ToString();
		public string Name;
		public int Damage = 10;
		public float Knockback = 1.0f;
		public float AttackRateInMilliseconds = 100;

		protected abstract void TriggerWeapon();
		protected abstract void UpdateWithRuntimeMetadata(WeaponRuntimeMetadata metadata);
		/// <summary>
		/// Adds the <see cref="WeaponRuntimeMetadata"/> to a dictionary. Only saved in memory.
		/// </summary>
		public void SaveRuntimeMetadata()
		{
			if (!_WeaponMetadata.ContainsKey(this.RuntimeGuid))
			{
				_WeaponMetadata.Add(this.RuntimeGuid, new WeaponRuntimeMetadata(this));
			}
			else
			{
				_WeaponMetadata[this.RuntimeGuid] = new WeaponRuntimeMetadata(this);
			}
		}
		/// <summary>
		/// Removes this from the metadata storage dictionary.
		/// </summary>
		public void DeleteRuntimeMetadata()
		{
			if (_WeaponMetadata.ContainsKey(this.RuntimeGuid))
			{
				_WeaponMetadata.Remove(this.RuntimeGuid);
			}
		}
		/// <summary>
		/// Creates a new <see cref="GameObject"/> from this and attaches it to <paramref name="parent"/>.
		/// Updates the copy with the saved metadata from whatever previous instance of this weapon existed.
		/// </summary>
		/// <param name="parent"></param>
		/// <returns></returns>
		public GameObject Copy(Transform parent)
		{
			var newWep = Instantiate(this.gameObject, parent.transform, false);
			newWep.IgnoreParentScaling();
			var newWepComp = newWep.GetComponent<Weapon>();
			newWepComp.RuntimeGuid = this.RuntimeGuid;
			if (_WeaponMetadata.ContainsKey(this.RuntimeGuid))
			{
				newWepComp.UpdateWithRuntimeMetadata(_WeaponMetadata[this.RuntimeGuid]);
			}
			return newWep;
		}
	}
}