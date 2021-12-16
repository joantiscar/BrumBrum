using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialegSeguit5 : GameDialogue
{
    public void Awake()
    {
        characterName = "Irix";
        playerName = "Hestia";
        dialogue = new string[] { "Debo matarlo.", 
        "Cariño, sabes mejor que nadie que debo hacer esto.",
        "Hemos fracasado en la misión. El mundo ya está condenado.",
        "La única manera de detener la destrucción total, es matando a Zeth.",
        "Y eso hará que también se destruya la semilla de la vida. Y con ella el resto del mundo.",
        "Pero eso dará esperanzas para un nuevo mundo dentro de a saber cuanto.",
        "No es solo mi deber, sino el deber de cualquiera que sea capaz de detener este desastre.",
        "Debéis traspasarme todo el poder que os quede.",
        "Yo ejecutaré mi técnica prohibida y daré mi vida en la batalla y acabaré con esto."};
        playerDialogue = new string[] {"¡No puedes hacer eso!",
        "..."};
        dialogue3 = new string[] {};
        dialogue4 = new string[] {};
        dialogueOrder = new int [] {0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0};
    }
}