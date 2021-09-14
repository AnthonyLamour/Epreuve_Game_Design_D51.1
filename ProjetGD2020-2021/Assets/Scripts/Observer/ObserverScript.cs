using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObserverScript : MonoBehaviour
{
//variables publiques
    //object pool des tires du joueur
    public GameObject playerShotPool;

    //joueur à observer
    public RectTransform playerToObserve;

    public Color32[] fireColors;

//variables publiques
    //boolean permetant de savoir si le joueur est en mode rapid fire
    private bool rapidFireOn;

    //délai entre chaque tire en mode rapid fire
    private float rapidFireDelay;
    //puissance des tires de bases
    private float basicFireStrength;
    //puissance des tires chargés
    private float chargeFireStrength;

    private int currentFireColor;

    // Start est appelé à la première activation de l'objet
    private void Start()
    {
        //initialisation de rapidFireOn à faux
        rapidFireOn = false;
        //initialisation de basicFireStrength
        basicFireStrength = 5;
        //initialisation de chargeFireStrength
        chargeFireStrength = 10;
    }

    //fonction permettant de faire tirer le joueur
    public void Fire(float speed)
    {
        //si le joueur n'est pas en mode rapid fire
        if (!rapidFireOn)
        {
            //variable permettant de parcourir l'object pool de tire du joueur
            int i = 0;

            //objet correspondant au prochain tire du joueur
            GameObject shot = null;

            //temps que le prochain tire n'est pas sélectionné et que toutes l'object pool de tire n'a pas été vérifié
            while (shot == null && i < playerShotPool.transform.childCount - 1)
            {
                //si le tire actuel de l'object pool de tire n'est pas actif
                if (!playerShotPool.transform.GetChild(i).gameObject.GetComponent<Image>().enabled)
                {
                    //set du prochain tire du joueur
                    shot = playerShotPool.transform.GetChild(i).gameObject;
                }
                //incrémentation du compteur
                i = i + 1;
            }

            //si un tire a été sélectionné
            if (shot != null)
            {
                //affichage du tire
                shot.GetComponent<Image>().enabled = true;
                shot.GetComponent<Image>().color = fireColors[currentFireColor];
                //set de la position du tire
                shot.GetComponent<RectTransform>().position = playerToObserve.position;
                //si le projectile part vers la droite
                if (speed > 0)
                {
                    if(currentFireColor != 1)
                    {
                        //début du mouvement du projectile vers la droite
                        shot.GetComponent<FireScript>().Move(speed, basicFireStrength);
                    }
                    else
                    {
                        //début du mouvement du projectile vers la droite
                        shot.GetComponent<FireScript>().Move(speed, basicFireStrength*2);
                    }
                    
                }
                //si le projectile part vers la gauche
                else
                {
                    if(currentFireColor != 1)
                    {
                        //début du mouvement du projectile vers la gauche
                        shot.GetComponent<FireScript>().Move(speed, -basicFireStrength);
                    }
                    else
                    {
                        //début du mouvement du projectile vers la gauche
                        shot.GetComponent<FireScript>().Move(speed, -basicFireStrength*2);
                    }
                    
                }
                
            }
        }
        //sinon
        else
        {
            //si l'object pool de tire permet d'enclancher un tire rapide
            if(playerShotPool.transform.GetChild(0).GetComponent<Image>().enabled == false &&
               playerShotPool.transform.GetChild(1).GetComponent<Image>().enabled == false &&
               playerShotPool.transform.GetChild(2).GetComponent<Image>().enabled == false)
            {
                //début du tire rapide
                StartCoroutine(RapidFire(speed));
            }
        }
        
    }

    //Coroutine permettatn de démarrer un tire rapide
    IEnumerator RapidFire(float speed)
    {
        //pour 3 tires
        for (int i = 0; i < 3; i++)
        {
            //sélection du prochain tire du joueur
            GameObject shot= playerShotPool.transform.GetChild(i).gameObject;
            //affichage du tire
            shot.GetComponent<Image>().enabled = true;
            //set de la position du tire
            shot.GetComponent<RectTransform>().position = playerToObserve.position;

            //si le projectile part vers la droite
            if (speed > 0)
            {
                //début du mouvement du projectile vers la droite
                shot.GetComponent<FireScript>().Move(speed, basicFireStrength);
            }
            //si le projectile part vers la gauche
            else
            {
                //début du mouvement du projectile vers la gauche
                shot.GetComponent<FireScript>().Move(speed, -basicFireStrength);
            }
            
            //application du délais entre chaque tire
            yield return new WaitForSeconds(rapidFireDelay);
        }
    }

    //fonction permettant d'utiliser un tire chargé
    public void ChargeFire(float speed)
    {
        //prochain tire du joueur
        GameObject shot = null;
        //si le tire chargé est disponible dans l'object pool de tire
        if(playerShotPool.transform.GetChild(playerShotPool.transform.childCount - 1).GetComponent<Image>().enabled == false)
        {
            //sélection du prochain tire du joueur
            shot = playerShotPool.transform.GetChild(playerShotPool.transform.childCount - 1).gameObject;
        }

        //si un tire a été sélectionné
        if (shot != null)
        {
            //affichage du tire
            shot.GetComponent<Image>().enabled = true;
            //set de la position du tire
            shot.GetComponent<RectTransform>().position = playerToObserve.position;
            shot.GetComponent<Image>().color = fireColors[currentFireColor];

            //si le projectile part vers la droite
            if (speed > 0)
            {
                if(currentFireColor != 1)
                {
                    //début du mouvement du projectile vers la droite
                    shot.GetComponent<FireScript>().Move(speed, chargeFireStrength);
                }
                else
                {
                    //début du mouvement du projectile vers la droite
                    shot.GetComponent<FireScript>().Move(speed, chargeFireStrength*2);
                }
                
            }
            //si le projectile part vers la gauche
            else
            {
                if(currentFireColor != 1)
                {
                    //début du mouvement du projectile vers la gauche
                    shot.GetComponent<FireScript>().Move(speed, -chargeFireStrength);
                }
                else
                {
                    //début du mouvement du projectile vers la gauche
                    shot.GetComponent<FireScript>().Move(speed, -chargeFireStrength*2);
                }
                
            }
            
        }

    }

    //fonction permettant d'activer ou de désactiver le tire rapide
    public void SwitchToRapidFire(float newRapidFireDelay)
    {
        //si tire rapide activé
        if (rapidFireOn)
        {
            //désactivation du tire rapide
            rapidFireOn = false;
        }
        //sinon
        else
        {
            //activation du tire rapide
            rapidFireOn = true;
            //set du délai de tire rapide
            rapidFireDelay = newRapidFireDelay;
        }
        
    }

    public void SetNewFireColor(int newFireColor)
    {
        currentFireColor = newFireColor;
    }
}
