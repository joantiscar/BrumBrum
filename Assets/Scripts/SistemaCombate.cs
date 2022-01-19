using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine.UI;
public class SistemaCombate : MonoBehaviour
{

    public GameObject[] pjs;
    public GameObject pjActual;
    public UICombate UICombate;

    private int ordenActual = 0;
    public int nEnemigos = 0;
    public int nAliados = 0;

    public bool derrota = false;
    public bool victoria = false;
    public bool gameover = false;

    private RaycastHit last_hit;

    public void compruebaVictoria(){
        derrota = nAliados == 0;
        victoria = nEnemigos == 0;
    }

    public void FinalizaTurno(){
        UICombate.FinalizaTurno();
        ordenActual++;
        if (ordenActual >= pjs.Length) ordenActual = 0;
        pjActual = pjs[ordenActual];
        pjActual.GetComponent<Character>().EmpiezaTurno();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        for(int i=0;i<pjs.Length;i++){
            if(pjs[i].GetComponent<Character>().user_controlled) nAliados++;
            else nEnemigos++;
            pjs[i].GetComponent<Character>().SistemaCombate = this;

        }
        pjActual.GetComponent<Character>().EmpiezaTurno();
        
        // Antes esto era necesario pero ahora ya no??? Idk dejemoslo comentado por si las moscas
        // if(pjActual.GetComponent<Character>().user_controlled){
        //     UICombate.adaptaUI(pjActual.GetComponent<Character>().habilidadesDisponibles,pjActual.GetComponent<Character>());
        //     UICombate.actualizaPP();
        //     UICombate.ActualizaDistancia();
        // }

    }

    // Update is called once per frame
    void Update()
    {   
        if(!gameover){
            if(!derrota && !victoria){
                if(pjActual.GetComponent<Character>().user_controlled){
                    
                    if (Input.GetMouseButtonDown(0)) {
                        RaycastHit hit;
                        
                        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
                            last_hit = hit;
                            if (hit.transform.gameObject.name == "Suelo"){
                                if (pjActual.GetComponent<Character>().Moverse(Vector3.Distance(hit.point, pjActual.transform.position))){
                                    pjActual.GetComponentInChildren<Animator>().SetFloat("Velocity", 1);
                                    pjActual.GetComponent<NavMeshAgent>().destination = hit.point;
                                    UICombate.ActualizaDistancia();
                                }
                                
                            }
                            else{
                                // Como pilla el objeto como tal, en plan, el modelo, tenemos que decirle que el objetivo es
                                // el gameObject del padre (Pj1 -> Modelo del personaje)
                                GameObject objetivo = hit.collider.gameObject.transform.parent.gameObject;
                                if(!objetivo.GetComponent<Character>().user_controlled){
                                    pjActual.GetComponent<Character>().objetivo = objetivo;
                                    objetivo.transform.GetChild(0).GetComponent<Renderer>().material.shader = Shader.Find("Outlined/Uniform");
                                }
                                else Debug.Log("No puedes tenerte a ti mismo o un aliado como objetivo!! De momento...");
                            }
                        }
                    }
                    if (Input.GetKeyDown("space")){
                        FinalizaTurno();
                    }

                    if (Input.GetKeyDown("a")){
                        //pjActual.GetComponentInChildren<Animator>().Play("Attack");
                        pjActual.GetComponent<Character>().Atacar();
                    }

                    
                    
                    for(int i=1;i<7;i++){
                        if (Input.GetKeyDown(i.ToString())){
                            UICombate.seleccionarHabilidad(i-1);
                        }
                    }

                    

                }

                //Debug.Log("Pos pers: " + pjActual.transform.position);
                //Debug.Log("Pos lhit: " + last_hit.point);

                if (pjActual.transform.position[0] == last_hit.point[0] && pjActual.transform.position[2] == last_hit.point[2]) 
                    pjActual.GetComponentInChildren<Animator>().SetFloat("Velocity", 0);
            }
            else{
                if (derrota) SceneManager.LoadScene ("GameOver");
                else SceneManager.LoadScene ("Victoria");
                
            }
        }


    }
}
