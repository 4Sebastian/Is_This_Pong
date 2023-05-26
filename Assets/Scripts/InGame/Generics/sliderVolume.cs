using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sliderVolume : MonoBehaviour
{
    public AudioSource musicSource;

    public AudioSource effectsSource;

    public Slider musicSlider;

    public Slider effectsSlider;


    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        effectsSlider.value = PlayerPrefs.GetFloat("effectsVolume");
    }

    public void saveLocalMusicVolume()
    {
        PlayerPrefs.SetFloat("musicVolume", musicSource.volume);
    }

    public void saveLocalEffectsVolume()
    {
        PlayerPrefs.SetFloat("effectsVolume", effectsSource.volume);
    }
}
