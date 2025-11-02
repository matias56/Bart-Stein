using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI; // Only needed if you want to set the initial slider value in code

public class VolumeController : MonoBehaviour
{
    // Assign your Audio Mixer in the Inspector
    public AudioMixer masterMixer;
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
        LoadMusicVolume();
        LoadSFXVolume();
    }

    // The function to be called by the slider's OnValueChanged event
    public void SetMusicVolume(float sliderValue)
    {
        // Use logarithmic conversion for human-perceived volume
        masterMixer.SetFloat("Music Vol", Mathf.Log10(sliderValue) * 20);
    }

    public void SetSFXVolume(float sliderValue)
    {
        // Use logarithmic conversion for human-perceived volume
        masterMixer.SetFloat("SFX Vol", Mathf.Log10(sliderValue) * 20);
    }

    public void LoadMusicVolume()
    {
        float value;
        // Get the current volume level from the Audio Mixer (in decibels)
        if (masterMixer.GetFloat("Music Vol", out value))
        {
            // Convert the Decibel value (logarithmic) back to a Slider value (linear 0.0001 to 1)
            // Note: Use a default value of 1 if conversion fails to avoid errors
            musicSlider.value = Mathf.Pow(10, value / 20);
        }
    }

    public void LoadSFXVolume()
    {
        float value;
        // Get the current volume level from the Audio Mixer (in decibels)
        if (masterMixer.GetFloat("SFX Vol", out value))
        {
            // Convert the Decibel value (logarithmic) back to a Slider value (linear 0.0001 to 1)
            // Note: Use a default value of 1 if conversion fails to avoid errors
            sfxSlider.value = Mathf.Pow(10, value / 20);
        }
    }
}