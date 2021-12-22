using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SistemaCombate : MonoBehaviour
{

    public GameObject[] pjs;
    public GameObject pjActual;
    public GameObject labelDistancia;

    private int ordenActual = 0;
    public int nEnemigos = 0;
    public int nAliados = 0;

    public bool derrota = false;
    public bool victoria = false;

    public void compruebaVictoria(){
        derrota = nAliados == 0;
        victoria = nEnemigos == 0;
    }

    public void FinalizaTurno(){
        pjActual.GetComponent<Character>().TerminaTurno();
        ordenActual++;
        if (ordenActual >= pjs.Length) ordenActual = 0;
        pjActual = pjs[ordenActual];
        pjActual.GetComponent<Character>().EmpiezaTurno();
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<pjs.Length;i++){
            if(pjs[i].GetComponent<Character>().user_controlled) nAliados++;
            else nEnemigos++;
            pjs[i].GetComponent<Character>().SistemaCombate = this;

        }
        pjActual.GetComponent<Character>().EmpiezaTurno();
    }

    // Update is called once per frame
    void Update()
    {   
        if(!derrota && !victoria){
            labelDistancia.GetComponent<Text>().text = pjActual.GetComponent<Character>().metrosRestantes.ToString();

            if (Input.GetMouseButtonDown(0)) {
                    RaycastHit hit;
                    
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
                        if (pjActual.GetComponent<Character>().Moverse(Vector3.Distance(hit.point, pjActual.transform.position))){
                            pjActual.GetComponent<NavMeshAgent>().destination = hit.point;
                        }
                    }
                }
            if (Input.GetKeyDown("space")){
                FinalizaTurno();
            }
            if (Input.GetKeyDown("1")){
                pjActual.GetComponent<Character>().Atacar();
            }
        }
        else{
            Debug.Log("GAMEOVER/VICTORIA");
        }

    }
}
