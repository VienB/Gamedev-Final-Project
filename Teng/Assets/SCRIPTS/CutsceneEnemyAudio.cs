using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneEnemyAudio : MonoBehaviour
{
    // AudioClip Variable
    public AudioClip[] enemyAudios;

    // AudioSource Variable
    private AudioSource audioSource;
    // Start is called before the first frame update
    private void Start()
    {
        // Finding component reference
        audioSource = GetComponent<AudioSource>();

        // Randomizing index value
        int clipIndex = Random.Range(0, 3);

        // Setting the audio clip to be played, then play the audio clip
        audioSource.clip = enemyAudios[clipIndex];
        audioSource.Play();
    }
}
