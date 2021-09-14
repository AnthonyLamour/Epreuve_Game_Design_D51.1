using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandRapidFire : ICommand
{
    //fonction permettant d'exécuter la commande enclanchant le rapid fire
    public void execute(GameObject actor, float rapidFireSpeed)
    {
        //exécution de la commande enclanchant le rapid fire
        actor.GetComponent<ObserverScript>().SwitchToRapidFire(rapidFireSpeed);
    }
}
