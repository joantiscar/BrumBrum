using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Narrador2 : GameDialogue
{
    public void Awake()
    {
        characterName = "";
        playerName = "";
        dialogue = new string[] { "Tuvo que pasar un siglo exacto hasta que la semilla de la vida renació.", 
        "Eso fue lo que dio inicio a un nuevo mundo, esta vez sin divisiones entre las razas racionales, tan solo humanos.", 
        "O eso se supone.",
        "Han pasado miles de años desde el denominado Renacer, y seguimos sin conocer otras razas capaces de hablar.",
        "Ni siquiera sabemos si existen la magia o el cielo, y mucho menos si esta leyenda es cierta.",
        "Pero eso es lo que la hace tan interesante.",
        "Esta historia explicaría muchas de las ruinas y artefactos que han sobrevivido al tiempo y conservamos hoy en día.",
        "Como he dicho, esto es tan solo una historia, y cada uno puede creer lo que quiera."};
        playerDialogue = new string[] {};
        dialogue3 = new string[] {};
        dialogue4 = new string[] {};
        dialogueOrder = new int [] {};
    }
}