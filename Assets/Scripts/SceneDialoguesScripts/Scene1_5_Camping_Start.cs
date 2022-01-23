using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1_5_Camping_Start : MonoBehaviour
{
    private GameObject npc_inicialDialogue;
    private objecteInteractiu objecteInt;
    private GameObject player;
    private bool firstDialogueIsCalled = false;
    private bool secondDialogueIsCalled = false;


    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Scenario_FirstHalfScene").GetComponent<AudioSource>().time = 3.5f;
        GameObject.Find("Scenario_FirstHalfScene").GetComponent<AudioSource>().Play();

        npc_inicialDialogue = GameObject.Find("Herc");
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

            Destroy(npc_inicialDialogue.GetComponent<GameDialogue>());
            npc_inicialDialogue.AddComponent<DialegHerc1>();
        }
        else if (!FindObjectOfType<controlDialegs>().animText.GetBool("Sign") && !secondDialogueIsCalled)
        {
            objecteInt.Start();
            secondDialogueIsCalled = true;
        }
        else if (secondDialogueIsCalled && !FindObjectOfType<controlDialegs>().animSeguit.GetBool("Seguit"))
        {
            GameObject.Find("Scenario_FirstHalfScene").GetComponent<AudioSource>().volume = 1;
            GameObject.Find("Scenario_FirstHalfScene").GetComponent<RandomCombat>().SetAble();
        }
    }
}
