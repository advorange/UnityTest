using UnityEngine;

namespace Assets.PlayerScripts
{
	public class PlayerMovement : MonoBehaviour
	{
		public Rigidbody RB;
		public float ForwardForce = 500f;
		public float SidewaysForce = 500f;

		private void FixedUpdate()
		{
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
			}
		}
	}
}