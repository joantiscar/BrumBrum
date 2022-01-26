using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonsInterficies : MonoBehaviour
{
    public void Revivir(){ // Reviu la batalla
        SceneManager.LoadScene(Singleton.currentScene());
    }

    public void Inici(){ // Torna al menu inici
        SceneManager.LoadScene("MenuInici");
    }

    public void ComencaJoc(){
        Singleton.reset();
        SceneManager.LoadScene("Scene1_Precamping");
    }

    public void Quit(){ // Surt del joc
        Application.Quit();
    }
}
