using Unity.VisualScripting;
using UnityEngine;

namespace Stuart.Scripts.Enemy
{
	public class AttackState : BaseState
	{
		private float lastAttackTime = 0f;

		public override void StateEnter(StateMachineController controller)
		{
			this.controller = controller;
		}

		public override void StateUpdate()
		{

			if (controller.PlayerInAttackRange())
			{
				Vector3 direction = -(controller.transform.position - controller.GetTarget().position).normalized;
				direction.y = 0;
				controller.transform.rotation =
					Quaternion.LookRotation(direction,
						controller.transform.up);
				//controller.agent.updateRotation = true;
				if (Time.time - lastAttackTime > controller.stats.combat.attackSpeed)
				{
					lastAttackTime = Time.time;
					Shoot(controller.GetShootPoint());
					Shoot(controller.GetShootPoint2());

				}
			}
			else if (controller.PlayerInChaseRange())
			{
				Debug.Log("in chase range");
				controller.ChangeState(controller.chaseState);
			}
			else
			{
				controller.ChangeState(controller.idleState);
			}
		}

		public override void StateExit()
		{
		}

		public void Shoot(Transform shootPoint)
		{
			if (shootPoint == null) return;
			controller.projectileSpawner.SpawnProjectile(shootPoint.position, controller.GetTarget(),
				controller.stats.combat.projectileData, controller.shooterTeam);
		}
	}
}