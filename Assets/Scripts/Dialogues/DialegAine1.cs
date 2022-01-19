using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialegAine1 : GameDialogue
{
    public void Awake()
    {
        characterName = "Áine";
        dialogue = new string[] { "Irix, lo siento muchísimo…", 
        "Gracias…" };
        playerDialogue = new string[] { "No es nada. Me has curado, te debo la vida." };
        dialogue3 = new string[] {};
        dialogue4 = new string[] {};
        dialogueOrder = new int[] {};
    }
}
