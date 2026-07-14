using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrolingSC : MorpinosSC
{
    protected override void Start()
    {
        base.Start();
        SettingMorpinos();
    }
    void SettingMorpinos()
    {
        originalSpd = 1;
        tempMoveSpd = originalSpd;
        atkDmg = 3;
        atkSpd = 3f;
        detectRange = 2;
        hp = 4;
        isContactEnemy = false;
    }

}
