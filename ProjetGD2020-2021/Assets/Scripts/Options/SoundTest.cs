using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour
{
    //soundToTest correspond au tableau des sons à tester
    public AudioClip[] soundToTest;


    //audioSource correspond à l'audio source du sound test
    private AudioSource audioSource;

    //est appeller à l'activation de l'objet
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();

    }

    //permet de changer le volume et enclancher le sound test
    public void TestSoundSFX(float newSFXVolume)
    {
        //reglage du volume
        audioSource.volume = newSFXVolume;
        //selection d'un son random
        int rnd = Random.Range(0, soundToTest.Length);
        audioSource.clip = soundToTest[rnd];
        //lancement du sound test
        audioSource.Play();
    }

}
