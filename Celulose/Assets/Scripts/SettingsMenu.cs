using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Toggle songToggle;
    public Slider volumeSlider;

    void Start()
    {
        // makes sure all the toggles and sliders match their current values

        float tempVol;
        audioMixer.GetFloat("volume", out tempVol);
        volumeSlider.value = Mathf.Pow(10,tempVol / 20);

        audioMixer.GetFloat("chip", out tempVol);
        if (tempVol == 0)
        {
            // if chiptune is on, set the toggle to on
            songToggle.isOn = true;
        }
        else
        {
            // if chiptune is off, set the toggle to off
            songToggle.isOn = false;
        }


    }

    public void setVolume(float volume)
    {
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }

    public void songChoice(bool choice)
    {
        if (choice)
        {
            audioMixer.SetFloat("chip", 0);
            audioMixer.SetFloat("hq",-80);
        }
        else
        {
            audioMixer.SetFloat("chip", -80);
            audioMixer.SetFloat("hq", 0);
        }
    }
}
