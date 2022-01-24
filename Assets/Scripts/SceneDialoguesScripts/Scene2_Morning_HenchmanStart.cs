using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene2_Morning_HenchmanStart : MonoBehaviour
{
    private GameObject npc_inicialDialogue;
    private objecteInteractiu objecteInt;
    private GameObject player;
    private bool firstDialogueIsCalled = false;
    private bool firstDialogueNextFrame = false;

    private GameObject characters;
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("Scenario_SecondScene").GetComponent<AudioSource>();
        audioSource.time = 0.6f;
        audioSource.Play();
        GameObject.Find("Scenario_SecondScene").GetComponent<RandomCombat>().SetAble();

        npc_inicialDialogue = GameObject.Find("Secuaz");
        objecteInt = npc_inicialDialogue.GetComponent<objecteInteractiu>();

        player = GameObject.FindGameObjectWithTag("Player");

        characters = GameObject.Find("HenchmanDialogueCharacters_Scene2");
        characters.SetActive(false);
    }

    
    // Update is called once per frame
    void Update()
    {
        if (player.isStatic)
        {
            player.isStatic = false;
        }

        if (!FindObjectOfType<controlDialegs>().animText.GetBool("Sign") && firstDialogueIsCalled)
        {
            firstDialogueIsCalled = false;
            objecteInt.Start();
            firstDialogueNextFrame = true;
        }
        else if (!firstDialogueIsCalled && !FindObjectOfType<controlDialegs>().animSeguit.GetBool("Seguit") && firstDialogueNextFrame) {
            audioSource.volume = 0.6f;
            GameObject.Find("Scenario_SecondScene").GetComponent<RandomCombat>().SetAble();
        }
    }
    

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            audioSource.volume = 0.2f;
            GameObject.Find("Scenario_SecondScene").GetComponent<RandomCombat>().SetDisable();
            player.GetComponent<AudioSource>().Stop();

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
