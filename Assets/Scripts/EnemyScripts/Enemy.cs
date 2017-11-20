using Assets.Scripts.EnemyScripts.AttackScripts;
using Assets.Scripts.EnemyScripts.MovementScripts;
using Assets.Scripts.HelperClasses;
using UnityEngine;
using Assets.Scripts.WeaponScripts.CollisionInteractionScripts;
using System.Linq;
using UnityEngine.AI;

namespace Assets.Scripts.EnemyScripts
{
	public class Enemy : Damagable
	{
		public float TimeToFocusOnOnePlayer = 60;
		public float AggroDistance = 30;
		public float AggroPathDistance = 60;
		//public EnemyMovement Movement;
		public EnemyAttack Attack;

		private NavMeshAgent _Agent;
		private Transform _TargettedPlayer;
		private float _LastHitPlayer;
		private int _CurrentlyTargettedPlayerIndex;

		private void Start()
		{
			this._Agent = this.GetComponent<NavMeshAgent>();
			TargetRandomPlayer(GetObjectHelper.GetPlayers(), - 1);
		}
		private void Update()
		{
			//this.Movement.Move(this._TargettedPlayer);
			this._Agent.SetDestination(this._TargettedPlayer.transform.position);
			if (this.Attack.Attack(this._TargettedPlayer))
			{
				this._LastHitPlayer = Time.time;
			}

			//Only search for new players if the current one hasn't been hit in a while
			//If the time to focus on the player is 0 or lower
			//then the enemy will only focus on one player ever
			if (this.TimeToFocusOnOnePlayer < 0.1f &&
				Time.time - this._LastHitPlayer > this.TimeToFocusOnOnePlayer)
			{
				var players = GetObjectHelper.GetPlayers();
				//Only aggro players where they're close enough
				//TODO: check for walls and stuff at some point
				var validToAggro = players.Where(x =>
				{
					return Vector3.Distance(x.transform.position, this.transform.position) < this.AggroDistance;
				});
				TargetRandomPlayer(players, this._CurrentlyTargettedPlayerIndex);
			}
		}

		private void TargetRandomPlayer(GameObject[] players, params int[] invalidToTarget)
		{
			var index = GenRandomPlayerIndex(players.Length);
			//Make sure not to target whatever is passed in as invalid
			while (invalidToTarget.Contains(index))
			{
				index = GenRandomPlayerIndex(players.Length);
			}
			this._CurrentlyTargettedPlayerIndex = index;
			this._TargettedPlayer = players[this._CurrentlyTargettedPlayerIndex].transform;
		}
		private int GenRandomPlayerIndex(int max)
		{
			return Mathf.Min(Random.Range(0, max), max - 1);
		}
	}
}
