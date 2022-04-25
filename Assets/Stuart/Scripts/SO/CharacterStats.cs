using UnityEngine;

namespace Stuart.Scripts.SO
{
    [CreateAssetMenu(fileName = "Character", menuName = "Character/Stats/Character")]

    public class CharacterStats : ScriptableObject
    {
        public string characterName = "New Character";
      [Range(1,1000)]  public float maxHealth;
   [Range(1,40)]   public float detectionRadius;
    }
}
