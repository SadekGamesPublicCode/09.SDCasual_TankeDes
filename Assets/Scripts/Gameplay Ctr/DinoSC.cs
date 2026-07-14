using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DinoSC : MonoBehaviour
{
    [HideInInspector] GeneralSC genCtr;
    [HideInInspector] GameplaySC gameplayCtr;
    [HideInInspector] ChallengeSC challengectr;
    [HideInInspector] Rigidbody2D rb;
    //[SerializeField] List<GameObject> bulletList = new List<GameObject>();
    [SerializeField] PBullet bullet;
    private int bulletToSpawn;
    private bool isGrounded, isAllowAbility;
    private float jumpForce = 7f;
    private float moveSpd = 3f;
    private int faceDir; //1 = face foward, 0 = face backwards
    public int livesAmountTotal, liveAmoutnLeft;
    private int deviceType; //0 = mobile, 1 = PC
    private int gameMode;
    private int countDeathOnAir;
    private int ap, hp, abilityAPCost;
    Vector2 startTouchPos;
    Vector3 charScale;
    internal Vector3 objectPos;
    void Start()
    {
        SettingStart();
        InitChar();
    }
    void Update()
    {
        DinoJumpByKey();
        DinoBackwardByKey();
        OnDinoAttack();
        DinoForwardByKey();
        if (gameObject.transform.position.y <= -3) DinoLose();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && gameMode == 1)
        {
            OnTakeDmage(1);
        }else if(collision.gameObject.tag == "Enemy" && gameMode == 2)
        {
            DinoLose();
        }
        else if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
    public void OnTakeDmage(int damageInTake)
    {
        livesAmountTotal -= damageInTake;
        if (gameMode == 1)
        {
            gameplayCtr.UnShowLive(livesAmountTotal);
        }
        if (livesAmountTotal <= 0)
        {
            DinoLose();
        }
    }
    private void SettingStart()
    {
        genCtr = GameObject.Find("GenGameControlMN").GetComponent<GeneralSC>();
        CheckGameMode();
        deviceType = genCtr.deviceMode;
    }
    private void InitChar()
    {
        
        rb = GetComponent<Rigidbody2D>();
        faceDir = 1;
        charScale = gameObject.transform.localScale;

        //Setting Player attribute;
        livesAmountTotal = 3;
        liveAmoutnLeft = 3;
        hp = 0; //replace this after done RPG mechanims
        ap = 0;
        isAllowAbility = false;
    }
    private void DinoLose()
    {
        switch (gameMode)
        {
            case 1:
                if (livesAmountTotal <= 0)
                {
                    Destroy(gameObject);
                    gameplayCtr.OnGameLose();
                    genCtr.ShowgameOver(true);
                }
                break;
            case 2:
                challengectr.OnGameLose();
                break;
        }
    }
    #region Player Actions by Key
    public void OnDinoAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            objectPos = gameObject.transform.position;
            Instantiate(bullet, new Vector3(objectPos.x + 1f, objectPos.y + 0.4f, 0), Quaternion.identity);
        }

    }
    public void OnAttackByTouch()
    {
        objectPos = gameObject.transform.position;
        Instantiate(bullet, new Vector3(objectPos.x + 1f, objectPos.y + 0.4f, 0), Quaternion.identity);
    }
    #endregion

    #region Internal Handles
    private void DinoJumpByKey()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }
    }
    private void DinoForwardByKey()
    {
        if (Input.GetKey(KeyCode.D)) { gameObject.transform.position += Vector3.right * Time.deltaTime * moveSpd; }
    }
    private void DinoBackwardByKey()
    {
        if (Input.GetKey(KeyCode.A)) { gameObject.transform.position += Vector3.left * Time.deltaTime * moveSpd; }
    }
    public void CharForwardConsole()
    {
        for (int i = 0; i < 10; i++)
        {
            //gameObject.transform.position += Vector3.right * Time.deltaTime * moveSpd;
            rb.velocity = new Vector2(moveSpd, rb.velocity.y);
        }
    }
    public void CharBackwardConsole()
    {
        for (int i = 0; i < 10; i++)
        {
            //gameObject.transform.position += Vector3.left * Time.deltaTime * moveSpd;
            rb.velocity = new Vector2(-moveSpd, rb.velocity.y);
        }
    }
    public void CharJumpConsole()
    {
        bool tempGrounded;
        tempGrounded = isGrounded;
        if (tempGrounded == true)
        {
            for(int i = 0; i< 10; i++)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                isGrounded = false;
            }
        }
    }
    public void CharDodgeConsole()
    {
        gameObject.transform.localScale = new Vector3(charScale.x, charScale.y/2, charScale.z);
        Invoke(nameof(ResetCharScale), 1.5f);
    }
    private void ResetCharScale() => gameObject.transform.localScale = charScale;
    #endregion
    private void CheckGameMode()
    {
        gameMode = genCtr.gameMode;
        switch (gameMode)
        {
            case 1:
                //Arcade
                gameplayCtr = GameObject.Find("OBJ_ArcadeMN").GetComponent<GameplaySC>();
                break;
            case 2:
                //Challenge
                challengectr = GameObject.Find("OBJ_CHallengeMN").GetComponent<ChallengeSC>();
                break;
        }
    }
    public void PlusAP()
    {

    }
}
