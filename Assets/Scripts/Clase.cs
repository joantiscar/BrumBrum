using System.Collections.Generic;
public class Clase{

    public struct Dato {
        public int nivel;
        public Habilidad habilidad;
        public Dato(int n, Habilidad h)
        {
            this.nivel = n;
            this.habilidad = h;
        }
    }

    public List<Dato> LevelupData = new List<Dato>{
        new Dato(0, Habilidades.AtaqueConEspada),
        new Dato(0, Habilidades.BolaDeFuego),
        new Dato(0, Habilidades.Curacion)
    };
}