using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Pause : MonoBehaviour
{
    bool paused = false;
    void Update(){
            if(Input.GetButtonDown("pauseButton"))
                paused = togglePause();
        }
    void OnGUI()
     {
         if(paused)
         {
             if(Input.GetButtonDown("pauseButton"))
                 paused = togglePause();
         }
     }

    bool togglePause(){
        if(Time.timeScale == 0f){
            Time.timeScale = 1f;
            return(false);
        }
        else{
            Time.timeScale = 0f;
            return(true);    
        }
    }
}