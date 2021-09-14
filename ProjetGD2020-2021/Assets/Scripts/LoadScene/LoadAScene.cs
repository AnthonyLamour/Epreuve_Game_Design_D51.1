using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAScene : MonoBehaviour
{
//variable publique
    //nom de la scène à charger
    public string sceneName;

    //fonction permettant de charger la scène
    public void LoadSceneByName()
    {
        //chargement de la scène du nom donné
        SceneManager.LoadScene(sceneName);
    }
}
