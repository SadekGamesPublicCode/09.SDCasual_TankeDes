using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIFV : DinoSC
{
    void Start()
    {
        tankID = 0;
        hp = 3;
        ap = 0;
        atkDmg = 1;
        bulletAmmount = bulletAmmountOrigin = 30;
        base.Start();
    }
}
