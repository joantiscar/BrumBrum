using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialegSeguit3 : GameDialogue
{
    public void Awake()
    {
        characterName = "Zeth";
        playerName = "Cyrus";
        name3 = "Irix";
        name4 = "Hestia";
        dialogue = new string[] {"Ahora ya nadie nos molestará.", 
        "Está claro que ninguno de mis secuaces hubiera podido con vosotros. Ni siquiera si os hubieran atacado todos juntos.", 
        "De lo contrario estaría totalmente decepcionado con vosotros.", 
        "Además, me parece que tenemos una cuenta pendiente.", 
        "Al principio pensé que era todo culpa tuya. Todo el mundo te quería más que a mí. Pero no podía estar más equivocado.",
        "Después de mi expulsión del cielo comprendí que me encantaba."};
        playerDialogue = new string[] {"¿Por qué has querido hacer las cosas de esta manera?"};
        dialogue3 = new string[] {"¿Por qué ha tenido que acabar todo así?",
        "¡No puedo mas, voy a matarlo con mis propias manos ahora mismo, yo solo!"};
        dialogue4 = new string[] {"Tranquilo. No te adelantes. No queremos acabar como la otra vez.",
        "¡Luchemos todos juntos!"};
        dialogueOrder = new int [] {0, 1, 0, 0, 0, 2, 0, 0, 2, 3, 3};
        desti = "CombatScene_CombatFinal";
    }
}
