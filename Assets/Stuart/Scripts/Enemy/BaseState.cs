using System;

namespace Stuart.Scripts.Enemy
{
	public abstract class BaseState
	{
		public StateMachineController controller;
		public abstract void StateEnter(StateMachineController controller);
		public abstract void StateUpdate();
		public abstract void StateExit();
	}
}