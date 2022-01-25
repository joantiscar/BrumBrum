using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine.UI;

public class SistemaCombate : MonoBehaviour
{

    public List<GameObject> pjs = new List<GameObject>();
    public GameObject pjActual;
    public UICombate UICombate;

    private int ordenActual = 0;
    public int nEnemigos = 0;
    public int nAliados = 0;

    public bool derrota = false;
    public bool victoria = false;
    public bool gameover = false;

    public bool apuntando = false; // Indica si estamos en modo lanzar habilidad o no
    public bool moviendose = false; // Indica si pjActual está moviendose o no, para bloquear inputs

    private RaycastHit last_hit; // Es la última posición donde se ha clicado, lo usamos para ver si ha llegado o no al destino
    
    public Character pjActualPersonaje;

    private GameObject lastOutline; // es basicamente el objetivo al que se atacará o curará
    private List<GameObject> objetivosArea; // lo mismo pero para en area
    private GameObject ultimoMirado; // el ultimo gameobject del que hemos mirado los datos

    private GameObject circuloArea;

    public void compruebaVictoria(){
        derrota = nAliados == 0;
        victoria = nEnemigos == 0;
    }

    public void FinalizaTurno(){
        if(pjActualPersonaje.user_controlled){
            UICombate.FinalizaTurno();
            pjActualPersonaje.TerminaTurno();
            deshabilitarTodosOutline();
        }
        ordenActual++;
        if (ordenActual >= pjs.Count){
            ordenActual = 0;
            recalculaOrden(); // Por si acaso le ha cambiado la velocidad a alguien
        }
        pjActual = pjs[ordenActual];
        pjActualPersonaje = pjActual.GetComponent<Character>();
        pjActualPersonaje.EmpiezaTurno();
    }

    public void deshabilitarTodosOutline(){
        foreach(var obj in objetivosArea){
                deshabilitarOutline(obj);
        }
        deshabilitarOutline(lastOutline);
    }

    public void deshabilitarOutline(GameObject pj){
        if(pj!=null){
            Debug.Log(pj.name);
            Outline o = pj.GetComponent<Outline>();
            o.outlineWidth = 0;
            o.UpdateMaterialProperties();
        }
    }

    public GameObject recalculaOrden(){

        pjs.Sort((p1,p2)=>p2.GetComponent<Character>().velocidadActual().CompareTo(p1.GetComponent<Character>().velocidadActual()));
        return pjs[0];
    }

    public bool enRango(GameObject caster, GameObject objetivo, Habilidad h){
        float distancia = Vector3.Distance(caster.transform.position, objetivo.transform.position);
        float radio = objetivo.GetComponentInChildren<CapsuleCollider>().radius; // Lo tenemos en cuenta por si acasito el modelo queda dentro pero la posicion no
        return distancia-radio <= h.range && distancia+radio >= h.range;
    }


    void cargarProtas(){
        pjs[0].GetComponent<Character>().copy(Singleton.instance().pjs[0]);
        pjs[1].GetComponent<Character>().copy(Singleton.instance().pjs[1]);
        pjs[2].GetComponent<Character>().copy(Singleton.instance().pjs[2]);
        pjs[3].GetComponent<Character>().copy(Singleton.instance().pjs[3]);
        pjs[4].GetComponent<Character>().copy(Singleton.instance().pjs[4]);
    }

    public void Empezar(){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        cargarProtas();
        pjActual = recalculaOrden();
        pjActualPersonaje = pjActual.GetComponent<Character>();

        for(int i=0;i<pjs.Count;i++){
            if(pjs[i].GetComponent<Character>().user_controlled) nAliados++;
            else nEnemigos++;
            pjs[i].GetComponent<Character>().SistemaCombate = this;
            pjs[i].GetComponent<Character>().iniciarEstado();
            pjs[i].GetComponent<Character>().UICombate = this.UICombate;

            Outline o = pjs[i].AddComponent<Outline>();
            o.outlineWidth = 0;
            o.outlineColor = Color.red;

        }

        pjActualPersonaje.EmpiezaTurno();
        
        objetivosArea = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Empezar();
    }

    GameObject getCharacter(Transform t){ // Se supone que encuentra el gameobject en la jerarquia que tiene el Character y te lo devuelve, pero lo escribí una noche tonta
        if(t.gameObject.GetComponent<Character>()!=null) return t.gameObject;
        if(t.parent != null) return getCharacter(t.parent);
        return null;
    }

    public void destruirCirculoArea(){
        if(circuloArea!=null){
            Destroy(circuloArea);
            circuloArea = null;

        }
    }

    public void dibujaCirculoArea(Habilidad h,Vector3 pos){ // Dibuja el circulo de la habilidad h en area en la posición pos
        destruirCirculoArea();
        circuloArea = new GameObject();
        circuloArea.DrawCircle(h.radius, .085f, new Color(1, 0, 0.48f));
        circuloArea.transform.position = new Vector3(pos.x,pos.y+0.05f,pos.z); // un poquito mas arriba
    }

