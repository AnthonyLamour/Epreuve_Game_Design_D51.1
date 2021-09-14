using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandJetPack : ICommand
{
    //fonction permettant d'exécuter la commande de jetpack
    public void execute(GameObject actor, float jump)
    {
        //exécution de la commande de jetpack
        actor.GetComponent<Rigidbody2D>().velocity = new Vector2(actor.GetComponent<Rigidbody2D>().velocity.x, jump);
    }

}
