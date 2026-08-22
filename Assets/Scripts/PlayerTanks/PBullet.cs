using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBullet : MonoBehaviour
{
    //Player bullet
    [HideInInspector] GeneralSC genCtr;
    [HideInInspector] DinoSC player;
    [HideInInspector] GameplaySC arcadeCtr;
    [HideInInspector] ChallengeSC challengeCtr;
    private float movespeed = 5f;
    private int selfDmg;
    private int gameMode;
    private float dmg;
    Vector3 playerPos;
    private void Start()
    {
        genCtr = GameObject.Find("GenGameControlMN").GetComponent<GeneralSC>();
        gameMode = genCtr.gameMode;
        if (gameMode == 1)
        {
            //Arcade
            arcadeCtr = GameObject.Find("OBJ_ArcadeMN").GetComponent<GameplaySC>();
        }
        else if (gameMode == 2)
        {
            
        }
        player = GameObject.Find(arcadeCtr.curPlayer.name).GetComponent<DinoSC>();
        selfDmg = player.atkDmg;
        print(selfDmg);
        Invoke(nameof(SelfDestruct), 5);
    }
    private void Update()
    {
        MoveLinear();
    }
    private void MoveLinear() 
    {
        playerPos = player.transform.position;
        gameObject.transform.position += Vector3.right * movespeed * Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EAmmo") Destroy(gameObject); 
    }
    internal void SelfDestruct() => Destroy(gameObject);
}
