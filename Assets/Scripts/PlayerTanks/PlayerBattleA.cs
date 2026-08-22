using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleA : DinoSC
{
    void Start()
    {
        tankID = 3;
        hp = 8;
        ap = 0;
        atkDmg = 3;
        bulletAmmount = bulletAmmountOrigin = 10;
        base.Start();
    }
}
