using System;
using Stuart.Scripts.SO;
using UnityEngine;
using UnityEngine.AI;

namespace Stuart.Scripts.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class StateMachineController : MonoBehaviour
    {
        private BaseState currentState;
        public NavMeshAgent agent { get; private set; }
        public BaseState idleState { get; private set; }
        public BaseState chaseState{ get; private set; }
        public BaseState attackState{ get; private set; }
        public BaseState deathState{ get; private set; }
        [Header("Stats")]
        public CharacterStats characterStats;
        public LocomotionStats locomotionStats;
        public CombatStats stats;
        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            ChangeState(idleState);
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
