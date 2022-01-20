using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class controlDialegs : MonoBehaviour
{
    public Animator animText;
    public Animator animDialeg;
    public Animator animSeguit;
    private Queue <string> dialogueQueue = new Queue <string>();
    private Queue <string> dialogueQueueNPC = new Queue <string>();
    private Queue <string> dialogueQueuePlayer = new Queue <string>();
    private Queue <string> dialogueQueueExtra1 = new Queue <string>();
    private Queue <string> dialogueQueueExtra2 = new Queue <string>();
    private Queue <int> dialogueQueueOrder = new Queue <int>();
    private Texts text;
    [SerializeField] TextMeshProUGUI screenText;
    [SerializeField] TextMeshProUGUI npcName;
    [SerializeField] TextMeshProUGUI npcDialogueBox;
    [SerializeField] TextMeshProUGUI playerResponse;
    [SerializeField] TextMeshProUGUI SeguitName;
    [SerializeField] TextMeshProUGUI SeguitText;
    [SerializeField] UnityEngine.UI.Image FletxaAmunt;
    [SerializeField] UnityEngine.UI.Image FletxaAbaix;
    
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
        if (text.order.Length > 0){
            animSeguit.SetBool("Seguit", true);
        }
        else if (text.playerDialogue.Length == 0) {
            animText.SetBool("Sign", true);
        }
        else{
            animDialeg.SetBool("Dialogue", true);
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

    public void ActivateSeguit ()
    {
        dialogueQueue.Clear();
        foreach (string saveTextNPC in text.dialogue)
        {
            dialogueQueueNPC.Enqueue(saveTextNPC);
        }
        foreach (string saveTextPlayer in text.playerDialogue)
        {
            dialogueQueuePlayer.Enqueue(saveTextPlayer);
        }
        foreach (string saveTextExtra1 in text.extraDialogue1)
        {
            dialogueQueueExtra1.Enqueue(saveTextExtra1);
        }
        foreach (string saveTextExtra2 in text.extraDialogue2)
        {
            dialogueQueueExtra2.Enqueue(saveTextExtra2);
        }
        foreach (int saveOrder in text.order)
        {
            dialogueQueueOrder.Enqueue(saveOrder);
        }
        NextSentenceSeguit();
    }

    public void NextSentenceSeguit ()
    {
        string actualSentence = "";
        if (ended){
            if(dialogueQueueOrder.Count == 0)
            {
                Debug.Log ("He acabat");
                SeguitName.text = "";
                SeguitText.text = "";
                CloseDialogueSeguit();
                return;
            }
            int actual = dialogueQueueOrder.Dequeue();
            if (actual == 0)
            {
                Debug.Log ("NPC");
                SeguitName.text = text.name;
                actualSentence = dialogueQueueNPC.Dequeue();
            }
            else if (actual == 1)
            {
                Debug.Log ("Player");
                SeguitName.text = text.namePlayer;
                actualSentence = dialogueQueuePlayer.Dequeue();
            }
            else if (actual == 2)
            {
                Debug.Log ("Extra1");
                SeguitName.text = text.name1;
                actualSentence = dialogueQueueExtra1.Dequeue();
            }
            else if (actual == 3)
            {
                Debug.Log ("Extra2");
                SeguitName.text = text.name2;
                actualSentence = dialogueQueueExtra2.Dequeue();
            }
            SeguitText.text = actualSentence;
            StartCoroutine(showCaracters(actualSentence));
        }
    }

    IEnumerator showCaracters (string textToShow)
    {
        ended = false;
        if (text.order.Length > 0)
        {
            SeguitText.text = "";
            foreach (char caracter in textToShow.ToCharArray())
            {
                SeguitText.text += caracter;
                yield return new WaitForSeconds(0.02f);
            }
        }
        else if (text.playerDialogue.Length == 0){
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

        void CloseDialogueSeguit ()
    {
        animSeguit.SetBool("Seguit", false);
        FindObjectOfType<ThirdPersonMovement>().isTalkKing();
        FindObjectOfType<Interaccio>().isTalkingStarted();
        FindObjectOfType<CameraSwitch>().isCameraOnGoing();
        if(text.desti != null) SceneManager.LoadScene (text.desti);
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
                FletxaAmunt.enabled = false;
                if (text.playerDialogue.Length - 1 > 0){
                    FletxaAbaix.enabled = true;
                }
                else{
                    FletxaAbaix.enabled = false;
                }
                playerResponse.text = text.playerDialogue[0];
                if(Input.GetKeyDown(KeyCode.Return))
                {
                    npcDialogueBox.text = text.dialogue[1];
                    StartCoroutine(showCaracters(text.dialogue[1]));
                }
                break;
            case 1:
                FletxaAmunt.enabled = true;
                if (text.playerDialogue.Length - 1 > 1){
                    FletxaAbaix.enabled = true;
                }
                else{
                    FletxaAbaix.enabled = false;
                }
                playerResponse.text = text.playerDialogue[1];
                if(Input.GetKeyDown(KeyCode.Return))
                {
                    npcDialogueBox.text = text.dialogue[2];
                    StartCoroutine(showCaracters(text.dialogue[2]));
                }
                break;
            case 2:
                FletxaAmunt.enabled = true;
                if (text.playerDialogue.Length - 1 > 2){
                    FletxaAbaix.enabled = true;
                }
                else{
                    FletxaAbaix.enabled = false;
                }
                playerResponse.text = text.playerDialogue[2];
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    npcDialogueBox.text = text.dialogue[3];
                    StartCoroutine(showCaracters(text.dialogue[3]));
                }
                break;
            case 3:
                FletxaAmunt.enabled = true;
                if (text.playerDialogue.Length - 1 > 3){
                    FletxaAbaix.enabled = true;
                }
                else{
                    FletxaAbaix.enabled = false;
                }
                playerResponse.text = text.playerDialogue[3];
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    npcDialogueBox.text = text.dialogue[4];
                    StartCoroutine(showCaracters(text.dialogue[4]));
                }
                break;
            case 4:
                FletxaAmunt.enabled = true;
                if (text.playerDialogue.Length - 1 > 4){
                    FletxaAbaix.enabled = true;
                }
                else{
                    FletxaAbaix.enabled = false;
                }
                playerResponse.text = text.playerDialogue[4];
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    npcDialogueBox.text = text.dialogue[5];
                    StartCoroutine(showCaracters(text.dialogue[5]));
                }
                break;
            case 5:
                FletxaAmunt.enabled = true;
                if (text.playerDialogue.Length - 1 > 5){
                    FletxaAbaix.enabled = true;
                }
                else{
                    FletxaAbaix.enabled = false;
                }
                playerResponse.text = text.playerDialogue[5];
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    npcDialogueBox.text = text.dialogue[6];
                    StartCoroutine(showCaracters(text.dialogue[6]));
                }
                break;
            case 6:
                FletxaAmunt.enabled = true;
                if (text.playerDialogue.Length - 1 > 6){
                    FletxaAbaix.enabled = true;
                }
                else{
                    FletxaAbaix.enabled = false;
                }
                playerResponse.text = text.playerDialogue[6];
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    npcDialogueBox.text = text.dialogue[7];
                    StartCoroutine(showCaracters(text.dialogue[7]));
                }
                break;
            case 7:
                FletxaAmunt.enabled = true;
                if (text.playerDialogue.Length - 1 > 7){
                    FletxaAbaix.enabled = true;
                }
                else{
                    FletxaAbaix.enabled = false;
                }
                playerResponse.text = text.playerDialogue[7];
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    npcDialogueBox.text = text.dialogue[8];
                    StartCoroutine(showCaracters(text.dialogue[8]));
                }
                break;
            case 8:
                FletxaAmunt.enabled = true;
                if (text.playerDialogue.Length - 1 > 8){
                    FletxaAbaix.enabled = true;
                }
                else{
                    FletxaAbaix.enabled = false;
                }
                playerResponse.text = text.playerDialogue[8];
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    npcDialogueBox.text = text.dialogue[9];
                    StartCoroutine(showCaracters(text.dialogue[9]));
                }
                break;
            case 9:
                FletxaAmunt.enabled = true;
                if (text.playerDialogue.Length - 1 > 9){
                    FletxaAbaix.enabled = true;
                }
                else{
                    FletxaAbaix.enabled = false;
                }
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
        FindObjectOfType<Interaccio>().isTalkingStarted();
        FindObjectOfType<CameraSwitch>().isCameraOnGoing();
    }
}
