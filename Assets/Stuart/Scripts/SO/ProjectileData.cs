using UnityEngine;

namespace Stuart.Scripts.SO
{
	[CreateAssetMenu(fileName = "New Projectile", menuName = "Projectile/Basic Projectile")]
	public class ProjectileData : ScriptableObject
	{
		[Header("Basic")] public float damage;
		public float speed;
		public bool isTracking = false;
		[Header("LifeTime")] public float lifeTime;
		public float maxRange;
		[Header("Render")] public Mesh mesh;
		public Material material;
	}
}