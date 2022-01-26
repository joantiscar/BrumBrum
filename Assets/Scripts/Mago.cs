using System.Collections.Generic;
public class Mago : Clase{


    public override List<Dato> LevelupData(){
        return new List<Dato>{
            new Dato(0, Habilidades.AtaqueConEspada),
            new Dato(2, Habilidades.EsquirlaDeHielo),
            new Dato(4, Habilidades.ColmilloVenenoso),
            new Dato(6, Habilidades.BolaDeFuego),
            new Dato(8, Habilidades.AreaDeProteccionMagica),
            new Dato(10, Habilidades.Meteorito)
        };
    }
}