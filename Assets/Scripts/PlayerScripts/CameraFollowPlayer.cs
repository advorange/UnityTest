using UnityEngine;

namespace Assets.PlayerScripts
{
	public class CameraFollowPlayer : MonoBehaviour
	{
		public Transform Player;
		public Vector3 Offset;

		private void Update()
		{
			this.transform.position = Player.position + Offset;
		}
	}
}