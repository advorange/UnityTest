using UnityEngine;

namespace Assets.PlayerScripts
{
	public class PlayerMovement : MonoBehaviour
	{
		public Rigidbody RigidBody;
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
				_MoveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
				_MoveDirection = transform.TransformDirection(_MoveDirection);
				_MoveDirection *= MovementSpeed;
				if (Input.GetButton("Jump"))
				{
					_MoveDirection.y = JumpSpeed;
				}
			}
			else
			{
				_MoveDirection.y -= FallSpeed * Time.deltaTime;
			}
			controller.Move(_MoveDirection * Time.deltaTime);

			/*
			if (Input.GetKey(Constants.FORWARDS_KEY))
			{
				RB.AddForce(0, 0, ForwardForce * Time.deltaTime);
			}
			else if (Input.GetKey(Constants.LEFT_KEY))
			{
				RB.AddForce(-SidewaysForce * Time.deltaTime, 0, 0);
			}
			else if (Input.GetKey(Constants.BACKWARDS_KEY))
			{
				RB.AddForce(0, 0, -ForwardForce * Time.deltaTime);
			}
			else if (Input.GetKey(Constants.RIGHT_KEY))
			{
				RB.AddForce(SidewaysForce * Time.deltaTime, 0, 0);
			}*/
		}
	}
}