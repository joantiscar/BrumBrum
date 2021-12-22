using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class IA : MonoBehaviour
{
    public Character Personaje;
    public Character Objetivo;
    Random r = new Random();
    List<int> habilidadesUsables;



    public void DefinirObjetivo(){
        Objetivo = NULL;
        for(int i = 0; i < Personaje.SistemaCombate.pjs.length; i++){
                if (Personaje.SistemaCombate.pjs[i].user_controlled){
                    if (Objetivo == NULL) Objetivo = Personaje.SistemaCombate.pjs[i];
                    else if (distance(Personaje.transform.parent.gameObject, Personaje.SistemaCombate.pjs[i]).transform.parent.gameObject
                    < distance(Personaje.transform.parent.gameObject, Objetivo.transform.parent.gameObject)) Objetivo = Personaje.SistemaCombate.pjs[i];
                }
        }
    }

    public void Moverse(){
        float distancia = Personaje.metrosRestantes;
        if (distancia >= 1){
             if (distance(Personaje.transform.parent.gameObject, Objetivo.transform.parent.gameObject) < distancia){
            distancia = distance(Personaje.transform.parent.gameObject, Objetivo.transform.parent.gameObject) - 1;
            }
            Vector3 destino = MoveTowards(Personaje.transform.position, Objetivo.transform.position, distancia);
            Personaje.transform.parent.gameObject.GetComponent<NavMeshAgent>().destination = distancia;
        }
       
    }

    public bool PuedeAtacar(){
        habilidadesUsables = new List<Habilidad>();
        
        for(int i = 0; i < Personaje.habilidadesDisponibles.Length; i++){
            if (Perosnaje.cooldowns[i] == 0 && Personaje.habilidadesDisponibles[i].coste <= Personaje.actPAtaquesç
            && distance(Personaje.transform.parent.gameObject, Objetivo.transform.parent.gameObject) <= Personaje.habilidadesDisponibles[i].range){
                habilidadesUsables.Add(i);
            }
        }
        return habilidadesUsables.Count > 0;
    }

    public void Atacar(){
        while(PuedeAtacar()){
            Personaje.habilidadSeleccionada = habilidadesUsables[r.Next(habilidadesUsables.Count)];
            Personaje.Atacar();
            DefinirObjetivo();
            Moverse();
        }
    }
}