using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSensitivity : MonoBehaviour
{
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = Singleton.sensitivity();
    }

    // Update is called once per frame
    public void SetSensitivity(float sliderValue)
    {
        Singleton.setSensitivity(sliderValue);
        ThirdPersonMovement player = GameObject.FindObjectOfType<ThirdPersonMovement>();
        if (player != null) player.changeSensitivity();
    }
}
