using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class UICombate : MonoBehaviour
{
    private Image[] imgs; // Las imagenes donde se ponen los sprites de las skills

    private Color transparente = new Color(0,0,0,0);
    private Color opaco = Color.white;

    private List<Habilidad> _habilidades; // Una array con las habilidades actuales
    
    public Transform cajaHabilidad;
    public Slider barraDistancia;
    public Slider barraHP;
    public Text LabelPP;
    public Text TextDebug;

    public GameObject selected;

    public int habilidadSeleccionada = -1;
    Character pjActual;

    public SistemaCombate SistemaCombate;

    void Awake()
    {
        imgs = GetComponentsInChildren<Image>();

    }

    public void adaptaUI(List<Habilidad> habilidades,Character pj){ //Llamada cada vez que empieza el turno de un personaje
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

        Character pjScript = pjActual.GetComponent<Character>();

        // Actualiza la barra con el maximo de metros
        barraDistancia.maxValue = (float) pjScript.metrosRestantes;
        barraDistancia.value = barraDistancia.maxValue;

        // Actualiza la barra con el HP
        barraHP.maxValue = (float) pjScript.hpMax;
        barraHP.value = (float) pjScript.hp;


        // Actualiza el label de PP
        LabelPP.text = pjActual.actPAtaques.ToString();
        
    }

    public void FinalizaTurno(){ // LLamada cada vez que acaba el turno de un personaje
        // Restart la habilidad seleccionada
        selected.SetActive(false);
        pjActual.habilidadSeleccionada = -1;
        habilidadSeleccionada = -1;
    }

    public void muestraDescripcion(int pos){
        // Cuando se hace hover sobre una imagen, nos llega la pos en habilidades de la imagen
        if(pos<_habilidades.Count){
            habilidadSeleccionada = pos;
            cajaHabilidad.gameObject.SetActive(true);
            cajaHabilidad.Find("Nombre").GetComponent<Text>().text = _habilidades[pos].name;
            cajaHabilidad.Find("Dano").GetComponent<Text>().text = _habilidades[pos].damage.ToString();
            cajaHabilidad.Find("Cooldown").GetComponent<Text>().text = pjActual.cooldowns[pos].ToString();
            cajaHabilidad.Find("nCost").GetComponent<Text>().text = _habilidades[pos].coste.ToString() + " PP";
            cajaHabilidad.Find("Descripcion").GetComponent<Text>().text = _habilidades[pos].description;
        }
    }

    public void escondeCaja(){
        cajaHabilidad.gameObject.SetActive(false);
    }

    public void seleccionarHabilidad(int h){
        if(h<_habilidades.Count && pjActual.cooldowns[h]==0 && !SistemaCombate.moviendose){

            Debug.Log("Habilidad cambiada a: "+_habilidades[h].name);
            TextDebug.text = "Habilidad cambiada a: "+_habilidades[h].name;

            pjActual.habilidadSeleccionada = h;

            selected.SetActive(true);
            selected.transform.position = imgs[h].transform.position;

            pjActual.destruirCirculoMov();
            pjActual.dibujaCirculoHab();

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
        // barraDistancia.value = (float) pjActual.GetComponent<Character>().metrosRestantes;
        pjActual.dibujaCirculoMov();
    }

}
