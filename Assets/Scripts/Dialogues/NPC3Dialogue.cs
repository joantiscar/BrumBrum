using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC3Dialogue : Dialogue
{
    public void Awake()
    {
        characterName = "NPC3";
        playerName = "Player";
        dialogue = new string[] { "Hola", "Soc un dialeg simple", "Fins la proxima!"};
        playerDialogue = new string[] {"Hola", "Qui ets?", "Adeu!"};
        dialogueOrder = new bool [] {true, false, false, true, false, true};
    }
}
