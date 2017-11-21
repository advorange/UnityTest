using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.HelperClasses
{
	public static class Scenes
	{
		private static ReadOnlyCollection<string> _SceneNames;
		public static ReadOnlyCollection<string> SceneNames => _SceneNames ?? (_SceneNames = CreateSceneNameList());

		public static readonly string Test = nameof(Test);
		public static readonly string Cave = nameof(Cave);
		public static readonly string Loading = nameof(Loading);

		private static ReadOnlyCollection<string> CreateSceneNameList()
		{
			Debug.Log("Created scene name list.");
			return typeof(Scenes).GetFields(BindingFlags.Public | BindingFlags.Static)
				.Where(x => x.FieldType == typeof(string))
				.Select(x => (x.GetValue(null) ?? "").ToString())
				.Where(x => !String.IsNullOrWhiteSpace(x))
				.ToList().AsReadOnly();
		}

		public static Scene GetSceneByName(string name)
		{
			try
			{
				return SceneManager.GetSceneByName(name);
			}
			catch (Exception e)
			{
				Debug.LogException(e);
				UnityEditor.EditorApplication.isPlaying = false;
				return default(Scene);
			}
		}
		public static bool DoesSceneExist(string tag)
		{
			return SceneNames.Contains(tag);
		}
	}
}