using Assets.Scripts.HelperClasses;
using UnityEngine;

namespace Assets.Scripts.PlayerScripts
{
	public class PlayerMovement : MonoBehaviour
	{
		public float MovementSpeed = 8.0f;
		public float JumpSpeed = 5.0f;
		public float FallSpeed = 20.0f;

		private Vector3 _MoveDirection;

		private void FixedUpdate()
		{
			//TODO: change the variables to something in the constants file
			var controller = this.GetComponent<CharacterController>();
			if (controller.isGrounded)
			{
				_MoveDirection = new Vector3(InputHelper.Horizontal, 0, InputHelper.Vertical);
				_MoveDirection = transform.TransformDirection(_MoveDirection) * MovementSpeed;
				if (InputHelper.Jump)
				{
					_MoveDirection.y = JumpSpeed;
				}
			}
			else
			{
				_MoveDirection.y -= FallSpeed * Time.deltaTime;
			}
			controller.Move(_MoveDirection * Time.deltaTime);
		}
	}
}