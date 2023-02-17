using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    // Slider Variables
    public Slider musicSlider;
    public Slider sfxSlider;
    private Slider backgroundMusicSlider;
    private Slider soundEffectsSlider;

    // AudioSource Variables
    public AudioSource backgroundMusicSource;
    public AudioSource menuMusicSource;
    public AudioSource sharkSource;
    public AudioSource powerUpSource;
    public AudioSource coinSound;

    // Start is called before the first frame update
    void Start()
    {
        // Finding component references
        backgroundMusicSlider = GameObject.FindGameObjectWithTag("SettingsBGM").GetComponent<Slider>();
        soundEffectsSlider = GameObject.FindGameObjectWithTag("SettingsSE").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        // Setting the values to equal the value of sliders.
        musicSlider.value = backgroundMusicSlider.value;
        sfxSlider.value = soundEffectsSlider.value;

        backgroundMusicSource.volume = backgroundMusicSlider.value;
        menuMusicSource.volume = backgroundMusicSlider.value;

        sharkSource.volume = soundEffectsSlider.value;
        powerUpSource.volume = soundEffectsSlider.value;
        coinSound.volume = soundEffectsSlider.value;

    }
}
