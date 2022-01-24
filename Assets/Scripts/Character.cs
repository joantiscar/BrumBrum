using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Character : MonoBehaviour
{
   
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


    // *********************************************
    // *            EXP                            *
    // *********************************************   
    public const int LEVEL_MAX = 99;
    public const int PLUS_EXPERIENCE = 4;
    public const int INICIAL_MAX_EXPERIENCE = 10;
    public int level = 20;
    public int experience;
    public int experience_max;
    public int upgrade_points = 0;



    // *********************************************
    // *            Stats                          *
    // *********************************************

    public int hpMax;
    public int attack;
    public int special_attack;
    public int defense;
	public int special_defense;
	public int velocity;
    public string nombre = "SinNombre";
    public string elemental_resistance;
    public double metrosMaximos = 10.0f;
    public GameObject objetivo;
    public string className = "Clase";
    private Clase clase;


    // *********************************************
    // *            STATE                          *
    // *********************************************

    
    public double metrosRestantes = 10.0f;
    public int hp;
    // Puntos para usar una habilidad: a los actuales se le suma base cada turno hasta que llegue a max
    public int habilidadSeleccionada = -1;
    public int basePAtaques = 5;
    public int maxPAtaques = 10;
    public int actPAtaques = 0;
    public List<Habilidad> habilidadesDisponibles;
    public List<int> cooldowns;
    public int ataqueActual(){
        double final = this.attack;
        if (mejoraAtaque) final *= 1.5;
        if (inspirado) final *= 1.1;
        if (quemado) final *= 0.5;
        return Convert.ToInt32(final);
    }
    public int ataqueEspecialActual(){
        double final = this.special_attack;
        if (mejoraAtaqueEspecial) final *= 1.5;
        if (inspirado) final *= 1.1;
        if (sangrado) final *= 0.5;
        return Convert.ToInt32(final);
    }
    public int defensaActual(){
        double final = this.attack;
        if (mejoraDefensa) final *= 1.5;
        if (inspirado) final *= 1.1;
        if (protegido) final *= 1.5;
        if (envenenado) final *= 0.75;
        return Convert.ToInt32(final);
    }
    public int defensaEspecialActual(){
        double final = this.defense;
        if (mejoraDefensaEspecial) final *= 1.5;
        if (inspirado) final *= 1.1;
        if (protegido) final *= 1.5;
        if (envenenado) final *= 0.75;
        return Convert.ToInt32(final);
    }
    public int velocidadActual(){
        double final = this.velocity;
        if (mejoraVelocidad) final *= 1.5;
        if (inspirado) final *= 1.1;
        if (envenenado) final *= 0.5;
        return Convert.ToInt32(final);
    }

    // *********************************************
    // *            Estados alterados              *
    // *********************************************

    bool aturdido = false;
    int turnosAturdido = 0;
    public void aturdir(){
        this.aturdido = true;
        this.turnosAturdido += 3;
    }
    bool envenenado = false;
    int turnosEnvenenado = 0;
    public void envenenar(){
        this.envenenado = true;
        this.turnosEnvenenado += 3;
    }
    bool quemado = false;
    int turnosQuemado = 0;
    public void quemar(){
        this.quemado = true;
        this.turnosQuemado += 3;
    }
    bool congelado = false;
    int turnosCongelado = 0;
    public void congelar(){
        this.congelado = true;
        this.turnosCongelado += 3;
    }
    bool protegido = false;
    int turnosProtegido = 0;
    public void proteger(){
        this.protegido = true;
        this.turnosProtegido += 3;
    }
    bool contraataque = false;
    int turnosContraataque = 0;
    public void contraatacar(){
        this.contraataque = true;
        this.turnosContraataque += 3;
    }
    bool sangrado = false;
    int turnosSangrado = 0;
    public void sangrar(){
        this.sangrado = true;
        this.turnosSangrado += 3;
    }
    bool miedo = false;
    int turnosMiedo = 0;
    public void asustar(){
        this.miedo = true;
        this.turnosMiedo += 3;
    }
    bool inmortal = false;
    int turnosInmortal = 0;
    public void inmortalizar(){
        this.inmortal = true;
        this.turnosInmortal += 3;
    }

    // *********************************************
    // *            Buffs                          *
    // *********************************************    
   
    bool inspirado = false;
    int turnosInspirado = 0;
    public void inspirar() {
        inspirado = true;
        turnosInspirado += 3; 
    }
    bool bendecido = false;
    int turnosBendecido = 0;
    public void bendecir() {
        bendecido = true;
        turnosBendecido += 3; 
    }
    bool resistenciaMiedo = false;
    int turnosResistenciaMiedo = 0;
    public void resistencirMiedo() {
        resistenciaMiedo = true;
        turnosResistenciaMiedo += 3; 
    }
    bool mejoraAtaque = false;
    int turnosMejoraAtaque = 0;
    public void mejorarAtaque() {
        mejoraAtaque = true;
        turnosMejoraAtaque += 3; 
    }
    bool mejoraAtaqueEspecial = false;
    int turnosMejoraAtaqueEspecial = 0;
    public void mejorarAtaqueEspecial() {
        mejoraAtaqueEspecial = true;
        turnosMejoraAtaqueEspecial += 3; 
    }
    bool mejoraDefensa = false;
    int turnosMejoraDefensa = 0;
    public void mejorarDefensa() {
        mejoraDefensa = true;
        turnosMejoraDefensa += 3; 
    }
    bool mejoraDefensaEspecial = false;
    int turnosMejoraDefensaEspecial = 0;
    public void mejorarDefensaEspeciak() {
        mejoraDefensaEspecial = true;
        turnosMejoraDefensaEspecial += 3; 
    }
    bool mejoraVelocidad = false;
    int turnosMejoraVelocidad = 0;
    public void mejorarVelocidad() {
        mejoraVelocidad = true;
        turnosMejoraVelocidad += 3; 
    }



    // *********************************************
    // *            UI                             *
    // *********************************************


    private UICombate UICombate;

    private GameObject circuloMov = null;
    private GameObject circuloHab = null;

    void cargarHabilidadesDeClase(){
        Type t = Type.GetType(className);
        clase = (Clase)Activator.CreateInstance(t);

        for(int i = 0; i < clase.LevelupData.Count; i++){
            if (level >= clase.LevelupData[i].nivel){
                habilidadesDisponibles.Add(clase.LevelupData[i].habilidad);
                cooldowns.Add(0);
            }   
        }
    }

    public void dibujaCirculoMov(){
        destruirCirculoMov();
        circuloMov = new GameObject();
        circuloMov.DrawCircle((float)metrosRestantes, .075f, Color.green);
    }

    public void destruirCirculoMov(){
        if(circuloMov!=null){
            Destroy(circuloMov);
            circuloMov = null;
        }
    }

    public void dibujaCirculoHab(){
        destruirCirculoHab();
        circuloHab = new GameObject();
        circuloHab.DrawCircle(habilidadesDisponibles[habilidadSeleccionada].range, .085f, Color.blue);
        circuloHab.transform.position = new Vector3(transform.position.x,transform.position.y+0.5f,transform.position.z); 
    }

    public void destruirCirculoHab(){
        if(circuloHab!=null){
            Destroy(circuloHab);
            circuloHab = null;
        }
    }


    // *********************************************
    // *            MISC                           *
    // *********************************************

    public bool user_controlled = false;
    
    public int exp_when_killed = 100;

    public SistemaCombate SistemaCombate;

    public Animator anim;
    




    void reducirTurnosBuffsDebuffsYEstadosAlterados(){

    }



    void Awake(){
        // TEST. En un futuro, constructor o algo
        cooldowns = new List<int>();
        habilidadesDisponibles = new List<Habilidad>();


        cargarHabilidadesDeClase();

        UICombate = GameObject.Find("SkillsImages").GetComponent<UICombate>();
        anim = GetComponentInChildren<Animator>();
        hp = hpMax;

    }

    void Update(){
        // Hacemos que el circulo de movimiento se mueva con el personaje, solo si no es null
        if(circuloMov!=null){
            if(metrosRestantes<=0.5f) destruirCirculoMov(); // Además, si es tricky clicar porque queda poco, es más fácil quitar el círculo y ya
            else circuloMov.transform.position = new Vector3(transform.position.x,transform.position.y+0.5f,transform.position.z);
        }
    }




    public void EmpiezaTurno(){

        reducirTurnosBuffsDebuffsYEstadosAlterados();
        // Primero miramos si estamos envenenados o quemados. De ser el caso perdemos vida
        
        

        if (envenenado){
            hp -= Convert.ToInt32(hpMax/20);
        }else if (quemado){
            hp -= Convert.ToInt32(hpMax/40);
        }

        // Al empezar el turno reseteamos los metros, restamos 1 a los cooldowns y añadimos los puntos base a los actuales
        metrosRestantes = metrosMaximos;

        for(int i = 0; i < cooldowns.Count; i++){
            if (cooldowns[i] > 0) cooldowns[i]--;
        }

        actPAtaques += basePAtaques;
        if(actPAtaques>maxPAtaques) actPAtaques=maxPAtaques;

        if(user_controlled){
            // Cambiamos todas las imagenes de las habilidades para adaptarse al personaje
            // Además también actualiza la barra de distancia
            UICombate.adaptaUI(habilidadesDisponibles,this);
            UICombate.actualizaPP();
            UICombate.ActualizaDistancia();

            objetivo = null;
        }
        else{
            this.transform.GetComponent<IA>().HacerTurno();
        }

        if (aturdido) SistemaCombate.FinalizaTurno();
        
        
    }
    
    public void TerminaTurno(){
        if (!user_controlled){
            SistemaCombate.FinalizaTurno();
        }
        else{
            destruirCirculoMov();
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

    public AnimationClip FindAnimation (Animator animator, string name) // Busca una animación name en el animator dado y te la da, incredibol
    {
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == name)
            {
                return clip;
            }
        }

        return null;
    }
    

    IEnumerator RutinaAtacar(){
        // Lanzamos la habilidad y la ponemos en cooldown
        // Pero antes hacemos la animacion y esperamos a que termine
        anim.Play("Attack"); 
        yield return new WaitForSeconds(FindAnimation(anim,"Attack").length);

        
    }

    public bool esSeleccionable(int h){
        return actPAtaques>=habilidadesDisponibles[h].coste && cooldowns[h]==0;
    }

    public void Atacar(){
        // Hay que mirar como hacer los hechizos de area (si los metemos)
        if(habilidadSeleccionada>=0){
            Habilidad habilidad = habilidadesDisponibles[habilidadSeleccionada];
            if (objetivo!=null){
                Character a = objetivo.GetComponent<Character>();
                Debug.Log("Objetivo a atacar: " + objetivo.GetComponent<Character>().nombre);
                
                StartCoroutine(RutinaAtacar());
                Habilidades.lanzar(this, a, habilidad);
                cooldowns[habilidadSeleccionada] += habilidad.cooldown;
                Debug.Log("Lanzando habilidad " + habilidad.name);
                // Restamos los puntos que se usan
                actPAtaques -= habilidad.coste;
                UICombate.actualizaPP();

                // anim.Play("Idle"); // Me da un warning State could not be found
                    
            }

        }
        

    }




    public bool takeHit(int damage, string element)
    {
        if (defense < damage) damage -= defense;
        if (elemental_resistance == element) damage /= 2;

        hp -= damage;
        if (hp <= 0){
            morir();
        }
        return true;
    }

    public bool recieveHeal(int ammount)
    {
        hp += ammount;
        if (hp > hpMax) hp = hpMax;
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
