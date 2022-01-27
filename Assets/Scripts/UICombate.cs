using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using System;
public class UICombate : MonoBehaviour
{
    private Image[] imgs; // Las imagenes donde se ponen los sprites de las skills

    private Color transparente = new Color(0,0,0,0);
    private Color opaco = Color.white;

    private List<Habilidad> _habilidades; // Una array con las habilidades actuales

    private bool empezado = false;
    
    public Transform cajaHabilidad;
    public Transform cajaDatos;
    public Slider barraHP;
    public Slider barraDistancia;
    public TextMeshPro LabelPP;
    public TextMeshPro LabelPPMax;
    public TextMeshPro TextLabel;
    public TextMeshPro pocionLabel;
    public GameObject TextBackground;
    public GameObject Turnos;
    public GameObject Estados;

    private List<TextMeshPro> nombres;
    private Image[] imgsEstados;
    private List<GameObject> aDestruir = new List<GameObject>();
    private Vector3 primerNombre;

    public GameObject selected; // El cuadrito que muestra el seleccionado

    public int habilidadSeleccionada = -1;
    Character pjActual;
    GameObject pjDatosActual;
    private List<string> missatges = new List<string>();
    int mensajeHP = 0;
    
    public SistemaCombate SistemaCombate;

    void Awake()
    {
        imgs = GetComponentsInChildren<Image>();
        nombres = Turnos.GetComponentsInChildren<TextMeshPro>().ToList();
        imgsEstados = Estados.GetComponentsInChildren<Image>();
    }

    public void muestraOrden(){ // Cambia el orden de turnos segun el dado por sistema de combate
        if(!empezado){
            Sprite enemigo = Resources.Load<Sprite>("UIEnemigoActivo");
            Sprite aliado = Resources.Load<Sprite>("UIAlaidoActivo");
            // Ponemos bien el primero: nombre y fondo
            nombres[0].gameObject.GetComponent<TextMeshPro>().text = SistemaCombate.pjs[0].GetComponent<Character>().nombre;
            if(!SistemaCombate.pjs[0].GetComponent<Character>().user_controlled) nombres[0].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = enemigo;
            else nombres[0].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = aliado;
            // Vamos copiando
            GameObject ant = nombres[0].gameObject;
            int i=1;
            while(i<SistemaCombate.pjs.Count){
                GameObject go = Instantiate(ant,Turnos.transform);

                if(ant==null)go.transform.position = new Vector3(go.transform.position.x,go.transform.position.y-13.5f,go.transform.position.z);
                else go.transform.position = new Vector3(ant.transform.position.x,ant.transform.position.y-13.5f,ant.transform.position.z);
                go.GetComponent<TextMeshPro>().text = SistemaCombate.pjs[i].GetComponent<Character>().nombre;

                if(!SistemaCombate.pjs[i].GetComponent<Character>().user_controlled) go.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = enemigo;
                else go.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = aliado;

                i++;
                ant = go;
            }
            nombres = Turnos.GetComponentsInChildren<TextMeshPro>().ToList();
            empezado = true;
        }
        else{
            for(int i=1;i<nombres.Count;i++){ // Destruimos todos menos el primero
                nombres[i].transform.SetParent(null,false);
                Destroy(nombres[i].gameObject);
            }
            foreach(var obj in aDestruir){
                obj.transform.SetParent(null,false);
                Destroy(obj);
            }
            aDestruir.Clear();
            nombres[0].transform.position = primerNombre;
            nombres = Turnos.GetComponentsInChildren<TextMeshPro>().ToList();
            empezado = false;
            muestraOrden();
        }
    }

    void Start(){
        cajaDatos.gameObject.SetActive(false);
        cajaHabilidad.gameObject.SetActive(false);
        TextLabel.text = "";
        pocionLabel.text = Singleton.nPocions().ToString();

        InvokeRepeating("ActualitzarCartell", 0f, 0.5f);

        primerNombre = Turnos.transform.Find("Pos").position;
    }

    void ActualitzarCartell(){
        if (missatges.Count == 0){
            TextLabel.gameObject.SetActive(false);
            TextBackground.SetActive(false);
        }else {
            if(mensajeHP==0){
                TextLabel.gameObject.SetActive(true);
                TextBackground.SetActive(true);
                TextLabel.text = missatges[0];
                mensajeHP = 4;
            }
            else{
                mensajeHP--;
                if(mensajeHP==0) missatges.RemoveAt(0);
            }
        }
    }



