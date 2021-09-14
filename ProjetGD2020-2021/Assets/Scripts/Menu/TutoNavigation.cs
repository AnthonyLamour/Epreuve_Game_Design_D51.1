using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TutoNavigation : MonoBehaviour
{
//variable publique
    //liste des panels de tuto
    public GameObject[] panelTuto;

//variables privées
    //gamepad dirigeant le menu
    private Gamepad leadGamePad;

    //début du prochain mouvement
    private float nextMove;
    //cooldown entre chaque déplacement dans le menu
    private float cooldown;

    //boolean permettant de savoir si le jeu est en mode 2 ou 4 joueurs
    private bool twoPlayer;

    //numéro du panel de tutoriel actuellement affiché
    private int currentTuto;

    //audioSource du tuto
    private AudioSource audioSource;

    // Start est appelé à la première activation de l'objet
    void Start()
    {
        //initialisation de leadGamePad
        leadGamePad = GameObject.FindGameObjectWithTag("LeadPad").GetComponent<SaveLeadPad>().GetLeadGamepad();
        //initialisation du mode 2 ou 4 joueurs
        twoPlayer = GameObject.FindGameObjectWithTag("LeadPad").GetComponent<SaveLeadPad>().GetTwoPlayer();
        //destruction de l'objet stockant les information précédemment listées
        Destroy(GameObject.FindGameObjectWithTag("LeadPad").gameObject);
        //initialisation du cooldown
        cooldown = 0.2f;
        //initialisation du début du prochain mouvement
        nextMove = 0;
        //initialisation de audioSource
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update est appelé à chaque frames
    void Update()
    {
        //si le bouton a est pressé
        if (leadGamePad.aButton.isPressed && Time.time > nextMove)
        {
            //set du début du prochain mouvement
            nextMove = Time.time + cooldown;
            //affichage du prochain tuto
            NextTuto();
        }
        //sinon si le bouton b est pressé
        else if (leadGamePad.bButton.isPressed && Time.time > nextMove)
        {
            //set du début du prochain mouvement
            nextMove = Time.time + cooldown;
            //affichage du tuto précédent
            PreviousTuto();
        }
        //sinon si le bouton start est pressé
        else if (leadGamePad.startButton.isPressed)
        {
            //retour au menu principale
            SceneManager.LoadScene("MainMenu");
        }
    }

    //fonction permettant d'afficher le prochain tuto
    private void NextTuto()
    {
        //si le tuto actuel n'est pas le dernier
        if (currentTuto < panelTuto.Length - 1)
        {
            //désactivation du tuto actuel
            panelTuto[currentTuto].SetActive(false);
            //changement du numéro de tuto actuel
            currentTuto++;
            //affichage du tuto actuel
            panelTuto[currentTuto].SetActive(true);
            //lancement du son
            audioSource.Play();
        }
        //sinon
        else
        {
            //si le mode de jeu est 2 joueurs
            if (twoPlayer)
            {
                //chargement de la scène d'assignation pour 2 joueurs
                SceneManager.LoadScene("Assignation2Player");
            }
            //sinon
            else
            {
                //chargement de la scène d'assignation pour 4 joueurs
                SceneManager.LoadScene("Assignation4Player");
            }
        }
    }

    //fonction permettant d'afficher le tuto précédent
    private void PreviousTuto()
    {
        //si le tuto actuellement affiché n'est pas le premier
        if (currentTuto > 0)
        {
            //désactivation du tuto actuel
            panelTuto[currentTuto].SetActive(false);
            //changement du numéro de tuto actuel
            currentTuto--;
            //affichage du tuto actuel
            panelTuto[currentTuto].SetActive(true);
            //lancement du son
            audioSource.Play();
        }
    }
}
