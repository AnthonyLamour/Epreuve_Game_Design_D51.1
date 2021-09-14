using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChoseWinner : MonoBehaviour
{
//variables privées
    //objet de score de l'équipe 1
    private ScoreController scoreT1;
    //objet de score de l'équipe 2
    private ScoreController scoreT2;

    // Start est appelé à la première activation de l'objet
    void Start()
    {
        //initialisation de scoreT1
        scoreT1 = GameObject.FindGameObjectWithTag("TextScoreT1").GetComponent<ScoreController>();
        //initialisation de scoreT2
        scoreT2 = GameObject.FindGameObjectWithTag("TextScoreT2").GetComponent<ScoreController>();

        //si le score de l'équipe 1 est supérieur à celui de l'équipe 2
        if (scoreT1.GetScore() > scoreT2.GetScore())
        {
            //afficher les bleus ont gagnés
            transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Blues win !";
        }
        //sinon
        else
        {
            //afficher les rouges ont gagnés
            transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Reds win !";
        }
    }

}
