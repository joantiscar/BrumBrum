using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class InteractToChangeScene : MonoBehaviour
{
    [SerializeField]
    private Text interactText;
    private bool interactAllowed;
    private bool interacted = false;
    public string scene;

    private Animator imageAnimator;
    //private Animator fadeInanimator;


    private void Start()
    {
        //pickupText = GameObject.FindGameObjectWithTag("ItemText").GetComponent<Text>();
        interactText.gameObject.SetActive(false);

        //image = GameObject.Find("PlayerOnWorld").GetComponentInChildren<>
        imageAnimator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Canvas>().GetComponentInChildren<Image>().GetComponent<Animator>();
        //Debug.Log(image);

        //Debug.Log(image.GetComponent<Animator>().runtimeAnimatorController);


        //fadeInanimator = new Animator();
        //fadeInanimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("FadeInAnimatorController");
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
            SceneManager.LoadScene(scene);
        }
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name.Equals("Third Person Player"))
        {
            interactText.gameObject.SetActive(true);
            interactAllowed = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.name.Equals("Third Person Player"))
        {
            interactText.gameObject.SetActive(false);
            interactAllowed = false;
        }
    }

}
