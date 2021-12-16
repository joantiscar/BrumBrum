using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialegHestia1 : GameDialogue
{
    public void Awake()
    {
        characterName = "Hestia";
        dialogue = new string[] { "Te quiero, cielo. Verás como mañana lo conseguimos.", 
        "¡Así me gusta!",
        "¡¡¡Shhhhh!!! Dime, ¿quién tiene siempre la razón? ¡Yo! Pues ya está." };
        playerDialogue = new string[] { "Así es, cariño.",
        "No sé cómo irá..." };
        dialogue3 = new string[] {};
        dialogue4 = new string[] {};
        dialogueOrder = new int[] {};
    }
}

