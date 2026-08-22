using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EBullet : MonoBehaviour
{
    GameplaySC arcadeCtr;
    ChallengeSC challengeCtr;
    GeneralSC genCtr;
    private Vector3 lastPlayerPos;
    void Start()
    {
        genCtr = GameObject.Find("GenGameControlMN").GetComponent<GeneralSC>();
        GetLastPlayerPos();
        Invoke(nameof(SelfDestruct), 20f);
    }

    private void GetLastPlayerPos()
    {
        if(genCtr.gameMode == 1)
        {
            arcadeCtr = GameObject.Find("OBJ_ArcadeMN").GetComponent<GameplaySC>(); 
            lastPlayerPos = arcadeCtr.curPlayer.transform.position;
            ExecuteMove();
        }   
        else if(genCtr.gameMode == 2) { challengeCtr = GameObject.Find("OBJ_CHallengeMN").GetComponent<ChallengeSC>(); }
    }
    void ExecuteMove()
    {
        gameObject.transform.DOMove(lastPlayerPos, 1f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") { }
        else if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "PBullet") { SelfDestruct(); }
    }
    void SelfDestruct() => Destroy(gameObject);
}
