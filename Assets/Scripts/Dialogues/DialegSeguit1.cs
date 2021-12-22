using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialegSeguit1 : GameDialogue
{
    public void Awake()
    {
        characterName = "Cyrus";
        playerName = "Irix";
        dialogue = new string[] { "¿Qué hace este cobarde aquí?", 
        "¡¿Y la defiende un ingenuo como tú?!", 
        "¡Que no fuiste capaz de atacarlo por tus necios recuerdos!", 
        "…",
        "Tienes toda la razón…",
        "Me contuve.",
        "Y fue un error que no pienso cometer una tercera vez."};
        playerDialogue = new string[] {"Es cierto que huir de la batalla dejándonos solos fue despreciable, pero me ha salvado de una muerte casi segura.", 
        "Sabes tan bien como yo que la necesitamos.", 
        "Tienes toda la razón: dudé, y eso provocó una derrota. Pero tú también lo hiciste.", 
        "Fue por eso que tuvimos que protegernos el uno al otro de esa forma desesperada.", 
        "Con que uno de los dos hubiera estado más concentrado, no hubiera pasado esto.",
        "Es imposible no sentir lástima por tenerlo justamente a él como enemigo.",
        "Sabes perfectamente de qué hablo...",
        "Deberíamos acampar, ya es tarde…"};
        dialogue3 = new string[] {};
        dialogue4 = new string[] {};
        dialogueOrder = new int [] {0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 1};
    }
}

