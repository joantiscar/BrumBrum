using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialegCyrus2 : GameDialogue
{
    public void Awake()
    {
        characterName = "Cyrus";
        dialogue = new string[] { "…", 
        "Un placer haber sido tu amigo.",
        "Nos veremos en el otro lado." };
        playerDialogue = new string[] { "Lo siento mucho...",
        "Viejo amigo..." };
        dialogue3 = new string[] {};
        dialogue4 = new string[] {};
        dialogueOrder = new int[] {};
    }
}
