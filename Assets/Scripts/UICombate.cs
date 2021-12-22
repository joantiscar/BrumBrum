using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class UICombate : MonoBehaviour
{
    private Image[] imgs;
    private Color transparente = new Color(0,0,0,0);
    private Color opaco = Color.white;
    private Habilidad[] _habilidades;
    private Transform cajaHabilidad;
    public int habilidadSeleccionada = -1;
    Character pjActual;

    void Awake()
    {
        imgs = GetComponentsInChildren<Image>();
        cajaHabilidad = transform.parent.Find("CajaSkill");
    }

    public void cambiaImagenes(Habilidad[] habilidades,Character pj){
        // Cambia las imagenes de la barra de abajo por las del parametro habilidades
        foreach (Image img in imgs)
        {
            img.color = opaco;
        }
        int i;
        for(i=0;i<habilidades.Length;i++)
        {
            string nombre = habilidades[i].name;
            // Usamos el nombre de la habilidad para saber qué imagen usar
            // EL PATH TIENE QUE ESTAR EN RESOURCES/...
            imgs[i].sprite = Resources.Load<Sprite>(nombre);
        }
        while(i<imgs.Length){
            imgs[i].color = transparente;
            i++;
        }
        _habilidades = habilidades;
        pjActual = pj;
    }

    public void muestraDescripcion(int pos){
        // Cuando se hace hover sobre una imagen, nos llega la pos en habilidades de la imagen
        if(pos<_habilidades.Length){
            habilidadSeleccionada = pos;
            cajaHabilidad.gameObject.SetActive(true);
            cajaHabilidad.Find("Nombre").GetComponent<Text>().text = _habilidades[pos].name;
            cajaHabilidad.Find("Dano").GetComponent<Text>().text = _habilidades[pos].damage.ToString();
            cajaHabilidad.Find("Descripcion").GetComponent<Text>().text = _habilidades[pos].description;
        }
    }

    public void escondeCaja(){
        //habilidadSeleccionada = -1;
        cajaHabilidad.gameObject.SetActive(false);
    }

    public void seleccionarHabilidad(int h){
        // HACER QUE HAYA UN POCO DE FEEDBACK EN LA UI
        if(h<_habilidades.Length){
            Debug.Log("Habilidad cambiada a: "+_habilidades[h].name);
            pjActual.habilidadSeleccionada = h;
        }
    }

}
