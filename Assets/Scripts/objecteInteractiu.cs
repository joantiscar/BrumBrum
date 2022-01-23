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
        //if (dialogues.transform.gameObject.name != transform.gameObject.name) dialogues = null;

        //dialogues = GameObject.Find(transform.gameObject.name).GetComponent<Dialogue>();

        //Debug.Log(dialogues.transform.gameObject.name);
        //Debug.Log(transform.gameObject.name);

        //texts.name = transform.gameObject.name;

        //dialogues = GetComponent<GameObject>().GetComponent<Dialogue>();


        texts = new Texts(dialogues.getDesti(), dialogues.getCharacterName(), dialogues.getPlayerName(), dialogues.getName3(), dialogues.getName4(), dialogues.getDialogue(), dialogues.getPlayerDialogue(), dialogues.getDialogue3(), dialogues.getDialogue4(), dialogues.getDialogueOrder());
    }


    public void Interactuate()
    {
        FindObjectOfType<controlDialegs>().ActivateDialogues(this.texts);
    }
}
