using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objecteInteractiu : MonoBehaviour
{
    //public Texts texts;
    private Texts texts;
    public Dialogue dialogues;


    public void Start()
    {
        dialogues = transform.gameObject.GetComponent<Dialogue>();
        //if (dialogues.transform.gameObject.name != transform.gameObject.name) dialogues = null;

        //dialogues = GameObject.Find(transform.gameObject.name).GetComponent<Dialogue>();

        //Debug.Log(dialogues.transform.gameObject.name);
        Debug.Log(transform.gameObject.name);

        //texts.name = transform.gameObject.name;

        //dialogues = GetComponent<GameObject>().GetComponent<Dialogue>();

        texts = new Texts(dialogues.getCharacterName(), dialogues.getPlayerName(),dialogues.getDialogue(), dialogues.getPlayerDialogue(), dialogues.getDialogueOrder());
    }


    public void Interactuate()
    {

        //texts = new Texts(transform.gameObject.name, dialogues.getDialogue(), dialogues.getPlayerDialogue());
        //Debug.Log(texts.dialogue[0]);

        /*
        texts.name = transform.gameObject.name;
        texts.dialogue = dialogues.getDialogue().Clone() as string[];
        texts.playerDialogue = dialogues.getPlayerDialogue().Clone() as string[];
        */

        FindObjectOfType<controlDialegs>().ActivateDialogues(this.texts);
    }
}
