using System.Collections.Generic;
public class Curandero : Clase{

    public List<Dato> LevelupData = new List<Dato>{
        new Dato(0, Habilidades.AtaqueConEspada),
        new Dato(2, Habilidades.Curacion),
        new Dato(4, Habilidades.Purificacion),
        new Dato(6, Habilidades.Bendicion),
        new Dato(8, Habilidades.AuraDeCuracion),
        new Dato(10, Habilidades.Renacer)
    };
}