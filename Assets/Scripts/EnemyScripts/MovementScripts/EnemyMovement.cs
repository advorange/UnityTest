using UnityEngine;

namespace Assets.Scripts.EnemyScripts.MovementScripts
{
	public abstract class EnemyMovement : MonoBehaviour
	{
		public abstract bool Move(GameObject target);
	}
}
