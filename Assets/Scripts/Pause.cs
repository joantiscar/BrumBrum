using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;


public class Pause : MonoBehaviour
{
    bool paused = false;
    bool esq_used = false;
    bool resume = false;
    public bool enCombate = false;
    public GameObject menu;
    public GameObject options;

    void Start(){
        if(menu==null && options==null){
            menu = this.transform.GetChild(1).transform.GetChild(2).transform.GetChild(4).gameObject;
            options = this.transform.GetChild(1).transform.GetChild(2).transform.GetChild(5).gameObject;
        }
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape) && !paused){
            paused = togglePause();
            Singleton.toggleMenu();
            if(!enCombate) FindObjectOfType<ThirdPersonMovement>().isTalkKing();
            menu.SetActive(true);
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

    public void opciones (){
        menu.SetActive(false);
        options.SetActive(true);
    }
    public void renaudarPartida (){
        menu.SetActive(false);
        paused = togglePause();
        if(!enCombate) FindObjectOfType<ThirdPersonMovement>().isTalkKing();
        Singleton.toggleMenu();
    }
    public void salirPartida (){
        Debug.Log ("MenuPrincipal");
    }
    public void salirJuego(){
        Debug.Log ("Adioh");
    }
    public void manualUsuario(){
        Debug.Log ("Controls");
    }
    public void volverMenu(){
        menu.SetActive(true);
        options.SetActive(false);
    }
    public void senibilidadRaton(){
        Debug.Log ("Desplegar Sensibilitat");
    }
    public void nivelSonido(){
        Debug.Log ("Desplegar Sonido");
    }
}