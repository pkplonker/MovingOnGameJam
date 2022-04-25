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
            controller.agent.updateRotation = false;
            Vector3 direction = -(controller.transform.position - controller.GetTarget().position).normalized;
            direction.y = 0;
            controller.transform.rotation =
                Quaternion.LookRotation(direction,
                    controller.transform.up);
            //controller.agent.updateRotation = true;
            Debug.Log("Attack");
            if (Time.time - lastAttackTime > controller.stats.combat.attackSpeed)
            {
                lastAttackTime = Time.time;
                Shoot();
            }
        }

        public override void StateExit()
        {
        }

        public void Shoot()
        {
            controller.projectileSpawner.SpawnProjectile(controller.GetShootPoint().position,controller.transform.forward, controller.stats.combat.projectileData);
        }
    }
}
