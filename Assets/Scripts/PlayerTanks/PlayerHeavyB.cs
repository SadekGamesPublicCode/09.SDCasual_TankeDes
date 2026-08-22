using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeavyB : DinoSC
{
    void Start()
    {
        tankID = 6;
        hp = 10;
        ap = 0;
        atkDmg = 6;
        bulletAmmount = bulletAmmountOrigin = 4;
        base.Start();
    }
}
