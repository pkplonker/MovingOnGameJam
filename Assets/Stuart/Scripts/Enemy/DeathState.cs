namespace Stuart.Scripts.Enemy
{
    public class DeathState : BaseState
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
