using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialegHestia1 : GameDialogue
{
    public void Awake()
    {
        characterName = "Hestia";
        dialogue = new string[] { "Te quiero, cielo. Verás como mañana lo conseguimos.", 
        "¡¡¡Shhhhh!!! Dime, ¿quién tiene siempre la razón? ¡Yo! Pues ya está.",
        "¡Así me gusta!" };
        playerDialogue = new string[] { "No sé cómo irá...",
        "Así es, cariño." };
        dialogue3 = new string[] {};
        dialogue4 = new string[] {};
        dialogueOrder = new int[] {};
    }
}

