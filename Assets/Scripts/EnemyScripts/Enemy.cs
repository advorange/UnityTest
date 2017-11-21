using Assets.Scripts.EnemyScripts.AttackScripts;
using Assets.Scripts.EnemyScripts.MovementScripts;
using Assets.Scripts.HelperClasses;
using UnityEngine;
using Assets.Scripts.WeaponScripts.CollisionInteractionScripts;
using System.Linq;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.WeaponScripts;

namespace Assets.Scripts.EnemyScripts
{
	public class Enemy : Damagable
	{
		private Dictionary<GameObject, float> _DamageFromEachPlayer = new Dictionary<GameObject, float>();

		public float FocusTime = 60.0f;
		public float DecideAgainTime => this.FocusTime / 2.0f;
		public float AggroDistance = 30.0f;
		public float AggroPathDistance => this.AggroDistance * 2.0f;
		public float Damage = 10.0f;
		public float UserSwitchDamageMultiplier = 1.05f;
		public float UserSwitchSpeedMultiplier = 1.10f;
		public float UserSwitchBuffSeconds = 2.0f;
		//public EnemyMovement Movement;
		public EnemyAttack Attack;
		public GameObject TargetedPlayer
		{
			get
			{
				return this._TargetedPlayer;
			}
			set
			{
				//If the enemy is switching from one player to another
				//give it a buff so that if there is any way to just
				//have the enemy go back and forth between players
				//the enemy won't be completely broken
				if (this._TargetedPlayer != null && value != null)
				{
					StartCoroutine(BuffWhenSwitchingUsers());
				}

				this._TargetedPlayer = value;
				this._Agent.isStopped = value == null;
				if (!this._Agent.isStopped)
				{
					this._Agent.SetDestination(value.transform.position);
				}
				this._LastDecided = Time.time;
				this._CurrentlyDeciding = false;
			}
		}

		private NavMeshAgent _Agent;
		private GameObject[] _Players;
		private GameObject _TargetedPlayer;
		private float _LastHitPlayer;
		private float _LastDecided;
		private bool _CurrentlyDeciding;

		private void Start()
		{
			this._Agent = this.GetComponent<NavMeshAgent>();
			this._Players = GetObjectHelper.GetPlayers();
		}
		private void Update()
		{
			if (this._TargetedPlayer != null)
			{
				//Update the targetted position if the player has moved
				if (this._Agent.destination != this._TargetedPlayer.transform.position)
				{
					this._Agent.SetDestination(this._TargetedPlayer.transform.position);
				}
				//Attack if they can
				if (this.Attack != null && this.Attack.Attack(this._TargetedPlayer))
				{
					this._LastHitPlayer = Time.time;
				}
			}

			//Only search for new players if the current one hasn't been hit in a while
			//If FocusTime is 0 or lower then the enemy will only focus on one player ever
			if (!this._CurrentlyDeciding && this.FocusTime > 0f
				&& Time.time - this._LastHitPlayer > this.FocusTime
				&& Time.time - this._LastDecided > this.DecideAgainTime)
			{
				StartCoroutine(DecideWhichPlayerToTarget());
			}
		}

		public void UpdatePlayers()
		{
			this._Players = GetObjectHelper.GetPlayers();
		}
		/// <summary>
		/// Used when a player has damaged this enemy.
		/// </summary>
		/// <param name="collision"></param>
		public void OnCollisionEnter(Collision collision)
		{
			//TODO: implement switching based on damage dealt
			//TODO: put back in default aggro (when a user gets near)
			//TODO: put in stationary for range person when they get range and LOS
			var damager = collision.gameObject.GetComponent<Damager>();
			if (damager)
			{
				if (!this._DamageFromEachPlayer.ContainsKey(damager.Player))
				{
					this._DamageFromEachPlayer.Add(damager.Player, 0);
				}
				this._DamageFromEachPlayer[damager.Player] += damager.Damage;
			}
		}
		private IEnumerator DecideWhichPlayerToTarget()
		{
			this._CurrentlyDeciding = true;
			Debug.Log($"{this.name} is deciding which player to target.");

			GameObject foundPlayer = null;
			var foundDist2 = float.MaxValue;
			var aggroDist2 = Mathf.Pow(this.AggroDistance, 2);
			foreach (var player in new List<GameObject>(this._Players))
			{
				//Only aggro players who are close enough
				var curDist2 = Vector3.SqrMagnitude((this.transform.position - player.transform.position));
				if (curDist2 > foundDist2 || curDist2 > aggroDist2)
				{
					continue;
				}

				//Only aggro players who are not behind a lot of objects
				var validDestination = this._Agent.SetDestination(player.transform.position) && this._Agent.remainingDistance < this.AggroPathDistance;
				if (!validDestination)
				{
					continue;
				}

				foundDist2 = curDist2;
				foundPlayer = player;
				yield return null;
			}

			this.TargetedPlayer = foundPlayer;
		}
		private IEnumerator BuffWhenSwitchingUsers()
		{
			this.Damage *= this.UserSwitchDamageMultiplier;
			this._Agent.speed *= this.UserSwitchSpeedMultiplier;
			yield return new WaitForSeconds(this.UserSwitchBuffSeconds);
			this.Damage /= this.UserSwitchDamageMultiplier;
			this._Agent.speed /= this.UserSwitchSpeedMultiplier;
		}
	}
}
