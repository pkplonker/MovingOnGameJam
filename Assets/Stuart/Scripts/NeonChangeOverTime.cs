using System;
using System.Collections;
using UnityEngine;

namespace Stuart.Scripts
{
    public class NeonChangeOverTime : MonoBehaviour
    {
        private bool goingUp;
        [SerializeField] [Range(0.5f,4f)] private float neonIntensityLow=1f;
        [SerializeField] [Range(0.5f,4f)] private float neonIntensityHigh = 2f;
        [SerializeField] private float changeSpeed = 2f;
        [SerializeField] private Material neonMaterial;

        [SerializeField] LevelAesteticData levelAesteticData;
        private void Update()
        {
            if (levelAesteticData.neonIntensity<=neonIntensityHigh)
            {
                levelAesteticData.neonIntensity =
                    Mathf.Lerp(neonIntensityLow, neonIntensityHigh, changeSpeed * Time.deltaTime);
            }
            else
            {
                levelAesteticData.neonIntensity =
                    Mathf.Lerp(neonIntensityHigh, neonIntensityLow, changeSpeed * Time.deltaTime);
            }
         
        }
    }
}
