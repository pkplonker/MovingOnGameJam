using UnityEngine;

namespace Stuart.Scripts.SO.Character
{
    [CreateAssetMenu(fileName = "Combat", menuName = "Character/Stats/Combat")]

    public class CombatStats : ScriptableObject
    {
      [Range(1,15)]  public float attackRadius = 2f;
      [Range(0, 10)] public float attackSpeed = 2f;
      public ProjectileData projectileData;

    }
}
