using Stuart.Scripts.SO;
using UnityEngine;

namespace Stuart.Scripts.Projectiles
{
	public class ProjectileSpawner : MonoBehaviour
	{
		public void SpawnProjectile(Vector3 spawnPoint, Transform target, ProjectileData data, int shooterTeam)
		{
			Debug.Log("Spawn projectile");
			Instantiate(data.prefab, spawnPoint, Quaternion.identity).GetComponent<Projectile>()
				.InitProjectile(data, target, shooterTeam);
		}
	}
}