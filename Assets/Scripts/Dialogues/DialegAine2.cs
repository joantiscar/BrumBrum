using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialegAine2 : GameDialogue
{
    public void Awake()
    {
        characterName = "Áine";
        dialogue = new string[] { "...",
        "A ti por salvarnos a todos…",
        "Está bien. Nos veremos de nuevo algún dia..."};
        playerDialogue = new string[] { "Gracias por salvarme la vida.",
        "No llores."};
        dialogue3 = new string[] {};
        dialogue4 = new string[] {};
        dialogueOrder = new int[] {};
    }
}
