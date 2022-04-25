using System;
using Stuart.Scripts.SO;
using Stuart.Scripts.SO.Character;
using UnityEngine;
using UnityEngine.AI;

namespace Stuart.Scripts.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class StateMachineController : MonoBehaviour, IStateChange
    {
        private BaseState currentState;
        [SerializeField] private Transform target;
        public NavMeshAgent agent { get; private set; }
        public BaseState idleState { get; private set; } = new IdleState();
        public BaseState chaseState { get; private set; } = new ChaseState();
        public BaseState attackState { get; private set; } = new AttackState();
        public BaseState deathState { get; private set; } = new DeathState();
        public float chaseRangeSqrMag { get; private set; }
        public float attackRangeSqrMag { get; private set; }
        private Damageable damageable;
        [Header("Stats")]
        
        public CombinedCharacterStats stats;

        public Transform GetTarget() => target;
        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            ChangeState(idleState);
            damageable = GetComponent<Damageable>();
        }

        private void OnEnable()
        {
            if(target==null) Debug.LogError("Target missing");
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
            chaseRangeSqrMag = Mathf.Pow(stats.character.detectionRadius,2) ;
            attackRangeSqrMag = Mathf.Pow(stats.combat.attackRadius,2) ;
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
            return GenericMath.InRange(transform.position, GetTarget().position, attackRangeSqrMag);
        }
        public bool PlayerInChaseRange()
        {
            return GenericMath.InRange(transform.position, GetTarget().position, chaseRangeSqrMag);
        }

        private void Die()
        {
            ChangeState(deathState);
        }
    }
}
