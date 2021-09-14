using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoOrFour : MonoBehaviour
{
//variable privée
    //boolean permettant de savoir si le jeu est en mode 2 ou 4 joueurs
    private bool twoPlayer;

    //fonction permettant de setter la valeur de twoPlayer
    public void SetTwoPlayer(bool newTwoPlayer)
    {
        //set de twoPlayer avec la nouvelle valeur
        twoPlayer = newTwoPlayer;
    }

    //fonction permettant de récupérer la valeur de twoPlayer
    public bool GetTwoPlayer()
    {
        //renvoi de twoPlayer
        return twoPlayer;
    }

}
