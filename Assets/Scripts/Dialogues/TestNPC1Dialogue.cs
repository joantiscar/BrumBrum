using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNPC1Dialogue : GameDialogue
{
    public void Awake()
    {
        characterName = "NPC1";
        dialogue = new string[] { "Hola, que tal?", "M'alegro", "Tot be", "Adeu" };
        playerDialogue = new string[] { "Molt be!", "I tu?", "Adeu!" };
        dialogue3 = new string[] {};
        dialogue4 = new string[] {};
        dialogueOrder = new int[] {};
    }
}
