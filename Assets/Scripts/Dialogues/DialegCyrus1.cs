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
        "Así se habla. Acabemos con ese insensato." };
        playerDialogue = new string[] { "No te preocupes, yo también tengo gran parte de culpa…",
        "No ha sido nada amigo. Mañana iremos a por todas." };
        dialogue3 = new string[] {};
        dialogue4 = new string[] {};
        dialogueOrder = new int[] {};
    }
}
