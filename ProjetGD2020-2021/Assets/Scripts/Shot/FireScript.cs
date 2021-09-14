using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireScript : MonoBehaviour
{
//variables privés
    //boolean permettant de savoir si le tire est en mouvement
    private bool isMoving;
    
    //vitesse de mouvement du tire
    private float moveSpeed;
    //puissance du tire
    private float fireStrength;

    //rigidBody2D du tire
    private Rigidbody2D fireRigidBody2D;

    //RectTransform du tire
    private RectTransform fireRectTransform;

    //audioSource des tirs
    private AudioSource audioSource;

    //fonction de déplacement du tire
    public void Move(float speed,float newFireStrength)
    {
        //set de isMoving à vrai
        isMoving = true;
        //set de la vitesse du tire
        moveSpeed = speed;
        //set de la puissance du tire
        fireStrength = newFireStrength;
        //set de fireRigidBody
        if (fireRigidBody2D == null)
        {
            fireRigidBody2D = this.GetComponent<Rigidbody2D>();
        }
        //set de fireRectTransform
        if (fireRectTransform == null)
        {
            fireRectTransform = this.GetComponent<RectTransform>();
        }
        //set de audioSource
        if (audioSource == null)
        {
            audioSource = this.GetComponent<AudioSource>();
        }
        //lancement du son
        audioSource.Play();
    }

    // Update est appelé à chaque frames
    private void Update()
    {
        //si le tire est en mouvement
        if (isMoving)
        {
            //ajout de vélocité au tire
            fireRigidBody2D.velocity= new Vector2(moveSpeed,
                                                  fireRectTransform.position.y);
        }
    }

    //fonction permettant de reset le tire
    public void ResetShot()
    {
        //désactivation de l'image du tire
        this.GetComponent<Image>().enabled = false;
        //set de isMoving à faux
        isMoving = false;
        //reset de la postion du tire
        fireRectTransform.position = new Vector2(-2500,2500);
        //reset de la vélocité du tire
        fireRigidBody2D.velocity = new Vector2(0, 0);
    }

    //fonction appelé en cas de collision avec un objet
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //si l'objet est un sol ou un laser
        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag=="Laser")
        {
            //reset du tire
            ResetShot();
        }
    }

    //fonction permettant de récupérer la puissance du tire
    public float GetFireStength()
    {
        //renvoi de la puissance du tire
        return fireStrength;
    }

}
