using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IniciMenuVictoria : MonoBehaviour
{
    public Animator nom1;
    public Animator nom2;
    public Animator nom3;
    public Animator boto1;
    public Animator boto2;
    public GameObject blocker;
    bool _nom1 = true;
    bool _nom2 = true;
    bool _nom3 = true;
    bool acabat = true;

    // Update is called once per frame
    void Start()
    {
        nom1.SetBool("Iniciar", true);
    }
    void Update()
    {
        if(nom1.GetCurrentAnimatorStateInfo(0).IsName("Ended") && _nom1){
            _nom1 = false;
            nom2.SetBool("Iniciar", true);
        }
        if(nom2.GetCurrentAnimatorStateInfo(0).IsName("Ended") && _nom2){
            _nom2 = false;
            nom3.SetBool("Iniciar", true);
        }
        if(nom3.GetCurrentAnimatorStateInfo(0).IsName("Ended") && _nom3){
            _nom3 = false;
            boto1.SetBool("Iniciar", true);
            boto2.SetBool("Iniciar", true);
        }
        if (boto1.GetCurrentAnimatorStateInfo(0).IsName("Ended") && acabat){
            acabat = false;
            blocker.SetActive(false);
        }
    }
}
