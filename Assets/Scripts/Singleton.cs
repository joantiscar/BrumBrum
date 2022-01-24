
public class Singleton {

    private static Singleton _instance;
    public bool _menu = false;

    public static Singleton instance (){
        if (_instance == null){
            _instance = new Singleton();
        }
        return _instance;
    }

    public static void toggleMenu (){
        instance()._menu = !instance()._menu;
    }

    public static bool menu (){
        return instance()._menu;
    }
    
    public static void afegirPocions (int n){
        //a
    }

    public static void restarPocions (int n){
        //a
    }
    
    public static void guanyarExp (int n){
        //b
    }
}
