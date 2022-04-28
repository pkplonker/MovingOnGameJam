using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{

    public FMODUnity.EventReference inputsound;
    bool playerismoving;
    public float walkingspeed;
    public float runningspeed;

void Update () 
    {
        if (Input.GetAxis ("Vertical") >= 0.01f || Input.GetAxis ("Horizontal") >= 0.01f || Input.GetAxis ("Vertical") <= -0.01f || Input.GetAxis ("Horizontal") <= -0.01f) 
        {
            //Debug.Log ("Moving");
            playerismoving = true;
        } 
        else if (Input.GetAxis ("Vertical") == 0 || Input.GetAxis ("Horizontal") == 0) 
        {
            //Debug.Log ("not moving");
            playerismoving = false;
        }
    }
    
    void CallFootsteps ()
    {
        if (playerismoving == true) 
        {
            //Debug.Log ("moving");
            FMODUnity.RuntimeManager.PlayOneShot (inputsound);
        } 
    }

    void Start ()
    {  
        InvokeRepeating ("CallFootsteps", 0, walkingspeed);
    }

    void OnDisable ()
    {
        playerismoving = false;
    }
}
