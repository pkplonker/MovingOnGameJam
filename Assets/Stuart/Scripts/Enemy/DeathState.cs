using Unity.VisualScripting;
using UnityEngine;

namespace Stuart.Scripts.Enemy
{
	public class DeathState : BaseState
	{
		private float currentDeathTime = 0;
		
		public override void StateEnter(StateMachineController controller)
		{
			this.controller = controller;
		}

		public override void StateUpdate()
		{
			Debug.Log("Death");
			
			currentDeathTime += Time.deltaTime;
			if (currentDeathTime > 2.5f)
			{
				controller.DestroySelf();
			}
			else
			{
			//	Shrink();
			}
		}

		private void Shrink()
		{
			controller.transform.localScale = Vector3.Lerp(controller.transform.localScale, Vector3.zero,
					1f * Time.deltaTime);
			
		}

		public override void StateExit()
		{
		}
	}
}