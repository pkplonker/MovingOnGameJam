using System;

namespace Stuart.Scripts.Enemy
{
    
    public abstract class BaseState
    {
        public abstract void StateEnter(StateMachineController controller);
        public abstract BaseState StateUpdate();
        public abstract void StateExit();

    }
}
