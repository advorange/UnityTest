using Assets.Scripts.WeaponScripts;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.HelperClasses
{
	public static class GetObjectHelper
	{
		public static GameObject GetPlayer() => FindGameObjectWithTag(Tags.Player);
		public static GameObject FindGameObjectWithTag(string tag)
		{
			try
			{
				return GameObject.FindGameObjectWithTag(tag);
			}
			catch (Exception e)
			{
				Debug.LogException(e);
				UnityEditor.EditorApplication.isPlaying = false;
				return null;
			}
		}
		public static bool DoesTagExist(string tag)
		{
			return Tags.AssetTags.Contains(tag);
		}

		public static IEnumerable<WeaponCollisionEffect> GetCollisionEffects(this WeaponCollisionEffect[] effects, WeaponEffectTargets targets)
		{
			//0 indicates nothing ever triggers
			//
			foreach (var bce in effects.Where(x => (x.Targets & targets) != 0))
			{
				yield return bce;
			}
		}

		public static void ToggleComponents(GameObject gameObject, Type[] componentTypes, bool value)
		{
			foreach (var componentType in componentTypes)
			{
				var components = gameObject.GetComponents(componentType);
				components.OfType<Renderer>().ToList().ForEach(x => x.enabled = value);
				components.OfType<Collider>().ToList().ForEach(x => x.enabled = value);
			}
		}
	}
}
