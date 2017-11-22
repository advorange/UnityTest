using UnityEngine;

namespace Assets.Scripts
{
	public abstract class Interactable : MonoBehaviour
	{
		public string Text;

		public abstract void Interact();
	}
}
