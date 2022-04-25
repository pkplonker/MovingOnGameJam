using UnityEngine;

namespace Stuart.Scripts.SO.Character
{
    [CreateAssetMenu(fileName = "New Combined Character Stats", menuName = "Character/Stats/Combined")]
    public class CombinedCharacterStats : ScriptableObject
    {
        public LocomotionStats locomotion;
        public CombatStats combat;
        public CharacterStats character;
    }
}
