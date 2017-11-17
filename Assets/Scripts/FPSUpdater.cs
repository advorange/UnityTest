using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
	public class FPSUpdater : MonoBehaviour
	{
		private void Update()
		{
			this.gameObject.GetComponent<Text>().text = (1.0f / Time.smoothDeltaTime).ToString();
		}
	}
}
