using System;
using System.Collections.Generic;
using UnityEngine;

namespace Stuart.Scripts
{
    public class LevelController : MonoBehaviour
    {
        private float startTime=0f;
        [SerializeField] private float timeFactor = 1f;
        [SerializeField] private float killsFactor = 1f;
        [SerializeField] private float healthFactor = 1f;
        [SerializeField] private Damageable player;
        private List<EnemyDamageable> enemies = new List<EnemyDamageable>();
        private void Start()
        {
            startTime = Time.time;
        }

        private void OnEnable()
        {
            player.OnDeath += PlayerDeath;
        }

        private void OnDisable()
        {
            player.OnDeath -= PlayerDeath;
        }

        private void PlayerDeath()
        {
            GameOver(false);
        }

        public void GameOver(bool isWin)
       {
           if (isWin) Win();
           else Lose();
       }

       private void Win()
       {
           Debug.Log("YOU WIN");

           float score = TimeScore(TimeTaken()) * KillsScore() * HealthScore();

       }

       private float HealthScore()
       {
           return healthFactor * ( player.currentHealth/ player.GetMaxHealth());
       }

       private float KillsScore()
       {
           int alive=0;
           int dead=0;
           foreach (var enemy in enemies)
           {
               if (enemy == null) dead++;
               else alive++;
           }

           int total = alive + dead;
           if (total == 0) return killsFactor;
           return killsFactor * ((float)dead / total);
       }

       private float TimeScore(float timeTaken)
       {
           return timeFactor / timeTaken;
       }

       private void Lose()
       {
           Debug.Log("YOU LOSE");
       }
       private float TimeTaken()
       {
           return Time.time - startTime;
       }

       public void RegisterEnemy(EnemyDamageable enemyDamageable)
       {
           enemies.Add(enemyDamageable);
       }
    }
}
