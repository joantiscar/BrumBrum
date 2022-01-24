using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Pause : MonoBehaviour
{
    bool paused = false;

    void Update(){
        pause();
        if(Input.GetKeyDown(KeyCode.Escape))
            paused = togglePause();
    }
    //void OnGUI(){}
    void pause()
     {
         if(paused)
         {
             if(Input.GetKeyDown(KeyCode.Escape))
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