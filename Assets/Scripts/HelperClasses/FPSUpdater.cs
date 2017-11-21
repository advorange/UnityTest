using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.HelperClasses
{
	public class FPSUpdater : MonoBehaviour
	{
		private void Update()
		{
			this.gameObject.GetComponent<Text>().text = (Time.timeScale / Time.smoothDeltaTime).ToString();
		}
	}
}
