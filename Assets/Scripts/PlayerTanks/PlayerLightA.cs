using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightA : DinoSC
{
    void Start()
    {
        tankID = 1;
        hp = 6;
        ap = 0;
        atkDmg = 3;
        bulletAmmount = bulletAmmountOrigin = 15;
        base.Start();
    }
}
