using Stuart.Scripts.SO;
using TMPro;
using UnityEngine;

namespace Stuart.Scripts
{
    public class HighscoresEntry : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timeTaken;
        [SerializeField] private TextMeshProUGUI timeScore;
        [SerializeField] private TextMeshProUGUI killScore;
        [SerializeField] private TextMeshProUGUI healthScore;
        [SerializeField] private TextMeshProUGUI overallScore;

        public void Init(ScoreData scores)
        {
            int minutes = (int)(scores.timeTaken/60);
            int seconds = (int)(scores.timeTaken-(int)(scores.timeTaken/60));
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
            timeTaken.text = "Time: " + timeString;
            timeScore.text = "Time Score: " + (int)scores.timeScore;
            killScore.text = "Kill Score: " + (int)scores.killsScore;
            healthScore.text = "Health Score: " + (int)scores.healthScore;
            overallScore.text = "Overall score: " + (int)scores.overallScore;
        }
    }
}
