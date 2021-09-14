using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandLeft : ICommand
{
    //fonction permettant d'exécuter la commande aller à gauche
    public void execute(GameObject actor, float speed)
    {
        //exécution de la commande aller à gauche
        actor.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, actor.GetComponent<Rigidbody2D>().velocity.y);
        //rotation du joueur si besoin
        actor.GetComponent<RectTransform>().rotation = new Quaternion(actor.GetComponent<RectTransform>().rotation.x, 180, actor.GetComponent<RectTransform>().rotation.z, actor.GetComponent<RectTransform>().rotation.w);
    }

}
