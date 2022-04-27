using System;
using Stuart.Scripts.SO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Stuart.Scripts
{
    public class MainMenu : MonoBehaviour
    {
      

        public void Settings()
        {
            Debug.Log("Open Settings");
        }
        
        public void Play()
        {
            Debug.Log("Play");
            SceneManager.LoadScene("Level1");
        }

        public void Highscores()
        {
            Debug.Log("Open Highscores");
        }

        public void Exit()
        {
            Debug.Log("Exit");

            Application.Quit();
        }
    }
}
