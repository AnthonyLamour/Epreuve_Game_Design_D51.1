using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SaveLeadPad : MonoBehaviour
{
//variables privées
    //gamepad dirigeant le menu
    private Gamepad leadGamePad;
    //boolean permettant de savoir si le jeu est en mode 2 ou 4 joueurs
    private bool twoPlayer;

    // Start est appelé à la première activation de l'objet
    private void Start()
    {
        //ne pas détruire cet objet au chargement car il contient des infos importante pour les scènes suivantes
        DontDestroyOnLoad(this.gameObject);
    }

    // Update est appelé à chaque frames
    void Update()
    {
        //si le gamepad dirigeant le menu n'est pas setté
        if (leadGamePad == null)
        {
            //si le bouton a d'un gamepad est pressé
            if (Gamepad.current.aButton.isPressed)
            {
                //set du gamepad dirigeant le menu avec le gamepad actuel
                leadGamePad = Gamepad.current;
            }
        }
    }

    //fonction permettant de récupérer le gamepad dirigeant le menu
    public Gamepad GetLeadGamepad()
    {
        //renvoi de leadGamePad
        return leadGamePad;
    }

    //fonction permettant de setter le mode 2 ou 4 joueurs
    public void SetTwoPlayer(bool newTwoPlayer)
    {
        //set de twoPlayer
        twoPlayer = newTwoPlayer;
    }

    //fonction permettant de récupérer si le jeu est en mode 2 ou 4 joueurs
    public bool GetTwoPlayer()
    {
        //renvoi de twoPlayer
        return twoPlayer;
    }
}
