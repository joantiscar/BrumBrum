using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCombat : MonoBehaviour
{
    public float probability;
    public bool able;


    // Start is called before the first frame update
    void Start()
    {
        able = false;
        probability /= 10;
    }

    // Update is called once per frame
    void Update()
    {
        if(able && thereIsCombat())
        {
            Debug.Log("There is combat");
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
