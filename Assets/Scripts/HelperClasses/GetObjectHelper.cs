using Assets.Scripts.WeaponScripts;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.HelperClasses
{
	public static class GetObjectHelper
	{
		public static GameObject[] GetPlayers() => GameObject.FindGameObjectsWithTag(Tags.Player);

		public static IEnumerable<WeaponCollisionEffect> GetCollisionEffects(this WeaponCollisionEffect[] effects, WeaponEffectTargets targets)
		{
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
