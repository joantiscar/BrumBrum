using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class InteractToChangeScene : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI interactText;
    private bool interactAllowed;
    private bool interacted = false;
    public string scene;
    private bool wasActive = false;

    private Animator imageAnimator;
    //private Animator fadeInanimator;


    private void Start()
    {
        //pickupText = GameObject.FindGameObjectWithTag("ItemText").GetComponent<Text>();
        interactText.gameObject.SetActive(false);
        interactText.text = "";

        //image = GameObject.Find("PlayerOnWorld").GetComponentInChildren<>
        imageAnimator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Canvas>().GetComponentInChildren<Image>().GetComponent<Animator>();
        //Debug.Log(image);

        //Debug.Log(image.GetComponent<Animator>().runtimeAnimatorController);


        //fadeInanimator = new Animator();
        //fadeInanimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("FadeInAnimatorController");
        switch (SceneManager.GetActiveScene().name){
            case "Scene1_Precamping":
                interactText.text = "Pulsa E para acampar";
            break;
            case "Scene1-5_Camping":
                interactText.text = "Pulsa E para descansar";
            break;
            case "Scene2_Morning":
                interactText.text = "Pulsa E para entrar al castillo";
            break;
            case "Scene3_Castle":
                interactText.text = "Pulsa E para entrar a la sala del trono";
            break;
            case "Scene5_Flight_Part2":
                interactText.text = "Pulsa E para salir del castillo";
            break;
            case "Scene6_Final":
                interactText.text = "Pulsa E para acabar con Zeth";
            break;
        }
    }

    private void Update()
    {
        if (interactAllowed && Input.GetKeyDown(KeyCode.E))
        {
            //player.GetComponent<Canvas>().GetComponent<Image>()
            //
            interacted = true;
            //Debug.Log(image.GetComponent<Animator>().runtimeAnimatorController);

            //Destroy(image.GetComponent<Animator>());



            //image.GetComponent<Animator>().runtimeAnimatorController = null;
            //image.GetComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Assets/Animacions/FadeInAnimatorController.controller", typeof(RuntimeAnimatorController));

            //image.GetComponent<Animator>().runtimeAnimatorController = (Instantiate(Resources.Load("FadeInAnimatorController")) as RuntimeAnimatorController);


            //Debug.Log(image.GetComponent<Animator>().runtimeAnimatorController);



            //Debug.Log(fadeInanimator.runtimeAnimatorController);
            //image.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("FadeInAnimatorController");
            //Debug.Log(image.GetComponent<Animator>().runtimeAnimatorController);



            imageAnimator.SetBool("Fade", true);


            //SceneManager.LoadScene(scene);
        }
        else if (interacted && imageAnimator.GetCurrentAnimatorStateInfo(0).IsName("Default"))
        {
            //Debug.Log("canvia ara");
            if (scene == "Scene6_Final_2")
            {
                SceneManager.LoadScene(scene, LoadSceneMode.Additive);
                Destroy(GameObject.Find("InicialDialogue_SixthScene"));
                GameObject.Find("Scenario_SixthScene").GetComponent<AudioSource>().volume = 0.3f;
            }
            else SceneManager.LoadScene(scene);
        }
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name.Equals("Third Person Player"))
        {
            interactText.gameObject.SetActive(true);
            interactAllowed = true;
            wasActive = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.name.Equals("Third Person Player"))
        {
            interactText.gameObject.SetActive(false);
            interactAllowed = false;
            wasActive = false;
        }
    }

    public void NoInteraction (){
        interactText.gameObject.SetActive(false);
        interactAllowed = false;
    }
    public void YesInteraction (){
        if (wasActive){
            interactText.gameObject.SetActive(true);
            interactAllowed = true;
        }
    }
}
