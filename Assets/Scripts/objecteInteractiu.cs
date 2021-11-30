using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objecteInteractiu : MonoBehaviour
{
    //public Texts texts;
    private Texts texts;
    public GameDialogue dialogues;

    public void Start()
    {
        dialogues = transform.gameObject.GetComponent<GameDialogue>();
        texts = new Texts(dialogues.getName(), dialogues.getDialogue(), dialogues.getPlayerDialogue());
    }

    public void Interactuate()
    {
        FindObjectOfType<controlDialegs>().ActivateDialogues(this.texts);
    }
}
