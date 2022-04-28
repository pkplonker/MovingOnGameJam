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
			if (controller.PlayerInAttackRange())
			{
				controller.ChangeState(controller.attackState);
			}
			else if (controller.PlayerInChaseRange())
			{
				controller.ChangeState(controller.chaseState);
			}
		}


		public override void StateExit()
		{
		}
	}
}