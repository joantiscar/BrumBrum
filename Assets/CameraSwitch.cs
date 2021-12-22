using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraSwitch : MonoBehaviour
{

    public GameObject camStatic1;
    public GameObject camPlayer1;

    bool isStatic = false;
    void Update()
    {
        if(isStatic){
            camStatic1.SetActive(true);
            camPlayer1.SetActive(false);
        }
        else if(!isStatic){
            camStatic1.SetActive(false);
            camPlayer1.SetActive(true);
        }
    }

    public void isCameraOnGoing()
    {
        isStatic = !isStatic;
    }
}
