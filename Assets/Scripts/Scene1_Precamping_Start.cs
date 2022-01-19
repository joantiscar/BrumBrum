using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1_Precamping_Start : MonoBehaviour
{

    private GameObject npc_inicialDialogue;
    private objecteInteractiu objecteInt;
    private GameObject player;
    private bool firstDialogueIsCalled = false;
    private bool secondDialogueIsCalled = false;


    // Start is called before the first frame update
    void Start()
    {
        npc_inicialDialogue = GameObject.Find("NPC_InicialDialogue");
        objecteInt = npc_inicialDialogue.GetComponent<objecteInteractiu>();

        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if (!firstDialogueIsCalled && objecteInt.dialogues != null)
        {
            firstDialogueIsCalled = true;
            npc_inicialDialogue.GetComponent<objecteInteractiu>().Interactuate();
            player.GetComponent<Interaccio>().isTalkingStarted();
            
            Destroy(npc_inicialDialogue.GetComponent<GameDialogue>());
            npc_inicialDialogue.AddComponent<DialegSeguit1>();
            
            /*
            npc_inicialDialogue.GetComponent<objecteInteractiu>().Start();
            Debug.Log(objecteInt.dialogues);*/

        }
        else if (!FindObjectOfType<controlDialegs>().animText.GetBool("Sign") && !secondDialogueIsCalled)
        {
            /*
            Destroy(npc_inicialDialogue.GetComponent<GameDialogue>());
            npc_inicialDialogue.AddComponent<DialegSeguit1>();
            */
            npc_inicialDialogue.GetComponent<objecteInteractiu>().Start();
            //Debug.Log(objecteInt.dialogues);

            secondDialogueIsCalled = true;
            npc_inicialDialogue.GetComponent<objecteInteractiu>().Interactuate();
            player.GetComponent<Interaccio>().isTalkingStarted();

            Destroy(npc_inicialDialogue.GetComponent<GameDialogue>());
            Destroy(npc_inicialDialogue.GetComponent<objecteInteractiu>());
            npc_inicialDialogue.transform.gameObject.tag = "Untagged";

            Destroy(this.transform.gameObject);
        }
    }

}
