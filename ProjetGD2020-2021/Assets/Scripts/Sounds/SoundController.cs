using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{

    private float SFXVolume;
    private float musicVolume;
    private SoundTest soundTest;
    private AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        SFXVolume = 1;
        musicVolume = 1;
    }

    public void SetSFXVolume(float newValue)
    {
        SFXVolume = newValue;
        soundTest = GameObject.FindGameObjectWithTag("SoundTest").GetComponent<SoundTest>();
        //lancement du sound test
        soundTest.TestSoundSFX(SFXVolume);
    }

    public void SetMusicVolume(float newValue)
    {
        musicVolume = newValue;
        musicSource = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        //application de la modification du volume de la musique
        musicSource.volume = musicVolume;
    }

    public float GetSFXVolume()
    {
        return SFXVolume;
    }

    public float GetMusicVolume()
    {
        return musicVolume;
    }

}
