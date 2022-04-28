using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

namespace Stuart.Scripts
{
    public class NeonChangeOverTime : MonoBehaviour
    {
        [SerializeField] private LevelAesteticData levelAesteticData;
        [SerializeField] [Range(0.5f,4f)] private float neonIntensityLow=1f;
        [SerializeField] [Range(0.5f,4f)] private float neonIntensityHigh = 2f;
        [SerializeField] private float changeSpeed = 2f;
      
        private void Update()
        {
            ChangeNeonIntensitySettings();
        }
        private void ChangeNeonIntensitySettings()
        {
            
            levelAesteticData.neonIntensity= Mathf.PingPong(Time.time*changeSpeed, neonIntensityHigh-neonIntensityLow);

          
        }
    }
}
