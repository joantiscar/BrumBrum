using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonsInterficies : MonoBehaviour
{
    public void Revivir(){ // Reviu la batalla
        SceneManager.LoadScene("CombateAnimado");
    }

    public void Inici(){ // Torna al menú inici
        SceneManager.LoadScene("MenuInici");
    }

    public void ComencaJoc(){
        SceneManager.LoadScene("MecaniquesAcabades");
    }

    public void Quit(){ // Surt del joc
        Application.Quit();
    }
}
