using System;
using Stuart.Scripts.SO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Stuart.Scripts
{
    public class MainMenu : MonoBehaviour
    {
        private void OnEnable()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void Settings()
        {
            Debug.Log("Open Settings");
        }
        
        public void Play()
        {
            Debug.Log("Play");
            SceneManager.LoadScene("First Level");
        }

        public void Highscores()
        {
            Debug.Log("Open Highscores");
            SceneManager.LoadScene("Highscores");

        }

        public void Exit()
        {
            Debug.Log("Exit");

            Application.Quit();
        }
    }
}
