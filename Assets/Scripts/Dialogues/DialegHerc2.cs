using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialegHerc2 : GameDialogue
{
    public void Awake()
    {
        characterName = "Herc";
        dialogue = new string[] { "Em-", 
        "Ha sido un honor pelear a tu lado.",
        "Gracias hermano."};
        playerDialogue = new string[] { "Dime.",
        "También lo ha sido pelear al tuyo."};
        dialogue3 = new string[] {};
        dialogue4 = new string[] {};
        dialogueOrder = new int[] {};
    }
}
