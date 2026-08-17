using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(SelfDestruct), 20f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * 5f;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") { }
        else if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "PBullet") { SelfDestruct(); }
    }
    void SelfDestruct() => Destroy(gameObject);
}
