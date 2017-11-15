using System;
using UnityEngine;

namespace Assets.Scripts.HelperClasses
{
	public static class GetObjectHelper
	{
		public static GameObject FindGameObjectWithTag(string tag)
		{
			try
			{
				return GameObject.FindGameObjectWithTag(tag);
			}
			catch (Exception e)
			{
				Debug.Log(e);
				UnityEditor.EditorApplication.isPlaying = false;
				return null;
			}
		}
	}
}
