using System;
using UnityEditor;
using UnityEngine;

namespace Assets.EditorBitMask
{
	public static class EditorExtension
	{
		public static int DrawBitMaskField(Rect aPosition, int aMask, Type aType, GUIContent aLabel)
		{
			var itemNames = Enum.GetNames(aType);
			var itemValues = Enum.GetValues(aType) as int[];

			int val = aMask;
			int maskVal = 0;
			for (int i = 0; i < itemValues.Length; i++)
			{
				if (val == 0 || (itemValues[i] != 0 && (val & itemValues[i]) == itemValues[i]))
				{
					maskVal |= 1 << i;
				}
			}
			int newMaskVal = EditorGUI.MaskField(aPosition, aLabel, maskVal, itemNames);
			int changes = maskVal ^ newMaskVal;

			for (int i = 0; i < itemValues.Length; ++i)
			{
				//Has this list item changed?
				if ((changes & (1 << i)) == 0)
				{
					continue;
				}
				//Has it been set?
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

				val |= itemValues[i];
			}
			return val;
		}
	}
}