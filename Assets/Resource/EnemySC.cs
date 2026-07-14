using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class EnemySC : MonoBehaviour
{
    [SerializeField] internal EBullet ebullet;
    [SerializeField] internal GeneralSC genCtr;
    [SerializeField] internal GameplaySC arcadeCtr;
    [SerializeField] internal ChallengeSC challengerCtr;
    internal float moveSpd;
    internal int selfScore, atkPoint, fireRate, selfHP;
    internal bool isGrounded;
    internal Vector3 objectPos;
    internal Collider2D colBody;
    protected virtual void Start()
    {
        isGrounded = false;
        colBody = GetComponent<Collider2D>();
        colBody.isTrigger = true;
        isGrounded = false;
        genCtr = GameObject.Find("GenGameControlMN").GetComponent<GeneralSC>();
        if(genCtr.gameMode == 1)
        {
            arcadeCtr = GameObject.Find("OBJ_ArcadeMN").GetComponent<GameplaySC>();
        }
        else if(genCtr.gameMode == 2)
        {
            challengerCtr = GameObject.Find("OBJ_CHallengeMN").GetComponent<ChallengeSC>();
        }
    }

    internal void Update()
    {
        if(arcadeCtr.isEnablePlay == true && isGrounded == true)
        {
            OnMoveLinear();
        }
    }
    internal void OnMoveLinear() { gameObject.transform.position += Vector3.left * moveSpd * Time.deltaTime; }
    internal void OnAutoAttack() 
    {
        objectPos = gameObject.transform.position;
        Instantiate(ebullet, new Vector3(objectPos.x - 1f, objectPos.y + 0.4f, 0), Quaternion.identity);
    }
    internal void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "PBullet")
        {
            OnCaculatingSelfHP();
        }
    }
    internal void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            colBody.isTrigger = false;
            isGrounded = true;
        }
    }
    internal void OnHandleIncreasePoint()
    {
        if(genCtr.gameMode == 1)
        {
            //Add point to Arcade
        }else if(genCtr.gameMode == 2)
        {
            //Add point to challenge
        }
    }
    internal void OnCaculatingSelfHP()
    {
        selfHP --;
        if(selfHP <= 0)
        {
            Destroy(gameObject);
            OnHandleIncreasePoint();
        }
    }
}
