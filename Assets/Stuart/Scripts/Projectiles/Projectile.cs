using System;
using Stuart.Scripts.SO;
using UnityEngine;

namespace Stuart.Scripts.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        private ProjectileData data;
        private Vector3 direction;
        public void InitProjectile(ProjectileData data, Vector3 direction)
        {
            this.data = data;
            this.direction = direction.normalized;
            Invoke(nameof(DestroyObject),data.lifeTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            HandleCollision(other);
        }

        private void HandleCollision(Collider other)
        {
            if (other.CompareTag("Projectile")) return;
            Damageable damageable = other.GetComponent<Damageable>();
            if (damageable == null)
            {
                HandleHitVFX();
                return;
            }

            damageable.TakeDamage(data.damage);
            HandleHitVFX();
        }

      

        private void Update()
        {
            
            HandleMovement();
        }

        protected virtual void HandleMovement()
        {
          transform.position=  Vector3.Lerp(transform.position, transform.position + direction, data.speed*Time.deltaTime);
        }

        private void HandleHitVFX()
        {
            //play some VFX?
            DestroyObject();
        }

        private void DestroyObject()
        {
            Destroy(gameObject);
        }
    }
}
