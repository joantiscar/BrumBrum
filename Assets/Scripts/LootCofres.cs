using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LootCofres : MonoBehaviour
{
    int escena;
    // Start is called before the first frame update
    void Start()
    {
        switch (SceneManager.GetActiveScene().name){
            case "Scene1_Precamping":
                escena = 0;
            break;
            case "Scene1-5_Camping":
                escena = 1;
            break;
            case "Scene2_Morning":
                escena = 2;
            break;
            case "Scene3_Castle":
                escena = 3;
            break;
        }
    }

    public void getLoot() {
        int loot;
        int quantitat;
        switch (escena){
            case 0:
                Debug.Log(escena);
                loot = Random.Range(0, 2);
                if (loot == 0){
                    quantitat = Random.Range(1, 3);
                    Debug.Log(quantitat + " pocions");
                }
                else {
                    Debug.Log ("50 d'experiencia");
                }
            break;
            case 1:
                Debug.Log(escena);
                loot = Random.Range(0, 2);
                if (loot == 0){
                    quantitat = Random.Range(2, 4);
                    Debug.Log(quantitat + " pocions");
                }
                else {
                    Debug.Log ("100 d'experiencia");
                }
            break;
            case 2:
                Debug.Log(escena);
                loot = Random.Range(0, 2);
                if (loot == 0){
                    quantitat = Random.Range(3, 5);
                    Debug.Log(quantitat + " pocions");
                }
                else {
                    Debug.Log ("150 d'experiencia");
                }
            break;
            case 3:
                Debug.Log(escena);
                loot = Random.Range(0, 2);
                if (loot == 0){
                    quantitat = Random.Range(4, 6);
                    Debug.Log(quantitat + " pocions");
                }
                else {
                    Debug.Log ("200 d'experiencia");
                }
            break;
        }
    }
}
