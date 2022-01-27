using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LootCofres : MonoBehaviour
{
    private int escena;
    bool nextframe = false;
    bool done = false;
    string nombre = null;
    private AudioSource audioSource;
    public GameObject missatgeLoot;
    TextMeshProUGUI TextLoot;
    public Animator animMissatgeLoot;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("Loot").GetComponent<AudioSource>();
        Debug.Log (missatgeLoot);
        TextLoot = missatgeLoot.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
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

    void Update (){
        if (animMissatgeLoot.GetCurrentAnimatorStateInfo(0).IsName("AnimacioPanelLoot") && !nextframe && !done && nombre != null){
            nextframe = true;
        }
        else if (animMissatgeLoot.GetCurrentAnimatorStateInfo(0).IsName("AnimacioPanelLoot") && nextframe && !done){
            animMissatgeLoot.SetBool("Show", false);
            nextframe = false;
            done = true;
            Destroy(gameObject);
        }
    }

    public void getLoot() {
        nombre = transform.gameObject.name;
        int loot;
        int quantitat;
        audioSource.Play();

        RandomCombat ran = GameObject.FindObjectOfType<RandomCombat>();
        ran.SetAble();
        ran.waiting = ran.waitTill;

        switch (escena){
            case 0:
                Debug.Log(escena);
                loot = Random.Range(0, 2);
                if (loot == 0){
                    quantitat = Random.Range(1, 3);
                    Singleton.afegirPocions(quantitat);
                    if (quantitat == 1){
                        TextLoot.text = "Has conseguido " + quantitat + " poción";
                    }
                    else{
                        TextLoot.text = "Has conseguido " + quantitat + " pociones";
                    }
                    animMissatgeLoot.SetBool("Show", true);
                    nextframe = false;
                    done = false;
                }
                else {
                    TextLoot.text = "Has conseguido 5 de experiencia";
                    Singleton.guanyarExp(5);
                    animMissatgeLoot.SetBool("Show", true);
                    nextframe = false;
                    done = false;
                }
            break;
            case 1:
                Debug.Log(escena);
                loot = Random.Range(0, 2);
                if (loot == 0){
                    quantitat = Random.Range(2, 4);
                    Singleton.afegirPocions(quantitat);
                    TextLoot.text = "Has conseguido " + quantitat + " pociones";
                    animMissatgeLoot.SetBool("Show", true);
                    nextframe = false;
                    done = false;
                }
                else {
                    TextLoot.text = "Has conseguido 10 de experiencia";
                    Singleton.guanyarExp(10);
                    animMissatgeLoot.SetBool("Show", true);
                    nextframe = false;
                    done = false;
                }
            break;
            case 2:
                Debug.Log(escena);
                loot = Random.Range(0, 2);
                if (loot == 0){
                    quantitat = Random.Range(3, 5);
                    Singleton.afegirPocions(quantitat);
                    TextLoot.text = "Has conseguido " + quantitat + " pociones";
                    animMissatgeLoot.SetBool("Show", true);
                    nextframe = false;
                    done = false;
                }
                else {
                    TextLoot.text = "Has conseguido 15 de experiencia";
                    Singleton.guanyarExp(15);
                    animMissatgeLoot.SetBool("Show", true);
                    nextframe = false;
                    done = false;
                }
            break;
            case 3:
                Debug.Log(escena);
                loot = Random.Range(0, 2);
                if (loot == 0){
                    quantitat = Random.Range(4, 6);
                    Singleton.afegirPocions(quantitat);
                    TextLoot.text = "Has conseguido " + quantitat + " pociones";
                    animMissatgeLoot.SetBool("Show", true);
                    nextframe = false;
                    done = false;
                }
                else {
                    TextLoot.text = "Has conseguido 20 de experiencia";
                    Singleton.guanyarExp(20);
                    animMissatgeLoot.SetBool("Show", true);
                    nextframe = false;
                    done = false;
                }
            break;
        }
    }
}
