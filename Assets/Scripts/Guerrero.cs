using System.Collections.Generic;
public class Guerrero : Clase{


    public override List<Dato> LevelupData(){
        return new List<Dato>{
        new Dato(0, Habilidades.AtaqueConEspada),
        new Dato(2, Habilidades.TajoCruzado),
        new Dato(4, Habilidades.FragorDeLaBatalla),
        new Dato(6, Habilidades.Remolino),
        new Dato(8, Habilidades.Furia),
        new Dato(10, Habilidades.Masacre),
        new Dato(0, Habilidades.Pocion)
        };
    }
}