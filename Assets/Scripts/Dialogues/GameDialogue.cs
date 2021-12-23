using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDialogue : MonoBehaviour
{
    protected string[] dialogue;
    protected string[] playerDialogue;
    protected string[] dialogue3;
    protected string[] dialogue4;
    protected int[] dialogueOrder;
    protected string characterName;
    protected string playerName;
    protected string name3;
    protected string name4;
    protected string desti;

    public string getCharacterName()
    {
        return characterName;
    }
    public string getPlayerName()
    {
        return playerName;
    }
    public string getName3()
    {
        return name3;
    }
    public string getName4()
    {
        return name4;
    }
    public string[] getDialogue()
    {
        return dialogue;
    }
    public string[] getPlayerDialogue()
    {
        return playerDialogue; 
    }
    public string[] getDialogue3()
    {
        return dialogue3; 
    }
    public string[] getDialogue4()
    {
        return dialogue4; 
    }
    public int[] getDialogueOrder()
    {
        return dialogueOrder;
    }
    public string getDesti()
    {
        return desti;
    }
}
