using System.Collections.Generic;
public class Luchador : Clase{


    public override List<Dato> LevelupData(){
        return new List<Dato>{
        new Dato(0, Habilidades.Punetazo),
        new Dato(2, Habilidades.Partenueces),
        new Dato(4, Habilidades.LanzamientoDeRoca),
        new Dato(6, Habilidades.PalmadaSonica),
        new Dato(8, Habilidades.Terremoto),
        new Dato(10, Habilidades.PunetazoDeUnaPulgada),
        new Dato(0, Habilidades.Pocion)
        };
    }
}