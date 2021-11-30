using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class controlDialegs : MonoBehaviour
{
    public Animator animText;
    public Animator animDialeg;
    private Queue <string> dialogueQueue = new Queue <string>();
    private Texts text;
    [SerializeField] TextMeshProUGUI screenText;
    [SerializeField] TextMeshProUGUI npcName;
    [SerializeField] TextMeshProUGUI npcDialogueBox;
    [SerializeField] TextMeshProUGUI playerResponse;
    
    bool isTalking = false;
    bool ended = true;
    int curResponseTracker = 0;

    void Update()
    {
        if (isTalking == true){
            ChangeDialogue();
        }
    }
    public void ActivateDialogues(Texts textObjecte)
    {
        FindObjectOfType<ThirdPersonMovement>().isTalkKing();
        FindObjectOfType<CameraSwitch>().isCameraOnGoing();
        text = textObjecte;
        //text = new Texts(textObjecte.name, textObjecte.dialogue, textObjecte.playerDialogue);
        switch (text.playerDialogue.Length)
        {
            case 0:
                animText.SetBool("Sign", true);
                break;

            default: 
                animDialeg.SetBool("Dialogue", true); //Crida a ActivaDialeg(); La resta de casos tambe cridaran
                break;
        }
    }

    public void ActiveText()
    {
        dialogueQueue.Clear();
        foreach (string saveText in text.dialogue)
        {
            dialogueQueue.Enqueue(saveText);
        }
        NextSentence();
    }

    public void NextSentence ()
    {
        if (ended){
            if(dialogueQueue.Count == 0)
            {
                screenText.text = "";
                CloseDialogue();
                return;
            }

            string actualSentence = dialogueQueue.Dequeue();
            screenText.text = actualSentence;
            StartCoroutine(showCaracters(actualSentence));
        }
    }

    IEnumerator showCaracters (string textToShow)
    {
        ended = false;
        if (text.playerDialogue.Length == 0){
            screenText.text = "";
            foreach (char caracter in textToShow.ToCharArray())
            {
                screenText.text += caracter;
                yield return new WaitForSeconds(0.02f);
            }
        }
        else{
            npcDialogueBox.text = "";
            foreach (char caracter in textToShow.ToCharArray())
            {
                npcDialogueBox.text += caracter;
                yield return new WaitForSeconds(0.02f);
            }
        }
        ended = true;
    }

    void CloseDialogue ()
    {
        animText.SetBool("Sign", false);
        FindObjectOfType<ThirdPersonMovement>().isTalkKing();
        FindObjectOfType<Interaccio>().isTalkingStarted();
        FindObjectOfType<CameraSwitch>().isCameraOnGoing();
    }

    public void ActivateDialogue()
    {
        StartConversation();
    }

    private void ChangeDialogue (){
        if (ended)
        {
            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                curResponseTracker++;
                if (curResponseTracker >= text.playerDialogue.Length - 1)
                {
                    curResponseTracker = text.playerDialogue.Length - 1;
                }
            }
            else if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                curResponseTracker--;
                if(curResponseTracker < 0)
                {
                    curResponseTracker = 0;
                }
            }
            switch (curResponseTracker)
            {
            case 0:
                playerResponse.text = text.playerDialogue[0];
                if(Input.GetKeyDown(KeyCode.Return))
                {
                    npcDialogueBox.text = text.dialogue[1];
                    StartCoroutine(showCaracters(text.dialogue[1]));
                }
                break;
            case 1:
                playerResponse.text = text.playerDialogue[1];
                if(Input.GetKeyDown(KeyCode.Return))
                {
                    npcDialogueBox.text = text.dialogue[2];
                    StartCoroutine(showCaracters(text.dialogue[2]));
                }
                break;
            case 2:
                playerResponse.text = text.playerDialogue[2];
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    npcDialogueBox.text = text.dialogue[3];
                    StartCoroutine(showCaracters(text.dialogue[3]));
                }
                break;
            case 3:
                playerResponse.text = text.playerDialogue[3];
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    npcDialogueBox.text = text.dialogue[4];
                    StartCoroutine(showCaracters(text.dialogue[4]));
                }
                break;
            case 4:
                playerResponse.text = text.playerDialogue[4];
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    npcDialogueBox.text = text.dialogue[5];
                    StartCoroutine(showCaracters(text.dialogue[5]));
                }
                break;
            case 5:
                playerResponse.text = text.playerDialogue[5];
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    npcDialogueBox.text = text.dialogue[6];
                    StartCoroutine(showCaracters(text.dialogue[6]));
                }
                break;
            case 6:
                playerResponse.text = text.playerDialogue[6];
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    npcDialogueBox.text = text.dialogue[7];
                    StartCoroutine(showCaracters(text.dialogue[7]));
                }
                break;
            case 7:
                playerResponse.text = text.playerDialogue[7];
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    npcDialogueBox.text = text.dialogue[8];
                    StartCoroutine(showCaracters(text.dialogue[8]));
                }
                break;
            case 8:
                playerResponse.text = text.playerDialogue[8];
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    npcDialogueBox.text = text.dialogue[9];
                    StartCoroutine(showCaracters(text.dialogue[9]));
                }
                break;
            case 9:
                playerResponse.text = text.playerDialogue[9];
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    npcDialogueBox.text = text.dialogue[10];
                    StartCoroutine(showCaracters(text.dialogue[10]));
                }
                break; 
            default:
                EndDialogue();
                break;
            }
            if(Input.GetKeyDown(KeyCode.E) && isTalking == true)
            {
                EndDialogue();
            }
        }
    }
    private void StartConversation()
    {
        isTalking = true;
        curResponseTracker = 0;
        npcName.text = text.name;
        npcDialogueBox.text = text.dialogue[0];
        StartCoroutine(showCaracters(text.dialogue[0]));
    }

    private void EndDialogue()
    {
        isTalking = false;
        curResponseTracker = 0;
        npcName.text = "";
        npcDialogueBox.text = "";
        playerResponse.text = "";
        animDialeg.SetBool("Dialogue", false);
        FindObjectOfType<ThirdPersonMovement>().isTalkKing();
        FindObjectOfType<CameraSwitch>().isCameraOnGoing();
        FindObjectOfType<Interaccio>().isTalkingStarted();
    }
}
