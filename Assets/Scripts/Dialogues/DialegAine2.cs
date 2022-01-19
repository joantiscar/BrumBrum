using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialegAine2 : GameDialogue
{
    public void Awake()
    {
        characterName = "Áine";
        dialogue = new string[] { "…", 
        "Gracias por salvarme la vida.",
        "No llores."};
        playerDialogue = new string[] { "A ti por salvarnos a todos…",
        "Está bien. Nos veremos de nuevo algún dia..."};
        dialogue3 = new string[] {};
        dialogue4 = new string[] {};
        dialogueOrder = new int[] {};
    }
}
