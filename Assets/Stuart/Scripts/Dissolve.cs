using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Stuart.Scripts
{
    public class Dissolve : MonoBehaviour
    {
        private Damageable damageable;
        [SerializeField] private float dissolveSpeed = 2f;
        [SerializeField] float newValue;
        private Material[] materials;
        [SerializeField] Shader shader;

        private void Awake()
        {
            damageable = GetComponent<Damageable>();
            if(damageable==null) Destroy(this);
            
        }

        private void OnEnable()
        {
            if(damageable!=null)  damageable.OnDeath += TriggerDissolve;
        }

        private void OnDisable()
        {
             if(damageable!=null)  damageable.OnDeath -= TriggerDissolve;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                TriggerDissolve();
                Debug.Log("starting dissolve");
            }
        }

        public void TriggerDissolve()
        {
            MeshRenderer mr = GetComponent<MeshRenderer>();
            if (mr == null)
            {
                mr = GetComponentInChildren<MeshRenderer>();
            }

            if (mr == null)
            {Debug.Log("Unable to locate mesh renderer");
                return;
            }

            materials = mr.materials;
            foreach (var mat in materials)
            {
                if (mat.shader != shader)
                {
                    mat.shader = shader;
                }
            }
           newValue = materials[3].GetFloat("NoiseStrength") - (dissolveSpeed * Time.deltaTime);
           StartCoroutine(DissolveCor());

        }

        IEnumerator DissolveCor()
        {
            while (newValue > -10)
            {
               
                //Debug.Log("New strength = " + newValue);
                foreach (var mat in materials)
                {
                    if (mat == null) continue;
                   
                        SetHeight(mat, newValue);

                   
                    {
                    //    mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, mat.color.a - Time.deltaTime);
                    //mat.shader = shader;

                    }
                }
//Debug.Log(newValue);
                newValue = materials[3].GetFloat("NoiseStrength") - (dissolveSpeed * Time.deltaTime);
                yield return null;
            }

            dissolveSpeed -= Time.deltaTime;
            if (dissolveSpeed < 1) dissolveSpeed = 1;
        }

        private void SetHeight(Material mat, float height)
        {
            mat.SetFloat("NoiseStrength",newValue);
           // mat.SetFloat("CutOffHeight",height);

        }
    }
}
