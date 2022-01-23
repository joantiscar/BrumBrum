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

    public bool apuntando = false;
    public bool blocked = false;

    private RaycastHit last_hit;
    
    public bool moviendose = false;
    private Character pjActualPersonaje;

    public GameObject lastOutline; // es basicamente el objetivo

    public void compruebaVictoria(){
        derrota = nAliados == 0;
        victoria = nEnemigos == 0;
    }

    public void FinalizaTurno(){
        if(pjActualPersonaje.user_controlled){
            UICombate.FinalizaTurno();
            pjActualPersonaje.TerminaTurno();
        }
        ordenActual++;
        if (ordenActual >= pjs.Length) ordenActual = 0;
        pjActual = pjs[ordenActual];
        pjActualPersonaje = pjActual.GetComponent<Character>();
        pjActualPersonaje.EmpiezaTurno();
    }

    public void deshabilitarOutline(){
        Outline o = lastOutline.GetComponent<Outline>();
        o.outlineWidth = 0;
        o.UpdateMaterialProperties();
        lastOutline = null;
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

            Outline o = pjs[i].AddComponent<Outline>();
            o.outlineWidth = 0;
            o.outlineColor = Color.red;

        }
        pjActualPersonaje = pjActual.GetComponent<Character>();
        pjActualPersonaje.EmpiezaTurno();
        
    
    }

    GameObject getCharacter(Transform t){
        if(t.parent == null || t.gameObject.GetComponent<Character>()!=null) return t.gameObject;
        if(t.parent != null) return getCharacter(t.parent);
        return null;
    }

    // Update is called once per frame
    void Update()
    {   
        if(!gameover){
            if(!derrota && !victoria){
                if(pjActualPersonaje.user_controlled && !moviendose && !blocked){
                    
                    if (Input.GetMouseButtonDown(0)) {
                        RaycastHit hit;
                        
                        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
                            last_hit = hit;
                            if (!apuntando && hit.transform.gameObject.name == "Suelo"){ // Si no estás en modo apuntar con la habilidad, moverse
                                if (pjActualPersonaje.Moverse(Vector3.Distance(hit.point, pjActual.transform.position))){
                                    pjActual.GetComponentInChildren<Animator>().SetFloat("Velocity", 1);
                                    moviendose = true;
                                    pjActual.GetComponent<NavMeshAgent>().destination = hit.point;
                                    UICombate.ActualizaDistancia();
                                }
                                
                            }
                            else{
                                if(apuntando){
                                    // Como pilla el objeto como tal, en plan, el modelo, tenemos que decirle que el objetivo es
                                    // el gameObject del padre (Pj1 -> Modelo del personaje)
                                    GameObject objetivo = hit.collider.gameObject.transform.parent.gameObject;
                                    if(lastOutline!=null){
                                        
                                        pjActualPersonaje.objetivo = objetivo;
                                        
                                        pjActualPersonaje.Atacar();

                                        UICombate.deseleccionarHabilidad();
                                        apuntando = false;
                                        
                                        deshabilitarOutline();
                                    }
                                    else{
                                        Debug.Log("El enemigo ersta fuera de rango o no puedes curar a un enemigo o atacar a un aliado!!");
                                    }

                                }
                            }
                        }
                    }
                    else if (Input.GetKeyDown("space")){
                        FinalizaTurno();
                    }
                    // else if (Input.GetKeyDown("a")){
                    //     pjActualPersonaje.Atacar();
                    // }
                    else if(apuntando){
                        if(Input.GetKeyDown("0") || Input.GetKeyDown("escape") || Input.GetMouseButtonDown(1)){
                            UICombate.deseleccionarHabilidad();
                            apuntando = false;
                            deshabilitarOutline();
                        }
                        else{

                            RaycastHit hit;
                            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
                                GameObject objetivo = getCharacter(hit.collider.gameObject.transform);//hit.collider.gameObject.transform.parent.gameObject;
                                
                                // Miramos si esta a rango y si es un enemigo y tenemos una habilidad de atacar o un aliado y de curar/bufar y seleccionamos solo personajes
                                // FALTA BUFOS!!
                                if(objetivo!=null && objetivo.GetComponent<Character>() != null 
                                && Vector3.Distance(pjActual.transform.position, objetivo.transform.position) <= pjActualPersonaje.habilidadesDisponibles[pjActualPersonaje.habilidadSeleccionada].range // en rango
                                && (pjActualPersonaje.habilidadesDisponibles[pjActualPersonaje.habilidadSeleccionada].damages && !objetivo.GetComponent<Character>().user_controlled) ||
                                    (pjActualPersonaje.habilidadesDisponibles[pjActualPersonaje.habilidadSeleccionada].heals && objetivo.GetComponent<Character>().user_controlled)){
                                        if(objetivo!=lastOutline){
                                            lastOutline = objetivo;
                                            Outline o = objetivo.GetComponent<Outline>();
                                            o.outlineWidth = 3.7f;
                                            o.UpdateMaterialProperties();
                                        }
                                }
                                else if(lastOutline!=null){
                                    deshabilitarOutline();

                                }

                            }
                        }

                    }

                    for(int i=1;i <= pjActualPersonaje.habilidadesDisponibles.Count;i++){
                        if (Input.GetKeyDown(i.ToString())){
                            UICombate.seleccionarHabilidad(i-1);
                        }
                    }

                }

                // Miramos si ha llegado a su destino dandole un poco de margen para que no se quede atascau siempre
                if (pjActual.transform.position[0] >= last_hit.point[0] - 0.2 && pjActual.transform.position[0] <= last_hit.point[0] + 0.2 && 
                    pjActual.transform.position[2] >= last_hit.point[2] - 0.2 && pjActual.transform.position[2] <= last_hit.point[2] + 0.2) {
                    pjActual.GetComponentInChildren<Animator>().SetFloat("Velocity", 0);
                    moviendose = false;
                }

                //Debug.Log("Pos pers: " + pjActual.transform.position);
                //Debug.Log("Pos lhit: " + last_hit.point);

                
                
            }
            else{
                if (derrota) SceneManager.LoadScene ("GameOver");
                else SceneManager.LoadScene ("Victoria");
                
            }
        }


    }
}
