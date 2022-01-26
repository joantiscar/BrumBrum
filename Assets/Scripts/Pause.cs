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
    public GameObject menu;
    public GameObject options;
    public Animator animSensibilitat;
    public GameObject SliderSensibilitat;
    public Animator animSo;
    public GameObject SliderSo;

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
        if ((sensibilitat || so) && ended){
            if (animSensibilitat.GetCurrentAnimatorStateInfo(0).IsName("Default") && animSo.GetCurrentAnimatorStateInfo(0).IsName("Default")){
                sensibilitat = false;
                so = false;
                ended = false;
                menu.SetActive(true);
                options.SetActive(false);
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

public void opciones (){
        menu.SetActive(false);
        options.SetActive(true);
    }
    public void renaudarPartida (){
        menu.SetActive(false);
        paused = togglePause();
        FindObjectOfType<ThirdPersonMovement>().isTalkKing();
        Singleton.toggleMenu();
    }
    public void salirPartida (){
        SceneManager.LoadScene("MenuInici");
    }
    public void salirJuego(){
        Application.Quit();
    }
    public void manualUsuario(){
        Debug.Log ("Controls");
    }
    public void volverMenu(){
        ended = true;
        if (sensibilitat || so){
            if (sensibilitat){
                animSensibilitat.SetBool("Iniciat", false);
                SliderSensibilitat.SetActive(true);
            }
            if (so){
                animSo.SetBool("Iniciat", false);
                SliderSo.SetActive(false);
            }
        }
        else{
            ended = false;
            menu.SetActive(true);
            options.SetActive(false);
        }
    }
    public void senibilidadRaton(){
        if (!sensibilitat){
            animSensibilitat.SetBool("Iniciat", true);
            SliderSensibilitat.SetActive(true);
            sensibilitat = true;
        }
        else {
            animSensibilitat.SetBool("Iniciat", false);
            SliderSensibilitat.SetActive(false);
            sensibilitat = false;
        }

    }
    public void nivelSonido(){
        if (!so){
            animSo.SetBool("Iniciat", true);
            SliderSo.SetActive(true);
            so = true;
        }
        else {
            animSo.SetBool("Iniciat", false);
            SliderSo.SetActive(false);
            so = false;
        }
    }
}