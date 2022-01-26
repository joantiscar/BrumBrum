using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;

    void Start()
    {
        slider.value = Singleton.volume();
    }


    public void SetLevel(float sliderValue)
    {
        Singleton.setVolume(sliderValue);
        mixer.SetFloat("GeneralVolume", Mathf.Log10(sliderValue) * 20);
    }
}