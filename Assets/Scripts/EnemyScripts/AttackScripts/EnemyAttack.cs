using UnityEngine;

namespace Assets.Scripts.EnemyScripts.AttackScripts
{
	public abstract class EnemyAttack : MonoBehaviour
	{
		public abstract bool Attack(Transform target);
	}
}
