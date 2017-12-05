using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Assets.Scripts.PlayerScripts
{
	public static class PlayerManager
	{
		private const string DEFAULT_PLAYER_PATH = "Prefabs/Player.prefab";
		private static string _SavePath => Path.Combine(Application.persistentDataPath, "save.dat");

		public static GameObject CreateNewCharacter()
		{
			return Resources.Load<GameObject>(DEFAULT_PLAYER_PATH);
		}
		public static void SaveCharacter(GameObject character)
		{
			//TODO
		}
		public static GameObject LoadCharacter()
		{
			return null;
		}
	}
}
