using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class RandomCombat : MonoBehaviour
{
    public float probability = 1f;
    public bool able;
    private bool inCombat;
    private string battleScene;
    private bool fadeIn = false;

    public int waitTill = 500;
    public int waiting = 0;
    public GameObject ThirdPersonCamera;

    private GameObject scene;
    private Animator imageAnimator;

    Random r = new Random();

    // Start is called before the first frame update
    void Start()
    {
        able = false;
        inCombat = false;
        probability /= 10;
        waiting = waitTill;

        scene = GameObject.Find("MovementScene");

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player!=null)
            imageAnimator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Canvas>().GetComponentInChildren<Image>().GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Singleton.menu()) {
            if (!inCombat && waiting > 0)
            {
                waiting--;
            }

            if (able && thereIsCombat() && !inCombat && waiting <= 0 &&
                (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
            {
                waiting = waitTill;

                //Debug.Log("There is combat");
                //battle = SceneManager.LoadScene("CombatDemo");
                inCombat = true;
                Singleton.enCombate = true;
                //scene.SetActive(false);

                //SceneManager.LoadScene("CombatDemo", LoadSceneMode.Additive);

                /*
                if (SceneManager.GetActiveScene().name == "Scene3_Castle")
                {
                    SceneManager.LoadScene("CombatScene_CombatCastleScenario", LoadSceneMode.Additive);
                    battleScene = "CombatScene_CombatCastleScenario";
                }
                else
                {
                    SceneManager.LoadScene("CombatScene_CombatForestScenario", LoadSceneMode.Additive);
                    battleScene = "CombatScene_CombatForestScenario";
                }
                */

                fadeIn = true;
                imageAnimator.SetBool("Fade", true);
            }
            else if (inCombat && fadeIn && imageAnimator.GetCurrentAnimatorStateInfo(0).IsName("Default"))
            {
                Singleton.randomEnemigos(r.Next(1,6)); // dame enemigos del 1 al 5

                //SceneManager.LoadScene("CombatDemo", LoadSceneMode.Additive); battleScene = "CombatDemo";
                if (SceneManager.GetActiveScene().name == "Scene3_Castle")
                {
                    SceneManager.LoadScene("CombatScene_CombatCastleScenario", LoadSceneMode.Additive);
                    battleScene = "CombatScene_CombatCastleScenario";
                }
                else
                {
                    SceneManager.LoadScene("CombatScene_CombatForestScenario", LoadSceneMode.Additive);
                    battleScene = "CombatScene_CombatForestScenario";
                }

                scene.SetActive(false);
            }
            if (inCombat && Input.GetKeyDown(KeyCode.X))
            {
                inCombat = false;
                Singleton.enCombate = false;

                /*scene.SetActive(true);
                Destroy(GameObject.Find("New Game Object"));*/
                SceneManager.UnloadSceneAsync(battleScene);

                //imageAnimator.SetBool("Fade", false);
                //imageAnimator.Play("FadeOut");
                ExitCombat();
            }
            if (inCombat && Input.GetKeyDown(KeyCode.O))
            {
                GameObject.FindObjectOfType<SistemaCombate>().derrota = true;
            }
            if (inCombat && Input.GetKeyDown(KeyCode.P))
            {
                GameObject.FindObjectOfType<SistemaCombate>().victoria = true;
            }
        }
    }

    private bool thereIsCombat()
    {
        bool combat = false;


        float num = UnityEngine.Random.Range(0f, 99f);
        //Debug.Log(num);
        if (num <= probability)
        {
            combat = true;

        }

        return combat;
    }

    public void SetAble()
    {
        able = true;
    }

    public void SetDisable()
    {
        able = false;
    }

    public void ExitCombat()
    {
        scene.SetActive(true);
        Destroy(GameObject.Find("New Game Object"));
        SceneManager.UnloadSceneAsync(battleScene);
        Debug.Log ("Que tal?");
        //if (GameObject.FindObjectOfType<SistemaCombate>().victoria){
            ThirdPersonCamera.SetActive(false);
            ThirdPersonCamera.SetActive(true);
        //}
        inCombat = false;
        Singleton.enCombate = false;

        scene.SetActive(true);


        SistemaCombate sistema = FindObjectOfType<SistemaCombate>();
        GameObject pjAct = sistema.pjActual;

        sistema.destruirCirculoArea();
        pjAct.GetComponent<Character>().destruirCirculoHab();
        pjAct.GetComponent<Character>().destruirCirculoMov();

        /*
        Character[] chars = FindObjectsOfType<Character>();

        foreach(Character c in chars)
        {
            Debug.Log(c.hpMax);
        }
        */


        //Destroy(GameObject.Find("New Game Object"));
        SceneManager.UnloadSceneAsync(battleScene);

        //imageAnimator.SetBool("Fade", false);
        //imageAnimator.Play("FadeOut");
    }
}
