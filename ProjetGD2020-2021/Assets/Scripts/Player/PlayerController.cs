using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
//variables publiques
    //observer du joueur
    public GameObject observer;
    
    //position de spawn du joueur
    public Transform spawnPos;
    
    //numéro du joueur
    public int number;

//variables privées
    //boolean permettant de savoir si le joueur est en mouvement
    private bool isMoving;
    //boolean permettant de savoir si le joueur est en train de respawn
    private bool isRespawning;
    private bool initialRespawn;
    private bool isDomageBuff;

    //rigidBody2D du joueur
    private Rigidbody2D playerRigidBody2D;

    //inputhandler du joueur
    private InputHandler PlayerInpuHandler;
    
    //début du prochain tire chargé
    private float nextChargeShot;
    //délai de respawn du joueur
    private float respawnDelay;
    //temps de fin de respawn du joueur
    private float endRespawnTime;
    //vitesse du joueur
    private float speed;
    //force de saut du joueur
    private float jumpForce;
    //vitesse de tire du joueur
    private float fireSpeed;
    //vitesse de tire rapide du joueur
    private float rapidFireSpeed;
    //délai de chargement du tire charger du joueur
    private float chargingDelay;
    //vitesse du tire chargé du joueur
    private float chargeFireSpeed;
    private float domageBuffDuration;
    private float domageBuffEnd;

    //image du corps du joueur
    private Image playerBody;
    //image de la roue du joueur
    private Image playerWheel;

    //animator de la roue du joueur
    private Animator playerWheelAnimator;

    //system de particule du corps du joueur
    private ParticleSystem playerBodyParticle;

    //gamepad du joueur
    private Gamepad playerGamepad;

    //RectTransform du joueur
    private RectTransform playerRectTransform;

    // Start est appelé à la première activation de l'objet
    void Start()
    {
        //on fonction du numéro du joueur
        switch (number)
        {
            //si joueur 1
            case 1:
                //si mode 2 joueur
                if (GameObject.FindGameObjectWithTag("Assignation").GetComponent<TwoOrFour>().GetTwoPlayer())
                {
                    //assignation du gamepad
                    playerGamepad = GameObject.FindGameObjectWithTag("Assignation").GetComponent<AssignationScript>().GetGamePadPlayer1();
                    this.transform.GetChild(2).GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Assignation").GetComponent<AssignationScript>().GetCostumeJ1();
                }
                //sinon
                else
                {
                    //assignation du gamepad
                    playerGamepad = GameObject.FindGameObjectWithTag("Assignation").GetComponent<Assignation4players>().GetGamePadPlayer1();
                    this.transform.GetChild(2).GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Assignation").GetComponent<Assignation4players>().GetCostumeJ1();
                }
                break;
            //si joueur 2
            case 2:
                //si mode 2 joueur
                if (GameObject.FindGameObjectWithTag("Assignation").GetComponent<TwoOrFour>().GetTwoPlayer())
                {
                    //assignation du gamepad
                    playerGamepad = GameObject.FindGameObjectWithTag("Assignation").GetComponent<AssignationScript>().GetGamePadPlayer2();
                    this.transform.GetChild(2).GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Assignation").GetComponent<AssignationScript>().GetCostumeJ2();
                }
                //sinon
                else
                {
                    //assignation du gamepad
                    playerGamepad = GameObject.FindGameObjectWithTag("Assignation").GetComponent<Assignation4players>().GetGamePadPlayer3();
                    this.transform.GetChild(2).GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Assignation").GetComponent<Assignation4players>().GetCostumeJ3();
                }
                break;
            //si joueur 3
            case 3:
                //si mode 2 joueur
                if (GameObject.FindGameObjectWithTag("Assignation").GetComponent<TwoOrFour>().GetTwoPlayer())
                {
                    //désactivation du joueur
                    this.gameObject.SetActive(false);
                }
                //sinon
                else
                {
                    //assignation du gamepad
                    playerGamepad = GameObject.FindGameObjectWithTag("Assignation").GetComponent<Assignation4players>().GetGamePadPlayer2();
                    this.transform.GetChild(2).GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Assignation").GetComponent<Assignation4players>().GetCostumeJ2();
                }
                break;
            //si joueur 4
            case 4:
                //si mode 2 joueur
                if (GameObject.FindGameObjectWithTag("Assignation").GetComponent<TwoOrFour>().GetTwoPlayer())
                {
                    //désactivation du joueur
                    this.gameObject.SetActive(false);
                }
                //sinon
                else
                {
                    //assignation du gamepad
                    playerGamepad = GameObject.FindGameObjectWithTag("Assignation").GetComponent<Assignation4players>().GetGamePadPlayer4();
                    this.transform.GetChild(2).GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Assignation").GetComponent<Assignation4players>().GetCostumeJ4();
                }
                break;
        }

        //si le joueur est encore actif
        if (this.gameObject.activeInHierarchy)
        {
            //initialisation de speed
            speed = 5;
            //initialisation de jumpForce
            jumpForce = 7;
            //initialisation de fireSpeed
            fireSpeed = 10;
            //initialisation de rapidFireSpeed
            rapidFireSpeed = 0.1f;
            //initialisation de chargingDelay
            chargingDelay = 1;
            //initialisation de chargeFireSpeed
            chargeFireSpeed = 5;
            //initialisation du début du prochain tire chargé
            nextChargeShot = 0;
            //initialisation du délai de respawn
            respawnDelay = 3;
            //initialisation de endRespawnTime
            endRespawnTime = Time.time + respawnDelay;

            //initialisation de PlayerInputHandler
            PlayerInpuHandler = new InputHandler(playerGamepad);
            //initialisation des commandes de PlayerInpurHandler
            PlayerInpuHandler.SetCommand();

            //initialisation de isMoving à faux
            isMoving = false;
            //initialisation de isRespawning à vrai
            isRespawning = false;
            initialRespawn = true;

            //initialisation de la position du joueur
            transform.position = spawnPos.position;

            //initialisation de playerRigidBody2D
            playerRigidBody2D = this.GetComponent<Rigidbody2D>();

            //initialisation de playerBody
            playerBody = this.transform.GetChild(1).GetComponent<Image>();
            //initialisation de playerWheel
            playerWheel = this.transform.GetChild(0).GetComponent<Image>();

            //initialisation de playerWheel
            playerWheelAnimator = this.transform.GetChild(0).gameObject.GetComponent<Animator>();

            //initialisation de playerBodyParticle
            playerBodyParticle = this.transform.GetChild(1).GetComponent<ParticleSystem>();

            //initialisation de playerRectTransform
            playerRectTransform = this.GetComponent<RectTransform>();

            isDomageBuff = false;
            domageBuffDuration = 5;
            domageBuffEnd = 0;

            //désactivation de la gravité sur le joueur
            playerRigidBody2D.gravityScale = 0;
            //début du clipping de respawn du joueur
            StartCoroutine(clipping());
        }
        
    }

    // Update est appelé toutes les frames
    void Update()
    {
        //si le joueur respawn
        if (initialRespawn)
        {
            //si le joueur a fini de respawn
            if (Time.time > endRespawnTime)
            {
                //arrêt de la coroutine
                this.StopAllCoroutines();
                //réaffichage du joueur
                playerWheel.enabled = true;
                playerBody.enabled = true;
                this.transform.GetChild(2).GetComponent<Image>().enabled = true;
                //réactivation de la gravité sur le joueur
                playerRigidBody2D.gravityScale = 1;
                initialRespawn = false;
            }
            
        }
        //sinon
        else
        {


            if (isDomageBuff && Time.time > domageBuffEnd)
            {
                observer.GetComponent<ObserverScript>().SetNewFireColor(0);
                isDomageBuff = false;
            }

            if (isRespawning)
            {

                //si le joueur a fini de respawn
                if (Time.time > endRespawnTime)
                {
                    //arrêt de la coroutine
                    this.StopAllCoroutines();
                    //réaffichage du joueur
                    playerWheel.enabled = true;
                    playerBody.enabled = true;
                    this.transform.GetChild(2).GetComponent<Image>().enabled = true;
                    //set de isRespawning à faux
                    isRespawning = false;
                }

            }

            //si le joueur chute
            if (playerRigidBody2D.velocity.y < -0.1)
            {
                //arrêt des particules de jetpack
                playerBodyParticle.Stop();
            }

            //récupération des commandes
            List<ICommand> command = PlayerInpuHandler.handleInput();

            //si des commandes ont été rentrées
            if (command.Count != 0)
            {
                //pour chaque commande
                for (var i = 0; i < command.Count; i++)
                {
                    //si il s'agit d'un jetpack
                    if (command[i].GetType() == typeof(CommandJetPack))
                    {
                        //exécution de la commande jetpack
                        command[i].execute(this.gameObject, jumpForce);
                        //activation des particules de jetpack
                        playerBodyParticle.Play();
                    }
                    //sinon si il s'agit d'une commande de tire
                    else if (command[i].GetType() == typeof(CommandFire))
                    {
                        //mise a jour du temps de chargement du tire chargé
                        nextChargeShot = Time.time + chargingDelay;
                        this.transform.GetChild(3).GetComponent<ChargeShotFeedback>().BeginCharge();
                        //exécution du tire en fonction de l'orientation du joueur
                        if (playerRectTransform.rotation.y == 0)
                        {
                            command[i].execute(observer, fireSpeed);
                        }
                        else
                        {
                            command[i].execute(observer, -fireSpeed);
                        }

                    }
                    //sinon si il s'agit d'une commande de tire rapide
                    else if (command[i].GetType() == typeof(CommandRapidFire))
                    {
                        //exécution de la commande de tire rapide
                        command[i].execute(observer, rapidFireSpeed);
                    }
                    //sinon si il s'agit d'une commande de tire chargé
                    else if (command[i].GetType() == typeof(CommandChargeFire))
                    {
                        this.transform.GetChild(3).GetComponent<ChargeShotFeedback>().EndCharge();
                        //si le tire chargé peut être exécuté
                        if (Time.time > nextChargeShot && nextChargeShot != 0)
                        {
                            //exécution de la commande de tire chargé en fonction de l'orientation du joueur
                            if (playerRectTransform.rotation.y == 0)
                            {
                                command[i].execute(observer, chargeFireSpeed);
                            }
                            else
                            {
                                command[i].execute(observer, -chargeFireSpeed);
                            }
                            //reset du temps avant le prochain tire chargé
                            nextChargeShot = 0;
                        }
                    }
                    //sinon
                    else
                    {
                        //exécution de la commande
                        command[i].execute(this.gameObject, speed);
                        //si le joueur est immobile
                        if (!isMoving)
                        {
                            //modification de isMoving à true
                            isMoving = true;
                            //réactivation de l'animator de la roue
                            playerWheelAnimator.enabled = true;
                        }
                    }

                }

            }
        }
        
        //si le joueur est immobile
        if (playerRigidBody2D.velocity.x == 0)
        {
            //désactivation de l'animator de la roue
            playerWheelAnimator.enabled=false;
            //modification de isMoving à faux
            isMoving = false;
        }
    }

    //fonction appelé en cas de collision avec un objet
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //si l'objet est une balle ou un laser
        if ((collision.tag == "Ball" || collision.tag=="Laser") && isRespawning==false)
        {
            //reset de la position du joueur
            this.transform.position = spawnPos.position;
            //modification de isRespawning à vrai
            isRespawning = true;
            //mise à jour de endRespawnTime
            endRespawnTime = Time.time + respawnDelay;
            //début du clipping du joueur
            StartCoroutine(clipping());
            //reset de la vélocité du joueur
            playerRigidBody2D.velocity = new Vector2(0, 0);
        }

        if (collision.tag == "BonusDomage")
        {
            collision.GetComponent<BonusDomage>().SendToPlayer(number);
            observer.GetComponent<ObserverScript>().SetNewFireColor(1);
            
            isDomageBuff = true;
            domageBuffEnd = Time.time + domageBuffDuration;
        }
    }

    //coroutine permettant de faire clipper le joueur lors de son respawn
    IEnumerator clipping()
    {
        //boucle infinie
        while (true)
        {
            //si l'image du joueur est active
            if (playerWheel.enabled)
            {
                //désactivation de l'image du joueur et de sa roue
                playerWheel.enabled = false;
                playerBody.enabled = false;
                this.transform.GetChild(2).GetComponent<Image>().enabled = false;
            }
            //sinon
            else
            {
                //activation de l'image du joueur et de sa roue
                playerWheel.enabled = true;
                playerBody.enabled = true;
                this.transform.GetChild(2).GetComponent<Image>().enabled = true;
            }
            //attente de 0.2 seconde
            yield return new WaitForSeconds(0.2f);
        }
            
        
    }


}  
