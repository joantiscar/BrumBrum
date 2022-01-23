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

    private Animator imageAnimator;
    private bool interacted = false;


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
        }
        else if (!FindObjectOfType<controlDialegs>().animSeguit.GetBool("Seguit") /*&& !secondDialogueIsCalled*/ && !interacted)
        {
            //GameObject.Find("Scenario_FourthScene").GetComponent<AudioSource>().volume = 1;
            //Destroy(npc_inicialDialogue.transform.parent.gameObject);

            interacted = true;
            imageAnimator.SetBool("Fade", true);

            Destroy(npc_inicialDialogue.GetComponent<CapsuleCollider>());
            Destroy(npc_inicialDialogue.GetComponent<DialegSeguit5>());
            Destroy(objecteInt);
        }
        else if (interacted && imageAnimator.GetCurrentAnimatorStateInfo(0).IsName("Default"))
        {
            imageAnimator.SetBool("Fade", false);
            GameObject.Find("Scenario_SixthScene").GetComponent<AudioSource>().volume = 1;
        }
    }
}
