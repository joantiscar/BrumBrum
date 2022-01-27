using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene4_Throne_Start : MonoBehaviour
{
    private GameObject npc_inicialDialogue;
    private objecteInteractiu objecteInt;
    private GameObject player;
    private bool firstDialogueIsCalled = false;


    // Start is called before the first frame update
    void Start()
    {
        npc_inicialDialogue = GameObject.Find("NPC_Evil_StartDialogue");
        objecteInt = npc_inicialDialogue.GetComponent<objecteInteractiu>();

        player = GameObject.FindGameObjectWithTag("Player");
        player.isStatic = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!firstDialogueIsCalled && objecteInt.dialogues != null)
        {
            firstDialogueIsCalled = true;
            objecteInt.Interactuate();
            player.GetComponent<Interaccio>().isTalkingStarted();

            Destroy(npc_inicialDialogue.GetComponent<DialegSeguit3>());
            Destroy(npc_inicialDialogue.GetComponent<CapsuleCollider>());
            Destroy(objecteInt);
        }
        else if (!FindObjectOfType<controlDialegs>().animSeguit.GetBool("Seguit"))
        {
            GameObject.Find("Scenario_FourthScene").GetComponent<AudioSource>().volume = 0.5f;
        }
    }
}
