using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IFV_Enemy : EnemySC
{
    void Start()
    {
        base.Start();
        moveSpd = 2f;
        selfScore = 5;
        fireRate = 3;
        selfHP = 10;

        hpSlide.maxValue = selfHP;
        hpSlide.minValue = 0;
        hpSlide.value = selfHP;

        InvokeRepeating(nameof(OnAutoAttack), 0f, fireRate);
    }
    void Update()
    {
        base.Update();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
}
