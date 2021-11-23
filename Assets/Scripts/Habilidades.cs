using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidades
{
    public static Habilidad BolaDeFuego = new Habilidad("Bola de fuego", "El usuario lanza una bola de fuego hacia su oponente", "fire", 3, 50, 5);
    public static Habilidad EsquirlaDeHielo = new Habilidad("Esquirla de hielo", "El usuario lanza una esquirla de hielo hacia su oponente", "ice", 3, 50, 5);
    public static Habilidad AtaqueConEspada = new Habilidad("Ataque con espada", "Ataque con espada", "physical", 3, 50, 5);
    public static Habilidad ColmilloVenenoso = new Habilidad("Colmillo venenoso", "El usuario lanza un proyecitl venenoso hacia su oponente", "poison", 3, 50, 5);


    
    public static void lanzar(Character caster, Character objetivo, Habilidad habilidad){
        switch(habilidad.name){
            default:
                break;
        }

        if (habilidad.damages){
            if (objetivo.hit(caster.special_attack + habilidad.damage, habilidad.type)){
                switch(habilidad.name){
                    case "Bola de fuego":
                        // TODO QUEMAR
                        break;
                    case "Esquirla de hielo":
                        // TODO CONGELAR
                        break;
                    case "Colmillo venenoso":
                        // TODO VENENO
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
