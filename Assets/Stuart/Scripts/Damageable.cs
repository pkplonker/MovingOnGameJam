using System;
using Stuart.Scripts.SO.Character;
using UnityEngine;

namespace Stuart.Scripts
{
	public class Damageable : MonoBehaviour
	{
		public float currentHealth = 1f;
		[SerializeField] private CharacterStats stats;
		public bool isDead { get; private set; }
		public event Action<float> OnHealthChanged;
		public event Action OnDeath;
		public int teamId = 1;

		private void Awake()
		{
			if (stats != null) currentHealth = stats.maxHealth;
		}

		public virtual void Heal(float amount)
		{
			if (amount <= 0)
			{
				Debug.LogWarning("Cannot heal negative");
				return;
			}

			currentHealth += amount;
			if (currentHealth > stats.maxHealth) currentHealth = stats.maxHealth;
			OnHealthChanged?.Invoke(currentHealth);
		}

		public virtual void TakeDamage(float amount)
		{
			if (amount <= 0)
			{
				Debug.LogWarning("Cannot damage negative");
				return;
			}

			currentHealth -= amount;
			if (currentHealth <= 0)
			{
				currentHealth = 0;
				Die();
			}

			OnHealthChanged?.Invoke(currentHealth);
		}

		private void Die()
		{
			OnDeath?.Invoke();
			isDead = true;
		}
	}
}