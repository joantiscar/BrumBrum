using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;
public class UICombate : MonoBehaviour
{
    private Image[] imgs; // Las imagenes donde se ponen los sprites de las skills

    private Color transparente = new Color(0,0,0,0);
    private Color opaco = Color.white;

    private List<Habilidad> _habilidades; // Una array con las habilidades actuales
    
    public Transform cajaHabilidad;
    public Transform cajaDatos;
    public Slider barraHP;
    public Text LabelPP;
    public TextMeshPro TextLabel;
    public GameObject TextBackground;

    public GameObject selected; // El cuadrito que muestra el seleccionado

    public int habilidadSeleccionada = -1;
    Character pjActual;
    GameObject pjDatosActual;
    private List<string> missatges = new List<string>();
    public SistemaCombate SistemaCombate;

    void Awake()
    {
        imgs = GetComponentsInChildren<Image>();

    }

    void Start(){
        cajaDatos.gameObject.SetActive(false);
        cajaHabilidad.gameObject.SetActive(false);
        // LabelPP.text = pjActual.actPAtaques.ToString();
        TextLabel.text = "";

        InvokeRepeating("ActualitzarCartell", 0f, 3f);

    }

    void ActualitzarCartell(){
        if (missatges.Count == 0){
            TextLabel.gameObject.SetActive(false);
            TextBackground.SetActive(false);
        }else {
            TextLabel.gameObject.SetActive(true);
            TextBackground.SetActive(true);
            TextLabel.text = missatges[0];
            missatges.RemoveAt(0);
        }
    }



    public void habilitarUI(bool habilitar){
        barraHP.gameObject.transform.parent.gameObject.SetActive(habilitar);
        LabelPP.gameObject.transform.parent.gameObject.SetActive(habilitar);
        this.gameObject.SetActive(habilitar);
        this.transform.parent.Find("FondoSkills").gameObject.SetActive(habilitar);
    }

    public void adaptaUI(List<Habilidad> habilidades,Character pj){ //Llamada cada vez que empieza el turno de un personaje
        if(pj.user_controlled){
            habilitarUI(true);

            // Cambia las imagenes de la barra de abajo por las del parametro habilidades
            foreach (Image img in imgs)
            {
                img.color = opaco;
            }
            int i;
            for(i=0;i<habilidades.Count;i++)
            {
                string nombre = habilidades[i].name;
                // Usamos el nombre de la habilidad para saber qué imagen usar
                // EL PATH TIENE QUE ESTAR EN RESOURCES/... PORFA HACEDLE CASO A ESTO SI LO TOCAIS EN ALGUN MOMENTO SANKIU VERY MUCH
                imgs[i].sprite = Resources.Load<Sprite>(nombre);
            }
            while(i<imgs.Length){
                imgs[i].color = transparente;
                i++;
            }
            _habilidades = habilidades;
            pjActual = pj;

            // Actualiza la barra con el HP
            barraHP.maxValue = (float) pj.hpMax;
            barraHP.value = (float) pj.hp;


            // Actualiza el label de PP
            LabelPP.text = pjActual.actPAtaques.ToString();

            
        }
        else{
            escondeDatos();
            habilitarUI(false);
        }
        
    }

    public void FinalizaTurno(){ // Llamada cada vez que acaba el turno de un personaje
        // Restart la habilidad seleccionada
        selected.SetActive(false);
        pjActual.habilidadSeleccionada = -1;
        habilidadSeleccionada = -1;
    }

    public void muestraDescripcion(int pos){
        // Cuando se hace hover sobre una imagen, nos llega la pos en habilidades de la imagen
        if(pos<_habilidades.Count){
            habilidadSeleccionada = pos;
            Habilidad h = _habilidades[pos];
            cajaHabilidad.gameObject.SetActive(true);
            cajaHabilidad.Find("Nombre").GetComponent<Text>().text = h.name;
            cajaHabilidad.Find("Dano").GetComponent<Text>().text = h.damage.ToString();
            cajaHabilidad.Find("Cooldown").GetComponent<Text>().text = pjActual.cooldowns[pos].ToString();
            cajaHabilidad.Find("nCost").GetComponent<Text>().text = h.coste.ToString() + " PP";
            cajaHabilidad.Find("Descripcion").GetComponent<Text>().text = h.description;
            if(h.heals){
                cajaHabilidad.Find("LabelDano").GetComponent<Text>().text = "Cura:";
            }
            else{
                cajaHabilidad.Find("LabelDano").GetComponent<Text>().text = "Daño:";
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

    public void ActualizaDistancia(){
        // Actualizamos la GUI con la distancia
        pjActual.dibujaCirculoMov();
    }

    public void mostrarDatos(GameObject personaje){
        cajaDatos.gameObject.SetActive(true);

        pjDatosActual = personaje;

        Character c = personaje.GetComponent<Character>();

        cajaDatos.Find("Nombre").GetComponent<Text>().text = c.nombre;
        cajaDatos.Find("HP").GetComponent<Text>().text = c.hp.ToString() + " HP";
        cajaDatos.Find("Nivel").GetComponent<Text>().text = "Lvl "+c.level.ToString();
        cajaDatos.Find("Estado").GetComponent<Text>().text = "Ningun estado alterado\n (TEXTO DEBUG)";

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

}
