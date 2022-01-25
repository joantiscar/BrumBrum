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
}
