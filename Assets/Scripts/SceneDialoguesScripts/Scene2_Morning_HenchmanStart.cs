using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2_Morning_HenchmanStart : MonoBehaviour
{
    private GameObject npc_inicialDialogue;
    private objecteInteractiu objecteInt;
    private GameObject player;
    private bool firstDialogueIsCalled = false;
    // private bool secondDialogueIsCalled = false;

    private GameObject characters;
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("Scenario_SecondScene").GetComponent<AudioSource>();
        audioSource.time = 0.6f;
        audioSource.Play();

        npc_inicialDialogue = GameObject.Find("NPC_Henchman");
        objecteInt = npc_inicialDialogue.GetComponent<objecteInteractiu>();

        player = GameObject.FindGameObjectWithTag("Player");

        characters = GameObject.Find("HenchmanDialogueCharacters_Scene2");
        characters.SetActive(false);
    }

    
    // Update is called once per frame
    void Update()
    {
        if (!FindObjectOfType<controlDialegs>().animText.GetBool("Sign") && firstDialogueIsCalled)
        {
            firstDialogueIsCalled = false;
            objecteInt.Start();
        }
        else if (!firstDialogueIsCalled && !FindObjectOfType<controlDialegs>().animSeguit.GetBool("Seguit")) {
            audioSource.volume = 1f;
        }
    }
    

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            audioSource.volume = 0.4f;

            GameObject.Find("PlayerOnWorld").transform.position = new Vector3(100.5f, 0, 55);
            GameObject.Find("PlayerOnWorld").transform.rotation = Quaternion.Euler(0, 25, 0);
            player.transform.position = new Vector3(100.5f, 3.65f, 55);
            player.transform.rotation = Quaternion.Euler(0, 25, 0);
            characters.SetActive(true);

            Destroy(transform.gameObject.GetComponent<BoxCollider>());
            
            firstDialogueIsCalled = true;
            objecteInt.Interactuate();
            player.GetComponent<Interaccio>().isTalkingStarted();

            Destroy(npc_inicialDialogue.GetComponent<GameDialogue>());
            npc_inicialDialogue.AddComponent<DialegSecuaz1>();
        }
    }
}
