using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    
    public  AudioSource audioSource;
    public AudioSource mainMusic;
    
    public AudioClip[] audioClips;
  
    public  void PlaySkipBoard()
    {
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }
    public void PlayLost()
    {
        mainMusic.Stop();
        audioSource.clip = audioClips[3];
        audioSource.Play();
    }
    public void PlayWin()
    {
        mainMusic.Stop();
        audioSource.clip = audioClips[2];
        audioSource.Play();
    }
    public void PlayPlatformHit()
    {
        audioSource.clip = audioClips[6];
        audioSource.Play();
    }
}
