using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidades                                                                                                                                    // 0  true    false  false    true
{                                  
    // Base                                                                                                                             // cooldown coste damage range radius damages heals special targetEnemy
    public static Habilidad AtaqueConEspada = new Habilidad("Ataque con espada", "Ataque con espada", "physical", 0, 2, 10, 8.0f);
    public static Habilidad Pocion = new Habilidad("Pocion", "Bebe una poción para recuperar salud", "none", 0, 1, 10, 0.0001f, 0.0f, false, true, true, false);

    // Paladin
    public static Habilidad Proteger = new Habilidad("Proteger", "Prepara su escudo para defenderse de ataques enemigos", "none", 4, 2, 0, 0.001f, 0.0f, false, false, true, false);
    public static Habilidad Rezar = new Habilidad("Rezar", "Realiza una plegaria para recuperar salud", "none", 3, 2, 40, 0.0001f, 0.0f, false, true, true, false);
    public static Habilidad Devocion = new Habilidad("Devoción", "Reza a su dios para mejorar las estadísticas de un aliado", "none", 4, 2, 0, 15.0f, 0.0f, false, false, false, false);
    public static Habilidad GolpeDeEscudo = new Habilidad("Golpe de escudo", "Golpea a un enemigo con su escudo para aturdirlo", "physical", 3, 3, 10, 10f);
    public static Habilidad CastigoDivino = new Habilidad("Castigo divino", "Canaliza la energía de su divinidad en su arma para hacer arder a sus enemigos", "physical", 4, 4, 80, 15f);
    
    // Mago                                                                                         
    public static Habilidad BolaDeFuego = new Habilidad("Bola de fuego", "Lanza una bola de fuego hacia su oponente", "fire", 4, 4, 20, 15f, 3f, true, false, true);
    public static Habilidad EsquirlaDeHielo = new Habilidad("Esquirla de hielo", "Lanza una esquirla de hielo hacia su oponente", "ice", 2, 2, 10, 20f, 0.0f, true, false, true);
    public static Habilidad ColmilloVenenoso = new Habilidad("Colmillo venenoso", "Lanza un proyectil venenoso hacia su oponente", "poison", 2, 2 , 10, 15.0f, 0.0f);
    public static Habilidad AreaDeProteccionMagica = new Habilidad("Aura de protección mágica", "Mejora la defensa especial de todos los aliados en un área", "none", 4, 2, 0, 15f, 4f, false, false, false, false);
    public static Habilidad Meteorito = new Habilidad("Meteorito", "Hace que caiga un meteorito del cielo sobre sus rivales", "fire", 6, 4, 100, 10f, 4f, true, false, true);

    // Curandero
    public static Habilidad Curacion = new Habilidad("Curación", "Canaliza energía divina para sanar a un aliado", "none", 2, 2 , 30, 20f, 0.0f, false, true, true, false);
    public static Habilidad Purificacion = new Habilidad("Purificación", "Elimina los estados alterados de un aliado", "none", 3, 2 , 0, 20f, 0.0f, false, false, false,false);
    public static Habilidad Bendicion = new Habilidad("Bendición", "Da a un aliado la bendición de los dioses, mejorando todas sus estadísticas", "none", 3, 2 , 0, 20f, 0.0f, false, false, false, false);
    public static Habilidad AuraDeCuracion = new Habilidad("Aura de curación", "Canaliza energía divina para sanar a todos los aliados en un área", "none", 3, 4 , 20, 15f, 4f, false, true, true, false);
    public static Habilidad Renacer = new Habilidad("Renacer", "Provoca el renacimiento de un aliado, devolviendo asi su salud al maximo y curando todos sus estados alterados", "none", 4, 8 , 999999, 10f, 0.0f, false, true, true, false);

    // Guerrero
    public static Habilidad TajoCruzado = new Habilidad("Tajo cruzado", "Ataque perpendicular con dos espadas", "physical", 2, 2, 40, 1.75f);
    public static Habilidad FragorDeLaBatalla = new Habilidad("Fragor de la batalla", "Aprecia la grandiosidad del combate que está por venir y mejora sus estadísticas ofensivas", "none", 2, 2, 0, 0.001f, 0.0f, false, false, false, false);
    public static Habilidad Remolino = new Habilidad("Remolino", "Beyblade Beyblade!", "physical", 3, 4, 50, 0.001f, 1.75f, true, false, false, false);
    public static Habilidad Furia = new Habilidad("Furia", "Entra en furia, aumentando el daño pero disminuyendo las defensas", "none", 4, 2, 0, 0.001f, 0.0f, false, false, false, false);
    public static Habilidad Masacre = new Habilidad("Masacre", "Ataca a bocajarro a un enemigo hasta quedarse sin energías, inflingiendo muchisimo daño", "none", 4, 8, 200, 1.75f, 0.0f, true, false, false, false);

    // Luchador
    public static Habilidad Punetazo = new Habilidad("Puñetazo", "Ataque con los puños", "physical", 0, 2, 10, 1.3f);
    public static Habilidad Partenueces = new Habilidad("Partenueces", "Ataque a las partes del contrincante", "physical", 2, 2, 30, 1.3f, 0.0f, true, false, false, true);
    public static Habilidad LanzamientoDeRoca = new Habilidad("Lanzamiento de roca", "Lanza una roca enorme hacia un enemigo", "physical", 3, 3, 40, 2.0f, 0.0f, true, false, false, true);
    public static Habilidad PalmadaSonica = new Habilidad("Palmada sonica", "Da una palmada de fuerza incalculable, emanando potentes ondas de sonido que dañan a los enemigos", "sound", 3, 3, 80, 0.001f, 1.75f, true, false, false, true);
    public static Habilidad Terremoto = new Habilidad("Terremoto", "Pisa el suelo con toda su fuerza, creando una onda expansiva que daña a los enemigos encima de este", "physical", 4, 4, 50, 1.0f, 2.0f, true, false, false, true);
    public static Habilidad PunetazoDeUnaPulgada = new Habilidad("Puñetazo de una pulgada", "Lanza un potente puñetazo al pecho del rival, matándolo instantáneamente si no tiene suficiente salud", "none", 4, 8, 200, 1.0f, 0.0f, true, false, false, true);




   public static bool unaEntre(int max){
        
        return Random.Range(0, max - 1) == 0;
    }

    public static void lanzar(Character caster, Character objetivo, Habilidad habilidad){
        Character objetivoScript = objetivo.GetComponent<Character>();
        switch(habilidad.name){
            default:
                break;
        }

        if (habilidad.damages){
            bool hits;
            if (habilidad.special) hits = objetivo.takeHitSpecial(caster.ataqueEspecialActual() + habilidad.damage, habilidad.type);
            else hits = objetivo.takeHit(caster.ataqueActual() + habilidad.damage, habilidad.type);
            if (hits){
                switch(habilidad.name){
                    
                    case "Bola de fuego":
                        if (unaEntre(10)) objetivoScript.quemar();
                        break;
                    case "Esquirla de hielo":
                        if (unaEntre(10)) objetivoScript.congelar();
                        break;
                    case "Colmillo venenoso":
                        if (unaEntre(10)) objetivoScript.envenenar();
                        break;
                    case "Golpe de escudo":
                        objetivoScript.aturdir();
                        break;
                    case "Castigo divino":
                        if (unaEntre(2)) objetivoScript.quemar();
                        break;
                    case "Puñetazo de una pulgada":
                        if (objetivoScript.hp < (objetivoScript.hpMax * 0.3)) objetivo.takeHit(99999999, "true");
                        break;
                    default:
                        break;
                }
            }
        }else if (habilidad.heals){
            objetivoScript.recieveHeal(habilidad.damage + caster.ataqueEspecialActual());
            if (habilidad.name == "Renacer") objetivoScript.renacer();
        }else{
            switch(habilidad.name){
                case "Proteger":
                    objetivoScript.proteger();
                    break;
                case "Devoción":
                    objetivoScript.bendecir();
                    break;
                case "Fragor de la batalla":
                    objetivoScript.mejorarAtaque();
                    objetivoScript.mejorarAtaqueEspecial();
                    objetivoScript.mejorarVelocidad();
                    break;
                case "Furia":
                    objetivoScript.enfurecer();
                    break;
                case "Aura de protección mágica":
                    objetivoScript.mejorarDefensaEspecial();
                    break;
                default:
                        break;
            }
        }
    }
    
}
