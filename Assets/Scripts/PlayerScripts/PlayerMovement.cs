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
	}
}