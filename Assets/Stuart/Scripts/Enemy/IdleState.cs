using UnityEngine;

namespace Stuart.Scripts.Enemy
{
    public class IdleState : BaseState
    {
        public override void StateEnter(StateMachineController controller)
        {
            this.controller = controller;
        }

        public override void StateUpdate()
        {
            if (PlayerInRange()!=null)
            {
                Debug.Log("in range");
            }
        }

        private GameObject PlayerInRange()
        {
            if (!Physics.SphereCast(controller.transform.position, controller.stats.character.detectionRadius,
                    Vector3.zero, out RaycastHit hit, 1, controller.GetPlayerLayer())) return null;
            Debug.Log(hit.collider.gameObject);
            return hit.collider.gameObject;

        }

        public override void StateExit()
        {
        }
    }
}
