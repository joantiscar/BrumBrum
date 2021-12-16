using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Narrador1 : GameDialogue
{
    public void Awake()
    {
        characterName = "";
        playerName = "";
        dialogue = new string[] { "Para cuando el equipo se reencontró, la caída del cielo ya empezaba a ser preocupante.", 
        "La buena noticia era que todos ellos ya estaban curados.", 
        "Nada más verse, lo primero fue confirmar que todos estaban bien."};
        playerDialogue = new string[] {};
        dialogue3 = new string[] {};
        dialogue4 = new string[] {};
        dialogueOrder = new int [] {};
    }
}