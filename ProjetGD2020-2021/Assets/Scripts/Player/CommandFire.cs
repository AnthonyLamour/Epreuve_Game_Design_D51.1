using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandFire : ICommand
{
    //fonction permettant d'exécuter la commande de tire de base
    public void execute(GameObject actor, float fireSpeed)
    {
        //exécution de la commande de tire de base
        actor.GetComponent<ObserverScript>().Fire(fireSpeed);
    }

}
