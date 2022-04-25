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
                controller.agent.ResetPath();
                
                controller.ChangeState(controller.attackState);
            }
            else
            {
                controller.ChangeState(controller.idleState);
            }
        }

        public override void StateExit()
        {
        }
    }
}
