using System;
using UnityEngine;

namespace Stuart.Scripts
{
    public class LevelEnd : MonoBehaviour
    {
        private LevelController levelController;
        private void OnEnable()
        {
            levelController = FindObjectOfType<LevelController>();
            if(levelController==null) Debug.LogError("Missing level controller");
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player")) levelController.GameOver(true);
        }
    }
}
