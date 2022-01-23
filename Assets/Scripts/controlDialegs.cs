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
    int actualAnswer = -1; 
    bool [] respostes;
    bool acabar = false;
    float waitTime = 0.02f;
    bool iniciat = false;
    bool dialeg_acabat = false;

    void Update()
    {
        if (animText.GetBool("Sign") && iniciat){
            if(Input.GetKeyDown(KeyCode.Return)){
                NextSentence ();
            }
        }
        else if (animSeguit.GetBool("Seguit") && iniciat){
            if(Input.GetKeyDown(KeyCode.Return)){
                NextSentenceSeguit ();
            }
        }
        else if (isTalking == true){
            ChangeDialogue();
        }
        if (dialeg_acabat && animText.GetCurrentAnimatorStateInfo(0).IsName("Default") && animDialeg.GetCurrentAnimatorStateInfo(0).IsName("Default") 
            && animSeguit.GetCurrentAnimatorStateInfo(0).IsName("Default")){
            GameObject.FindObjectOfType<InteractToChangeScene>().YesInteraction();
            FindObjectOfType<ThirdPersonMovement>().isTalkKing();
            FindObjectOfType<Interaccio>().isTalkingStarted();
            FindObjectOfType<CameraSwitch>().isCameraOnGoing();
            dialeg_acabat = false;
        }
    }
    public void ActivateDialogues(Texts textObjecte)
    { 
        if (!dialeg_acabat){
            GameObject.FindObjectOfType<InteractToChangeScene>().NoInteraction();
            FindObjectOfType<ThirdPersonMovement>().isTalkKing();
            FindObjectOfType<CameraSwitch>().isCameraOnGoing();
        }
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
            iniciat = true;
            waitTime = 0.02f;
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
        else { waitTime = 0f; }
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
            iniciat = true;
            waitTime = 0.02f;
            if(dialogueQueueOrder.Count == 0)
            {
                SeguitName.text = "";
                SeguitText.text = "";
                CloseDialogueSeguit();
                return;
            }
            int actual = dialogueQueueOrder.Dequeue();
            if (actual == 0)
            {
                SeguitName.text = text.name;
                actualSentence = dialogueQueueNPC.Dequeue();
            }
            else if (actual == 1)
            {
                SeguitName.text = text.namePlayer;
                actualSentence = dialogueQueuePlayer.Dequeue();
            }
            else if (actual == 2)
            {
                SeguitName.text = text.name1;
                actualSentence = dialogueQueueExtra1.Dequeue();
            }
            else if (actual == 3)
            {
                SeguitName.text = text.name2;
                actualSentence = dialogueQueueExtra2.Dequeue();
            }
            SeguitText.text = actualSentence;
            StartCoroutine(showCaracters(actualSentence));
        }
        else { waitTime = 0f; }
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
                yield return new WaitForSeconds(waitTime);
            }
        }
        else if (text.playerDialogue.Length == 0){
            screenText.text = "";
            foreach (char caracter in textToShow.ToCharArray())
            {
                screenText.text += caracter;
                yield return new WaitForSeconds(waitTime);
            }
        }
        else{
            npcDialogueBox.text = "";
            foreach (char caracter in textToShow.ToCharArray())
            {
                npcDialogueBox.text += caracter;
                yield return new WaitForSeconds(waitTime);
            }
        }
        ended = true;
    }

    void CloseDialogue ()
    {
        iniciat = false;
        animText.SetBool("Sign", false);
        dialeg_acabat = true;
    }

    void CloseDialogueSeguit ()
    {
        iniciat = false;
        animSeguit.SetBool("Seguit", false);
        dialeg_acabat = true;
        if(text.desti != null) SceneManager.LoadScene (text.desti);
    }

    public void ActivateDialogue()
    {
        StartConversation();
    }

    private void ChangeDialogue (){
        if (ended)
        {
            waitTime = 0.02f;
            if(Input.GetKeyDown(KeyCode.DownArrow) && !acabar)
            {
                curResponseTracker++;
                if (curResponseTracker >= text.playerDialogue.Length - 1)
                {
                    curResponseTracker = text.playerDialogue.Length - 1;
                }
            }
            else if(Input.GetKeyDown(KeyCode.UpArrow) && !acabar)
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
                if (respostes[0]){
                    playerResponse.color = new Color32 (180, 180, 180, 255);
                }
                else {
                    playerResponse.color = new Color32 (255, 255, 255, 255);
                }
                playerResponse.text = text.playerDialogue[0];
                if(Input.GetKeyDown(KeyCode.Return) && actualAnswer != 0)
                {
                    if (text.playerDialogue.Length - 1 == 0){
                        acabar = true;
                    }
                    respostes [0] = true;
                    playerResponse.color = new Color32 (180, 180, 180, 255);
                    playerResponse.text = text.playerDialogue[0];
                    actualAnswer = 0;
                    npcDialogueBox.text = text.dialogue[1];
                    StartCoroutine(showCaracters(text.dialogue[1]));
                }
                else if (Input.GetKeyDown(KeyCode.Return) && acabar) {
                    EndDialogue();
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
                if (respostes[1]){
                    playerResponse.color = new Color32 (180, 180, 180, 255);
                }
                else {
                    playerResponse.color = new Color32 (255, 255, 255, 255);
                }
                playerResponse.text = text.playerDialogue[1];
                if(Input.GetKeyDown(KeyCode.Return) && actualAnswer != 1)
                {
                    if (text.playerDialogue.Length - 1 == 1){
                        acabar = true;
                    }
                    respostes [1] = true;
                    playerResponse.color = new Color32 (180, 180, 180, 255);
                    playerResponse.text = text.playerDialogue[1];
                    actualAnswer = 1;
                    npcDialogueBox.text = text.dialogue[2];
                    StartCoroutine(showCaracters(text.dialogue[2]));
                }
                else if (Input.GetKeyDown(KeyCode.Return) && acabar) {
                    EndDialogue();
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
                if (respostes[2]){
                    playerResponse.color = new Color32 (180, 180, 180, 255);
                }
                else {
                    playerResponse.color = new Color32 (255, 255, 255, 255);
                }
                playerResponse.text = text.playerDialogue[2];
                if(Input.GetKeyDown(KeyCode.Return) && actualAnswer != 2)
                {
                    if (text.playerDialogue.Length - 1 == 2){
                        acabar = true;
                    }
                    respostes [2] = true;
                    playerResponse.color = new Color32 (180, 180, 180, 255);
                    playerResponse.text = text.playerDialogue[2];
                    actualAnswer = 2;
                    npcDialogueBox.text = text.dialogue[3];
                    StartCoroutine(showCaracters(text.dialogue[3]));
                }
                else if (Input.GetKeyDown(KeyCode.Return) && acabar) {
                    EndDialogue();
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
                if (respostes[3]){
                    playerResponse.color = new Color32 (180, 180, 180, 255);
                }
                else {
                    playerResponse.color = new Color32 (255, 255, 255, 255);
                }
                playerResponse.text = text.playerDialogue[3];
                if(Input.GetKeyDown(KeyCode.Return) && actualAnswer != 3)
                {
                    if (text.playerDialogue.Length - 1 == 3){
                        acabar = true;
                    }
                    respostes [3] = true;
                    playerResponse.color = new Color32 (180, 180, 180, 255);
                    playerResponse.text = text.playerDialogue[3];
                    actualAnswer = 3;
                    npcDialogueBox.text = text.dialogue[4];
                    StartCoroutine(showCaracters(text.dialogue[4]));
                }
                else if (Input.GetKeyDown(KeyCode.Return) && acabar) {
                    EndDialogue();
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
                if (respostes[4]){
                    playerResponse.color = new Color32 (180, 180, 180, 255);
                }
                else {
                    playerResponse.color = new Color32 (255, 255, 255, 255);
                }
                playerResponse.text = text.playerDialogue[4];
                if(Input.GetKeyDown(KeyCode.Return) && actualAnswer != 4)
                {
                    if (text.playerDialogue.Length - 1 == 4){
                        acabar = true;
                    }
                    respostes [4] = true;
                    playerResponse.color = new Color32 (180, 180, 180, 255);
                    playerResponse.text = text.playerDialogue[4];
                    actualAnswer = 4;
                    npcDialogueBox.text = text.dialogue[5];
                    StartCoroutine(showCaracters(text.dialogue[5]));
                }
                else if (Input.GetKeyDown(KeyCode.Return) && acabar) {
                    EndDialogue();
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
                if (respostes[5]){
                    playerResponse.color = new Color32 (180, 180, 180, 255);
                }
                else {
                    playerResponse.color = new Color32 (255, 255, 255, 255);
                }
                playerResponse.text = text.playerDialogue[5];
                if(Input.GetKeyDown(KeyCode.Return) && actualAnswer != 5)
                {
                    if (text.playerDialogue.Length - 1 == 5){
                        acabar = true;
                    }
                    respostes [5] = true;
                    playerResponse.color = new Color32 (180, 180, 180, 255);
                    playerResponse.text = text.playerDialogue[5];
                    actualAnswer = 5;
                    npcDialogueBox.text = text.dialogue[6];
                    StartCoroutine(showCaracters(text.dialogue[6]));
                }
                else if (Input.GetKeyDown(KeyCode.Return) && acabar) {
                    EndDialogue();
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
                if (respostes[6]){
                    playerResponse.color = new Color32 (180, 180, 180, 255);
                }
                else {
                    playerResponse.color = new Color32 (255, 255, 255, 255);
                }
                playerResponse.text = text.playerDialogue[6];
                if(Input.GetKeyDown(KeyCode.Return) && actualAnswer != 6)
                {
                    if (text.playerDialogue.Length - 1 == 6){
                        acabar = true;
                    }
                    respostes [6] = true;
                    playerResponse.color = new Color32 (180, 180, 180, 255);
                    playerResponse.text = text.playerDialogue[6];
                    actualAnswer = 6;
                    npcDialogueBox.text = text.dialogue[7];
                    StartCoroutine(showCaracters(text.dialogue[7]));
                }
                else if (Input.GetKeyDown(KeyCode.Return) && acabar) {
                    EndDialogue();
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
                if (respostes[7]){
                    playerResponse.color = new Color32 (180, 180, 180, 255);
                }
                else {
                    playerResponse.color = new Color32 (255, 255, 255, 255);
                }
                playerResponse.text = text.playerDialogue[7];
                if(Input.GetKeyDown(KeyCode.Return) && actualAnswer != 7)
                {
                    if (text.playerDialogue.Length - 1 == 7){
                        acabar = true;
                    }
                    respostes [7] = true;
                    playerResponse.color = new Color32 (180, 180, 180, 255);
                    playerResponse.text = text.playerDialogue[7];
                    actualAnswer = 7;
                    npcDialogueBox.text = text.dialogue[8];
                    StartCoroutine(showCaracters(text.dialogue[8]));
                }
                else if (Input.GetKeyDown(KeyCode.Return) && acabar) {
                    EndDialogue();
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
                if (respostes[8]){
                    playerResponse.color = new Color32 (180, 180, 180, 255);
                }
                else {
                    playerResponse.color = new Color32 (255, 255, 255, 255);
                }
                playerResponse.text = text.playerDialogue[8];
                if(Input.GetKeyDown(KeyCode.Return) && actualAnswer != 8)
                {
                    if (text.playerDialogue.Length - 1 == 8){
                        acabar = true;
                    }
                    respostes [8] = true;
                    playerResponse.color = new Color32 (180, 180, 180, 255);
                    playerResponse.text = text.playerDialogue[8];
                    actualAnswer = 8;
                    npcDialogueBox.text = text.dialogue[9];
                    StartCoroutine(showCaracters(text.dialogue[9]));
                }
                else if (Input.GetKeyDown(KeyCode.Return) && acabar) {
                    EndDialogue();
                }
                break;
            case 9:
                FletxaAmunt.enabled = true;
                FletxaAbaix.enabled = false;
                if (respostes[9]){
                    playerResponse.color = new Color32 (180, 180, 180, 255);
                }
                else {
                    playerResponse.color = new Color32 (255, 255, 255, 255);
                }
                playerResponse.text = text.playerDialogue[9];
                if(Input.GetKeyDown(KeyCode.Return) && actualAnswer != 9)
                {
                    if (text.playerDialogue.Length - 1 == 9){
                        acabar = true;
                    }
                    respostes [9] = true;
                    playerResponse.color = new Color32 (180, 180, 180, 255);
                    playerResponse.text = text.playerDialogue[9];
                    actualAnswer = 9;
                    npcDialogueBox.text = text.dialogue[10];
                    StartCoroutine(showCaracters(text.dialogue[10]));
                }
                else if (Input.GetKeyDown(KeyCode.Return) && acabar) {
                    EndDialogue();
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
        else if (Input.GetKeyDown(KeyCode.Return)){
            waitTime = 0f;
        }
    }
    private void StartConversation()
    {
        isTalking = true;
        curResponseTracker = 0;
        npcName.text = text.name;
        npcDialogueBox.text = text.dialogue[0];
        respostes = new bool [text.playerDialogue.Length];
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
        dialeg_acabat = true;
        FletxaAmunt.enabled = false;
        FletxaAbaix.enabled = false;
        actualAnswer = -1; 
        acabar = false;
    }
}