    public void mueveCirculo(Vector3 pos){
        circuloArea.transform.position = new Vector3(pos.x,pos.y+0.05f,pos.z);
    }

    public bool ratonDentro(Vector3 posCaster, Habilidad h, Vector3 posRaton){ // true si el ratón está dentro del área delimitada por la posicion del caster y el rango de la habilidad
        float distancia = Vector3.Distance(posCaster, posRaton);
        return distancia <= h.range;
    }

    public void personajesEnArea(Habilidad h, Vector3 centro){ // Consigue los objetivos y los highlightea
        Collider[] colliders = Physics.OverlapSphere(centro, h.radius);
        List<GameObject> aux = new List<GameObject>(objetivosArea);
        foreach(var obj in aux){
            if(Array.IndexOf(colliders,obj.GetComponentInChildren<Collider>())==-1){ // Si un objetivo ya no está en los colliders, lo quitamos
                deshabilitarOutline(obj);
                objetivosArea.Remove(obj);
            }
        }
        foreach(var col in colliders){
            GameObject obj = getCharacter(col.transform);
            if(obj!=null && !objetivosArea.Contains(obj)){
                Character c = obj.GetComponent<Character>();
                if((h.targetEnemy && !c.user_controlled) || (!h.targetEnemy && c.user_controlled)){ // Si es un personaje válido, lo añadimos
                    objetivosArea.Add(obj);
                    highlightPersonaje(h, c.gameObject);
                }
            }
        }
        
    }

    public void highlightPersonaje(Habilidad h, GameObject pj){ // Le da un outline al personaje
        
        Outline o = pj.GetComponent<Outline>();
        o.outlineWidth = 3.7f;
        // Si cura, hago el circulo verde, sino, rojo
        if(!h.targetEnemy){
            o.outlineColor = new Color(0.72f, 1, 0.21f);
        }else if(h.targetEnemy){
            o.outlineColor = Color.red;
        }
        o.UpdateMaterialProperties();
    }

