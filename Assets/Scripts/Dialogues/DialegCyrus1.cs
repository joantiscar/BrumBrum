using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialegCyrus1 : GameDialogue
{
    public void Awake()
    {
        characterName = "Cyrus";
        dialogue = new string[] { "Siento haberte atacado tan directamente…", 
        "Gracias…",
        "No ha sido nada amigo. Mañana iremos a por todas." };
        playerDialogue = new string[] { "No te preocupes, yo también tengo gran parte de culpa…",
        "Así se habla. Acabemos con ese insensato." };
        dialogue3 = new string[] {};
        dialogue4 = new string[] {};
        dialogueOrder = new int[] {};
    }
}
