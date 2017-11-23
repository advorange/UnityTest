using Assets.Scripts.HelperClasses;
using System;
using System.Linq;
using UnityEditor;

namespace Assets.EditorScripts
{
	[CustomEditor(typeof(SceneSwitcher))]
	public class SceneNamePopup : Editor
	{
		public string[] Options => Scenes.SceneNames.ToArray();
		public int Index = 0;

		public void Awake()
		{
			this.Index = Array.IndexOf(this.Options, (this.target as SceneSwitcher).SceneName);
		}
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();
			//Create a dropdown menu to pick from valid scenes
			this.Index = EditorGUILayout.Popup("Scene", this.Index, this.Options);

			//Set the values in the sceneswitcher
			var sceneSwitcher = this.target as SceneSwitcher;
			sceneSwitcher.SceneName = this.Options[this.Index];
			if (sceneSwitcher.TieTextToSceneName)
			{
				sceneSwitcher.Text = $"go to {sceneSwitcher.SceneName}";
			}

			//Apply the values, have the editor know something was editted, and allow undoing
			this.serializedObject.ApplyModifiedProperties();
			EditorUtility.SetDirty(this.target);
			Undo.RecordObject(this.target, "Scene");
		}
	}
}
