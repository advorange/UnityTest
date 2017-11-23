using Assets.Scripts.HelperClasses;
using Assets.Scripts.WeaponScripts;
using System;
using System.Linq;
using UnityEditor;

namespace Assets.EditorScripts
{
	[CustomEditor(typeof(WeaponCollision))]
	public class IgnoredTagPopup : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();
			var weaponCollision = this.target as WeaponCollision;

			var addIndex = EditorGUILayout.Popup("Add Ignored Tag", -1, Tags.AssetTags.ToArray());
			var removeIndex = EditorGUILayout.Popup("Remove Ignored Tag", -1, weaponCollision.IgnoredTags);
			if (addIndex >= 0)
			{
				AddTag(weaponCollision, addIndex);
			}
			else if (removeIndex >= 0)
			{
				RemoveTag(weaponCollision, removeIndex);
			}
			else
			{
				return;
			}

			//Apply the values, have the editor know something was editted, and allow undoing
			this.serializedObject.ApplyModifiedProperties();
			EditorUtility.SetDirty(this.target);
			Undo.RecordObject(this.target, "Ignored Tag");
		}
		private void RemoveEmptyTags(WeaponCollision wc)
		{
			wc.IgnoredTags = wc.IgnoredTags.Where(x => !String.IsNullOrWhiteSpace(x)).ToArray();
		}
		private void AddTag(WeaponCollision wc, int index)
		{
			RemoveEmptyTags(wc);
			//Resize to fit the new tag
			var curLen = wc.IgnoredTags.Length;
			Array.Resize(ref wc.IgnoredTags, curLen + 1);
			wc.IgnoredTags[curLen] = Tags.AssetTags[index];
		}
		private void RemoveTag(WeaponCollision wc, int index)
		{
			wc.IgnoredTags[index] = null;
			RemoveEmptyTags(wc);
		}
	}
}