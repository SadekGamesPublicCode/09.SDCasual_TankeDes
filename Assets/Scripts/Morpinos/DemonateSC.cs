using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonateSC : MorpinosSC
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
        atkDmg = 5;
        detectRange = 8;
        atkSpd = 2f;
        hp = 5;
        isContactEnemy = false;
    }
}
