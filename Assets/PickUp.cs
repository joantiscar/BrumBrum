using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    [SerializeField]
    private Text pickupText;
    private bool pickUpAllowed;

    private void Start()
    {
        pickupText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(pickUpAllowed && Input.GetKeyDown(KeyCode.E))Pick();
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.name.Equals("Third Person Player"))
        {
            pickupText.gameObject.SetActive(true);
            pickUpAllowed = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.name.Equals("Third Person Player"))
        {
            pickupText.gameObject.SetActive(false);
            pickUpAllowed = false;
        }
    }

    private void Pick()
    {
        Destroy(gameObject);
        pickupText.gameObject.SetActive(false);
    }
}
