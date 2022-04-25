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
            Vector3.RotateTowards(controller.transform.position, controller.GetTarget().position,1000000,100000);
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
