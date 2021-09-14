using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
//variable privé
    //score actuel de l'équipe
    private int currentScore;

    //texte de score
    private TextMeshProUGUI scoreText;

    // Start est appelé à la première activation de l'objet
    private void Start()
    {
        //initialisation du score à 0
        currentScore = 0;
        //initialisation de scoreText
        scoreText = this.GetComponent<TextMeshProUGUI>();

        //initialisation du texte de score
        scoreText.text = "0" + currentScore.ToString();
    }

    //fonction permettant d'ajouter du score
    public void AddScore()
    {
        //incrémentation du score
        currentScore++;
        //si le score est inférieur à 10
        if (currentScore < 10)
        {
            //mise à jour du texte de score
            scoreText.text = "0" + currentScore.ToString();
        }
        //sinon
        else
        {
            //mise à jour du texte de score
            scoreText.text = currentScore.ToString();
        }
        
    }

    //fonction permettant de récupérer le score actuel de l'équipe
    public int GetScore()
    {
        //renvoi du score de l'équipe
        return currentScore;
    }
}
