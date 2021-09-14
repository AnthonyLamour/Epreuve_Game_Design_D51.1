using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler
{
//variables privées
    //commande de joystick up
    private ICommand buttonJU_;
    //commande de joystick right
    private ICommand buttonJR_;
    //commande de joystick left
    private ICommand buttonJL_;
    //commande de bouton de tire pressé
    private ICommand buttonAR_;
    //commande de trigger right
    private ICommand buttonRTrigger_;
    //commande de bouton de tire relacher
    private ICommand buttonARUP_;

    //gamepad du joueur
    private Gamepad playerPad;

    //boolean permettant de savoir si la touche de tire est pressée
    private bool firePessed;
    //boolean permattant de savoir si la touche de tire rapide est pressé
    private bool rapidFirePessed;

    //constructeur de la class
    public InputHandler(Gamepad newPlayerPad)
    {
        //initialisation du gamepad du joueur
        playerPad = newPlayerPad;
        //initialisation de firePressed à faux
        firePessed = false;
        //initialisation de rapidFirePressed à faux
        rapidFirePessed = false;
    }

    //fonction permettant d'initialiser la liste des commandes
    public void SetCommand()
    {
        //joystick up lié à jetpack
        buttonJU_ = new CommandJetPack();
        //joysitck right lié à aller à droite
        buttonJR_ = new CommandRight();
        //joystick left lié à aller à gauche
        buttonJL_ = new CommandLeft();
        //bouton de tire lié au tire normale
        buttonAR_ = new CommandFire();
        //trigger right lié au tire rapide
        buttonRTrigger_ = new CommandRapidFire();
        //boutn de tire relacher lié au tire chargé
        buttonARUP_ = new CommandChargeFire();
    }

    //fonction permettant de gérer les inputs du joueur
    public List<ICommand> handleInput()
    {
        //liste de commande a exécuter
        List<ICommand> commandList = new List<ICommand>() { };

        //si joystick up
        if (playerPad.leftStick.up.ReadValue()>0.5)
        {
            //ajout de command joystick up
            commandList.Add(buttonJU_);
        }
        //si joystick left
        if (playerPad.leftStick.left.ReadValue()>0.5)
        {
            //ajout de command joystick left
            commandList.Add(buttonJL_);
        }
        //si joystick right
        if (playerPad.leftStick.right.ReadValue()>0.5)
        {
            //ajout de command joystick right
            commandList.Add(buttonJR_);
        }
        //si bouton X pressé
        if (playerPad.xButton.isPressed && !firePessed)
        {
            //ajout de command bouton de tire
            commandList.Add(buttonAR_);
            //set de firePressed à vrai
            firePessed = true;
        }
        //si bouton R1 pressé
        if (playerPad.rightShoulder.isPressed && !rapidFirePessed)
        {
            //ajout de command bouton trigger right
            commandList.Add(buttonRTrigger_);
            //set de rapidFirePressed à vrai
            rapidFirePessed = true;
        }
        //si bouton R1 relaché
        if (!playerPad.rightShoulder.isPressed && rapidFirePessed)
        {
            //ajout de command bouton trigger right
            commandList.Add(buttonRTrigger_);
            //set de rapidFirePressed à faux
            rapidFirePessed = false;
        }
        //si bouton X relaché
        if (!playerPad.xButton.isPressed && firePessed)
        {
            //ajout de command bouton tire relaché
            commandList.Add(buttonARUP_);
            //set de firePressed à faux
            firePessed = false;
        }

        //renvoi de la liste de commande
        return commandList;

    }
}
