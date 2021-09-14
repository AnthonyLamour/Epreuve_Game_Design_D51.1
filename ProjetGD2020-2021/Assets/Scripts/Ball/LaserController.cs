using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
//variables privées

    //transform du laser
    private RectTransform laserRectTransform;

    //vitesse du laser
    private float speed;
    private float speed2;

    //boolean permettant de savoir si le laser est prêt à bouger
    private bool isReady;

    // Start est appelé à la première activation de l'objet
    void Start()
    {
        //initialisation du transform du laser
        laserRectTransform = this.transform.parent.gameObject.GetComponent<RectTransform>();
        //initialisation de la vitesse du laser
        speed = 10f;
        speed2 = 1f;
        //initialisation de isReady à faux
        isReady = false;
    }

    // Update appelé toutes les frames
    void Update()
    {
        //si le laser est prêt à se déplacer
        if (isReady)
        {
            if (this.transform.localScale.y > 0.1f)
            {
                this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y - speed2 * Time.deltaTime, this.transform.localScale.z);
            }
            else
            {
                Destroy(this.transform.parent.gameObject);
            }
        }
        else
        {

            this.transform.localScale = new Vector3(this.transform.localScale.x + speed * Time.deltaTime, this.transform.localScale.y, this.transform.localScale.z);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "But")
        {
            isReady = true;
        }
    }

}
