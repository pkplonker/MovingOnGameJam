using UnityEngine;

namespace Stuart.Scripts.Enemy
{
    public class IdleState : BaseState
    {
        public override void StateEnter(StateMachineController controller)
        {
            this.controller = controller;
            controller.SetDestination(controller.transform.position);

        }

        public override void StateUpdate()
        {
            Debug.Log("Idle");
            if (controller.PlayerInAttackRange())
            {
                Debug.Log("in attack range");
                controller.ChangeState(controller.attackState);
            }else if (controller.PlayerInChaseRange())
            {
                Debug.Log("in chase range");
                controller.ChangeState(controller.chaseState);
            }
        }

    
        public override void StateExit()
        {
        }
    }
}
