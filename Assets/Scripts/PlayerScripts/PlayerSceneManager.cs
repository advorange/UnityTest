using Assets.Scripts.HelperClasses;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.PlayerScripts
{
	/// <summary>
	/// Moves the player between scenes when a new one is loaded.
	/// </summary>
	public class PlayerSceneManager : MonoBehaviour
	{
		private Transform _Spawn;

		private void Awake()
		{
			//Allow users to go between loading scenes
			DontDestroyOnLoad(this.gameObject);
			SceneManager.sceneLoaded += this.SceneLoaded;
		}

		private void SceneLoaded(Scene scene, LoadSceneMode mode)
		{
			//If it's the loading scene then don't bother
			if (scene.name == Scenes.Loading)
			{
				return;
			}

			var children = Tags.FindChildrenWithTag(scene, Tags.Spawn);
			if (!children.Any())
			{
				this._Spawn = default(Transform);
				return;
			}

			//Only use the most recently created spawn
			this._Spawn = children.Last().GetComponent<Transform>();
			//If no spawn point then just leave current position
			if (!this._Spawn)
			{
				return;
			}

			this.transform.position = this._Spawn.position;
			this.transform.rotation = this._Spawn.rotation;
		}
	}
}
