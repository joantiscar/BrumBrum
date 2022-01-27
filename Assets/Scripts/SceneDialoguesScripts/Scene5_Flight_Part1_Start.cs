using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scene5_Flight_Part1_Start : MonoBehaviour
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
        npc_inicialDialogue = GameObject.Find("NPC_Evil_StartDialogue");
        objecteInt = npc_inicialDialogue.GetComponent<objecteInteractiu>();

        player = GameObject.FindGameObjectWithTag("Player");
        player.isStatic = false;

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
            interacted = true;
            imageAnimator.SetBool("Fade", true);
        }
        else if (interacted && imageAnimator.GetCurrentAnimatorStateInfo(0).IsName("Default"))
        {
            SceneManager.LoadScene("Scene5_Flight_Part2");
        }
    }
}
