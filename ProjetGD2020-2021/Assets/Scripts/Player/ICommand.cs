using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    //fonction d'exécution de la commande
    void execute(GameObject actor, float speed);
}
