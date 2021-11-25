using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Character : MonoBehaviour
{
    public const int LEVEL_MAX = 99;
    public const int PLUS_EXPERIENCE = 4;
    public const int INICIAL_MAX_EXPERIENCE = 10;

    public int hp;
    public int attack;
    public int special_attack;
    public int defense;
	public int special_defense;
	public int velocity;
		
    public string elemental_resistance;
    public int level;
    public int experience;
    public int experience_max;
    public int upgrade_points = 0;
    public GameObject objetivo;

    public double metrosMaximos = 10.0f;
    public double metrosRestantes = 10.0f;

    public Habilidad[] habilidadesDisponibles;
    int[] cooldowns;

    private ImgHabilidades imgs;

    void Start(){
        cooldowns = new int[] { 0, 0, 0 };
        habilidadesDisponibles = new Habilidad[] {
            Habilidades.BolaDeFuego,
            Habilidades.AtaqueConEspada,
            Habilidades.EsquirlaDeHielo
        };

        imgs = GameObject.Find("SkillsImages").GetComponent<ImgHabilidades>();
    }

    public void EmpiezaTurno(){
        imgs.cambiaImagenes(habilidadesDisponibles);
        
        metrosRestantes = metrosMaximos;

        for(int i = 0; i < cooldowns.Length; i++){
            if (cooldowns[i] > 0) cooldowns[i]--;
        }
        
    }
    
    public void TerminaTurno(){
    }

    public bool Moverse(double distancia){
        bool puedeMoverse = distancia <= metrosRestantes;
        if (puedeMoverse){
            metrosRestantes -= distancia;
            metrosRestantes = Math.Round(metrosRestantes, 1);
        }
       return puedeMoverse;
    }

    public void Atacar(){
        int habilidadSeleccionada = 0;
        if (this.cooldowns[habilidadSeleccionada] == 0){
            Character a = objetivo.GetComponent<Character>();
            if (Vector3.Distance(this.transform.position, objetivo.transform.position) <= habilidadesDisponibles[habilidadSeleccionada].range){
                Habilidades.lanzar(this, a, habilidadesDisponibles[habilidadSeleccionada]);
                cooldowns[habilidadSeleccionada] += habilidadesDisponibles[habilidadSeleccionada].cooldown;
                Debug.Log("Lanzando habilidad");
            }else{
                Debug.Log("Fuera de rango");
            }
            
        }else{
            Debug.Log("Skill en cooldown");
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



    public void is_hit(int enemy_attack)
    {
        string element = "fire";
        hit(enemy_attack, element);
    }

    public bool hit(int damage, string element)
    {
        if (defense < damage) damage -= defense;
        if (elemental_resistance == element) damage /= 2;
        hp -= damage;
        return true;
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
        if (upgrade_points <= 0) Debug.Log("not enogth upgrade points");
        else
        {
            switch(up_stat){
                case "hp":
                    hp += 5;
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