    // Update is called once per frame
    void Update()
    {   
        if(!gameover){
            if(!derrota && !victoria){
                if(pjActualPersonaje.user_controlled){
                    
                    RaycastHit hit;
                    bool hitDado = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100); // Asi solo se hace una vez y no 3, nos dice si le hemos dado a algo

                    // INPUTS
                    if(!moviendose){ // No podemos hacer inputs si nos estamos moviendo

                        if (Input.GetMouseButtonDown(0)) { // Clic izquierdo hace varias cosas dependiendo del modo
                            if(hitDado){
                                if(apuntando){// Si en modo habilidad y hay un objetivo en el punto de mira, atacamos y volemos a modo moverse
                                    if(pjActualPersonaje.habilidadesDisponibles[pjActualPersonaje.habilidadSeleccionada].radius==0.0f && lastOutline!=null){  // Habilidad normal
                                        pjActualPersonaje.objetivo = lastOutline;
                                        pjActualPersonaje.Atacar();

                                        deshabilitarOutline(lastOutline);
                                        lastOutline = null;
                                    }
                                    else if(objetivosArea.Count!=0){ // En area

                                        pjActualPersonaje.objetivos = objetivosArea;
                                        pjActualPersonaje.Atacar();

                                        foreach(var obj in objetivosArea){
                                            deshabilitarOutline(obj);
                                        }

                                        objetivosArea.Clear();
                                    }

                                    UICombate.deseleccionarHabilidad();
                                    destruirCirculoArea();
                                    apuntando = false;
                                }  
                                else{ // Casteamos el ray a ver donde se ha clicado

                                    last_hit = hit;

                                    // Habría que quitar lo de "Suelo" for future progress...?
                                    if (!apuntando && hit.transform.gameObject.name == "Suelo"){ // Si no estás en modo apuntar con la habilidad, moverse
                                        if (pjActualPersonaje.Moverse(Vector3.Distance(hit.point, pjActual.transform.position))){
                                            pjActual.transform.LookAt(hit.point);
                                            pjActual.GetComponentInChildren<Animator>().SetFloat("Velocity", 1);
                                            moviendose = true;
                                            pjActual.GetComponent<NavMeshAgent>().destination = hit.point;
                                            UICombate.ActualizaDistancia();
                                        }
                                        
                                    }
                                }
                            }
                        }
                        else if (Input.GetKeyDown("space")){
                            FinalizaTurno();
                        }
                        else if(apuntando && (Input.GetKeyDown("0") || Input.GetKeyDown("escape") || Input.GetMouseButtonDown(1))){
                            UICombate.deseleccionarHabilidad();
                            apuntando = false;
                            deshabilitarTodosOutline();
                            destruirCirculoArea();
                        }
                        else if (Input.GetKeyDown("q") && Singleton.instance().pocions > 0){
                            pjActualPersonaje.pocion();
                            UICombate.actualizaPP();
                        }
                        else{
                            // Vamos mirando las teclas de 1 a n habilidades para seleccionar una habilidad
                            for(int i=1;i <= pjActualPersonaje.habilidadesDisponibles.Count;i++){
                                if (Input.GetKeyDown(i.ToString())){
                                    UICombate.seleccionarHabilidad(i-1);
                                    

                                }
                            }
                        }
                    }

                    if(apuntando){
                        if (hitDado) {

                            GameObject objetivo = getCharacter(hit.collider.gameObject.transform);
                            Habilidad h = pjActualPersonaje.habilidadesDisponibles[pjActualPersonaje.habilidadSeleccionada];

                            if(h.radius>0.0f){ // HABILIDAD EN AREA
                                if(ratonDentro(pjActual.transform.position,h,hit.point)){
                                    // Dibujamos el circulo o lo movemos, pero que se muestra, seguro
                                    if(circuloArea==null)dibujaCirculoArea(h,hit.point);  // si queremos que el circulo solo este en el suelo, cambiar el if y añadir && objetivo!=null 
                                    else mueveCirculo(hit.point);
                                    
                                    personajesEnArea(h,hit.point); // Obtenemos los personajes y además los objetivos

                                }else{ // el ratón está fuera, quitamos el circulo
                                    destruirCirculoArea();
                                    deshabilitarTodosOutline();
                                }
                            } 
                            else if(h.radius==0.0f && objetivo!=null){ // HABILIDAD NORMAL
                                // Miramos si esta a rango y si es un enemigo y tenemos una habilidad de atacar o un aliado y de curar/bufar y seleccionamos solo personajes
                                if(enRango(pjActual,objetivo,h) // en rango
                                && ((h.targetEnemy && !objetivo.GetComponent<Character>().user_controlled) ||
                                    (!h.targetEnemy && objetivo.GetComponent<Character>().user_controlled))){ // miramos el targetenemy y si apunta a un enemigo on no
                                            
                                        // Miramos que el objetivo del ray y el ultimo objetivo mirado no sean el mismo para ir más rápido
                                        if(objetivo!=lastOutline){
                                            lastOutline = objetivo;
                                            highlightPersonaje(h,lastOutline);
                                        }
                                }
                                else if(lastOutline!=null){
                                    deshabilitarOutline(lastOutline);
                                    lastOutline = null;

                                }
                            }
                            else{
                                deshabilitarOutline(lastOutline); // Le estamos dando a algo que no es un Character
                                lastOutline = null;
                            }
                        }
                        else{
                            deshabilitarOutline(lastOutline); // Le estamos dando a algun sitio no válido
                            lastOutline = null;

                        }
                    }

                    if (hitDado) {
                        GameObject objetivo = getCharacter(hit.collider.gameObject.transform);

                        // Mostramos los datos del personaje, da igual en qué modo estemos, ni si está en rango...
                        if(objetivo!=null && ultimoMirado!=objetivo){
                            UICombate.mostrarDatos(objetivo);
                            ultimoMirado = objetivo;
                        }
                        else if(objetivo==null){ // No es un personaje objetivo
                            UICombate.escondeDatos();
                            ultimoMirado = null;
                        }
                    }
                    else{ // Le damos a un sitio no válido, como el fondo (igual esto luego no sirve)
                        UICombate.escondeDatos();
                        ultimoMirado = null;

                    }


                    

                }

                // Miramos si ha llegado a su destino dandole un poco de margen para que no se quede atascau siempre
                if (moviendose && 
                    pjActual.transform.position[0] >= last_hit.point[0] - 0.2 && pjActual.transform.position[0] <= last_hit.point[0] + 0.2 && 
                    pjActual.transform.position[2] >= last_hit.point[2] - 0.2 && pjActual.transform.position[2] <= last_hit.point[2] + 0.2) {
                    pjActual.GetComponentInChildren<Animator>().SetFloat("Velocity", 0);
                    pjActual.GetComponent<NavMeshAgent>().isStopped = true;
                    pjActual.GetComponent<NavMeshAgent>().isStopped = false;
                    moviendose = false;
                }                
                
            }
            else{
                if (derrota) SceneManager.LoadScene("GameOver");
                else
                {
                    //SceneManager.LoadScene("Victoria");


                    string currentScene = SceneManager.GetActiveScene().name;
                    // Debug.Log(currentScene);

                    
                    if (currentScene == "CombatScene_CombatFinal")
                    {
                        SceneManager.LoadScene("Scene5_Flight_Part1");
                    }
                    else
                    {
                        GameObject.FindObjectOfType<RandomCombat>().ExitCombat();
                    }
                    

                }

            }

        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            GameObject.FindObjectOfType<SistemaCombate>().derrota = true;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameObject.FindObjectOfType<SistemaCombate>().victoria = true;
        }

    }
}
