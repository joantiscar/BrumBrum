using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class ImgHabilidades : MonoBehaviour
{
    private Image[] imgs;
    private Color transparente = new Color(0,0,0,0);
    private Color opaco = Color.white;

    void Awake()
    {
        imgs = GetComponentsInChildren<Image>();
    }

    public void cambiaImagenes(Habilidad[] habilidades){
        foreach (Image img in imgs)
        {
            img.color = opaco;
        }
        int i;
        for(i=0;i<habilidades.Length;i++)
        {
            string nombre = habilidades[i].name;
            imgs[i].sprite = Resources.Load<Sprite>(nombre);
        }
        while(i<imgs.Length){
            imgs[i].color = transparente;
            i++;
        }
    }

    void Update()
    {
        // LO DEL RATON
    }
}
