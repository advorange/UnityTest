using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.EditorBitMask
{
	[CustomPropertyDrawer(typeof(BitMaskAttribute))]
	public class EnumBitMaskPropertyDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
		{
			//Add the actual int value behind the field name
			label.text = $"{label.text} ({prop.intValue})";
			prop.intValue = EditorExtension.DrawBitMaskField(position, prop.intValue, ((BitMaskAttribute)this.attribute).propType, label);
		}
	}
}
