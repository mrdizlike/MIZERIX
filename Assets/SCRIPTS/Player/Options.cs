using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public Look Look_Script;

    public AudioMixer Mixer;

    public Slider MouseSensetivity_Slider;
    public Slider Sound_Slider;

    public bool isMenu;

    void Start()
    {
        float musicVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        Sound_Slider.value = musicVolume;
        Mixer.SetFloat("MasterVolume", Mathf.Log10(musicVolume) * 20);

        float sensetivityValue = PlayerPrefs.GetFloat("Sensitivity", MouseSensetivity_Slider.value);
        MouseSensetivity_Slider.value = sensetivityValue;
    }

    public void Update()
    {
        if(!isMenu)
        {           
            Look_Script.xSensitivity = MouseSensetivity_Slider.value;
            Look_Script.ySensitivity = MouseSensetivity_Slider.value;
        }
    }

    public void ChangeMasterVolume(float value)
    {
        Mixer.SetFloat("MainAudio", Mathf.Log10(value) * 20);
    }

    public void ChangeSensitivity(float value)
    {
        PlayerPrefs.SetFloat("Sensitivity", value);
    }
}
