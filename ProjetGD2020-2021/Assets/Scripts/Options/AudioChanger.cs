using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioChanger : MonoBehaviour
{

    private GameObject soundManager;

    private void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager");
    }

    public void ChangeAudioValue()
    {
        if (this.gameObject.tag == "MusicSlider")
        {
            soundManager.GetComponent<SoundController>().SetMusicVolume(this.GetComponent<Slider>().value/10);
            
        }
        else
        {
            soundManager.GetComponent<SoundController>().SetSFXVolume(this.GetComponent<Slider>().value/10);
        }
    }
}
