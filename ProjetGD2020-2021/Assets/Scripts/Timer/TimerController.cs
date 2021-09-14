using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{
//variables publiques
    //panel de gameover
    public GameObject panelGameOver;
    //objet à désactiver en fin de partie
    public GameObject[] objectToStop;
    public GameObject[] bonusList;

    public RectTransform bonusSpawnT1;
    public RectTransform bonusSpawnT2;

//variables privées
    //temps de jeu total
    private float totalTime;
    //temps restant
    private float timeLeft;
    private float spawnNextBonus;
    private float bonusDelay;

    //scale de base de la barre de timer
    private Vector2 baseScale;

    //RectTransform de la barre de timer
    private RectTransform timerRectTransform;

    // Start est appelé à la première activation de l'objet
    void Start()
    {
        //initialisaiton du temps de jeu total
        totalTime = 5 * 60;
        //totalTime = 30;
        //initialisaiton du temps restant
        timeLeft = totalTime;
        //initialisation de timerRectTransform
        timerRectTransform = this.GetComponent<RectTransform>();
        //initialisation du scale de base de la barre de timer
        baseScale = timerRectTransform.localScale;
        bonusDelay = 30;
        spawnNextBonus = Time.time + bonusDelay;
    }

    // Update est appelé à chaque frames
    void Update()
    {
        //modification du temps restant
        timeLeft = totalTime - Time.time;
        //si il reste du temps
        if (timeLeft > 0)
        {
            //mise a jour du scale de la barre de timer
            timerRectTransform.localScale = new Vector2((baseScale.x * timeLeft) / totalTime,
                                                         timerRectTransform.localScale.y);
            if (Time.time > spawnNextBonus)
            {
                int rndBonus = Random.Range(0, bonusList.Length);
                Instantiate(bonusList[rndBonus], bonusSpawnT1);
                Instantiate(bonusList[rndBonus], bonusSpawnT2);
                spawnNextBonus = Time.time + bonusDelay;
            }
        }
        //sinon
        else
        {
            //affichage du panel de gameover
            panelGameOver.SetActive(true);
            //pour chaque objet à désactiver
            for(int i=0; i < objectToStop.Length; i++)
            {
                //désactivation de l'objet
                objectToStop[i].SetActive(false);
            }
        }
        
    }
}
