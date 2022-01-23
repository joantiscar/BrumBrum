using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialegSeguit2_5 : GameDialogue
{
    public void Awake()
    {
        characterName = "Cyrus";
        playerName = "";
        name3 = "";
        name4 = "";
        dialogue = new string[] {"¡MALDITO ESCLAVO!",
        "Sabía que era una trampa. Estamos rodeados.",
        "¡Habrá que hacerse paso hasta la sala del trono!"};
        playerDialogue = new string[] {};
        dialogue3 = new string[] {};
        dialogue4 = new string[] {};
        dialogueOrder = new int[] { 0, 0, 0 };
    }
}
