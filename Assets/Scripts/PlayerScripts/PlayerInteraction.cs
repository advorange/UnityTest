using Assets.Scripts.HelperClasses;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.PlayerScripts
{
	public class PlayerInteraction : MonoBehaviour
	{
		public float InteractDistance = 1.0f;

		private Transform _Player;
		private Text _InteractionText;
		private RaycastHit _Info;

		private void Start()
		{
			this._Player = this.GetComponent<Transform>();
			this._InteractionText = Tags.FindChildrenWithTag(this.gameObject, Tags.Interaction)[0].GetComponent<Text>(); 
		}
		private void Update()
		{
			if (!Physics.Raycast(this._Player.position, this._Player.forward.normalized, out this._Info, this.InteractDistance))
			{
				this._InteractionText.text = null;
				return;
			}

			var interactable = this._Info.collider.GetComponent<Interactable>();
			if (!interactable)
			{
				this._InteractionText.text = null;
				return;
			}

			this._InteractionText.text = $"Press {GlobalVariables.InteractKey} to {interactable.Text.TrimEnd('.')}.";
			if (InputHelper.Interact)
			{
				interactable.Interact();
			}
		}
	}
}
