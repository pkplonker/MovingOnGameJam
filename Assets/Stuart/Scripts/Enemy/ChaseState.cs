using UnityEngine;

namespace Stuart.Scripts.Enemy
{
    public class ChaseState : BaseState
    {
        
        public override void StateEnter(StateMachineController controller)
        {
            this.controller = controller;

        }

        public override void StateUpdate()
        {
            Debug.Log("Chase");

            if (controller.PlayerInAttackRange())
            {
                Debug.Log("in attack range");
                controller.agent.SetDestination(controller.transform.position);
                
                controller.ChangeState(controller.attackState);
            }
            else
            {
                controller.SetDestination(controller.GetTarget().position);
            }
        }

        public override void StateExit()
        {
        }
    }
}