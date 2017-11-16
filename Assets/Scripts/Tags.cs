using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Assets.Scripts
{
	public static class Tags
	{
		public static ReadOnlyCollection<string> AssetTags = GenerateTagList();

		public static readonly string Player = "Player";
		public static readonly string Ammo = "CurrentAmmo";
		public static readonly string Magazine = "MagazineSize";
		public static readonly string Bullet = "Bullet";
		public static readonly string Gun = "Gun";

		private static ReadOnlyCollection<string> GenerateTagList()
		{
			//TODO: see why this doesn't work as intended.
			var tags = typeof(Tags).GetFields(BindingFlags.Public | BindingFlags.Static);
			var strings = tags.Where(x =>
			{
				var isStr = x.FieldType == typeof(string);
				Debug.Log(isStr);
				return isStr;
			});
			var values = strings.Select(x =>
			{
				var str = (x.GetValue(null) ?? "").ToString();
				Debug.Log(str);
				return str;
			});
			return
				
				values
				.Where(x => !String.IsNullOrWhiteSpace(x))
				.ToList().AsReadOnly();
		}
	}
}
