using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

namespace Stuart.Scripts
{
    public class NeonChangeOverTime : MonoBehaviour
    {
        private bool goingUp;
        [SerializeField] [Range(0.5f,4f)] private float bloomIntensityLow=3f;
        [SerializeField] [Range(0.5f,4f)] private float bloomIntensityHigh = 4f;
        [SerializeField] private float changeSpeed = 2f;
        [SerializeField] Bloom bloom;
      
        private void Update()
        {
            ChangeBloomIntensitySettings();
        }
        private void ChangeBloomIntensitySettings()
        {
           
            
            //var postProcessVolume = FindObjectOfType<PostProcessVolume>();
            var postProcessVolume = GetComponent<Volume>();
            if (postProcessVolume == null)
            {
                Debug.LogWarning("No post processing");
                return;
            }
            Bloom bloom = postProcessVolume.profile.GetSetting<Bloom>();

            var intensity = new UnityEngine.Rendering.PostProcessing.FloatParameter();
            intensity.value = bloomIntensityLow+ Mathf.PingPong(Time.time*changeSpeed, bloomIntensityHigh-bloomIntensityLow);

            bloom.intensity = intensity;
            bloom.intensity.value = intensity.value;
            bloom.enabled.value = true;   
        }
    }
}
