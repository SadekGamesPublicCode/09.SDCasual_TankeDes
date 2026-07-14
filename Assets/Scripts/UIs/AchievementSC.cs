using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementSC : MonoBehaviour
{
    [HideInInspector] GameplaySC menu;
    void Start()
    {
        menu = GameObject.Find("OBJ_GameplayControl").GetComponent<GameplaySC>();
    }
}
