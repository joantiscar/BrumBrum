public class DialegSeguit4 : GameDialogue
{
    public void Awake()
    {
        characterName = "";
        playerName = "Irix";
        name3 = "Zeth";
        dialogue = new string[] { "*Zeth mira la semilla que tiene en la mano*",
        "*Zeth acerca la Luna Negra a su pecho y esta lo atraviesa, fusionándose con él*"};
        playerDialogue = new string[] {"¡Detente!",
        "¡Huid! El poder ha empezado a brotar del cuerpo de Zeth de forma descontrolada."};
        dialogue3 = new string[] {"Mi orgullo no me permite perder, y mucho menos de esta manera.",
        "¡¡¡AAAAHHHHHHHH!!!"};
        dialogue4 = new string[] {};
        dialogueOrder = new int [] {0, 1, 2, 0, 2, 1};
    }
}

