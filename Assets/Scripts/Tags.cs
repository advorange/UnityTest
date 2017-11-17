using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Assets.Scripts
{
	public static class Tags
	{
		private static ReadOnlyCollection<string> _AssetTags;
		public static ReadOnlyCollection<string> AssetTags => _AssetTags ?? (_AssetTags = CreateAssetTagList());

		public static readonly string Player = "Player";
		public static readonly string Ammo = "CurrentAmmo";
		public static readonly string Magazine = "MagazineSize";
		public static readonly string Bullet = "Bullet";
		public static readonly string Gun = "Gun";
		public static readonly string FPS = "FPS";

		private static ReadOnlyCollection<string> CreateAssetTagList()
		{
			Debug.Log("Created asset tag list.");
			return typeof(Tags).GetFields(BindingFlags.Public | BindingFlags.Static)
				.Where(x => x.FieldType == typeof(string))
				.Select(x => (x.GetValue(null) ?? "").ToString())
				.Where(x => !String.IsNullOrWhiteSpace(x))
				.ToList().AsReadOnly();
		}
	}
}
