using System;
using UnityEngine;

namespace Assets.Scripts.EditorBitMask
{
	public class BitMaskAttribute : PropertyAttribute
	{
		public Type propType;

		public BitMaskAttribute(Type aType)
		{
			this.propType = aType;
		}
	}
}