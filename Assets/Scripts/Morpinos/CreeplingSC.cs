using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreeplingSC : MorpinosSC
{
    protected override void Start()
    {
        base.Start();
        SettingMorpinos();
    }
    void SettingMorpinos()
    {
        originalSpd = 3;
        tempMoveSpd = originalSpd;
        atkDmg = 1;
        atkSpd = 1f;
        detectRange = 1;
        hp = 1;
        isContactEnemy = false;
    }
}
