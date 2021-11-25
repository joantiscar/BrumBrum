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
    private int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        pjActual.GetComponent<Character>().EmpiezaTurno();
    }

    // Update is called once per frame
    void Update()
    {   
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
            pjActual.GetComponent<Character>().TerminaTurno();
            i++;
            if (i >= pjs.Length) i = 0;
            pjActual = pjs[i];
            pjActual.GetComponent<Character>().EmpiezaTurno();
        }
        if (Input.GetKeyDown("1")){
            pjActual.GetComponent<Character>().Atacar();
        }

    }
}
