using System;
using System.Collections.Generic;
using Stuart.Scripts.SO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Stuart.Scripts
{
    public class LevelController : MonoBehaviour
    {
        private float startTime;
        [SerializeField] private int level;
        [SerializeField] private float timeFactor = 1f;
        [SerializeField] private float killsFactor = 1f;
        [SerializeField] private float healthFactor = 1f;
        [SerializeField] private Damageable player;
        private List<EnemyDamageable> enemies = new List<EnemyDamageable>();
        [SerializeField] private GameOverData gameOverData;
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
            float timeTaken = TimeTaken();
            float killsScore = KillsScore();
            float healthScore = HealthScore();
            float timeScore = TimeScore(timeTaken);
            gameOverData.AddScore(new ScoreData(isWin,level,killsScore,timeScore,healthScore,timeTaken));
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
               if (enemy == null)
               {
                   if (TryGetComponent(out Damageable damageable))
                   {
                       if (damageable.isDead) dead++;
                       else alive++;
                   }
                   else dead++;
               }
               else dead++;
           }

           int total = alive + dead;
           if (total == 0) return killsFactor;
           return (killsFactor * ((float)dead / total));
       }

       private float TimeScore(float timeTaken)
       {
           return (timeFactor / timeTaken);
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
