using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialegSeguit2 : GameDialogue
{
    public void Awake()
    {
        characterName = "Secuaz";
        playerName = "Cyrus";
        name3 = "Irix";
        name4 = "Hestia";
        dialogue = new string[] {"Relajaos. No he venido a luchar.", 
        "Mi Señor quiere que os dirijáis directamente al trono, sin pelear.", 
        "Tenéis dos opciones.", 
        "Primera: seguirme e ir directamente a luchar contra quien de verdad debéis.", 
        "Segunda: quedaros aquí esperando a que os ataquemos todos a la vez.",
        "Si venís conmigo, el Señor Zeth os despejará el camino creando una barrera mágica para que el resto de nosotros no os ataque.",
        "Bueno, ahí tienes razón. Pero qué más dará si os atacamos dentro o fuera del castillo.",
        "Si entráis os quedareis sin escapatoria, es cierto. ¿Pero qué son unos cuantos secuaces para el grupo que debe derrotar al Señor Oscuro?"};
        playerDialogue = new string[] {"¿Y por qué íbamos a creerte?", 
        "Lo más fácil es que sea una trampa.", 
        "¿Pero qué haces?"/*, 
        "Mmm…", 
        "Pensémoslo primero."*/};
        dialogue3 = new string[] {"Está bien. Te acompañaremos."};
        dialogue4 = new string[] {"No, Cyrus. Irix tiene razón.", 
        "Tenemos que aceptar la oferta.", 
        "Es posible que sea una trampa, pero esta vez creo que Zeth no miente. Debe tener algo en mente."};
        dialogueOrder = new int [] {0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 2, 1, 3, 3, 3/*, 1, 1*/};
    }
}
