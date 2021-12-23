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
    public string name1;
    public string[] extraDialogue1;
    public string name2;
    public string[] extraDialogue2;
    public int[] order;
    public string desti;


    public Texts(string dest, string nC, string nP, string n1, string n2, string[] d, string[] pd, string[] p3, string[] p4, int[] o)
    {
        desti = dest;
        name = nC;
        namePlayer = nP;
        name1 = n1;
        name2 = n2;
        dialogue = d.Clone() as string[];
        playerDialogue = pd.Clone() as string[];
        extraDialogue1 = p3.Clone() as string[];
        extraDialogue2 = p4.Clone() as string[];
        order = o.Clone() as int[];
    }
}
