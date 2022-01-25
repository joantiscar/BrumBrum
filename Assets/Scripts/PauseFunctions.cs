using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseFunctions : MonoBehaviour
{
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
    public void volverMenu(){
        FindObjectOfType<Pause>().volverMenu();
    }
    public void senibilidadRaton(){
        FindObjectOfType<Pause>().senibilidadRaton();
    }
    public void nivelSonido(){
        FindObjectOfType<Pause>().nivelSonido();
    }
}
