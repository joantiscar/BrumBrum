using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaccio : MonoBehaviour
{
    private GameObject triggeringNpc;
    private bool triggering;
    private bool isRPressed = false;
    public GameObject npcText;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(triggering)
        {
            if(!isRPressed) npcText.SetActive(true);
            else npcText.SetActive(false);

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
            npcText.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "NPC")
        {
            triggering = true;
            triggeringNpc = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "NPC")
        {
            triggering = false;
            triggeringNpc = null;
        }
    }
    public void isTalkingStarted(){
        isRPressed = !isRPressed;
    }
}
