using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC1Dialogue : GameDialogue
{
    public void Awake()
    {
        characterName = "NPC1";
        dialogue = new string[] { "Hola", "M'alegro", "Tot be", "Adeu" };
        playerDialogue = new string[] { "Molt be!", "I tu?", "Adeu!" };
        dialogueOrder = new bool[] {};
    }
}
