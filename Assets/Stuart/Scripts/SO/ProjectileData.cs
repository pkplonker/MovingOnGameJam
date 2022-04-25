using UnityEngine;

namespace Stuart.Scripts.SO
{
    [CreateAssetMenu(fileName = "New Projectile", menuName = "Projectile/Basic Projectile")]
    public class ProjectileData : ScriptableObject
    {
        [Header("Basic")]
        public float damage;
        public float speed;
        [Header("LifeTime")]
        public float lifeTime;
        public float maxRange;
        [Header("Render")]
        public GameObject prefab;
        
        
    }
}
