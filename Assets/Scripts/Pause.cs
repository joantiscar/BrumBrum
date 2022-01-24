using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Pause : MonoBehaviour
{
    bool paused = false;
    bool esq_used = false;
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            paused = togglePause();
            esq_used = true;
        }
        else esq_used = false;
        pause();
    }
    //void OnGUI(){}
    void pause()
     {
         if(paused && !esq_used)
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