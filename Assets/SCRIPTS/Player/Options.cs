using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public Look Look_Script;

    public Slider MouseSensetivity_Slider;
    public Slider Sound_Slider;

    void Start()
    {
        Sound_Slider.onValueChanged.AddListener(val => ChangeMasterVolume(val));
    }

    void Update()
    {
        Look_Script.xSensitivity = MouseSensetivity_Slider.value;
        Look_Script.ySensitivity = MouseSensetivity_Slider.value;
    }

    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }
}
