using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBullet : MonoBehaviour
{
    //Player bullet
    [HideInInspector] GeneralSC genCtr;
    [HideInInspector] DinoSC player;
    private float movespeed = 5f;
    private int gameMode;
    private float dmg;
    Vector3 playerPos;
    private void Start()
    {
        genCtr = GameObject.Find("GenGameControlMN").GetComponent<GeneralSC>();
        gameMode = genCtr.gameMode;
        player = GameObject.Find("OBJ_Tank(Clone)").GetComponent<DinoSC>();
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
