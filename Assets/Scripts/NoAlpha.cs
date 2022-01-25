using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoAlpha : MonoBehaviour{
    public Image image;

    void Start()
    {
        //Any desired value between 0 and 1.
        image.alphaHitTestMinimumThreshold = 0.9f;
    }
}
