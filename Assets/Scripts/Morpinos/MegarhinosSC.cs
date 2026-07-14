using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegarhinosSC : MorpinosSC
{
    protected override void Start()
    {
        base.Start();
        SettingMorpinos();
    }

    void SettingMorpinos()
    {
        originalSpd = 0.5f;
        tempMoveSpd = originalSpd;
        atkDmg = 3;
        atkSpd = 5f;
        detectRange = 1;
        hp = 10;
        isContactEnemy = false;
    }
}
