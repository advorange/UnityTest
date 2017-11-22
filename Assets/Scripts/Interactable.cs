using UnityEngine;

namespace Assets.Scripts
{
	public abstract class Interactable : MonoBehaviour
	{
		public string Text;

		//TODO: have this be shown based on trigger area and not raycast
		public abstract void Interact();
	}
}
