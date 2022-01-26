using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraSwitch : MonoBehaviour
{

    public GameObject camStatic;
    public GameObject camPlayer;
    Camera camStatic1;
    Camera camPlayer1;

    bool isStatic = false;

    void Start (){
        camStatic1 = camStatic.gameObject.GetComponent<Camera>();
        camPlayer1 = camPlayer.transform.gameObject.GetComponent<Camera>();
    }
    void Update()
    {
        if(isStatic){
            camStatic1.enabled = true;
            camPlayer1.enabled = false;
        }
        else if(!isStatic){
            camStatic1.enabled = false;
            camPlayer1.enabled = true;
        }
    }

    public void isCameraOnGoing()
    {
        isStatic = !isStatic;
    }
}
