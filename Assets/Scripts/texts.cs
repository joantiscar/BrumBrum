using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Texts
{
    public string name;
    [TextArea(1, 15)]
    public string[] dialogue;
    [TextArea(1, 15)]
    public string[] playerDialogue;

    public Texts(string n, string[] d, string[] pd)
    {
        name = n;
        dialogue = d.Clone() as string[];
        playerDialogue = pd.Clone() as string[];
    }
}
