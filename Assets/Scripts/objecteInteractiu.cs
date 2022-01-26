using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objecteInteractiu : MonoBehaviour
{
    private Texts texts;
    public GameDialogue dialogues;

    public void Start()
    {
        dialogues = transform.gameObject.GetComponent<GameDialogue>();
        texts = new Texts(dialogues.getDesti(), dialogues.getCharacterName(), dialogues.getPlayerName(), dialogues.getName3(), dialogues.getName4(), dialogues.getDialogue(), dialogues.getPlayerDialogue(), dialogues.getDialogue3(), dialogues.getDialogue4(), dialogues.getDialogueOrder());
    }


    public void Interactuate()
    {
        FindObjectOfType<controlDialegs>().ActivateDialogues(this.texts);
    }
}
