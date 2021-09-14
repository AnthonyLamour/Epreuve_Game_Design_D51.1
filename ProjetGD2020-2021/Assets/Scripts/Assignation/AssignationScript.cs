using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class AssignationScript : MonoBehaviour
{
    public GameObject CostumeJ1;
    public GameObject CostumeJ2;

//variables privées
    //textmesh des différents boutons de joueurs
    private TextMeshProUGUI textMeshProJ1;
    private TextMeshProUGUI textMeshProJ2;

    //Animator des boutons des différents joueurs
    private Animator buttonJ1Animator;
    private Animator buttonJ2Animator;

    //gamepad de chaque joueurs
    private Gamepad GamepadPlayer1;
    private Gamepad GamepadPlayer2;

    private Sprite chosenCostumeJ1;
    private Sprite chosenCostumeJ2;

    //début du prochain mouvement
    private float nextMove;
    //cooldown entre chaque déplacement dans le menu
    private float cooldown;

    //boolean permettant de valider le test avec une manette
    private bool devMode;
    //booleans permettant de savoir si les 2 touches pour activer le test à une manette sont pressées
    private bool key1;
    private bool key2;
    private bool player1Ready;
    private bool player2Ready;

    //audioSource de menu d'assignation
    private AudioSource audioSource;

    // Start est appelé à la première activation de l'objet
    void Start()
    {
        //ne pas détruire l'objet pendant les chargements car il contient des infos nécessaire au autres scènes
        DontDestroyOnLoad(this.gameObject);

        //initialisation de devMode à faux
        devMode = false;

        //indication que le mode de jeu est 4 joueurs et non 2
        this.GetComponent<TwoOrFour>().SetTwoPlayer(true);

        //initialisation de buttonJ1Animator
        buttonJ1Animator = GameObject.FindGameObjectWithTag("BoutonAssignationJ1").GetComponent<Animator>();
        //initialisation de buttonJ2Animator
        buttonJ2Animator = GameObject.FindGameObjectWithTag("BoutonAssignationJ2").GetComponent<Animator>();

        //initialisation de textMexhProJ1
        textMeshProJ1 = GameObject.FindGameObjectWithTag("BoutonAssignationJ1").GetComponent<Transform>().GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        //initialisation de textMexhProJ2
        textMeshProJ2 = GameObject.FindGameObjectWithTag("BoutonAssignationJ2").GetComponent<Transform>().GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();

        //si la première touche est pressée ou relachée modification de key1
        this.GetComponent<PlayerInput>().actions["Key1"].performed += context => key1 = true;
        this.GetComponent<PlayerInput>().actions["Key1"].canceled += context => key1 = false;
        //si la deuxième touche est pressée ou relachée modification de key2
        this.GetComponent<PlayerInput>().actions["Key2"].performed += context => key2 = true;
        this.GetComponent<PlayerInput>().actions["Key2"].canceled += context => key2 = false;

        //initialisation du cooldown
        cooldown = 0.2f;
        //initialisation du début du prochain mouvement
        nextMove = 0;

        player1Ready = false;
        player2Ready = false;

        //initialisation de audioSource
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update appelé à chaque frame
    void Update()
    {
        //si les 2 touches ont été pressées
        if (key1 && key2)
        {
            //activation du mode 1 manette
            devMode = true;
        }

        //si le mode 1 manette n'est pas activé
        if (!devMode)
        {
            //lorsqu'un gamepad utilise sa touche x
            if (Gamepad.current.xButton.wasPressedThisFrame)
            {
                //si le gamepad du joueur 1 n'a pas été setté
                if (GamepadPlayer1 == null)
                {
                    //assignation du gamepad
                    GamepadPlayer1 = AssignGamePad(textMeshProJ1, buttonJ1Animator);
                    CostumeJ1.SetActive(true);
                    
                }
                else if (GamepadPlayer1 == Gamepad.current && player1Ready == false)
                {
                    chosenCostumeJ1 = SaveCurrentCostume(textMeshProJ1,CostumeJ1,buttonJ1Animator);
                    player1Ready = true;
                    //vérification des gamepads
                    CheckGamePads();
                }
                //sinon si le gamepad actuel n'est pas celui du joueur 1 et que le gamepad du joueur 2 n'a pas été setté
                else if (GamepadPlayer1 != Gamepad.current && GamepadPlayer2 == null)
                {
                    //assignation du gamepad
                    GamepadPlayer2 = AssignGamePad(textMeshProJ2, buttonJ2Animator);
                    CostumeJ2.SetActive(true);
                }
                else if (GamepadPlayer2 == Gamepad.current && player2Ready == false)
                {
                    chosenCostumeJ2 = SaveCurrentCostume(textMeshProJ2, CostumeJ2, buttonJ2Animator);
                    player2Ready = true;
                    //vérification des gamepads
                    CheckGamePads();
                }

            }
        }
        //si le mode 1 manette est activé
        else
        {
            //lorsqu'un gamepad utilise sa touche x
            if (Gamepad.current.xButton.wasPressedThisFrame)
            {
                //si le gamepad du joueur 1 n'a pas été setté
                if (GamepadPlayer1 == null)
                {
                    //assignation du gamepad
                    GamepadPlayer1 = AssignGamePad(textMeshProJ1, buttonJ1Animator);
                    CostumeJ1.SetActive(true);
                }
                else if (GamepadPlayer1 == Gamepad.current && player1Ready == false)
                {
                    chosenCostumeJ1 = SaveCurrentCostume(textMeshProJ1, CostumeJ1, buttonJ1Animator);
                    player1Ready = true;
                    //vérification des gamepads
                    CheckGamePads();
                }
                //sinon si le gamepad du joueur 2 n'a pas été setté
                else if (GamepadPlayer2 == null)
                {
                    //assignation du gamepad
                    GamepadPlayer2 = AssignGamePad(textMeshProJ2, buttonJ2Animator);
                    CostumeJ2.SetActive(true);
                }
                else if (GamepadPlayer2 == Gamepad.current && player2Ready == false)
                {
                    chosenCostumeJ2 = SaveCurrentCostume(textMeshProJ2, CostumeJ2, buttonJ2Animator);
                    player2Ready = true;
                    //vérification des gamepads
                    CheckGamePads();
                }
            }
        }

        if (GamepadPlayer1 != null && player1Ready == false)
        {
            if (GamepadPlayer1.leftStick.left.ReadValue() > 0.5 && Time.time > nextMove)
            {
                //set du début du prochain mouvement
                nextMove = Time.time + cooldown;
                CostumeJ1.GetComponent<CostumeChoice>().ShowPreviousCostume();
            }
            else if (GamepadPlayer1.leftStick.right.ReadValue() > 0.5 && Time.time > nextMove)
            {
                //set du début du prochain mouvement
                nextMove = Time.time + cooldown;
                CostumeJ1.GetComponent<CostumeChoice>().ShowNextCostume();
            }
        }

        if (GamepadPlayer2 != null && player2Ready == false)
        {
            if (GamepadPlayer2.leftStick.left.ReadValue() > 0.5 && Time.time > nextMove)
            {
                //set du début du prochain mouvement
                nextMove = Time.time + cooldown;
                CostumeJ2.GetComponent<CostumeChoice>().ShowPreviousCostume();
            }
            else if (GamepadPlayer2.leftStick.right.ReadValue() > 0.5 && Time.time > nextMove)
            {
                //set du début du prochain mouvement
                nextMove = Time.time + cooldown;
                CostumeJ2.GetComponent<CostumeChoice>().ShowNextCostume();
            }
        }

    }

    //fonction permettant de récupérer le gamepad assigné au joueur 1
    public Gamepad GetGamePadPlayer1()
    {
        //renvoi du gamepad du joueur 1
        return GamepadPlayer1;
    }

    //fonction permettant de récupérer le gamepad assigné au joueur 2
    public Gamepad GetGamePadPlayer2()
    {
        //renvoi du gamepad du joueur 2
        return GamepadPlayer2;
    }

    //fonction permettant de récupérer le costume du joueur 1
    public Sprite GetCostumeJ1()
    {
        return chosenCostumeJ1;
    }

    //fonction permettant de récupérer le costume du joueur 2
    public Sprite GetCostumeJ2()
    {
        return chosenCostumeJ2;
    }

    //fonction permettant d'assigner un gamepad
    private Gamepad AssignGamePad(TextMeshProUGUI textToChange, Animator buttonAnimator)
    {
        //modification du text du bouton
        textToChange.text = "Chose your skin";
        //activation de l'animation du bouton
        buttonAnimator.enabled = true;
        //lancement du son de validation
        audioSource.Play();
        //renvoi du gamePad actuel
        return Gamepad.current;
    }

    //fonction permettant d'enregistrer le costume du joueur
    private Sprite SaveCurrentCostume(TextMeshProUGUI textToChange,GameObject costume, Animator buttonAnimator)
    {
        //modification du text du bouton
        textToChange.text = "OK !";
        //activation de l'animation du bouton
        buttonAnimator.enabled = true;
        //lancement du son de validation
        audioSource.Play();
        //renvoi du gamePad actuel
        return costume.GetComponent<CostumeChoice>().GetCurrentCostume();
    }

    //fonction permettant de vérifiier si tout les gamepads sont assignés
    private void CheckGamePads()
    {
        //si les gamepads sont settés
        if (player1Ready == true && player2Ready == true)
        {
            //destruction de l'objet tenant la musique actuelle
            Destroy(GameObject.FindGameObjectWithTag("Music"));
            //chargement du niveau
            SceneManager.LoadScene("MainLevel");
        }
    }

    
}
