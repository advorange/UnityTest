using Assets.Scripts.HelperClasses;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.PlayerScripts
{
	public class PlayerMovement : MonoBehaviour
	{
		public float MovementSpeed = 8.0f;
		public float JumpSpeed = 5.0f;
		public float FallSpeed = 20.0f;

		private CharacterController _Controller;
		private Vector3 _MoveDirection;
		private Transform _Spawn;

		private void Awake()
		{
			//Allow users to go between loading scenes
			DontDestroyOnLoad(this.gameObject);
			SceneManager.sceneLoaded += this.SceneLoaded;
		}
		private void Start()
		{
			this._Controller = this.GetComponent<CharacterController>();
		}
		private void FixedUpdate()
		{
			if (this._Controller.isGrounded)
			{
				this._MoveDirection = new Vector3(InputHelper.Horizontal(), 0, InputHelper.Vertical());
				this._MoveDirection = this.transform.TransformDirection(this._MoveDirection) * this.MovementSpeed;
				if (InputHelper.Jump)
				{
					this._MoveDirection.y = this.JumpSpeed;
					this._MoveDirection.x += .01f * this.MovementSpeed;
				}
			}
			else
			{
				this._MoveDirection.y -= this.FallSpeed * Time.deltaTime;
			}
			this._Controller.Move(this._MoveDirection * Time.deltaTime);
		}

		private void SceneLoaded(Scene scene, LoadSceneMode mode)
		{
			if (scene.name == Scenes.Loading)
			{
				return;
			}

			//Only use the most recently created spawn
			this._Spawn = Tags.FindChildrenWithTag(scene, Tags.Spawn).Last().GetComponent<Transform>();
			//If no spawn point then just leave the default transform's position
			this.transform.position = this._Spawn?.position ?? this.transform.position;
			this.transform.rotation = this._Spawn?.rotation ?? this.transform.rotation;
		}
	}
}