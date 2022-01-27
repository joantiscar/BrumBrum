using UnityEngine;


public class Singleton {

    private static Singleton _instance;
    public bool _menu = false;
    public bool _levelUpPanel = false;
    public Character[] pjs;
    public int pocions = 10;
    public static bool enCombate = false;
    public float _volume = 1;
    public float _sensitivity = 0.5f;
    public string _currentScene;
    public bool _godMode = false;
    public bool dialegsIniciats = false;
    public int _nEnemigos = 0;

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
    

    public static void setLevelupMenu (bool status){
        instance()._levelUpPanel = status;
    }

    public static bool LevelupPanel (){
        return instance()._levelUpPanel;
    }

    public static bool menu (){
        return instance()._menu;
    }

    public static int nPocions(){
        return instance().pocions;
    }
    
    public static void afegirPocions (int n){
        instance().pocions += n;
    }

    public static void restarPocio (){
        instance().pocions--;
    }

    public static void randomEnemigos (int n){
        instance()._nEnemigos = n;
    }

    public static int getEnemigos (){
        return instance()._nEnemigos;
    }
    
    public static void guanyarExp (int n){
        for (int i = 0; i < instance().pjs.Length; i++){
            instance().pjs[i].take_xp(n);
        }
    }

    public static void setVolume(float v)
    {
        instance()._volume = v;
    }

    public static float volume()
    {
        return instance()._volume;
    }

    public static void setSensitivity(float s)
    {
        instance()._sensitivity = s;
    }

    public static float sensitivity()
    {
        return instance()._sensitivity;
    }

    public static void setCurrentScene(string c)
    {
        instance()._currentScene = c;
    }

    public static string currentScene()
    {
        return instance()._currentScene;
    }


    public void iniciarPjs(){
        pjs = new Character[5];

        if(guarrada0 == null) guarrada0 = new GameObject();
        guarrada0.AddComponent<Character>();
        Character pj0 = guarrada0.GetComponent<Character>();
        if(guarrada1 == null) guarrada1 = new GameObject();
        guarrada1.AddComponent<Character>();
        Character pj1 = guarrada1.GetComponent<Character>();
        if(guarrada2 == null) guarrada2 = new GameObject();
        guarrada2.AddComponent<Character>();
        Character pj2 = guarrada2.GetComponent<Character>();
        if(guarrada3 == null) guarrada3 = new GameObject();
        guarrada3.AddComponent<Character>();
        Character pj3 = guarrada3.GetComponent<Character>();
        if(guarrada4 == null) guarrada4 = new GameObject();
        guarrada4.AddComponent<Character>();
        Character pj4 = guarrada4.GetComponent<Character>();

        if (instance()._godMode){
            // Luchador
            pj0.nombre = "Herc";
            pj0.className = "Luchador";
            pj0.level = 99;
            pj0.attack = 1000;
            pj0.special_attack = 1000;
            pj0.defense = 1000;
            pj0.special_defense = 1000;
            pj0.velocity = 1000;
            pj0.hpMax = 1000;
            pj0.elemental_resistance = "fire";
            pj0.user_controlled = true;

            // Pala
            pj1.nombre = "Irix";
            pj1.className = "Paladin";
            pj1.level = 99;
            pj1.attack = 1000;
            pj1.special_attack = 1000;
            pj1.defense = 1000;
            pj1.special_defense = 1000;
            pj1.velocity = 1000;
            pj1.hpMax = 1000;
            pj1.elemental_resistance = "physical";
            pj1.user_controlled = true;

            // Mago
            pj2.nombre = "Áine";
            pj2.className = "Mago";
            pj2.level = 99;
            pj2.attack = 1000;
            pj2.special_attack = 1000;
            pj2.defense = 1000;
            pj2.special_defense = 1000;
            pj2.velocity = 1000;
            pj2.hpMax = 1000;
            pj2.elemental_resistance = "ice";
            pj2.user_controlled = true;

            // Curandero
            pj3.nombre = "Hestia";
            pj3.className = "Curandero";
            pj3.level = 99;
            pj3.attack = 1000;
            pj3.special_attack = 1000;
            pj3.defense = 1000;
            pj3.special_defense = 1000;
            pj3.velocity = 1000;
            pj3.hpMax = 1000;
            pj3.elemental_resistance = "holy";
            pj3.user_controlled = true;

            // Guerrero
            pj4.nombre = "Cyrus";
            pj4.className = "Guerrero";
            pj4.level = 99;
            pj4.attack = 1000;
            pj4.special_attack = 1000;
            pj4.defense = 1000;
            pj4.special_defense = 1000;
            pj4.velocity = 1000;
            pj4.hpMax = 1000;
            pj4.elemental_resistance = "physical";
            pj4.user_controlled = true;
        }
        else{
            // Luchador
            pj0.nombre = "Herc";
            pj0.className = "Luchador";
            pj0.level = 10;
            pj0.attack = 20;
            pj0.special_attack = 5;
            pj0.defense = 20;
            pj0.special_defense = 5;
            pj0.velocity = 5;
            pj0.hpMax = 70;
            pj0.elemental_resistance = "fire";
            pj0.user_controlled = true;
            pj0.upgrade_points = 5;

            // Pala
            pj1.nombre = "Irix";
            pj1.className = "Paladin";
            pj1.level = 10;
            pj1.attack = 10;
            pj1.special_attack = 10;
            pj1.defense = 12;
            pj1.special_defense = 12;
            pj1.velocity = 10;
            pj1.hpMax = 50;
            pj1.elemental_resistance = "physical";
            pj1.user_controlled = true;
            pj1.upgrade_points = 5;


            // Mago
            pj2.nombre = "Áine";
            pj2.className = "Mago";
            pj2.level = 10;
            pj2.attack = 7;
            pj2.special_attack = 15;
            pj2.defense = 7;
            pj2.special_defense = 15;
            pj2.velocity = 15;
            pj2.hpMax = 40;
            pj2.elemental_resistance = "ice";
            pj2.user_controlled = true;
            pj2.upgrade_points = 5;


            // Curandero
            pj3.nombre = "Hestia";
            pj3.className = "Curandero";
            pj3.level = 10;
            pj3.attack = 5;
            pj3.special_attack = 20;
            pj3.defense = 5;
            pj3.special_defense = 20;
            pj3.velocity = 20;
            pj3.hpMax = 35;
            pj3.elemental_resistance = "holy";
            pj3.user_controlled = true;
            pj3.upgrade_points = 5;


            // Guerrero
            pj4.nombre = "Cyrus";
            pj4.className = "Guerrero";
            pj4.level = 10;
            pj4.attack = 15;
            pj4.special_attack = 8;
            pj4.defense = 15;
            pj4.special_defense = 8;
            pj4.velocity = 7;
            pj4.hpMax = 60;
            pj4.elemental_resistance = "physical";
            pj4.user_controlled = true;
            pj4.upgrade_points = 5;

        }

        pjs[0] = pj0;
        pjs[1] = pj1;
        pjs[2] = pj2;
        pjs[3] = pj3;
        pjs[4] = pj4;
    }

    public static void reset(){
        instance().iniciarPjs();
        instance().pocions = 10;
    }
    public static void ActivateGodMode(){
        instance()._godMode = true;
        Debug.Log ("godMode active");
    }
    public static void toggleDialegs (){
        instance().dialegsIniciats = !instance().dialegsIniciats;
    }
    public static bool dialegs (){
        return instance().dialegsIniciats;
    }
}
