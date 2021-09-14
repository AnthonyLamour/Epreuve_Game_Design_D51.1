using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
//variables privées
    //objet de score de l'équipe 1
    private ScoreController scoreT1;
    //objet de score de l'équipe 2
    private ScoreController scoreT2;
    
    //boolean permettant de savoir si il s'agit du but de l'équipe 1
    private bool isTeam1;

    //audioSource des buts
    private AudioSource audioSource;

    // Start appelé à la première activation de l'objet
    void Start()
    {
        //initialisation de scoreT1
        scoreT1 = GameObject.FindGameObjectWithTag("TextScoreT1").GetComponent<ScoreController>();
        //initialisation de scoreT2
        scoreT2 = GameObject.FindGameObjectWithTag("TextScoreT2").GetComponent<ScoreController>();

        //si le but est à droite
        if (this.GetComponent<RectTransform>().position.x > 0)
        {
            //initialisation de isTeam1 à faux
            isTeam1 = false;
        }
        //sinon
        else
        {
            //initialisation de isTeam1 à vrai
            isTeam1 = true;
        }

        //initialisation de audioSource
        audioSource = this.GetComponent<AudioSource>();
    }

    //fonction appelé en cas de collision avec un objet
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //si l'objet est une balle
        if (collision.tag == "Ball")
        {
            //si le but est celui de l'équipe 1
            if (isTeam1)
            {
                //augmentation du score de l'équipe 2
                scoreT2.AddScore();
            }
            //sinon
            else
            {
                //augmentation du score de l'équipe 1
                scoreT1.AddScore();
            }
            //lancement du son de but
            audioSource.Play();
        }   
    }

}
