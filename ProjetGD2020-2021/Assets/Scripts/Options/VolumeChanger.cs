using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeChanger : MonoBehaviour
{

    private GameObject soundManager;

    // Start is called before the first frame update
    void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager");
        if (this.gameObject.tag == "Music")
        {
            if (soundManager != null)
            {
                this.GetComponent<AudioSource>().volume = soundManager.GetComponent<SoundController>().GetMusicVolume();
            }
            
        }
        else
        {
            if (soundManager != null)
            {
                this.GetComponent<AudioSource>().volume = soundManager.GetComponent<SoundController>().GetSFXVolume();
            }
            
        }
    }
}
