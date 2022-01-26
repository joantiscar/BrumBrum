using System.Collections.Generic;
public class Paladin : Clase{


   public override List<Dato> LevelupData(){
        return new List<Dato>{
           new Dato(0, Habilidades.AtaqueConEspada),
        new Dato(2, Habilidades.Proteger),
        new Dato(4, Habilidades.Rezar),
        new Dato(6, Habilidades.Devocion),
        new Dato(8, Habilidades.GolpeDeEscudo),
        new Dato(10, Habilidades.CastigoDivino),
        new Dato(0, Habilidades.Pocion)
        };
    }
}