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
    public string scene;

    private void Start()
    {
        //pickupText = GameObject.FindGameObjectWithTag("ItemText").GetComponent<Text>();
        interactText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (interactAllowed && Input.GetKeyDown(KeyCode.E))
        {
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
