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
            SceneManager.LoadScene("Level1", LoadSceneMode.Additive);
        }

        public void Highscores()
        {
            Debug.Log("Open highscores");
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}
