using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNPC2Dialogue : GameDialogue
{
    public void Awake()
    {
        characterName = "NPC2";
        dialogue = new string[] { "Hola", "Soc un dialeg simple"};
        playerDialogue = new string[] {};
        dialogue3 = new string[] {};
        dialogue4 = new string[] {};
        dialogueOrder = new int[] {};
    }
}
