using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialegHestia2 : GameDialogue
{
    public void Awake()
    {
        characterName = "Hestia";
        dialogue = new string[] { "…", 
        "Yo también te amo."};
        playerDialogue = new string[] { "Te amo…"};
        dialogue3 = new string[] {};
        dialogue4 = new string[] {};
        dialogueOrder = new int[] {};
    }
}

