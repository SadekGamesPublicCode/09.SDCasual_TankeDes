using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DinoSC : MonoBehaviour
{
    [HideInInspector] internal GeneralSC genCtr;
    [HideInInspector] internal GameplaySC gameplayCtr;
    [HideInInspector] internal ChallengeSC challengectr;
    [HideInInspector] internal Rigidbody2D rb;
    [SerializeField] internal PBullet bullet;
    internal int bulletAmmount, bulletAmmountOrigin;
    internal bool isGrounded, isAllowAbility, isAllowFire;
    internal float jumpForce = 7f;
    internal float moveSpd = 3f;
    internal int faceDir; //1 = face foward, 0 = face backwards
    internal int deviceType, gameMode; //0 = mobile, 1 = PC
    internal int ap, hp, tankID;
    public int atkDmg;
    internal Vector2 startTouchPos;
    internal Vector3 charScale;
    internal Vector3 objectPos;
    protected virtual void Start()
    {
        SettingStart();
        gameplayCtr.UpdateHPBar(0, 0);
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
        hp -= damageInTake;
        if (gameMode == 1)
        {
            //Call UI to derease HPbar
            gameplayCtr.UpdateHPBar(1, hp);
        }
        else if(gameMode == 2)
        {
            DinoLose();
        }

        if (hp <= 0)
        {
            DinoLose();
        }
    }
    private void SettingStart()
    {
        genCtr = GameObject.Find("GenGameControlMN").GetComponent<GeneralSC>();
        rb = GetComponent<Rigidbody2D>();

        CheckGameMode();
        
        deviceType = genCtr.deviceMode;
        faceDir = 1;
        charScale = gameObject.transform.localScale;
        isAllowAbility = false;
        isAllowFire = true;
    }
    private void DinoLose()
    {
        switch (gameMode)
        {
            case 1:
                if (hp <= 0)
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
            OnAttack();
        }

    }
    public void OnAttackByTouch()
    {
        objectPos = gameObject.transform.position;
        OnAttack();
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
    public void OnDodge()
    {
        gameObject.transform.localScale = new Vector3(transform.position.x / 2, transform.position.y, transform.position.z);
        Invoke(nameof(OnResetScale), 1f);
    }
    private void OnResetScale()
    {
        gameObject.transform.localScale = new Vector3(transform.position.x *2, transform.position.y, transform.position.z);
    }
    internal void OnAttack()
    {
        if(isAllowFire == true)
        {
            bulletAmmount--;
            Instantiate(bullet, new Vector3(objectPos.x + 1f, objectPos.y + 0.4f, 0), Quaternion.identity);
            if (gameMode == 1) gameplayCtr.UpdateCurAmmo(bulletAmmount);
            else if (gameMode == 2) { }
            if (bulletAmmount == 0)
            {
                //Reload
                isAllowFire = false;
                if (gameMode == 1) gameplayCtr.UpdateCurAmmo(-1);
                else if (gameMode == 2) { }
                Invoke(nameof(OnReload), 3f);
            }
        }
        //make this behaviour depend
    }
    internal void OnReload()
    {
        bulletAmmount = bulletAmmountOrigin;
        isAllowFire = true;
        if (gameMode == 1) gameplayCtr.UpdateCurAmmo(bulletAmmount);
        else if (gameMode == 2) { }
    }
}
