using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandChargeFire : ICommand
{
    //fonction permettatn d'exécuter la commande de tire chargé
    public void execute(GameObject actor, float fireSpeed)
    {
        //exécution de la commande de tire chargé
        actor.GetComponent<ObserverScript>().ChargeFire(fireSpeed);
    }
}
