using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public Rigidbody RB;
	public float ForwardForce = 500f;
	public float SidewaysForce = 500f;

	private void FixedUpdate()
	{
		//Move forward
		if (Input.GetKey(KeyCode.W))
		{
			RB.AddForce(0, 0, ForwardForce * Time.deltaTime);
		}
		//Move left
		else if (Input.GetKey(KeyCode.A))
		{
			RB.AddForce(-SidewaysForce * Time.deltaTime, 0, 0);
		}
		//Move backwards
		else if (Input.GetKey(KeyCode.S))
		{
			RB.AddForce(0, 0, -ForwardForce * Time.deltaTime);
		}
		//Move right
		else if (Input.GetKey(KeyCode.D))
		{
			RB.AddForce(SidewaysForce * Time.deltaTime, 0, 0);
		}
	}
}