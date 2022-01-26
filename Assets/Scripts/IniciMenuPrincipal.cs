using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IniciMenuPrincipal : MonoBehaviour
{
    public Animator inici;
    public Animator titol;
    public Animator boto1;
    public Animator boto2;
    public Animator boto3;
    public Animator boto4;
    public GameObject blocker;
    bool _inici = true;
    bool _titol = true;
    bool acabat = true;

    // Update is called once per frame
    void Update()
    {
        if(inici.GetCurrentAnimatorStateInfo(0).IsName("Ended") && _inici){
            _inici = false;
            titol.SetBool("Iniciar", true);
        }
        if(titol.GetCurrentAnimatorStateInfo(0).IsName("Ended") && _titol){
            _titol = false;
            boto1.SetBool("Iniciar", true);
            boto2.SetBool("Iniciar", true);
            boto3.SetBool("Iniciar", true);
            boto4.SetBool("Iniciar", true);
        }
        if (boto1.GetCurrentAnimatorStateInfo(0).IsName("Ended") && acabat){
            acabat = false;
            blocker.SetActive(false);
        }
    }
}
