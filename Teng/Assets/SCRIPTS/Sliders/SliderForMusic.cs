using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderForMusic : MonoBehaviour
{
    // Slider Variables
    private Slider backgroundMusicSlider;

    // AudioSource Variables
    private AudioSource backgroundMusicSource;
    private AudioSource menuMusicSource;

    // Start is called before the first frame update
    void Start()
    {
        // Finding component references
        backgroundMusicSlider = GetComponent<Slider>();
        backgroundMusicSource = GameObject.FindGameObjectWithTag("BackgroundMusic").GetComponent<AudioSource>();
        menuMusicSource = GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Setting the values to equal the value of sliders
        backgroundMusicSource.volume = backgroundMusicSlider.value;
        menuMusicSource.volume = backgroundMusicSlider.value;
    }
}
