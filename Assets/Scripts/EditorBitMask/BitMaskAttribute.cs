using System;
using UnityEngine;

namespace Assets.EditorBitMask
{
	public class BitMaskAttribute : PropertyAttribute
	{
		public Type propType;

		public BitMaskAttribute(Type aType)
		{
			propType = aType;
		}
	}
}