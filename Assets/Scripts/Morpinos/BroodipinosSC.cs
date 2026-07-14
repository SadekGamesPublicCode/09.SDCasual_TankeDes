using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroodipinosSC : MorpinosSC
{
    protected override void Start()
    {
        base.Start();
        SettingMorpinos();
    }
    void SettingMorpinos()
    {
        originalSpd = 2;
        tempMoveSpd = originalSpd;
        atkDmg = 1;
        atkSpd = 3f;
        detectRange = 5;
        hp = 1;
        isContactEnemy = false;
    }
}
