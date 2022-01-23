using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidad
{
    public string name;
    public string description;
    public string type;
    public int cooldown;
    public int coste;
    public int damage;
    public float range;
    public float radius = 0.0f;
    public bool damages = true;
    public bool heals = true;
    public bool targetEnemy = true;
    public bool special = false;
    public bool aoe = false;
    

    public Habilidad(string a_name, string a_description, string a_type, int a_cooldown, int a_coste, int a_damage, 
    float a_range, float a_radius = 0.0f, bool a_damages = true, bool a_heals = false, bool a_special = false, bool a_targetEnemy = true){
        name = a_name;
        description = a_description;
        type = a_type;
        cooldown = a_cooldown;
        coste = a_coste;
        damage = a_damage;
        range = a_range;
        radius = a_radius;
        damages = a_damages;
        heals = a_heals;
        special = a_special;
        targetEnemy = a_targetEnemy;
    }
}
