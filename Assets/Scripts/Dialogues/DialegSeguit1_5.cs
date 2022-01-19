using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialegSeguit1_5 : GameDialogue
{
    public void Awake()
    {
        characterName = "Herc";
        playerName = "Áine";
        name3 = "Irix";
        name4 = "Cyrus";
        dialogue = new string[] {"Siento mucho haber atacado sin pensar.",
        "Por cómo se veía, pensaba que el tipo ese sería un flojo, pero estaba del todo equivocado."};
        playerDialogue = new string[] { "Yo también lo siento mucho...", 
        "Me dio tanto miedo que fui incapaz de moverme. Prometo que no volverá a pasar." };
        dialogue3 = new string[] {"Perdón por no haber sido capaz de enfrentar a Zeth.",
        "*sonríe*"};
        dialogue4 = new string[] {"No fuiste el único que fue incapaz de hacerle daño."};
        dialogueOrder = new int[] {0, 0, 1, 1, 2, 3, 2};
    }
}
