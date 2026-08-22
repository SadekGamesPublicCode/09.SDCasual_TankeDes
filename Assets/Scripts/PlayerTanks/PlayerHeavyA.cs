using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeavyA : DinoSC
{
    void Start()
    {
        tankID = 5;
        hp = 10;
        ap = 0;
        atkDmg = 5;
        bulletAmmount = bulletAmmountOrigin = 5;
        base.Start();
    }
}
