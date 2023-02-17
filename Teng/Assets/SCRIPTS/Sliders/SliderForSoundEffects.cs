using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderForSoundEffects : MonoBehaviour
{
    //Slider Varialbe
    private Slider soundEffectsSlider;
    
    //Audio souces variables
    public AudioSource sharkSource;
    public AudioSource powerUpSource;
    public AudioSource coinSound;


    void Start()
    {
        // Finding component references
        soundEffectsSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        // Setting necessary sound effects volume to equal the slider value
        if(sharkSource.gameObject != null)
        {
            sharkSource.volume = soundEffectsSlider.value;
        }
        if(powerUpSource.gameObject != null)
        {
            powerUpSource.volume = soundEffectsSlider.value;
        }
        coinSound.volume = soundEffectsSlider.value;
    }
}
