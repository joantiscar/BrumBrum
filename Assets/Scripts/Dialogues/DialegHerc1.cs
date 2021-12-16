using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialegHerc1 : GameDialogue
{
    public void Awake()
    {
        characterName = "Herc";
        dialogue = new string[] { "Lo vamos a destrozar.", 
        "¡¡¡LO VAMOS A DESTROZAAAAAR!!!" };
        playerDialogue = new string[] { "or seso e si." };
        dialogue3 = new string[] {};
        dialogue4 = new string[] {};
        dialogueOrder = new int[] {};
    }
}
