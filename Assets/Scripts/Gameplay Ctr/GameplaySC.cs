using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplaySC : MonoBehaviour
{
    [HideInInspector] GeneralSC genControl;
    [HideInInspector] DataSC data;
    [SerializeField] List<DinoSC> playerList = new List<DinoSC>();
    [SerializeField] GameObject ground, moutainBG, biomeBG;
    [SerializeField] SpawnerSC spawnControl;
    [SerializeField] List<Sprite> groundToSpawn = new List<Sprite>();

    [SerializeField] List<Sprite> biomeBGList = new List<Sprite>();
    [SerializeField] Text ingameScoreTxt, levelTxt, curAmmoTxt;
    [SerializeField] Slider playerHPBar;
    public DinoSC curPlayer;
    public int curScore, curLvl, maxLvl;
    public bool isEnablePlay; //Pause/Resume checker
    public string playerName;
    public int curPlayerApparance;
    private void Awake()
    {
        genControl = GameObject.Find("GenGameControlMN").GetComponent<GeneralSC>();
        data = GameObject.Find("OBJ_Data").GetComponent<DataSC>();
        genControl.AssistGameplayElements(2);
        isEnablePlay = false;
    }

    private void Start()
    {
        StartCoroutine(GroundAnim()); //Control of ground, cloud and other relate object on scene;
        StartCoroutine(BiomeBGAnim());
        StartCoroutine(MoutainAnim());
        GenerateGameplay();
        OnDecideRandomBackroundApparance();
    }

    private void OnDecideRandomBackroundApparance()
    {
        int tempOrder;
        tempOrder = Random.Range(0, groundToSpawn.Count);
        ground.GetComponent<SpriteRenderer>().sprite = groundToSpawn[tempOrder];
        biomeBG.GetComponent<SpriteRenderer>().sprite = biomeBGList[tempOrder];
    }

    public void OnPause()
    {
        genControl.ShowPause(true);
        isEnablePlay = false;
        spawnControl.PauseGame(isEnablePlay);
    }
    public void OnResume()
    {
        genControl.ShowPause(false);
        isEnablePlay = true;
        spawnControl.ResumeGame(isEnablePlay);
    }
    public void OnSetting() { genControl.ShowSetting(); }
    public void OnPlayerInfo() { genControl.ShowPlayerInfo(); }
    public void OnExitGame() { Application.Quit(0); } //exitgame once Gameover

    public void GenerateGameplay()
    {
        curPlayer = Instantiate(playerList[curPlayerApparance], new Vector3(-4, 0, 0), Quaternion.identity); 
        playerName = curPlayer.name;
        spawnControl = GameObject.Find("OBJ_SpawnerArcade").GetComponent<SpawnerSC>();
        spawnControl.OnAssistSpawnerElements();

        CancelInvoke(nameof(AddScore));
        isEnablePlay = true;
        SetIngameStat();
        InvokeRepeating(nameof(AddScore), 1f, 1f);
    }
    void AddScore()
    {
        if(isEnablePlay == true)
        {
            curScore += 1;
            UpdateIngameScore();
            if (curScore >= curLvl * 10)
            {
                curLvl++;
                spawnControl.CaculatingTimeSpawn();
                UpdateIngameLvl();
            }
        }
        else if(isEnablePlay == false)
        {
            print("In pause game");
        }
    } 
    public void OnGameLose()
    {
        UpdatePlayerPrefsStat();
        spawnControl.StopGameplay();
        CancelInvoke(nameof(AddScore));
    }

    #region Background Anim
    private IEnumerator GroundAnim()
    {                
        yield return new WaitForSeconds(0.1f);
        if (ground.transform.position.x <= -8) ground.transform.position = new Vector3(8, 0, 0);
        else ground.transform.position += Vector3.left;
        StartCoroutine(GroundAnim());
    }

    private IEnumerator MoutainAnim()
    {
        yield return new WaitForSeconds(0.5f);
        if (moutainBG.transform.position.x <= -8) moutainBG.transform.position = new Vector3(8, 1, 0);
        else moutainBG.transform.position += Vector3.left;
        StartCoroutine(MoutainAnim());
    }
    private IEnumerator BiomeBGAnim()
    {
        yield return new WaitForSeconds(0.3f);
        if (biomeBG.transform.position.x <= -8) biomeBG.transform.position = new Vector3(8, 2, 0);
        else biomeBG.transform.position += Vector3.left;
        StartCoroutine(BiomeBGAnim());
    }
    #endregion
    public void UpdatePlayerPrefsStat()
    {
        int newLvl;
        int newTotalScore;
        int oldTotalScore;
        oldTotalScore = data.pTotalScore;
        newTotalScore = curScore + oldTotalScore;
        data.UpdateTotalScore(newTotalScore);
        if (maxLvl <= curLvl) 
        { 
            newLvl = curLvl;
            data.UpdateHighLv(newLvl);
        }
        PlayerPrefs.SetInt("soundState", 1);
        PlayerPrefs.SetInt("sfxState", 1);
    }
    private void SetIngameStat()
    {
        curLvl = 0;
        curScore = 0;
        ingameScoreTxt.text = curScore.ToString();
        levelTxt.text = curLvl.ToString();
    }
    private void UpdateIngameScore() => ingameScoreTxt.text = curScore.ToString();
    private void UpdateIngameLvl() => levelTxt.text = curLvl.ToString();

    #region Character controller
    public void AttackNormal()
    {
        curPlayer.OnAttackByTouch();
    }
    public void MoveForward()
    {
        curPlayer.CharForwardConsole();
    }
    public void MoveBackward()
    {
        curPlayer.CharBackwardConsole();
    }
    public void Dodge()
    {
        curPlayer.CharDodgeConsole();
    }
    #endregion

    public void UpdateHPBar(int state, int value)
    {
        if(state == 0) 
        {
            playerHPBar.maxValue = curPlayer.hp;
            playerHPBar.value = curPlayer.hp;
        }
        else if(state == 1)
        {
            playerHPBar.value = value;
        }

    }
    public void UpdateCurAmmo(int value)
    {
        //curAmmoTxt.text = curPlayer.bulletAmmount.ToString();
        if (value == -1)
        {
            curAmmoTxt.text = "RELOADING";
        }
        else
        {
            curAmmoTxt.text = value.ToString();
        }
    }
}
