using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using Random = System.Random;
public class IA : MonoBehaviour
{
    public Character Personaje;
    public Character Objetivo;
    Random r = new Random();
    List<int> habilidadesUsables;
    public String estado = "esperando";
    Vector3 destino;

    private bool atacando = false;

    private Animator anim;

    public void Start()
    {
        Personaje = this.transform.GetComponent<Character>();
        anim = GetComponentInChildren<Animator>();
    }


    public void DefinirObjetivo()
    {
        Vector3 posActual = Personaje.transform.transform.position;
        for (int i = 0; i < Personaje.SistemaCombate.pjs.Count; i++)
        {
            if (Personaje.SistemaCombate.pjs[i].GetComponent<Character>().user_controlled)
            {
                Vector3 posCandidato = Personaje.SistemaCombate.pjs[i].transform.position;
                Vector3 posObjetivo = Personaje.SistemaCombate.pjs[i].transform.position;
                if (Objetivo == null) Objetivo = Personaje.SistemaCombate.pjs[i].GetComponent<Character>();
                else if (Vector3.Distance(posActual, posCandidato) < Vector3.Distance(posActual, posObjetivo)) Objetivo = Personaje.SistemaCombate.pjs[i].GetComponent<Character>();
            }
        }
    }

    public void Moverse()
    {
        double distancia = Personaje.metrosRestantes;
        if (distancia >= 1)
        {
            Vector3 posActual = Personaje.transform.position;
            Vector3 posObjetivo = Objetivo.transform.position;
            if (Vector3.Distance(posActual, posObjetivo) < distancia)
            {
                distancia = Vector3.Distance(posActual, posObjetivo) - Objetivo.gameObject.GetComponentInChildren<CapsuleCollider>().radius-0.1;
            }
            destino = Vector3.MoveTowards(posActual, posObjetivo, (float)distancia);
            this.gameObject.GetComponent<NavMeshAgent>().destination = destino;
            estado = "moviendose";
        }
        else
        {
            estado = "preparado";
        }

    }

    public bool PuedeAtacar()
    {
        if (Personaje.SistemaCombate.nAliados == 0) return false;
        habilidadesUsables = new List<int>();
        for (int i = 0; i < Personaje.habilidadesDisponibles.Count; i++)
        {
            if (Personaje.cooldowns[i] == 0 && Personaje.habilidadesDisponibles[i].coste <= Personaje.actPAtaques
            && Vector3.Distance(Personaje.transform.position, Objetivo.transform.position) <= Personaje.habilidadesDisponibles[i].range)
            {
                habilidadesUsables.Add(i);
            }
        }
        Debug.Log(habilidadesUsables.Count);
        return habilidadesUsables.Count > 0;
    }

    public void HacerTurno()
    {
        estado = "empiezaTurno";
    }

    IEnumerator RutinaAtacar(){
        atacando = true;
        yield return new WaitForSeconds(2);
        atacando = false;
        
    }

    public void Update()
    {
        if(!Personaje.SistemaCombate.gameover){

            if (estado != "esperando")
            {
                switch (estado)
                {
                    case "empiezaTurno":
                        DefinirObjetivo();
                        Moverse();
                        break;
                    case "moviendose":
                        anim.SetFloat("Velocity", 1);
                        if (Vector3.Distance(Personaje.transform.position, destino) < 1.5)
                        {
                            estado = "preparado";
                            anim.SetFloat("Velocity", 0);
                            this.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                            this.gameObject.GetComponent<NavMeshAgent>().isStopped = false;
                        }
                        break;
                    case "preparado":
                        if (PuedeAtacar())
                        {
                            Personaje.habilidadSeleccionada = habilidadesUsables[r.Next(habilidadesUsables.Count)];
                            // DE MOMENTO????
                            if(Personaje.habilidadesDisponibles[Personaje.habilidadSeleccionada].radius>0.0f){ // en area
                                Personaje.objetivos.Clear();
                                Personaje.objetivos.Add(Objetivo.gameObject);
                            }
                            else{ // normal
                                Personaje.objetivo = Objetivo.gameObject;

                            }
                            Personaje.Atacar();
                            Objetivo = null;
                            estado = "atacando";
                            DefinirObjetivo();
                            StartCoroutine(RutinaAtacar());
                        }
                        else estado = "terminado";
                        break;
                    case "terminado":
                        Personaje.UICombate.actualizaTurno(Personaje);
                        Personaje.TerminaTurno();
                        // En la ui cambiamos lo de los turnos jeje se me habia olvidado
                        estado = "esperando";
                        break;
                    case "atacando":
                        if(!atacando) estado = "preparado";
                        break;
                    case "esperando":
                        break;
                    default:
                        Debug.Log(estado);
                        break;

                }
               
            }
            //  Debug.Log(estado);
        }
    }
}
