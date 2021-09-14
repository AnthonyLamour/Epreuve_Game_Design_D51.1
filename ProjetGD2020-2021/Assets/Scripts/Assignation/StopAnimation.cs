using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAnimation : MonoBehaviour
{
    //fonction permettant de stoper une animation
    public void AnimeStop()
    {
        //désactivation de l'animator
        this.GetComponent<Animator>().enabled = false;
    }
}
