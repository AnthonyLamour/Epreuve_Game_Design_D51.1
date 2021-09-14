using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
//variables publiques
    //prefabs des lasers émient par la balle lorsqu'elle est surchargée
    public GameObject laserLeft;
    public GameObject laserRight;

//variables privées
    //correspond au rigidbody de la balle
    private Rigidbody2D ballRigidbody2D;
    
    //position de base de la balle
    private Vector3 spawnPos;
    
    //cooldown avant que la balle ne commence a ce décharger
    private float surchargeCoolDown;
    //prochain début de déchargement de la balle
    private float nextCoolDown;
    //vitesse de déchargement de la balle
    private float coolSpeed;
    //vitesse de surcharge de la balle
    private float surchargeSpeed;
    //permet d'éviter le spawn de plusieurs laser d'affiler
    private float laserDelai;
    //vitesse de retour de la balle à sa position d'origine
    private float returnSpeed;

    //sprite de la balle
    private Image sprite;

    //couleur de base de la balle
    private Color baseColor;

    //boolean permettant de savoir si la balle est sur son trajet de retour
    private bool isGoingBack;

    //correspond au transform de la balle
    private Transform ballTransform;

    // Start est appelé à la première activation de l'objet
    private void Start()
    {
        //initialisation de ballTransform
        ballTransform = this.transform;
        //initialisation de isGoingBack à faux
        isGoingBack = false;
        //initialisation de la position de base de la balle
        spawnPos = transform.position;
        //initialisation du sprite de la balle
        sprite = ballTransform.GetChild(0).GetComponent<Image>();
        //initialisation de la couleur de base de la balle
        baseColor = sprite.color;
        //initialisation de la vitesse de surcharge de la balle
        surchargeSpeed = 10;
        //initialisation de la vitesse déchargement de la balle
        coolSpeed = 10;
        //initialisation du prochain début de déchargement de la balle
        nextCoolDown = 0;
        //initialisation du cooldown avant que la balle ne commence a ce décharger
        surchargeCoolDown = 5;
        //initialisation de laserDelai
        laserDelai = 0;
        //initialisation de returnSpeed
        returnSpeed = 10;
        //initialisation de ballRigidBody2D
        ballRigidbody2D = this.GetComponent<Rigidbody2D>();
    }

    // Update appelé à toutes les frames
    private void Update()
    {
        //si la balle est sur son trajet de retour
        if (isGoingBack)
        {
            //déplacement de la balle vers sa position de base
            ballTransform.position = Vector3.MoveTowards(ballTransform.position, spawnPos, returnSpeed * Time.deltaTime);
            
            //si la balle à atteint sa position de base
            if (ballTransform.position == spawnPos)
            {
                //reset de la vélocité de la balle
                ballRigidbody2D.velocity = new Vector2(0, 0);
                //reset de isGoingBack
                isGoingBack = false;
            }
        }

        //si la balle peut se déchargée et qu'elle est chargée
        if (Time.time > nextCoolDown && sprite.color!=baseColor)
        {
            //si la valeur de vert de la couleur est supérieur à 255-la vitesse de cooldown
            if (sprite.color.g > (255f - coolSpeed)/255f)
            {
                //modification de la couleur de la balle à sa couleur d'origine soit 1,1,1,1 ou 255,255,255,255
                sprite.color = new Color(1f, 1f, 1f, 1f);
            }
            //sinon
            else
            {
                //modification de la couleur de la balle vers sa couleur d'origine
                sprite.color = new Color(1, sprite.color.g + (coolSpeed)/255f, sprite.color.b + (coolSpeed)/255f, 1);
            }
            
        }
    }

    //fonction appelé en cas de collision avec un objet
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //si l'objet est un tire et que la balle n'est pas sur son trajet de retour
        if (collision.tag == "Shot" && !isGoingBack)
        {
            //ajout d'une impulsion à la balle en fonction de la puissance du tire
            ballRigidbody2D.AddForce(new Vector2(collision.GetComponent<FireScript>().GetFireStength(), 0), ForceMode2D.Impulse);
            //reset du tire
            collision.GetComponent<FireScript>().ResetShot();

            //repoussage du déchargement de la balle
            nextCoolDown = Time.time + surchargeCoolDown;
            
            //si la valeur de verte de la couleur est inférieur à la vitesse de surcharge
            if (sprite.color.g < (surchargeSpeed)/255f && Time.time>laserDelai)
            {
                //passage de la balle au rouge
                sprite.color = new Color(1f, 0f, 0f, 1f);

                //instantiation des lasers
                GameObject newLaser=Instantiate(laserLeft,ballTransform.parent);
                newLaser.GetComponent<RectTransform>().position = this.GetComponent<RectTransform>().position;
                newLaser=Instantiate(laserRight,ballTransform.parent);
                newLaser.GetComponent<RectTransform>().position = this.GetComponent<RectTransform>().position;

                //incrémentation de laserDelai;
                laserDelai = Time.time + 0.2f;

                //reset de la vélocité de la balle
                ballRigidbody2D.velocity = new Vector2(0, 0);
            }
            //sinon
            else
            {
                //modification de la couleur de la balle vers le rouge
                sprite.color = new Color(1f, sprite.color.g - (surchargeSpeed)/255f, sprite.color.b - (surchargeSpeed)/255f, 1f);
            }
        }

        //si l'objet est un but
        if (collision.tag == "But")
        {
            //initialisation du début du trajet retour de la balle
            isGoingBack = true;
        }
    }
}
