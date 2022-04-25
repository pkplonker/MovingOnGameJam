using UnityEngine;

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
			Debug.Log("Death");
		}

		public override void StateExit()
		{
		}
	}
}