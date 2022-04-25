using UnityEngine;

namespace Stuart.Scripts.Enemy
{
    public class AttackState : BaseState
    {
       
        public override void StateEnter(StateMachineController controller)
        {
            this.controller = controller;
        }

        public override void StateUpdate()
        {
        }

        public override void StateExit()
        {
        }
    }
}
