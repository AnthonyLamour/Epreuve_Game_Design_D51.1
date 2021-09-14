using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandRight : ICommand
{
    //fonction permettant d'exécuter la commande aller à droite
    public void execute(GameObject actor, float speed)
    {
        //exécution de la commande aller à droite
        actor.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, actor.GetComponent<Rigidbody2D>().velocity.y);
        //rotation du joueur si besoin
        actor.GetComponent<RectTransform>().rotation = new Quaternion(actor.GetComponent<RectTransform>().rotation.x, 0, actor.GetComponent<RectTransform>().rotation.z, actor.GetComponent<RectTransform>().rotation.w);
    }

}
