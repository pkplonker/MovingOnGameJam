using System;
using UnityEngine;

namespace Stuart.Scripts
{
    public class EnemyDamageable : Damageable
    {
        private LevelController levelController;
        private void Start()
        {
            levelController = FindObjectOfType<LevelController>();
            levelController.RegisterEnemy(this);
        }
    }
}
