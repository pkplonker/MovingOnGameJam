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
        [SerializeField] private LayerMask playerLayer;
        public NavMeshAgent agent { get; private set; }
        public BaseState idleState { get; private set; } = new IdleState();
        public BaseState chaseState { get; private set; } = new ChaseState();
        public BaseState attackState { get; private set; } = new AttackState();
        public BaseState deathState { get; private set; } = new DeathState();
        [Header("Stats")]
        
        public CombinedCharacterStats stats;

        public LayerMask GetPlayerLayer() => playerLayer;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            ChangeState(idleState);
        }
        private void Start()
        {
            agent.acceleration = stats.locomotion.acceleration;
            agent.angularSpeed = stats.locomotion.angularSpeed;
            agent.speed = stats.locomotion.movementSpeed;
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
            agent.SetDestination(destination);
        }
    }
}
