using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneWithParameter : MonoBehaviour
{
//variables publiques
    //nom de la scène à charger
    public string sceneName;
    //boolean permettant de savoir si mode 2 ou 4 joueurs
    public bool twoPlayer;

    //chargement de la scène
    public void LoadSceneByName()
    {
        //set du mode de jeu 2 ou 4 joueur
        GameObject.FindGameObjectWithTag("LeadPad").GetComponent<SaveLeadPad>().SetTwoPlayer(twoPlayer);
        //chargement de la scène avec le nom donné
        SceneManager.LoadScene(sceneName);
    }
}
