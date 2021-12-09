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
    public string namePlayer;
    public string[] playerDialogue;
    public bool[] order;


    public Texts(string nC, string nP, string[] d, string[] pd, bool[] o)
    {
        name = nC;
        namePlayer = nP;
        dialogue = d.Clone() as string[];
        playerDialogue = pd.Clone() as string[];
        order = o.Clone() as bool[];
    }
}
