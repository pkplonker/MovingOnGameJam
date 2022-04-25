using UnityEngine;

namespace Stuart.Scripts.SO
{
	[CreateAssetMenu(fileName = "Locomotion", menuName = "Character/Stats/Locomotion")]
	public class LocomotionStats : ScriptableObject
	{
		[Range(0, 20)] public float movementSpeed = 5;
		[Range(0, 5000)] public float angularSpeed = 1800;
		[Range(0, 30)] public float acceleration = 8;
	}
}