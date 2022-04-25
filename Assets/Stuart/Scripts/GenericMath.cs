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
    }
}
