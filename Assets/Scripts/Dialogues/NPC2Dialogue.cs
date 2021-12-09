using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC2Dialogue : Dialogue
{
    public void Awake()
    {
        characterName = "NPC2";
        dialogue = new string[] { "Hola", "Soc un dialeg simple"};
        playerDialogue = new string[] {};
        dialogueOrder = new bool[] {};
    }
}
