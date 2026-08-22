using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightB : DinoSC
{
    void Start()
    {
        tankID = 2;
        hp = 4;
        ap = 0;
        atkDmg = 5;
        bulletAmmount = bulletAmmountOrigin = 15;
        base.Start();
    }
}
