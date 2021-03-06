﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

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
		/// <summary>
		/// Returns every child a <see cref="Scene"/> has which is tagged with <paramref name="tag"/>.
		/// </summary>
		/// <param name="scene"></param>
		/// <param name="tag"></param>
		/// <returns></returns>
		public static GameObject[] FindChildrenWithTag(this Scene scene, string tag)
		{
			if (!DoesTagExist(tag))
			{
				return Array.Empty<GameObject>();
			}

			try
			{
				//Made this into a single LINQ query to see how stpuid it could be
				return scene.GetRootGameObjects()
					.Select(go => go.GetAllChildren().Concat(new[] { go }))
					.SelectMany(tfs => tfs)
					.Where(tf => tf.tag == tag)
					.Select(tf => tf.gameObject)
					.Distinct().ToArray();
			}
			catch (Exception e)
			{
				Debug.LogException(e);
				Application.Quit();
				return Array.Empty<GameObject>();
			}
		}
		/// <summary>
		/// Returns every child a <see cref="GameObject"/> has which is tagged with <paramref name="tag"/>.
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="tag"></param>
		/// <returns></returns>
		public static GameObject[] FindChildrenWithTag(this GameObject parent, string tag)
		{
			if (!DoesTagExist(tag))
			{
				return Array.Empty<GameObject>();
			}

			try
			{
				return parent.GetAllChildren()
					.Where(child => child.CompareTag(tag))
					.Select(child => child.gameObject)
					.Distinct().ToArray();
			}
			catch (Exception e)
			{
				Debug.LogException(e);
				Application.Quit();
				return Array.Empty<GameObject>();
			}
		}
		/// <summary>
		/// Returns every child a <see cref="GameObject"/> has, down to the deepest level.
		/// </summary>
		/// <param name="parent"></param>
		/// <returns></returns>
		public static GameObject[] GetAllChildren(this GameObject parent)
		{
			return parent.GetComponentsInChildren<Transform>().Select(t => t.gameObject).Distinct().ToArray();
		}
		/// <summary>
		/// Returns true if <see cref="AssetTags"/> contains <paramref name="tag"/>.
		/// </summary>
		/// <param name="tag"></param>
		/// <returns></returns>
		public static bool DoesTagExist(string tag)
		{
			return AssetTags.Contains(tag);
		}
	}
}
