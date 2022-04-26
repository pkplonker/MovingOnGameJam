using Unity.Mathematics;
using UnityEngine;

namespace Stuart.Scripts
{
	public static class GenericMath
	{
		public static float CalculateSqrMagDistance(Vector3 start, Vector3 end)
		{
			return (start - end).sqrMagnitude;
		}

		public static bool InRange(Vector3 start, Vector3 end, float sqrmMag)
		{
			return CalculateSqrMagDistance(start, end) <= sqrmMag;
		}

		public static float Remap(float os, float oe, float ns, float ne, float value)
		{  
			float n = Mathf.InverseLerp(os, oe, value);
			return  Mathf.Lerp(ns, ne, n);
			
		}
	}
}