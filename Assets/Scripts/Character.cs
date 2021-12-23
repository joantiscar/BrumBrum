using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Character : MonoBehaviour
{
    public const int LEVEL_MAX = 99;
    public const int PLUS_EXPERIENCE = 4;
    public const int INICIAL_MAX_EXPERIENCE = 10;

    public bool user_controlled = false;

    public int hp;
    public int hpMax;
    public int attack;
    public int special_attack;
    public int defense;
	public int special_defense;
	public int velocity;
	
    public string nombre = "PJ";

    public string elemental_resistance;
    public int level;
    public int experience;
    public int experience_max;
    public int upgrade_points = 0;
    public GameObject objetivo;

    public double metrosMaximos = 10.0f;
    public double metrosRestantes = 10.0f;

    // Puntos para usar una habilidad: a los actuales se le suma base cada turno hasta que llegue a max
    public int habilidadSeleccionada = 0;
    public int basePAtaques = 5;
    public int maxPAtaques = 10;
    public int actPAtaques = 0;

    public int exp_when_killed = 100;

    public Habilidad[] habilidadesDisponibles;

    public int[] cooldowns;

    private UICombate UICombate;
    public SistemaCombate SistemaCombate;

    private Animator anim;

    void Start(){
        // TEST. En un futuro, constructor o algo
        cooldowns = new int[] { 0, 0, 0 };
        habilidadesDisponibles = new Habilidad[] {
            Habilidades.BolaDeFuego,
            Habilidades.AtaqueConEspada,
            Habilidades.EsquirlaDeHielo
        };

        UICombate = GameObject.Find("SkillsImages").GetComponent<UICombate>();
        anim = GetComponentInChildren<Animator>();

        hp = hpMax;
    }

    public void EmpiezaTurno(){
        // Al empezar el turno reseteamos los metros, restamos 1 a los cooldowns y añadimos los puntos base a los actuales
        metrosRestantes = metrosMaximos;

        for(int i = 0; i < cooldowns.Length; i++){
            if (cooldowns[i] > 0) cooldowns[i]--;
        }

        actPAtaques += basePAtaques;
        if(actPAtaques>maxPAtaques) actPAtaques=maxPAtaques;

        if(user_controlled){
            // Cambiamos todas las imagenes de las habilidades para adaptarse al personaje
            // Además también actualiza la barra de distancia
            UICombate.adaptaUI(habilidadesDisponibles,this);
            objetivo = null;
        }
        else{
            this.transform.GetComponent<IA>().HacerTurno();
        }
        
        
    }
    
    public void TerminaTurno(){
        if (!user_controlled){
            SistemaCombate.FinalizaTurno();
        }
    }

    public bool Moverse(double distancia){
        // Devuelve true si puede moverse la distancia que se puede y la resta de los metros restantes
        bool puedeMoverse = distancia <= metrosRestantes;
        if (puedeMoverse){
            //anim.SetFloat("Velocity", 1);
            metrosRestantes -= distancia;
            metrosRestantes = Math.Round(metrosRestantes, 1);
        }
        return puedeMoverse;
    }

    

    IEnumerator RutinaAtacar(Habilidad habilidad, Character a){
        // Lanzamos la habilidad y la ponemos en cooldown
        // Pero antes hacemos la animacion y esperamos a que termine
        anim.Play("Attack");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);

        
    }

    public void Atacar(){
        // Hay que mirar como hacer los hechizos de area (si los metemos)
        Habilidad habilidad = habilidadesDisponibles[habilidadSeleccionada];
        if (objetivo!=null && actPAtaques>=habilidad.coste){
            if (this.cooldowns[habilidadSeleccionada] == 0){ // Si la habilidad esta disponible...
                Character a = objetivo.GetComponent<Character>();
                Debug.Log("OBjetivo a atacar: " + objetivo.GetComponent<Character>().nombre);
                // Miramos si estamos a rango de la habilidad
                if (Vector3.Distance(this.transform.position, objetivo.transform.position) <= habilidad.range){
                    StartCoroutine(RutinaAtacar(habilidad, a));
                    Habilidades.lanzar(this, a, habilidad);
                    cooldowns[habilidadSeleccionada] += habilidad.cooldown;
                    Debug.Log("Lanzando habilidad " + habilidad.name);
                    
                    // Restamos los puntos que se usan
                    actPAtaques -= habilidad.coste;

                    anim.Play("Idle");
                }else{
                    Debug.Log(habilidad.name + " fuera de rango");
                }
            }else{
                Debug.Log("Habilidad en enfriamiento");
            }
        }
        else{
            Debug.Log("No has seleccionado un enemigo o no tienes puntos de ataque!");

        }
        

    }


    public Character(int hp, int attack, int special_attack, int defense, int special_defense, int velocity, int level=1, string elemental_resistance="none")
    {
        this.hp = hp;
        this.attack = attack;
        this.special_attack = special_attack;
        this.defense = defense;
        this.special_defense = special_defense;
        this.velocity = velocity;
        this.level = level;
        this.elemental_resistance = elemental_resistance;

        this.experience = INICIAL_MAX_EXPERIENCE * (level - 1);
        for (int i = 2; i < level; i++)
            this.experience += PLUS_EXPERIENCE * (i - 1);

    }


    public bool takeHit(int damage, string element)
    {
        if (defense < damage) damage -= defense;
        if (elemental_resistance == element) damage /= 2;
        Debug.Log(damage);

        hp -= damage;
        if (hp <= 0){
            morir();
        }
        return true;
    }
    public bool takeHitSpecial(int damage, string element)
    {
        if (special_defense < damage) damage -= special_defense;
        if (elemental_resistance == element) damage /= 2;
        hp -= damage;
        if (hp <= 0){
            morir();
        }
        return true;
    }


    public void morir(){
        // Animacion porfi
        Debug.Log(nombre + ": Aaaaaa que me muero.");
        if(user_controlled) SistemaCombate.nAliados--;
        else SistemaCombate.nEnemigos--;
        SistemaCombate.compruebaVictoria();
        Destroy(this.gameObject);
    }

    public void take_xp(int xp)
    {
        experience += xp;
        while (level < LEVEL_MAX && experience >= experience_max) level_up();
        // Debug.Log("Level: " + level);
    }

    public void level_up()
    {
        level++;
        // experience -= experience_max;
        experience_max = experience_max + INICIAL_MAX_EXPERIENCE + PLUS_EXPERIENCE * (level-1);
        upgrade_points += 5;
    }

    public void use_upgrade_point(string up_stat)
    {
        // Debug.Log("upgrade_points: " + upgrade_points);
        if (upgrade_points <= 0) Debug.Log("not enough upgrade points");
        else
        {
            switch(up_stat){
                case "hp":
                    hpMax += 5;
                    break;
                case "attack":
                    attack += 5;
                    break;
                case "special_attack":
                    special_attack += 5;
                    break;
                case "defense":
                    defense += 5;
                    break;
                case "special_defense":
                    special_defense += 5;
                    break;
                case "velocity":
                    velocity += 5;
                    break;
                default:
                    hp += 5;
                    break;
            }
            upgrade_points--;
            

            Debug.Log("points used on " + up_stat);
        }
    }


}
