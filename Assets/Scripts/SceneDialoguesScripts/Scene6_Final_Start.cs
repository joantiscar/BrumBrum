using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene6_Final_Start : MonoBehaviour
{
    private GameObject npc_inicialDialogue;
    private objecteInteractiu objecteInt;
    private GameObject player;
    private bool firstDialogueIsCalled = false;
    private bool secondDialogueIsCalled = false;


    private Animator imageAnimator;
    private bool interacted = false;
    private bool final = false;


    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Scenario_SixthScene").GetComponent<AudioSource>().time = 1.5f;

        npc_inicialDialogue = GameObject.Find("NPC_Evil");
        objecteInt = npc_inicialDialogue.GetComponent<objecteInteractiu>();

        player = GameObject.FindGameObjectWithTag("Player");

        imageAnimator = player.GetComponentInChildren<Canvas>().GetComponentInChildren<Image>().GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!firstDialogueIsCalled && objecteInt.dialogues != null)
        {
            firstDialogueIsCalled = true;
            objecteInt.Interactuate();
            player.GetComponent<Interaccio>().isTalkingStarted();

            Destroy(npc_inicialDialogue.GetComponent<DialegSeguit5>());
            npc_inicialDialogue.AddComponent<Narrado1_5>();
            Debug.Log("k1");
        }
        else if (!FindObjectOfType<controlDialegs>().animSeguit.GetBool("Seguit") /*&& !secondDialogueIsCalled*/ && !interacted && !secondDialogueIsCalled)
        {
            objecteInt.Start();
            interacted = true;
            imageAnimator.SetBool("Fade", true);
        }
        else if (interacted && imageAnimator.GetCurrentAnimatorStateInfo(0).IsName("Default") && !secondDialogueIsCalled)
        {
            secondDialogueIsCalled = true;
            interacted = false;
            objecteInt.Interactuate();
            player.GetComponent<Interaccio>().isTalkingStarted();
        }
        else if (secondDialogueIsCalled && !FindObjectOfType<controlDialegs>().animText.GetBool("Sign") && !interacted)
        {
            Debug.Log("yass");
            interacted = true;
        }
        else if (secondDialogueIsCalled && interacted && !FindObjectOfType<controlDialegs>().animText.GetBool("Sign") && !final)
        {
            final = true;
            imageAnimator.SetBool("Fade", false);
            GameObject.Find("Scenario_SixthScene").GetComponent<AudioSource>().volume = 1;

            Destroy(npc_inicialDialogue.GetComponent<CapsuleCollider>());
            Destroy(npc_inicialDialogue.GetComponent<Narrado1_5>());
            Destroy(objecteInt);
        }
    }
}
