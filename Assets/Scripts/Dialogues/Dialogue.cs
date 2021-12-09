using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    protected string[] dialogue;
    protected string[] playerDialogue;
    protected bool[] dialogueOrder;
    protected string characterName;
    protected string playerName;

    public string getCharacterName()
    {
        return characterName;
    }
    public string getPlayerName()
    {
        return playerName;
    }
    public string[] getDialogue()
    {
        return dialogue;
    }
    public string[] getPlayerDialogue()
    {
        return playerDialogue; 
    }
    public bool[] getDialogueOrder()
    {
        return dialogueOrder;
    }
}
