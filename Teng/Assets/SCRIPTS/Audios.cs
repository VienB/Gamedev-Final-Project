using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audios : MonoBehaviour
{
    // AudioClip Variable
    public AudioClip[] audioClips;

    // AudioSource Variable
    private AudioSource audioSources;
    
    // Start is called before the first frame update
    void Start()
    {
        // Finding component reference
        audioSources = GetComponent<AudioSource>();
    }
    // This is for playing DeathSound clip
    public void DeathSound()
    {
        audioSources.clip = audioClips[0];
        audioSources.Play();
    }
    // This is for playing the BumpSound clip
    public void BumpSound()
    {
        int clipIndex = Random.Range(1, 3);
        audioSources.clip = audioClips[clipIndex];
        audioSources.Play();
    }
    // This is for playing the PigSound clip
    public void PigSound()
    {
        audioSources.clip = audioClips[3];
        audioSources.Play();
    }
    // This is for playing the KiteSound clip
    public void KiteSound()
    {
        audioSources.clip = audioClips[4];
        audioSources.Play();
    }
    // This is for playing the SharkSound clip
    public void SharkSound()
    {
        audioSources.clip = audioClips[5];
        audioSources.Play();
    }
    // This is for playing the RandomSound clip
    public void RandomSound()
    {
        int clipIndex = Random.Range(6, 8);
        audioSources.clip = audioClips[clipIndex];
        audioSources.Play();
    }
    // This is for playing the RandomCutscene clip
    public void RandomCutscene()
    {
        int clipIndex = Random.Range(8,11);
        audioSources.clip = audioClips[clipIndex];
        audioSources.Play();
    }
}
