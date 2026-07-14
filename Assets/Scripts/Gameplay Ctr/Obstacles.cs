using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    GameplaySC gameControl;
    private bool isGrounded;
    internal float moveSpd, jumpSpd;
    private void Start()
    {
        StartCoroutine(OnCountToDed());
    }
    private void Update()
    {
        OnMove();
    }
    private void OnMove()
    {
        if(isGrounded == true)
        {
            float currentY = gameObject.transform.position.y;
            float xPos = gameObject.transform.position.x - 0.05f;
            gameObject.transform.position = new Vector2(xPos, currentY);
        }
    }

    private IEnumerator OnCountToDed()
    {
        yield return new WaitForSeconds(15);
        Destroy(gameObject);
        StartCoroutine(OnCountToDed());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground") { isGrounded = true; }
        else if(collision.gameObject.tag == "Player") { Destroy(gameObject); }
    }

}
