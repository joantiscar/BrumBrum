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
    public void ActivateSeguit (){
        FindObjectOfType<controlDialegs>().ActivateSeguit();
    }
    public void NextSentence (){
        FindObjectOfType<controlDialegs>().NextSentence();
    }
    public void NextSentenceSeguit (){
        FindObjectOfType<controlDialegs>().NextSentenceSeguit();
    }
}
