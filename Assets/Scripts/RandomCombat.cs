using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomCombat : MonoBehaviour
{
    public float probability;
    public bool able;
    private bool inCombat;
    private string battleScene;

    private GameObject scene;


    // Start is called before the first frame update
    void Start()
    {
        //battle = null;
        able = false;
        inCombat = false;
        probability /= 10;

        scene = GameObject.Find("MovementScene");
    }

    // Update is called once per frame
    void Update()
    {
        if (able && thereIsCombat() && !inCombat)
        {
            Debug.Log("There is combat");
            //battle = SceneManager.LoadScene("CombatDemo");
            inCombat = true;
            scene.SetActive(false);
            //SceneManager.LoadScene("CombatDemo", LoadSceneMode.Additive);

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
            
        }
        if (inCombat && Input.GetKeyDown(KeyCode.Z))
        {
            inCombat = false;
            scene.SetActive(true);
            Destroy(GameObject.Find("New Game Object"));
            SceneManager.UnloadSceneAsync(battleScene);
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
