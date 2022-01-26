using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Character : MonoBehaviour
{
   
    public Character(){
        this.experience = INICIAL_MAX_EXPERIENCE * (level - 1);
        for (int i = 2; i < level; i++)
            this.experience += PLUS_EXPERIENCE * (i - 1);
    }

    public Character(int hp, int attack, int special_attack, int defense, int special_defense, int velocity, int level=1, string elemental_resistance="none")
    {
        this.hpMax = hp;
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
    public int level = 1;
    public int experience;
    public int experience_max;
    public int upgrade_points = 0;



    // *********************************************
    // *            Stats                          *
    // *********************************************

    public int hpMax = 20;
    public int attack = 5;
    public int special_attack = 5;
    public int defense = 5;
	public int special_defense = 5;
	public int velocity = 5;
    public string nombre = "SinNombre";
    public string elemental_resistance = "none";
    public double metrosMaximos = 10.0f;
    public GameObject objetivo;
    public List<GameObject> objetivos;
    public string className = "Clase";
    private Clase clase;


    // *********************************************
    // *            STATE                          *
    // *********************************************

    public int id = 0;
    public double metrosRestantes = 10.0f;
    public int hp = 20;
    // Puntos para usar una habilidad: a los actuales se le suma base cada turno hasta que llegue a max
    public int habilidadSeleccionada = -1;
    public int basePAtaques = 4;
    public int maxPAtaques = 8;
    public int actPAtaques = 0;
    public List<Habilidad> habilidadesDisponibles;
    public List<int> cooldowns;
    public int ataqueActual(){
        double final = this.attack;
        if (mejoraAtaque) final *= 1.5;
        if (furia) final *= 2;
        if (inspirado) final *= 1.1;
        if (bendecido) final *= 1.1;
        if (quemado) final *= 0.75;
        if (miedo) final *= 0.75;
        return Convert.ToInt32(final);
    }
    public int ataqueEspecialActual(){
        double final = this.special_attack;
        if (mejoraAtaqueEspecial) final *= 1.5;
        if (inspirado) final *= 1.1;
        if (bendecido) final *= 1.1;
        if (sangrado) final *= 0.5;
        return Convert.ToInt32(final);
    }
    public int defensaActual(){
        double final = this.attack;
        if (mejoraDefensa) final *= 1.5;
        if (inspirado) final *= 1.1;
        if (bendecido) final *= 1.1;
        if (contraataque) final *= 1.5;
        if (protegido) final *= 1.5;
        if (envenenado) final *= 0.75;
        if (furia) final *= 0.75;
        if (congelado) final *= 0.75;
        return Convert.ToInt32(final);
    }
    public int defensaEspecialActual(){
        double final = this.defense;
        if (mejoraDefensaEspecial) final *= 1.5;
        if (inspirado) final *= 1.1;
        if (bendecido) final *= 1.1;
        if (protegido) final *= 1.5;
        if (envenenado) final *= 0.75;
        if (furia) final *= 0.75;
        return Convert.ToInt32(final);
    }
    public int velocidadActual(){
        double final = this.velocity;
        if (mejoraVelocidad) final *= 1.5;
        if (inspirado) final *= 1.1;
        if (bendecido) final *= 1.1;
        if (envenenado) final *= 0.5;
        if (congelado) final *= 0.5;
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
    bool furia = false;
    int turnosFuria = 0;
    public void enfurecer(){
        this.furia = true;
        this.turnosFuria += 3;
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
    public bool mejoraDefensaEspecial = false;
    public int turnosMejoraDefensaEspecial = 0;
    public void mejorarDefensaEspecial() {
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


    public UICombate UICombate;

    private GameObject circuloMov = null;
    private GameObject circuloHab = null;

    void cargarHabilidadesDeClase(){
        switch(className){
            case "Clase":
                clase = new Clase();
                break;
            case "Curandero":
                clase = new Curandero();
                break;
            case "Guerrero":
                clase = new Guerrero();
                break;
            case "Mago":
                clase = new Mago();
                break;
            case "Paladin":
                clase = new Paladin();
                break;
            case "Luchador":
                clase = new Luchador();
                break;

        }
        List<Clase.Dato> dades = clase.LevelupData();
        for(int i = 0; i < dades.Count; i++){
            if (level >= dades[i].nivel){
                habilidadesDisponibles.Add(dades[i].habilidad);
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
    
    public int exp_when_killed = 0;

    public SistemaCombate SistemaCombate;

    public Animator anim;
    




    void reducirTurnosBuffsDebuffsYEstadosAlterados(){
        turnosAturdido--; if (turnosAturdido == 0) aturdido = false;
        turnosEnvenenado--; if (turnosEnvenenado == 0) envenenado = false;
        turnosQuemado--; if (turnosQuemado == 0) quemado = false;
        turnosCongelado--; if (turnosCongelado == 0) congelado = false;
        turnosSangrado--; if (turnosSangrado == 0) sangrado = false;
        turnosMiedo--; if (turnosMiedo == 0) miedo = false;
        turnosProtegido--; if (turnosProtegido == 0) protegido = false;
        turnosContraataque--; if (turnosContraataque == 0) contraataque = false;
        turnosFuria--; if (turnosFuria == 0) furia = false;
        turnosInmortal--; if (turnosInmortal == 0) inmortal = false;
        turnosInspirado--; if (turnosInspirado == 0) inspirado = false;
        turnosBendecido--; if (turnosBendecido == 0) bendecido = false;
        turnosMejoraAtaque--; if (turnosMejoraAtaque == 0) mejoraAtaque = false;
        turnosMejoraAtaqueEspecial--; if (turnosMejoraAtaqueEspecial == 0) mejoraAtaqueEspecial = false;
        turnosMejoraDefensa--; if (turnosMejoraDefensa == 0) mejoraDefensa = false;
        turnosMejoraDefensaEspecial--; if (turnosMejoraDefensaEspecial == 0) mejoraDefensaEspecial = false;
        turnosMejoraVelocidad--; if (turnosMejoraVelocidad == 0) mejoraVelocidad = false;
    }



    void Awake(){
        iniciarEstado();

    }

    public void iniciarEstado(){
        cooldowns = new List<int>();
        habilidadesDisponibles = new List<Habilidad>();
        objetivos = new List<GameObject>();

        cargarHabilidadesDeClase();

        anim = GetComponentInChildren<Animator>();
        renacer(true);
    }

    public void renacer(bool completo = false){
        hp = hpMax;
        turnosAturdido = 0;
        aturdido = false;
        turnosEnvenenado = 0;
        envenenado = false;
        turnosQuemado = 0;
        quemado = false;
        turnosCongelado = 0;
        congelado = false;
        turnosSangrado = 0;
        sangrado = false;
        turnosMiedo = 0;
        furia = false;
        turnosFuria = 0;
        if (completo){
            turnosProtegido = 0;
            protegido = false;
            turnosContraataque = 0;
            contraataque = false;
            miedo = false;
            turnosInmortal = 0;
            inmortal = false;
            turnosInspirado = 0;
            inspirado = false;
            turnosBendecido = 0;
            bendecido = false;
            turnosMejoraAtaque = 0;
            mejoraAtaque = false;
            turnosMejoraAtaqueEspecial = 0;
            mejoraAtaqueEspecial = false;
            turnosMejoraDefensa = 0;
            mejoraDefensa = false;
            turnosMejoraDefensaEspecial = 0;
            mejoraDefensaEspecial = false;
            turnosMejoraVelocidad = 0;
            mejoraVelocidad = false;
        }
        
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

        // Cambiamos todas las imagenes de las habilidades para adaptarse al personaje
        UICombate.adaptaUI(habilidadesDisponibles,this);
        if(user_controlled){
            UICombate.actualizaPP();
            UICombate.ActualizaDistancia();

            objetivo = null;
            objetivos = null;
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
            destruirCirculoHab();
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

        return animator.runtimeAnimatorController.animationClips[0];
    }
    

    IEnumerator RutinaAtacar(){
        // Lanzamos la habilidad y la ponemos en cooldown
        // Pero antes hacemos la animacion y esperamos a que termine
        string animName = "Attack";
        if (habilidadesDisponibles[habilidadSeleccionada].special || habilidadesDisponibles[habilidadSeleccionada].heals || !habilidadesDisponibles[habilidadSeleccionada].targetEnemy){
            animName = "Heal";
        }
        anim.Play(animName); 
        yield return new WaitForSeconds(FindAnimation(anim,animName).length);

        
    }


    public bool esSeleccionable(int h){
        return actPAtaques>=habilidadesDisponibles[h].coste && cooldowns[h]==0;
    }

    public void pocion(){
        if (Singleton.instance().pocions > 0 && actPAtaques > 0){
            Habilidades.lanzar(this, this, Habilidades.Pocion);
            UICombate.mostrarMissatge("Poción usada");
            actPAtaques--;
            UICombate.actualizaPP();
            Singleton.restarPocio();
        }
    }

    public void Atacar(){
        if(habilidadSeleccionada>=0){
            Habilidad habilidad = habilidadesDisponibles[habilidadSeleccionada];
            StartCoroutine(RutinaAtacar());
            if (habilidad.radius==0.0f && objetivo!=null){ // habilidad normal
                this.transform.LookAt(objetivo.transform);


                Character objetivoPJ = objetivo.GetComponent<Character>();
                Debug.Log("Objetivo a atacar: " + objetivoPJ.nombre);
                
                Habilidades.lanzar(this, objetivoPJ, habilidad);
                objetivo = null;
                    
            }
            else if(habilidad.radius>0.0f && objetivos.Count!=0){ // en area
                this.transform.LookAt(objetivos[0].transform);

                foreach(var obj in objetivos){
                    Character objetivoPJ = obj.GetComponent<Character>();
                    Debug.Log("Objetivo a atacar: " + objetivoPJ.nombre);
                    
                    Habilidades.lanzar(this, objetivoPJ, habilidad);

                }
            }
            else{
                Debug.Log("Algo falla");
            }

            cooldowns[habilidadSeleccionada] += habilidad.cooldown;
            UICombate.mostrarMissatge(this.nombre + " lanzó " + habilidad.name);
            // Restamos los puntos que se usan
            actPAtaques -= habilidad.coste;
            if(user_controlled){
                UICombate.actualizaPP();
                UICombate.deshabilitaHabilidad(habilidadSeleccionada);
            }

        }
        

    }




    public bool takeHit(int damage, string element)
    {
        if (defense < damage) damage -= defense;
        if (elemental_resistance == element) damage /= 2;

        anim.Play("Hit"); 

        hp -= damage;
        if (hp <= 0 && inmortal) hp = 1;
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
        anim.Play("Hit");
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
        SistemaCombate.removeCharacter(this.id);
        if (exp_when_killed > 0) Singleton.guanyarExp(exp_when_killed);
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
                    attack += 1;
                    break;
                case "special_attack":
                    special_attack += 1;
                    break;
                case "defense":
                    defense += 1;
                    break;
                case "special_defense":
                    special_defense += 1;
                    break;
                case "velocity":
                    velocity += 1;
                    break;
                default:
                    hp += 5;
                    break;
            }
            upgrade_points--;
            

            Debug.Log("points used on " + up_stat);
        }
    }


    public void copy(Character c){
        this.hpMax = c.hpMax;
        this.attack = c.attack;
        this.special_attack = c.special_attack;
        this.defense = c.defense;
        this.special_defense = c.special_defense;
        this.velocity = c.velocity;
        this.level = c.level;
        this.className = c.className;
        this.nombre = c.nombre;
        this.elemental_resistance = c.elemental_resistance;
        this.user_controlled = c.user_controlled;
    }


}
