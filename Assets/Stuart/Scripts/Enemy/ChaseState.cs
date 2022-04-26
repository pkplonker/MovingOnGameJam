using UnityEngine;
using UnityEngine.AI;

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
			else if (controller.PlayerInChaseRange())
			{
				controller.agent.SetDestination(controller.GetTarget().position);
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