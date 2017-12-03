using Assets.Scripts.HelperClasses;
using Assets.Scripts.WeaponScripts;
using System;
using System.Linq;
using UnityEditor;

namespace Assets.EditorScripts
{
	/// <summary>
	/// Only allows ignored tags to be chosen from a select list.
	/// </summary>
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
				RemoveEmptyTags(weaponCollision);
				//Resize to fit the new tag
				var curLen = weaponCollision.IgnoredTags.Length;
				Array.Resize(ref weaponCollision.IgnoredTags, curLen + 1);
				weaponCollision.IgnoredTags[curLen] = Tags.AssetTags[addIndex];
			}
			else if (removeIndex >= 0)
			{
				weaponCollision.IgnoredTags[removeIndex] = null;
				RemoveEmptyTags(weaponCollision);
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
	}
}