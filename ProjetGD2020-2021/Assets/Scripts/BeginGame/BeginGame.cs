using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BeginGame : MonoBehaviour
{
//variable publique
    //liste des sons du compte à rebours
    public AudioClip[] countDown;

    //barre de timer
    public GameObject timerBar;

//variables privées
    //audio source du compte à rebours
    private AudioSource audioSource;

    //liste des textes du compte à rebours
    private string[] countDownText;

    //text du compte à rebours
    private TextMeshProUGUI panelText;
    

    // Start est appelé à la première activation de l'objet
    void Start()
    {
        //changement du timescale à 0 afin que les joueurs ne spawn pas et que le temps ne commence pas à décrémenter lors du compte à rebours
        Time.timeScale = 0;
        //initialisation de audioSource
        audioSource = this.GetComponent<AudioSource>();
        //initialisation de countDownText
        countDownText = new string[4];
        countDownText[0] = "3";
        countDownText[1] = "2";
        countDownText[2] = "1";
        countDownText[3] = "GO !";
        //initialisation de panelText
        panelText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        //lancement de la coroutine de compte à rebours
        StartCoroutine(StartGame());
    }

    //coroutine de compte à rebours
    public IEnumerator StartGame()
    {
        //pour chaque son du compte à rebours
        for(int i=0; i<countDown.Length; i++)
        {
            //attente de 0.5 secondes
            yield return new WaitForSecondsRealtime(0.5f);
            //modification du texte affiché à l'écran
            panelText.text = countDownText[i];
            //set du prochain audio clip
            audioSource.clip = countDown[i];
            //lancement de l'audio
            audioSource.Play();
            //attente de la fin de l'audio clip
            yield return new WaitForSecondsRealtime(audioSource.clip.length);
        }
        //remise du timescale à la normale
        Time.timeScale = 1;
        //activation du timer
        timerBar.SetActive(true);
        //destruction de cet objet
        Destroy(this.gameObject);
        
    }
}
