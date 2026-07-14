using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class GameplaySC : MonoBehaviour
{
    [HideInInspector] GeneralSC genControl;
    [HideInInspector] DataSC data;
    [SerializeField] DinoSC character;
    [SerializeField] GameObject ground, moutainBG, biomeBG;
    [SerializeField] SpawnerSC spawnControl;
    //[SerializeField] List<GameObject> groundToSpawn = new List<GameObject>();
    [SerializeField] List<GameObject> livesArray = new List<GameObject>();
    [SerializeField] Camera curCamCor;
    [SerializeField] Text ingameScoreTxt, levelTxt;

    private Vector3 currentGroundPos;
    public int curScore, curLvl, maxLvl;
    public CinemachineVirtualCamera vcam;
    private Transform lookTarget;
    public bool isEnablePlay; //Pause/Resume checker
    private void Awake()
    {
        genControl = GameObject.Find("GenGameControlMN").GetComponent<GeneralSC>();
        data = GameObject.Find("OBJ_Data").GetComponent<DataSC>();
        genControl.AssistGameplayElements(2);
        isEnablePlay = false;
    }

    private void Start()
    {
        spawnControl = GameObject.Find("OBJ_SpawnerArcade").GetComponent<SpawnerSC>();
        StartCoroutine(GroundAnim()); //Control of ground, cloud and other relate object on scene;
        StartCoroutine(BiomeBGAnim());
        StartCoroutine(MoutainAnim());
        lookTarget = null;
        GenerateGameplay();
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
        character = Instantiate(character, new Vector3(-4, 0, 0), Quaternion.identity);
        spawnControl.OnAssistSpawnerElements();
        CancelInvoke(nameof(AddScore));
        isEnablePlay = true;
        lookTarget = GameObject.Find("OBJ_Tank(Clone)").GetComponent<Transform>().transform;
        SetIngameStat();
        if(livesArray.Count != 2) { ShowLives(); }
        InvokeRepeating(nameof(AddScore), 1f, 1f);
        //vcam.Follow = lookTarget;
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
        lookTarget = null;
        //vcam.Follow = null;
        spawnControl.StopGameplay();
        CancelInvoke(nameof(AddScore));
    }
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
    public void ShowLives()
    {
        //call by start/replay game
        if (livesArray[2].activeSelf == false)
        {
            livesArray[0].SetActive(true);
            livesArray[1].SetActive(true);
            livesArray[2].SetActive(true);
        }
    }
    public void UnShowLive(int liveOders)
    {
        //call each time hit
        if(liveOders >= 0)
        {
            livesArray[liveOders].SetActive(false);
        }
    }
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

    public IEnumerator OnChangeBackgroundColor()
    {
        yield return new WaitForSeconds(0.5f);
        //curCamCor.backgroundColor.g
    }

    #region Character controller
    public void AttackNormal()
    {
        print("in attack normal");
        character.OnAttackByTouch();
    }
    public void MoveForward()
    {
        print("in move forward");
        character.CharForwardConsole();
    }
    public void MoveBackward()
    {
        print("in move back");
        character.CharBackwardConsole();
    }
    #endregion
}
