using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidades                                                                                                                                    // 0  true    false  false
{                                                                                                                           // cooldown coste damage range radius damages heals special
    public static Habilidad BolaDeFuego = new Habilidad("Bola de fuego", "El usuario lanza una bola de fuego hacia su oponente", "fire", 4, 5, 20, 2, 0.75f, true, false, true);
    public static Habilidad EsquirlaDeHielo = new Habilidad("Esquirla de hielo", "El usuario lanza una esquirla de hielo hacia su oponente", "ice", 2, 2, 10, 1.0f, 0.0f, true, false, true);
    public static Habilidad AtaqueConEspada = new Habilidad("Ataque con espada", "Ataque con espada", "physical", 0, 1, 10, 0.25f);
    public static Habilidad ColmilloVenenoso = new Habilidad("Colmillo venenoso", "El usuario lanza un proyecitl venenoso hacia su oponente", "poison", 2, 2 , 10, 0.75f, 0.0f);
    public static Habilidad Curacion = new Habilidad("Curacion", "El usuario canaliza energia divina para sanar a un aliado", "holy", 2, 2 , 30, 1.0f, 0.0f, false, true, true,false);
    


    public static void lanzar(Character caster, Character objetivo, Habilidad habilidad){
        switch(habilidad.name){
            default:
                break;
        }

        if (habilidad.damages){
            bool hits;
            if (habilidad.special) hits = objetivo.takeHitSpecial(caster.ataqueEspecialActual() + habilidad.damage, habilidad.type);
            else hits = objetivo.takeHit(caster.ataqueActual() + habilidad.damage, habilidad.type);
            if (hits){
                int number;
                switch(habilidad.name){
                    
                    case "Bola de fuego":
                        number = Random.Range(0, 10);
                        if (number == 9) objetivo.quemar();
                        break;
                    case "Esquirla de hielo":
                        number = Random.Range(0, 10);
                        if (number == 9) objetivo.congelar();
                        break;
                    case "Colmillo venenoso":
                        number = Random.Range(0, 10);
                        if (number == 9) objetivo.envenenar();
                        break;
                    default:
                        break;
                }
            }
        }else if (habilidad.heals){
            objetivo.GetComponent<Character>().recieveHeal(habilidad.damage + caster.ataqueEspecialActual());
        }
    }
    public static void lanzarAOE(Character caster, Vector3 center, Habilidad habilidad){
        Collider[] hitColliders = Physics.OverlapSphere(center, habilidad.radius);
        foreach (var hitCollider in hitColliders)
        {
            Debug.Log(hitCollider.gameObject.GetComponent<Character>().nombre);
        }
    }
}
