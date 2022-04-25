using System;
using Stuart.Scripts.Projectiles;
using Stuart.Scripts.SO;
using UnityEngine;

namespace Stuart.Scripts
{
    public class StuartTest : MonoBehaviour
    {
        [SerializeField] private ProjectileData data;
        private ProjectileSpawner spawner;
        private void Awake()
        {
            spawner = gameObject.AddComponent<ProjectileSpawner>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                spawner.SpawnProjectile(transform.position,transform.forward, data);
            }
        }
    }
}
