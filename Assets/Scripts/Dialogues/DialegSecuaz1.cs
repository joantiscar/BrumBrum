using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialegSecuaz1 : GameDialogue
{
    public void Awake()
    {
        characterName = "Secuaz";
        dialogue = new string[] { "¿Qué habéis decidido?", 
        "Lo que esperaba.",
        "Necios." };
        playerDialogue = new string[] { "Aceptamos.",
        "Nos negamos." };
        dialogue3 = new string[] {};
        dialogue4 = new string[] {};
        dialogueOrder = new int[] {};
    }
}