using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamaraFundido : MonoBehaviour
{
    Image imagen;
    bool fading = true;

    // Start is called before the first frame update
    void Start()
    {
        imagen = this.GetComponentInChildren<Canvas>().GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(fading && imagen.color.a == 0){
            imagen.transform.parent.gameObject.SetActive(false);
            fading = false;
        }
    }
}
