using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    private RandomCombat randomCombat;


    // Start is called before the first frame update
    void Start()
    {
        randomCombat = GameObject.FindObjectOfType<RandomCombat>();
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name.Equals("Third Person Player"))
        {
            Debug.Log(randomCombat.able);
            randomCombat.SetDisable();
            Debug.Log(randomCombat.able);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.name.Equals("Third Person Player"))
        {
            Debug.Log(randomCombat.able);
            randomCombat.SetAble();
            Debug.Log(randomCombat.able);
        }
    }
}
