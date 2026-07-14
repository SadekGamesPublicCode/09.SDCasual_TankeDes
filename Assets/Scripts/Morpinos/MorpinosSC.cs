using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorpinosSC : MonoBehaviour
{
    Vector3 curPos;
    DinoSC character;
    [SerializeField] EBullet bullet;
    internal float tempMoveSpd, originalSpd;
    internal int atkDmg;
    internal float atkSpd;
    internal int detectRange;
    internal int hp;
    internal bool isContactEnemy;
    internal bool isGrounded;
    internal bool isFacing;
    protected virtual void Start()
    {
        character = GameObject.Find("OBJ_SpaceRanger(Clone)").GetComponent<DinoSC>();
        Invoke(nameof(SelfDestruct), 20f);
        InvokeRepeating(nameof(DetectCharacter), 0f, 0.25f);
        InvokeRepeating(nameof(AttackCharacter), 0f, atkSpd);
    }
    private void Update()
    {
        MoveForward();
    }
    void MoveForward()
    {
        if (isGrounded == true)
        {
            float xPos;
            xPos = -1 * Time.deltaTime * tempMoveSpd+ transform.position.x;
            transform.position = new Vector3(xPos, curPos.y, 0);
        }
    }
    void DetectCharacter()
    {
        if (character != null)
        {
            float distance = Vector3.Distance(transform.position, character.transform.position);
            if (distance <= detectRange)
            {
                isContactEnemy = true;
            }
            else isContactEnemy = false;
        }
        else if (character == null ) isContactEnemy = false;
    }
    void AttackCharacter()
    {
        if (isContactEnemy == true)
        {
            if (detectRange != 1)
            {
                PerformRangeAttack();
            }
            else if(detectRange == 1) 
            {
                PerformMeleeAttack();
            }
        }
    }
    void SelfDestruct() => Destroy(gameObject);
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") { isGrounded = true; curPos = transform.position; }
        else if (collision.gameObject.tag == "Player") 
        {
            isFacing = true;
            character.OnTakeDmage(atkDmg);
        }
        else if(collision.gameObject.tag == "PBullet") 
        {
            hp -= 1;
            if(hp <= 0)
            {
                SelfDestruct();
            }
        }
    }
    void PerformRangeAttack()
    {
        Instantiate(bullet, gameObject.transform.position, Quaternion.identity);
    }
    void PerformMeleeAttack()
    {
        if (isFacing == true)
        {
            tempMoveSpd = 0;
            Instantiate(bullet, gameObject.transform.position, Quaternion.identity);
        } else if (isFacing == false)
        {
            tempMoveSpd = originalSpd;
        }
    }
}
