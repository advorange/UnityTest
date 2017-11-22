using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Assets.Scripts.HelperClasses
{
	public static class Tags
	{
		private static ReadOnlyCollection<string> _AssetTags;
		public static ReadOnlyCollection<string> AssetTags => _AssetTags ?? (_AssetTags = CreateAssetTagList());

		public static readonly string Untagged = nameof(Untagged);
		public static readonly string Respawn = nameof(Respawn);
		public static readonly string Finish = nameof(Finish);
		public static readonly string EditorOnly = nameof(EditorOnly);
		public static readonly string MainCamera = nameof(MainCamera);
		public static readonly string Player = nameof(Player);
		public static readonly string GameController = nameof(GameController);
		public static readonly string CurrentAmmo = nameof(CurrentAmmo);
		public static readonly string MagazineSize = nameof(MagazineSize);
		public static readonly string Bullet = nameof(Bullet);
		public static readonly string Gun = nameof(Gun);
		public static readonly string FPS = nameof(FPS);
		public static readonly string Pause = nameof(Pause);
		public static readonly string Enemy = nameof(Enemy);
		public static readonly string Spawn = nameof(Spawn);
		public static readonly string Interaction = nameof(Interaction);
		public static readonly string Loading = nameof(Loading);

		private static ReadOnlyCollection<string> CreateAssetTagList()
		{
			Debug.Log("Created asset tag list.");
			return typeof(Tags).GetFields(BindingFlags.Public | BindingFlags.Static)
				.Where(x => x.FieldType == typeof(string))
				.Select(x => (x.GetValue(null) ?? "").ToString())
				.Where(x => !String.IsNullOrWhiteSpace(x))
				.ToList().AsReadOnly();
		}
		public static Transform[] FindChildrenWithTag(GameObject parent, string tag)
		{
			try
			{
				var allChildren = parent.GetComponentsInChildren<Transform>().Where(x => x.gameObject != parent);
				return allChildren.Where(x => x.tag == tag).ToArray();
			}
			catch (Exception e)
			{
				Debug.LogException(e);
				Application.Quit();
				return Array.Empty<Transform>();
			}
		}
		private static IEnumerable<Transform> GetAllChildren(Transform parent)
		{
			var children = new List<Transform>();
			foreach (Transform child in parent.GetComponent<Transform>())
			{
				children.AddRange(GetAllChildren(child));
			}
			return children;
		}
		public static bool DoesTagExist(string tag)
		{
			return AssetTags.Contains(tag);
		}
	}
}
