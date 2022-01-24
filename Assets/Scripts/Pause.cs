using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;


public class Pause : MonoBehaviour
{
    bool paused = false;
    bool esq_used = false;
    GameObject menu;

    void Start(){
        menu = this.transform.GetChild(1).transform.GetChild(2).transform.GetChild(4).gameObject;
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape) && !paused){
            paused = togglePause();
            esq_used = true;

            menu.SetActive(true);
        }
        else esq_used = false;
        pause();
    }
    //void OnGUI(){}
    void pause()
     {
         if(paused && !esq_used)
         {
             if(Input.GetKeyDown(KeyCode.Escape)){
                menu.SetActive(false);
                paused = togglePause();
             }
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