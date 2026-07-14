using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArachilingSC : MorpinosSC
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
        atkDmg = 1;
        atkSpd = 2f;
        detectRange = 8;
        hp = 3;
        isContactEnemy = false;
    }
}
