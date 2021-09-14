using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoneController : MonoBehaviour
{
    //fonction appelé en cas de sortie d'un objet
    private void OnTriggerExit2D(Collider2D collision)
    {
        //si l'objet est un tire
        if (collision.gameObject.tag == "Shot")
        {
            //reset du tire
            collision.gameObject.GetComponent<FireScript>().ResetShot();
        }
        //sinon si l'objet est un laser
        else if (collision.gameObject.tag == "Laser")
        {
            //destruction du laser
            Destroy(collision.transform.parent.gameObject);
        }
    }

}
