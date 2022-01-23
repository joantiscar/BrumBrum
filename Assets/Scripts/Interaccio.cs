using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interaccio : MonoBehaviour
{
    private GameObject triggeringNpc;
    private bool triggering;
    private bool isRPressed = false;
    [SerializeField] TextMeshProUGUI npcText;
    // Start is called before the first frame update

    // Update is called once per frame
    
    void Update()
    {
        if(triggering)
        {
            if(!isRPressed) npcText.gameObject.SetActive(true);
            else npcText.gameObject.SetActive(false);

            if(Input.GetKeyDown(KeyCode.R) && !isRPressed)
            {
                //Debug.Log("Interaccio: " + triggeringNpc.name);
                GetComponent<AudioSource>().Stop();
                GameObject.Find(triggeringNpc.name).GetComponent<objecteInteractiu>().Interactuate();
                isTalkingStarted();
            }
        }
        else 
        {
            npcText.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "NPC")
        {
            npcText.text = "Pula R para hablar con " + other.name;
            triggering = true;
            triggeringNpc = other.gameObject;
        }
        else if (other.tag == "Cartell"){
            npcText.text = "Pulsa R para leer el cartel";
            triggering = true;
            triggeringNpc = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "NPC" || other.tag == "Cartell")
        {
            triggering = false;
            triggeringNpc = null;
        }
    }
    public void isTalkingStarted(){
        isRPressed = !isRPressed;
    }
}
