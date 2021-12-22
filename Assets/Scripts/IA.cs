using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using Random=System.Random;
public class IA : MonoBehaviour
{
    public Character Personaje;
    public Character Objetivo;
    Random r = new Random();
    List<int> habilidadesUsables;
    public String estado = "esperando";
    Vector3 destino;

    public void Start(){
        Personaje = this.transform.GetComponent<Character>();
    }


    public void DefinirObjetivo(){
        Vector3 posActual = Personaje.transform.transform.position;
        for(int i = 0; i < Personaje.SistemaCombate.pjs.Length; i++){
                if (Personaje.SistemaCombate.pjs[i].GetComponent<Character>().user_controlled){
                    Vector3 posCandidato = Personaje.SistemaCombate.pjs[i].transform.position;
                    Vector3 posObjetivo = Personaje.SistemaCombate.pjs[i].transform.position;
                    if (Objetivo == null) Objetivo = Personaje.SistemaCombate.pjs[i].GetComponent<Character>();
                    else if (Vector3.Distance(posActual, posCandidato) < Vector3.Distance(posActual, posObjetivo)) Objetivo = Personaje.SistemaCombate.pjs[i].GetComponent<Character>();
                }
        }
    }

    public void Moverse(){
        double distancia = Personaje.metrosRestantes;
        if (distancia >= 1){
            Vector3 posActual = Personaje.transform.position;
            Vector3 posObjetivo = Objetivo.transform.position;
            if (Vector3.Distance(posActual, posObjetivo) < distancia){
                distancia = Vector3.Distance(posActual, posObjetivo) - 1;
            }
            destino = Vector3.MoveTowards(posActual, posObjetivo, (float) distancia);
            Personaje.transform.gameObject.GetComponent<NavMeshAgent>().destination = destino;
            estado = "moviendose";
        }
        estado = "preparado";
       
    }

    public bool PuedeAtacar(){
        habilidadesUsables = new List<int>();
        for(int i = 0; i < Personaje.habilidadesDisponibles.Length; i++){
            if (Personaje.cooldowns[i] == 0 && Personaje.habilidadesDisponibles[i].coste <= Personaje.actPAtaques
            && Vector3.Distance(Personaje.transform.position, Objetivo.transform.position) <= Personaje.habilidadesDisponibles[i].range){
                habilidadesUsables.Add(i);
            }
        }
        return habilidadesUsables.Count > 0;
    }

    public void Atacar(){
        // while(PuedeAtacar()){
        //     Personaje.habilidadSeleccionada = habilidadesUsables[r.Next(habilidadesUsables.Count)];
        //     Personaje.Atacar();
        // }
        estado = "terminado";
    }
    public void HacerTurno(){
        estado = "empiezaTurno";
    }
    public void Update(){
        switch (estado){
            case "empiezaTurno":
                DefinirObjetivo();
                Moverse();
                break;
            case "moviendose":
                if (Personaje.transform.position == destino){
                    estado = "preparado";
                }
                break;
            case "preparado":
                Atacar();
                break;
            case "terminado":
                Personaje.TerminaTurno();
                estado = "esperando";
                break;
            case "atacando":
            case "esperando":
                break;
            default:
                Debug.Log(estado);
                break;

        }
    }
}
