using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    protected string[] dialogue;
    protected string[] playerDialogue;
    protected string characterName;

    public string getName()
    {
        return characterName;
    }

    public string[] getDialogue()
    {
        return dialogue;
    }

    public string[] getPlayerDialogue()
    {
        return playerDialogue;
    }
}
