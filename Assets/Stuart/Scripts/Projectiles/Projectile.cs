using System;
using Stuart.Scripts.SO;
using Stuart.Scripts.SupportSystems;
using UnityEngine;

namespace Stuart.Scripts.Projectiles
{
	public class Projectile : MonoBehaviour
	{
		private ProjectileData data;
		private Vector3 direction;
		private Vector3 cachedTargetPos;
		private Transform target;
		private int shooterTeam;

		public void InitProjectile(ProjectileData data, Transform target, int shooterTeam)
		{
			this.target = target;
			this.data = data;
			this.shooterTeam = shooterTeam;
			direction = target.position - transform.position;
			direction = direction.normalized;
			Invoke(nameof(DestroyObject), data.lifeTime);
			cachedTargetPos = (target.position - transform.position) * 1000;
		}
		public void InitProjectile(ProjectileData data, Vector3 position, int shooterTeam)
		{
			this.data = data;
			this.shooterTeam = shooterTeam;
			if (data.isTracking)
			{
				Debug.Log("Tracking requires transform not vector");
				DestroyObject();
			}
				direction = position - transform.position;
				direction = direction.normalized;
				Invoke(nameof(DestroyObject), data.lifeTime);
				cachedTargetPos =position  * 100;
		}

		private void OnTriggerEnter(Collider other)
		{
			HandleCollision(other);
		}

		private void HandleCollision(Collider other)
		{
			if (other.CompareTag("Projectile")) return;
			Damageable damageable = other.transform.root.GetComponent<Damageable>();
			if (damageable == null)
			{ damageable = other.transform.root.GetComponentInChildren<Damageable>();

			}
			if (damageable != null && damageable.teamId != shooterTeam)
			{
				damageable.TakeDamage(data.damage);
				HandleHitVFX();
			}
			else if (damageable == null)
			{
				HandleHitVFX();
			}
		}


		private void Update()
		{
			HandleMovement();
		}

		protected virtual void HandleMovement()
		{
			if (data.isTracking)
			{
				transform.position =
					Vector3.MoveTowards(transform.position, target.position, data.speed * Time.deltaTime);
			}

			else
			{
				transform.position =
					Vector3.MoveTowards(transform.position, cachedTargetPos, data.speed * Time.deltaTime);
				
			}
			transform.LookAt(data.isTracking?  target.position:cachedTargetPos,Vector3.up);
			transform.eulerAngles += new Vector3(0, -90, 0);

		}

		private void HandleHitVFX()
		{
			//play some VFX?
			DestroyObject();
		}

		private void DestroyObject()
		{
			ProjectileObjectPool.instance.ReturnObject(gameObject);
		}
	}
}