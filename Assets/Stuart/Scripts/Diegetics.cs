using System;
using UnityEngine;

namespace Stuart.Scripts
{
	public class Diegetics : MonoBehaviour
	{
		private Damageable damageable;
		[SerializeField] private Material material;
		public Color startColor;
		public Color endColor;
		[SerializeField] private LevelAesteticData levelAesteticData;
		private void Awake()
		{
			damageable = GetComponent<Damageable>();
			//startColor *= levelAesteticData.neonIntensity;
			//endColor*=levelAesteticData.neonIntensity;
		}

		private void Start()
		{
			HealthChanged(damageable.GetMaxHealth());
		}

		private void OnEnable()
		{
			damageable.OnHealthChanged += HealthChanged;
		}

		private void OnDisable()
		{
			damageable.OnHealthChanged -= HealthChanged;
		}

		private void Update()
		{
			HealthChanged(damageable.currentHealth);
		}

		private void ChangeWorldColor(Color newColor)
		{
			material.SetColor("_EmissionColor", newColor* levelAesteticData.neonIntensity);
			material.color = newColor;
		}

		private void HealthChanged(float health)
		{
			Color newColor = Color.Lerp(endColor,startColor , health / damageable.GetMaxHealth());
			ChangeWorldColor(newColor);
		}
	}
}