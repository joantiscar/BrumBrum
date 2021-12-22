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
    public int range;
    public bool damages = true;
    public bool special = false;
    public bool aoe = false;
    public double radius = 0.0f;

    public Habilidad(string a_name, string a_description, string a_type, int a_cooldown, int a_coste, int a_damage, int a_range, bool a_damages = true, bool a_special = false){
        name = a_name;
        description = a_description;
        type = a_type;
        cooldown = a_cooldown;
        coste = a_coste;
        damage = a_damage;
        range = a_range;
        damages = a_damages;
        special = a_special;
    }
}
