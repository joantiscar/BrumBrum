using System.Collections.Generic;
public class Clase{

    public struct Dato { // que buen daaaatoooo
        public int nivel;
        public Habilidad habilidad;
        public Dato(int n, Habilidad h)
        {
            this.nivel = n;
            this.habilidad = h;
        }
    }

    public List<Dato> LevelupData = new List<Dato>{
        new Dato(0, Habilidades.AtaqueConEspada)
    };
}