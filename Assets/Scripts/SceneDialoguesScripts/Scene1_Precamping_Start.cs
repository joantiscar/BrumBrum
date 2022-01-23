using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene1_Precamping_Start : MonoBehaviour
{

    private GameObject npc_inicialDialogue;
    private objecteInteractiu objecteInt;
    private GameObject player;
    private bool firstDialogueIsCalled = false;
    private bool secondDialogueIsCalled = false;
    private bool fadeIn = false;
    private Animator animAux;


    // Start is called before the first frame update
    void Start()
    {
        npc_inicialDialogue = GameObject.Find("NPC_InicialDialogue");
        objecteInt = npc_inicialDialogue.GetComponent<objecteInteractiu>();

        player = GameObject.FindGameObjectWithTag("Player");
        player.isStatic = false;

        animAux = player.GetComponentInChildren<Canvas>().GetComponentInChildren<Image>().GetComponent<Animator>();

        animAux.SetBool("Fade", true);
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
            npc_inicialDialogue.AddComponent<DialegSeguit1>();
        }
        else if (!FindObjectOfType<controlDialegs>().animText.GetBool("Sign") && !secondDialogueIsCalled)
        {
            animAux.SetBool("Fade", false);

            objecteInt.Start();
            secondDialogueIsCalled = true;
            objecteInt.Interactuate();
            player.GetComponent<Interaccio>().isTalkingStarted();

            Destroy(npc_inicialDialogue.GetComponent<GameDialogue>());
            Destroy(objecteInt);
            npc_inicialDialogue.transform.gameObject.tag = "Untagged";
        }
        else if (!FindObjectOfType<controlDialegs>().animSeguit.GetBool("Seguit") && secondDialogueIsCalled && !fadeIn) {
            animAux.SetBool("Fade", true);
            fadeIn = true;
        }
        else if (fadeIn && animAux.GetCurrentAnimatorStateInfo(0).IsName("Default"))
        {
            animAux.SetBool("Fade", false);

            Destroy(this.transform.gameObject);
            GameObject.Find("Scenario_FirstScene").GetComponent<AudioSource>().volume = 1;
            GameObject.Find("Scenario_FirstScene").GetComponent<RandomCombat>().SetAble();
        }
    }

}
