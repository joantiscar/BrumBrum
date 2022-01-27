using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Pause : MonoBehaviour
{
    bool paused = false;
    bool resume = false;
    bool sensibilitat = false;
    bool so = false;
    bool ended = false;
    public bool enCombate = false;
    int anterior;
    int _godMode = 0;
    public GameObject ThirdPersonCamera;
    public GameObject menu;
    public GameObject options;
    public GameObject controls;
    public GameObject Sensibilitat;
    public GameObject SliderSensibilitat;
    public GameObject So;
    public GameObject SliderSo;
    public GameObject Layout1;
    public Animator Layout2;
    public GameObject Layout3;
    public UICombate UICombate;

    /*void Start(){
        if(menu==null && options==null){
            menu = this.transform.GetChild(1).transform.GetChild(2).transform.GetChild(4).gameObject;
            options = this.transform.GetChild(1).transform.GetChild(2).transform.GetChild(5).gameObject;
        }
    }*/
    void Start (){
        if (ThirdPersonCamera != null){
            ThirdPersonCamera.SetActive(false);
            ThirdPersonCamera.SetActive(true);
        }
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "MenuInici" && 
            SceneManager.GetActiveScene().name != "GameOver" && SceneManager.GetActiveScene().name != "Victoria" && !Singleton.LevelupPanel()){
            paused = togglePause();
            Singleton.toggleMenu();
            if(!enCombate/* && !Singleton.dialegs()*/) FindObjectOfType<ThirdPersonMovement>().isTalkKing();
            if (paused){
                menu.SetActive(true);
                if(enCombate) UICombate.habilitarUI(false,true);
            }
            else {
                menu.SetActive(false);
                options.SetActive(false);
                controls.SetActive(false);
                if(enCombate) UICombate.habilitarUI(true,true);
            }
        }
        if ((sensibilitat || so) && ended){
                sensibilitat = false;
                so = false;
                ended = false;
                menu.SetActive(true);
                options.SetActive(false);
        }
    }

    public bool togglePause(){
        if(Time.timeScale == 0f){
            Time.timeScale = 1f;
            return(false);
        }
        else{
            GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().Pause();
            Time.timeScale = 0f;
            return(true);    
        }
    }
    public void godMode (){
        _godMode ++;
        if (_godMode == 9){
            Layout1.SetActive(false);
            Layout2.SetBool("GodMode", true);
            Layout3.SetActive(true);
            Singleton.ActivateGodMode();
        }
    }
    public void empezarPartida(){
        Singleton.reset();
        SceneManager.LoadScene("Scene1_Precamping");
    }
    public void opciones (){
        menu.SetActive(false);
        options.SetActive(true);
    }
    public void renaudarPartida (){
        menu.SetActive(false);
        paused = togglePause();
        if(!enCombate) FindObjectOfType<ThirdPersonMovement>().isTalkKing();
        else if(enCombate) UICombate.habilitarUI(true,true);
        Singleton.toggleMenu();
    }
    public void salirPartida (){
        paused = togglePause();
        Singleton.toggleMenu();
        SceneManager.LoadScene("MenuInici");
    }
    public void salirJuego(){
        Application.Quit();
    }
    public void manualUsuarioMenuPrincipal(){
        anterior = 0;
        menu.SetActive(false);
        controls.SetActive(true);
    }
    public void manualUsuario(){
        anterior = 1;
        options.SetActive(false);
        controls.SetActive(true);
    }
    public void volverManualUsuario (){
        if (anterior == 0){
            menu.SetActive(true);
            controls.SetActive(false);
        }
        else {
            options.SetActive(true);
            controls.SetActive(false); 
        }
    }
    public void volverMenu(){
        ended = true;
        if (sensibilitat || so){
            if (sensibilitat){
                Sensibilitat.SetActive(false);
                SliderSensibilitat.SetActive(true);
            }
            if (so){
                So.SetActive(false);
                SliderSo.SetActive(false);
            }
        }
        else{
            ended = false;
            menu.SetActive(true);
            options.SetActive(false);
        }
    }
    public void senibilidadRatonMenuPausa(){
        if (!sensibilitat){
            Sensibilitat.SetActive(true);
            SliderSensibilitat.SetActive(true);
            sensibilitat = true;
        }
        else {
            Sensibilitat.SetActive(false);
            SliderSensibilitat.SetActive(false);
            sensibilitat = false;
        }

    }
    public void nivelSonidoMenuPausa(){
        if (!so){
            So.SetActive(true);
            SliderSo.SetActive(true);
            so = true;
        }
        else {
            
            So.SetActive(false);
            SliderSo.SetActive(false);
            so = false;
        }
    }
}