using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseFunctions : MonoBehaviour
{
    public void empezarPartida(){
        FindObjectOfType<Pause>().empezarPartida();
    }
    public void opciones (){
        FindObjectOfType<Pause>().opciones();
    }
    public void renaudarPartida (){
        FindObjectOfType<Pause>().renaudarPartida();
    }
    public void salirPartida (){
        FindObjectOfType<Pause>().salirPartida();
    }
    public void salirJuego (){
        FindObjectOfType<Pause>().salirJuego();
    }
    public void manualUsuario(){
        FindObjectOfType<Pause>().manualUsuario();
    }
    public void manualUsuarioMenuPrincipal(){
        FindObjectOfType<Pause>().manualUsuarioMenuPrincipal();
    }
    public void volverManualUsuario(){
        FindObjectOfType<Pause>().volverManualUsuario();
    }
    public void volverMenu(){
        FindObjectOfType<Pause>().volverMenu();
    }
    public void senibilidadRatonMenuPausa(){
        FindObjectOfType<Pause>().senibilidadRatonMenuPausa();
    }
    public void nivelSonidoMenuPausa(){
        FindObjectOfType<Pause>().nivelSonidoMenuPausa();
    }
}
