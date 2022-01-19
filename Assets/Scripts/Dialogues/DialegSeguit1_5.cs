using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialegSeguit1_5 : GameDialogue
{
    public void Awake()
    {
        characterName = "Herc";
        playerName = "�ine";
        name3 = "Irix";
        name4 = "Cyrus";
        dialogue = new string[] {"Siento mucho haber atacado sin pensar.",
        "Por c�mo se ve�a, pensaba que el tipo ese ser�a un flojo, pero estaba del todo equivocado."};
        playerDialogue = new string[] { "Yo tambi�n lo siento mucho...", 
        "Me dio tanto miedo que fui incapaz de moverme. Prometo que no volver� a pasar." };
        dialogue3 = new string[] {"Perd�n por no haber sido capaz de enfrentar a Zeth.",
        "*sonr�e*"};
        dialogue4 = new string[] {"No fuiste el �nico que fue incapaz de hacerle da�o."};
        dialogueOrder = new int[] {0, 0, 1, 1, 2, 3, 2};
    }
}
