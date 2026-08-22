using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleB : DinoSC
{
    void Start()
    {
        tankID = 4;
        hp = 7;
        ap = 0;
        atkDmg = 5;
        bulletAmmount = bulletAmmountOrigin = 10;
        base.Start();
    }
}
