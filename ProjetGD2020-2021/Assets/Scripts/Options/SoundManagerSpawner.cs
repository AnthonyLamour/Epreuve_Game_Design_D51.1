using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerSpawner : MonoBehaviour
{

    public GameObject soundManagerObj;
    public GameObject musicObj;

    // Start is called before the first frame update
    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("SoundManager").Length == 0)
        {
            Instantiate(soundManagerObj);
        }

        if (GameObject.FindGameObjectsWithTag("Music").Length == 0)
        {
            Instantiate(musicObj);
        }

    }
}
