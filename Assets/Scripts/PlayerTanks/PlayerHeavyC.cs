using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeavyC : DinoSC
{
    void Start()
    {
        tankID = 7;
        hp = 10;
        ap = 0;
        atkDmg = 7;
        bulletAmmount = bulletAmmountOrigin = 5;
        base.Start();
    }
}
