using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scene6_2_Final_Start : MonoBehaviour
{
    private GameObject npc_inicialDialogue;
    private objecteInteractiu objecteInt;
    private GameObject player;
    private bool firstDialogueIsCalled = false;
    private bool final = false;

    private Animator imageAnimator;


    // Start is called before the first frame update
    void Start()
    {
        npc_inicialDialogue = GameObject.Find("NPC_Model");
        objecteInt = npc_inicialDialogue.GetComponent<objecteInteractiu>();

        player = GameObject.FindGameObjectWithTag("Player");
        player.isStatic = false;

        imageAnimator = player.GetComponentInChildren<Canvas>().GetComponentInChildren<Image>().GetComponent<Animator>();

        imageAnimator.SetBool("Fade", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!firstDialogueIsCalled && objecteInt.dialogues != null && !final)
        {
            firstDialogueIsCalled = true;
            objecteInt.Interactuate();
            player.GetComponent<Interaccio>().isTalkingStarted();
        }
        else if (!FindObjectOfType<controlDialegs>().animText.GetBool("Sign") && firstDialogueIsCalled && !final)
        {
            final = true;
        }
        else if (GameObject.Find("ImatgeDialeg").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Default") && final)
        {
            SceneManager.LoadScene("Victoria");
        }
    }
}
