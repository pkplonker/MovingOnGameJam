using System;
using Stuart.Scripts.SO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Stuart.Scripts
{
    public class LevelOver : MonoBehaviour
    {
        [SerializeField] private GameOverData gameOverData;
        [SerializeField] private TextMeshProUGUI win;
        [SerializeField] private TextMeshProUGUI timeTaken;
        [SerializeField] private TextMeshProUGUI timeScore;
        [SerializeField] private TextMeshProUGUI killScore;
        [SerializeField] private TextMeshProUGUI healthScore;
        [SerializeField] private TextMeshProUGUI overallScore;
        public void Restart()
        {
            SceneManager.LoadScene("First Level");
        }
        public void MainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
        private void OnEnable()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }   
        private void Start()
        {
            ScoreData latestScore = gameOverData.GetLatestScore();
            if (latestScore == null)
            {
                Debug.Log("no data!");
                return;
            }

            int minutes = (int)(latestScore.timeTaken/60);
            int seconds = (int)(latestScore.timeTaken-(int)(latestScore.timeTaken/60));
            string timeString = "";
            if (minutes >= 10) timeString += minutes;
            else
            {
                timeString += '0';
                timeString += minutes;
            }

            timeString += ':';
            if (seconds >= 10) timeString += seconds;
            else
            {
                timeString += '0';
                timeString += seconds;
            }
            win.text = latestScore.isWin ? "You Win!" : "You lost!";
            timeTaken.text = "Time Taken: " + timeString;
            timeScore.text = "Time Score: " + (int)latestScore.timeScore;
            killScore.text = "Kill Score: " + (int)latestScore.killsScore;
            healthScore.text = "Health Score: " + (int)latestScore.healthScore;
            overallScore.text = "Overall score: " + (int)latestScore.overallScore;

        }
    }
}
