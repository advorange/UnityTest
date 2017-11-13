using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
	public Transform Player;
	public Vector3 Offset;

	// Update is called once per frame
	private void Update()
	{
		this.transform.position = Player.position + Offset;
		//Debug.Log(Player.position);
	}
}