    public void habilitarUI(bool habilitar, bool pausa = false){
        barraHP.gameObject.transform.parent.gameObject.SetActive(habilitar);
        LabelPP.gameObject.transform.parent.gameObject.SetActive(habilitar);
        this.gameObject.SetActive(habilitar && (!pausa || (pjActual!=null && pjActual.user_controlled)));
        this.transform.parent.GetChild(0).gameObject.SetActive(habilitar);
        barraDistancia.gameObject.SetActive(habilitar);
        Turnos.SetActive(!pausa || (habilitar && pausa));
    }

    public void adaptaUI(List<Habilidad> habilidades,Character pj){ //Llamada cada vez que empieza el turno de un personaje
        if(pj.user_controlled){
            habilitarUI(true);

            pjActual = pj;

            // Cambia las imagenes de la barra de abajo por las del parametro habilidades
            foreach (Image img in imgs)
            {
                img.color = opaco;
                img.transform.GetChild(0).gameObject.SetActive(false);
            }
            imgs[6].transform.GetChild(0).gameObject.SetActive(true);
            int i;
            for(i=0;i<habilidades.Count;i++)
            {
                // Usamos el nombre de la habilidad para saber qué imagen usar
                // EL PATH TIENE QUE ESTAR EN RESOURCES/... PORFA HACEDLE CASO A ESTO SI LO TOCAIS EN ALGUN MOMENTO SANKIU VERY MUCH
                imgs[i].sprite = Resources.Load<Sprite>(habilidades[i].name);
                if(pjActual.cooldowns[i]>0){
                    deshabilitaHabilidad(i); // Miramos el cooldown, si es mayor que 0, en gris
                
                }
                else imgs[i].material = null;
            }
            while(i<imgs.Length-1){ // ponemos el resto menos la poti transparentes
                imgs[i].color = transparente;
                i++;
            }
            _habilidades = habilidades;

            barraHP.maxValue = (float) pj.hpMax;
            barraDistancia.maxValue = (float) pj.metrosMaximos - 0.5f;
            actualizaHP();
            ActualizaDistancia();

            // Actualiza el label de PP
            LabelPP.text = pjActual.actPAtaques.ToString();
            LabelPPMax.text = pjActual.maxPAtaques.ToString();

            // Miramos si tiene potis para ponerlo en gris o no
            if(Singleton.nPocions()==0) deshabilitaHabilidad(6); 
            
        }
        else{
            escondeDatos();
            habilitarUI(false);
        }
        
    }

    public void actualizaTurno(Character pj, bool muerto){
        int pos; 
        if(muerto) pos = SistemaCombate.pjs.IndexOf(pj.gameObject);
        else pos = SistemaCombate.ordenActual;
        if(pos<nombres.Count-1){
            if(!pj.user_controlled){
                Sprite enemigo = Resources.Load<Sprite>("UIEnemigoInactivo");
                nombres[pos].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = enemigo;
            }
            else{
                Sprite aliado = Resources.Load<Sprite>("UIAliadoInactivo");
                nombres[pos].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = aliado;
            }
            if(muerto){
                aDestruir.Add(nombres[pos].gameObject);
                nombres.RemoveAt(pos);
            }

        }
    }

    public void FinalizaTurno(){ // Llamada cada vez que acaba el turno de un personaje
        // Restart la habilidad seleccionada
        selected.SetActive(false);
        pjActual.habilidadSeleccionada = -1;
        habilidadSeleccionada = -1;
        if(SistemaCombate.ordenActual!=SistemaCombate.pjs.Count)actualizaTurno(pjActual,false);

    }

    public void muestraDescripcion(int pos){
        // Cuando se hace hover sobre una imagen, nos llega la pos en habilidades de la imagen
        if(pos<_habilidades.Count || pos==6){ // 6 es la pocion
            Habilidad h;
            if(pos==6){
                h = Habilidades.Pocion;
            }else{
                habilidadSeleccionada = pos;
                h = _habilidades[pos];
            }
            cajaHabilidad.gameObject.SetActive(true);
            cajaHabilidad.Find("Nombre").GetComponent<TextMeshPro>().text = h.name;
            cajaHabilidad.Find("Dano").GetComponent<TextMeshPro>().text = h.damage.ToString();
            if(pos==6) cajaHabilidad.Find("Cooldown").GetComponent<TextMeshPro>().text = "—";
            else cajaHabilidad.Find("Cooldown").GetComponent<TextMeshPro>().text = h.cooldown.ToString();
            cajaHabilidad.Find("nCost").GetComponent<TextMeshPro>().text = h.coste.ToString();
            cajaHabilidad.Find("Descripcion").GetComponent<TextMeshPro>().text = h.description;
            if(h.heals){
                cajaHabilidad.Find("Fondo").GetComponent<Image>().sprite = Resources.Load<Sprite>("HUDS/HUD_JUGADOR_AUX/HUD_CURA");
            }
            else{
                cajaHabilidad.Find("Fondo").GetComponent<Image>().sprite = Resources.Load<Sprite>("HUDS/HUD_JUGADOR_AUX/DANO");
            }
        }
    }

