using Stuart.Scripts.SO;
using UnityEngine;

namespace Stuart.Scripts.Projectiles
{
    public class ProjectileSpawner : MonoBehaviour
    {
        public void SpawnProjectile(Vector3 spawnPoint,Vector3 direction , ProjectileData data)
        {
            direction.Normalize();
            Debug.Log("Spawn projectile");
           Instantiate(data.prefab, spawnPoint,Quaternion.identity,transform).GetComponent<Projectile>().InitProjectile(data,direction);
        }
    }
}
