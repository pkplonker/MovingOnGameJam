using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Stuart.Scripts.SO
{
    [CreateAssetMenu(fileName = "New GameOverData", menuName = "Gameover Data")]
    public class GameOverData : ScriptableObject
    {
        public List<ScoreData> scoreDatas;

        public void AddScore(ScoreData data)
        {
            if (scoreDatas == null)
            {
                scoreDatas = new List<ScoreData>();
            }
            scoreDatas.Add(data);
        }
        public ScoreData GetLatestScore()
        {
            if (scoreDatas == null) return null;
            return scoreDatas[scoreDatas.Count - 1];
        }
    }
[Serializable]
    public class ScoreData
    {
        public bool isWin;
        public int level;
        public float overallScore;
        public float healthScore;
        public float killsScore;
        public float timeScore;
        public float timeTaken;
        public ScoreData(bool isWin, int level, float killsScore, float timeScore, float healthScore, float timeTaken)
        {
            this.isWin = isWin;
            overallScore = timeScore*killsScore*healthScore;
            this.level = level;
            this.killsScore = killsScore;
            this.healthScore = healthScore;
            this.timeScore = timeScore;
            this.timeTaken = timeTaken;
            SceneManager.LoadScene("LevelOverScreen");
        }

       
    }
}
