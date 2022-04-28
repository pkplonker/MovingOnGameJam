using System;
using Stuart.Scripts.Projectiles;
using Stuart.Scripts.SO;
using Stuart.Scripts.SO.Character;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace Stuart.Scripts.Enemy
{
	[RequireComponent(typeof(NavMeshAgent))]
	[RequireComponent(typeof(ProjectileSpawner))]
	public class StateMachineController : MonoBehaviour, IStateChange
	{
		private BaseState currentState;
		[SerializeField] private Transform target;
		[SerializeField] Transform shootPoint;
		[SerializeField] Transform shootPoint2;

		[SerializeField] private LayerMask playerLayer;
		public NavMeshAgent agent { get; private set; }
		public BaseState idleState { get; } = new IdleState();
		public BaseState chaseState { get; } = new ChaseState();
		public BaseState attackState { get; } = new AttackState();
		public BaseState deathState { get; } = new DeathState();
		public float chaseRangeSqrMag { get; private set; }
		public float attackRangeSqrMag { get; private set; }
		private Damageable damageable;
		public ProjectileSpawner projectileSpawner { get; private set; }
		[Header("Stats")] public CombinedCharacterStats stats;
		public Damageable enemyDamageable { get; private set; }

		public int shooterTeam { get; private set; }

		public Transform GetTarget() => target;
		public Transform GetShootPoint() => shootPoint;
		public Transform GetShootPoint2() => shootPoint2;

		private void Awake()
		{
			if(target==null) Debug.LogError("YOU NEED A TARGET");
			agent = GetComponent<NavMeshAgent>();
			ChangeState(idleState);
			damageable = GetComponent<Damageable>();
			projectileSpawner = GetComponent<ProjectileSpawner>();
			shooterTeam = GetComponent<Damageable>().teamId;
			enemyDamageable = target.GetComponent<Damageable>();
		}

		private void OnEnable()
		{
			if (target == null) Debug.LogError("Target missing");
			damageable.OnDeath += Die;
		}

		private void OnDisable()
		{
			damageable.OnDeath -= Die;
		}

		private void Start()
		{
			agent.acceleration = stats.locomotion.acceleration;
			agent.angularSpeed = stats.locomotion.angularSpeed;
			agent.speed = stats.locomotion.movementSpeed;
			chaseRangeSqrMag = Mathf.Pow(stats.character.detectionRadius, 2);
			attackRangeSqrMag = Mathf.Pow(stats.combat.attackRadius, 2);
			gameObject.tag = "Enemy";
		}

		private void Update()
		{
			currentState.StateUpdate();
		}

		public void ChangeState(BaseState newState)
		{
			newState.StateExit();
			currentState = newState;
			currentState.StateEnter(this);
		}

		public void SetDestination(Vector3 destination)
		{
			if (destination == agent.destination) return;
			agent.SetDestination(destination);
		}


		public bool PlayerInAttackRange()
		{
			if (enemyDamageable.isDead) return false;
			if (GenericMath.InRange(transform.position, GetTarget().position, attackRangeSqrMag)) //in range
			{
				Vector3 shotPos = shootPoint.position - new Vector3(0, 0.3f, 0);
				RaycastHit hit;
				Debug.DrawRay(shotPos, (target.position - shotPos).normalized, Color.red);
				if (Physics.SphereCast(shotPos, .3f, (target.position - shotPos).normalized, out hit,
					stats.combat.attackRadius, playerLayer))
				{
					if (hit.collider.transform == target) return true;
				}
			}

			return false;
		}

		public bool PlayerInChaseRange()
		{
			return !enemyDamageable.isDead && GenericMath.InRange(transform.position, GetTarget().position, chaseRangeSqrMag);
		}

		private void Die()
		{
			ChangeState(deathState);
		}

		public void DestroySelf()
		{
			Destroy(gameObject);
		}
	}
}