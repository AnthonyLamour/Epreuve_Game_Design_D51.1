using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CreditsNavigation : MonoBehaviour
{
    //variable publique
    //liste des panels de credits
    public GameObject[] panelCredits;

    //variables privées
    //gamepad dirigeant le menu
    private Gamepad leadGamePad;

    //début du prochain mouvement
    private float nextMove;
    //cooldown entre chaque déplacement dans le menu
    private float cooldown;

    //numéro du panel de credit actuellement affiché
    private int currentCredit;

    //audioSource du tuto
    private AudioSource audioSource;

    // Start est appelé à la première activation de l'objet
    void Start()
    {
        //initialisation de leadGamePad
        leadGamePad = GameObject.FindGameObjectWithTag("LeadPad").GetComponent<SaveLeadPad>().GetLeadGamepad();
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
        if (currentCredit < panelCredits.Length - 1)
        {
            //désactivation du tuto actuel
            panelCredits[currentCredit].SetActive(false);
            //changement du numéro de tuto actuel
            currentCredit++;
            //affichage du tuto actuel
            panelCredits[currentCredit].SetActive(true);
            //lancement du son
            audioSource.Play();
        }
    }

    //fonction permettant d'afficher le tuto précédent
    private void PreviousTuto()
    {
        //si le tuto actuellement affiché n'est pas le premier
        if (currentCredit > 0)
        {
            //désactivation du tuto actuel
            panelCredits[currentCredit].SetActive(false);
            //changement du numéro de tuto actuel
            currentCredit--;
            //affichage du tuto actuel
            panelCredits[currentCredit].SetActive(true);
            //lancement du son
            audioSource.Play();
        }
    }
}
