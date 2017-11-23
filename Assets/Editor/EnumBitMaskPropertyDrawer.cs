using Assets.Scripts;
using System;
using UnityEditor;
using UnityEngine;

namespace Assets.EditorScripts
{
	[CustomPropertyDrawer(typeof(BitMaskAttribute))]
	public class EnumBitMaskPropertyDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
		{
			//Add the actual int value behind the field name
			label.text = $"{label.text} ({prop.intValue})";
			prop.intValue = DrawBitMaskField(position, prop.intValue, ((BitMaskAttribute)this.attribute).propType, label);
		}

		public static int DrawBitMaskField(Rect aPosition, int aMask, Type aType, GUIContent aLabel)
		{
			var itemNames = Enum.GetNames(aType);
			var itemValues = Enum.GetValues(aType) as int[];

			var val = aMask;
			var maskVal = 0;
			for (int i = 0; i < itemValues.Length; i++)
			{
				var curVal = itemValues[i];
				if ((curVal == 0 && val == 0) || (curVal != 0 && (val & curVal) == curVal))
				{
					maskVal |= 1 << i;
				}
			}

			var newMaskVal = EditorGUI.MaskField(aPosition, aLabel, maskVal, itemNames);
			var changes = maskVal ^ newMaskVal;
			for (int i = 0; i < itemValues.Length; i++)
			{
				//Has this list item changed?
				if ((changes & (1 << i)) == 0)
				{
					continue;
				}
				//Has it been reset?
				else if ((newMaskVal & (1 << i)) == 0)
				{
					val &= ~itemValues[i];
					continue;
				}
				//If "0" is set, just set the val to 0
				else if (itemValues[i] == 0)
				{
					val = 0;
					break;
				}
				//Set it
				val |= itemValues[i];
			}
			return val;
		}
	}
}
