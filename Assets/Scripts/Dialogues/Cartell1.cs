using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cartell1 : GameDialogue
{
    public void Awake()
    {
        characterName = "";
        dialogue = new string[] { "Direcci�n Castillo de Zeth", 
            "(Qu� conveniente, no?)"};
        playerDialogue = new string[] { };
        dialogue3 = new string[] { };
        dialogue4 = new string[] { };
        dialogueOrder = new int[] { };
    }
}
