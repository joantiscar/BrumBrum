using UnityEngine;


public class Singleton {

    private static Singleton _instance;
    public bool _menu = false;
    public Character[] pjs;
    public int pocions = 10;

    private GameObject guarrada0 = new GameObject();
    private GameObject guarrada1 = new GameObject();
    private GameObject guarrada2 = new GameObject();
    private GameObject guarrada3 = new GameObject();
    private GameObject guarrada4 = new GameObject();

    public static Singleton instance (){
        if (_instance == null){
            _instance = new Singleton();
            _instance.iniciarPjs();

        }
        return _instance;
    }

    public static void toggleMenu (){
        instance()._menu = !instance()._menu;
    }

    public static bool menu (){
        return instance()._menu;
    }
    
    public static void afegirPocions (int n){
        instance().pocions += n;
    }

    public static void restarPocions (int n){
        instance().pocions--;
    }
    
    public static void guanyarExp (int n){
        for (int i = 0; i < instance().pjs.Length; i++){
            instance().pjs[i].take_xp(n);
        }
    }
    
    public void iniciarPjs(){
        pjs = new Character[5];

        // Guerrero
        guarrada0.AddComponent<Character>();
        Character pj0 = guarrada0.GetComponent<Character>();
        pj0.nombre = "Luchador";
        pj0.className = "Luchador";
        pj0.level = 1;
        pj0.attack = 20;
        pj0.special_attack = 5;
        pj0.defense = 20;
        pj0.special_defense = 5;
        pj0.velocity = 5;
        pj0.hpMax = 70;
        pj0.elemental_resistance = "fire";
        pj0.user_controlled = true;

        // Pala
        guarrada1.AddComponent<Character>();
        Character pj1 = guarrada1.GetComponent<Character>();
        pj1.nombre = "Paladin";
        pj1.className = "Paladin";
        pj1.level = 1;
        pj1.attack = 10;
        pj1.special_attack = 10;
        pj1.defense = 12;
        pj1.special_defense = 12;
        pj1.velocity = 10;
        pj1.hpMax = 50;
        pj1.elemental_resistance = "physical";
        pj1.user_controlled = true;

        // Mago
        guarrada2.AddComponent<Character>();
        Character pj2 = guarrada2.GetComponent<Character>();
        pj2.nombre = "Mago";
        pj2.className = "Mago";
        pj1.level = 1;
        pj1.attack = 7;
        pj1.special_attack = 15;
        pj1.defense = 7;
        pj1.special_defense = 15;
        pj1.velocity = 13;
        pj1.hpMax = 40;
        pj2.elemental_resistance = "ice";
        pj2.user_controlled = true;

        // Curandero
        guarrada3.AddComponent<Character>();
        Character pj3 = guarrada3.GetComponent<Character>();
        pj3.nombre = "Curandero";
        pj3.className = "Curandero";
        pj1.level = 1;
        pj1.attack = 5;
        pj1.special_attack = 20;
        pj1.defense = 5;
        pj1.special_defense = 20;
        pj1.velocity = 20;
        pj1.hpMax = 35;
        pj3.elemental_resistance = "holy";
        pj3.user_controlled = true;

        // Guerrero
        guarrada4.AddComponent<Character>();
        Character pj4 = guarrada4.GetComponent<Character>();
        pj4.nombre = "Guerrero";
        pj4.className = "Guerrero";
        pj1.level = 1;
        pj1.attack = 15;
        pj1.special_attack = 8;
        pj1.defense = 15;
        pj1.special_defense = 8;
        pj1.velocity = 7;
        pj1.hpMax = 60;
        pj4.elemental_resistance = "physical";
        pj4.user_controlled = true;

        pjs[0] = pj0;
        pjs[1] = pj1;
        pjs[2] = pj2;
        pjs[3] = pj3;
        pjs[4] = pj4;
    }
}
