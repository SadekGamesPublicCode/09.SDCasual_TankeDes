using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeavyD : DinoSC
{
    void Start()
    {
        tankID = 8;
        hp = 10;
        ap = 0;
        atkDmg = 8;
        bulletAmmount = bulletAmmountOrigin = 5;
        base.Start();
    }
}
