using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene3_Castle_Start : MonoBehaviour
{
    private GameObject npc_inicialDialogue;
    private objecteInteractiu objecteInt;
    private GameObject player;
    private bool firstDialogueIsCalled = false;


    // Start is called before the first frame update
    void Start()
    {
        Singleton.setCurrentScene("Scene3_Castle");

        npc_inicialDialogue = GameObject.Find("NPC_InicialDialogue");
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
        }
        else if (!FindObjectOfType<controlDialegs>().animSeguit.GetBool("Seguit") /*&& !secondDialogueIsCalled*/)
        {
            GameObject.Find("Scenario_ThirdScene").GetComponent<AudioSource>().volume = 0.5f;
            GameObject.FindObjectOfType<RandomCombat>().SetAble();
            Destroy(npc_inicialDialogue.transform.parent.gameObject);
        }
    }
}
