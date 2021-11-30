using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialegs : MonoBehaviour
{
    public void ActivateText (){
        FindObjectOfType<controlDialegs>().ActiveText();
    }
    public void ActivateDialogue (){
        FindObjectOfType<controlDialegs>().ActivateDialogue();
    }
    public void NextSentence (){
        FindObjectOfType<controlDialegs>().NextSentence();
    }
}
