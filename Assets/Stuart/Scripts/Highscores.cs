using System;
using System.Collections.Generic;
using System.Linq;
using Stuart.Scripts.SO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Stuart.Scripts
{
    public class Highscores : MonoBehaviour
    {
        [SerializeField] private GameOverData gameOverData;
        [SerializeField] private GameObject highscoresUIPrefab;
        [SerializeField] private Transform entryUIContainer;
        private List<ScoreData> top5Scores = new List<ScoreData>();
        private List<GameObject> highscoreUIObjects = new List<GameObject>();
        [SerializeField] private int numberOfScoresToDisplay = 5;

        public void Back()
        {
            SceneManager.LoadScene(0);
        }

        private void OnEnable()
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            GetTopFiveScores();
            RemoveOldScores();
            AddNewScores();
        }

        private void GetTopFiveScores()
        {
            top5Scores = gameOverData.scoreDatas.OrderBy(o=>o.overallScore).ToList();
            if (top5Scores.Count > 1)
            {
                top5Scores.Reverse(0, top5Scores.Count);

            }
            if (gameOverData.scoreDatas.Count > 5)
            {
                top5Scores.RemoveRange(numberOfScoresToDisplay,gameOverData.scoreDatas.Count-numberOfScoresToDisplay);
            }
        }

        private void RemoveOldScores()
        {
            foreach (var obj in highscoreUIObjects)
            {
                Destroy(obj);
            }
        }

        private void AddNewScores()
        {
            foreach (var scores in top5Scores)
            {
              HighscoresEntry entry = Instantiate(highscoresUIPrefab, entryUIContainer).GetComponent<HighscoresEntry>();
              highscoreUIObjects.Add(entry.gameObject);
              entry.Init(scores);
            }
        }
    }
}
