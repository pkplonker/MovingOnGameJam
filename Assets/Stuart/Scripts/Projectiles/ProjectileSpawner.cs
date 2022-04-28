using Stuart.Scripts.SO;
using Stuart.Scripts.SupportSystems;
using UnityEngine;

namespace Stuart.Scripts.Projectiles
{
	public class ProjectileSpawner : MonoBehaviour
	{
		public void SpawnProjectile(Vector3 spawnPoint, Transform target, ProjectileData data, int shooterTeam)
		{
			Debug.Log("Spawn projectile");
			GameObject newObj = ProjectileObjectPool.instance.GetObject(spawnPoint, Quaternion.identity, data);
			newObj.GetComponent<Projectile>().InitProjectile(data, target, shooterTeam);
			/*
			Instantiate(data.prefab, spawnPoint, Quaternion.identity).GetComponent<Projectile>()
				.InitProjectile(data, target, shooterTeam);*/
		}
		
		public void SpawnProjectile(Vector3 spawnPoint, Vector3 target, ProjectileData data, int shooterTeam)
		{
			Debug.Log("Spawn projectile");
			GameObject newObj = PlayerProjectileObjectPool.instance.GetObject(spawnPoint, Quaternion.identity, data);
			newObj.GetComponent<Projectile>().InitProjectile(data, target, shooterTeam);
			/*
			Instantiate(data.prefab, spawnPoint, Quaternion.identity).GetComponent<Projectile>()
				.InitProjectile(data, target, shooterTeam);*/
		}
	}
}