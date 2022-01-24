using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RandomCombat : MonoBehaviour
{
    public float probability;
    private bool able;
    private bool inCombat;
    private string battleScene;
    private bool fadeIn = false;

    public int waitTill;
    private int waiting = 0;

    private GameObject scene;
    private Animator imageAnimator;


    // Start is called before the first frame update
    void Start()
    {
        //battle = null;
        able = false;
        inCombat = false;
        probability /= 10;

        scene = GameObject.Find("MovementScene");


        imageAnimator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Canvas>().GetComponentInChildren<Image>().GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Singleton.menu()){
            if (!inCombat && waiting > 0)
            {
                waiting--;
            }

            if (able && thereIsCombat() && !inCombat && waiting <= 0 && 
                (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)))
            {
                waiting = waitTill;

                //Debug.Log("There is combat");
                //battle = SceneManager.LoadScene("CombatDemo");
                inCombat = true;
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
            if (inCombat && Input.GetKeyDown(KeyCode.Z))
            {
                inCombat = false;
                scene.SetActive(true);
                Destroy(GameObject.Find("New Game Object"));
                SceneManager.UnloadSceneAsync(battleScene);
            }
        }
    }

    private bool thereIsCombat()
    {
        bool combat = false;

        float num = Random.Range(0f, 99f);
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
}
