using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNPC3Dialogue : GameDialogue
{
    public void Awake()
    {
        characterName = "NPC3";
        playerName = "Player";
        dialogue = new string[] { "Hola", "Soc un dialeg simple", "Fins la proxima!"};
        playerDialogue = new string[] {"Hola", "Qui ets?", "Adeu!"};
        dialogue3 = new string[] {};
        dialogue4 = new string[] {};
        dialogueOrder = new int [] {0, 1, 1, 0, 1, 0};
    }
}
