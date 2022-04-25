using System;
using Stuart.Scripts.SO;
using UnityEngine;

namespace Stuart.Scripts.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        private ProjectileData data;
        private Vector3 direction;
        private Vector3 startPos;
        private Vector3 cachedTargetPos;
        private Rigidbody rb;
        private Transform target;
        public void InitProjectile(ProjectileData data, Transform target)
        {
            this.target = target;
            this.data = data;
            direction = target.position - transform.position;
            direction = direction.normalized;
            Invoke(nameof(DestroyObject),data.lifeTime);
            startPos = target.position;
            rb = GetComponent<Rigidbody>();
            cachedTargetPos = (target.position-transform.position ) * 1000;
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
            if (data.isTracking) transform.position=     Vector3.MoveTowards(transform.position, target.position, data.speed * Time.deltaTime);
            else transform.position = Vector3.MoveTowards(transform.position, cachedTargetPos, data.speed * Time.deltaTime);
            
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