    public void escondeCaja(){
        cajaHabilidad.gameObject.SetActive(false);
    }

    public void seleccionarHabilidad(int h){
        // Miramos si nos han pasado una h dentro de los valores adecuados, si es seleccionable y si no se esta moviendo el personaje
        if(h<_habilidades.Count && pjActual.esSeleccionable(h) && !SistemaCombate.moviendose){

            Debug.Log("Habilidad cambiada a: "+_habilidades[h].name);

            pjActual.habilidadSeleccionada = h;

            selected.SetActive(true);
            selected.transform.position = imgs[h].transform.position;
            selected.GetComponent<EventTrigger>().triggers[0].callback.AddListener((data) => { muestraDescripcion(h); });
            
            pjActual.destruirCirculoMov();
            pjActual.dibujaCirculoHab();
            SistemaCombate.destruirCirculoArea();
            SistemaCombate.deshabilitarTodosOutline();

            SistemaCombate.apuntando = true;

        }
    }

    public void deseleccionarHabilidad(){
        selected.SetActive(false);
        pjActual.habilidadSeleccionada = -1;
        pjActual.destruirCirculoHab();
        pjActual.dibujaCirculoMov();
    }

    public void actualizaPP(){
        LabelPP.text = pjActual.actPAtaques.ToString();
    }

    public void actualizaHP(){
        // Actualiza la barra con el HP
        // barraHP.maxValue = (float) pjActual.hpMax;
        barraHP.value = (float) pjActual.hp;
    }

    public void ActualizaDistancia(){
        // Actualizamos la GUI con la distancia
        barraDistancia.value = (float) pjActual.metrosRestantes - 0.5f;
        pjActual.dibujaCirculoMov();
    }

    public void mostrarDatos(GameObject personaje){
        cajaDatos.gameObject.SetActive(true);

        pjDatosActual = personaje;

        Character c = personaje.GetComponent<Character>();

        cajaDatos.Find("Nombre").GetComponent<TextMeshPro>().text = c.nombre;
        cajaDatos.Find("HP").GetComponent<TextMeshPro>().text = c.hp.ToString() + " HP";
        cajaDatos.Find("Nivel").GetComponent<TextMeshPro>().text = c.level.ToString();

        // Actualiza la barra con el HP
        Slider barra = cajaDatos.Find("Barra").GetComponent<Slider>();
        barra.maxValue = (float) c.hpMax;
        barra.value = (float) c.hp;
    }

    public void escondeDatos(){
        
        cajaDatos.gameObject.SetActive(false);
        pjDatosActual = null;
    }

    public void actualizaDatos(){
        if(cajaDatos.gameObject.activeSelf){
            cajaDatos.Find("Barra").GetComponent<Slider>().value = (float) pjDatosActual.GetComponent<Character>().hp;
        }
    }

    public void mostrarMissatge(string missatge){
        missatges.Add(missatge);
    }

    public void bebePocion(){
        pjActual.pocion();
        pocionLabel.text = Singleton.nPocions().ToString();
        if(Singleton.nPocions()==0) deshabilitaHabilidad(6);
    }

    public void deshabilitaHabilidad(int h){
        if(pjActual.habilidadesDisponibles[h].cooldown>0){
            imgs[h].material = Resources.Load<Material>("Gray");
            imgs[h].transform.GetChild(0).gameObject.SetActive(true);
            imgs[h].transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = pjActual.cooldowns[h].ToString();

        }
    }

    public void habilitaEstado(int estado){
        imgsEstados[estado].material = null;
    }

    public void deshabilitaEstado(int estado){
        imgsEstados[estado].material = Resources.Load<Material>("Gray");
    }

}
