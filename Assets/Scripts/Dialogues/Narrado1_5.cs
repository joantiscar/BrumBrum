using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Narrado1_5 : GameDialogue
{
    public void Awake()
    {
        characterName = "";
        playerName = "";
        dialogue = new string[] { "Todos traspasan su poder a Irix.",
        "Irix ejecuta la técnica.",
        "Empieza el combate.",
        "Irix es completamente invencible.",
        "Decide detenerse antes de eliminar a su enemigo para despedirse..."};
        playerDialogue = new string[] { };
        dialogue3 = new string[] { };
        dialogue4 = new string[] { };
        dialogueOrder = new int[] { };
    }
}
