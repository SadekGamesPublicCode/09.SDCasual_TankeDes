using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSC : MonoBehaviour
{
    [HideInInspector] PlayerInforSC infor;
    public string deviceID;
    public bool isFirstPlay;

    //Player's data variables
    public string pName;
    public int pHighScore, pTotalScore, pHighLv, pStoryLvl, pGems;
    public int pSFX, pTheme;
    public int pAbility, pWeapon, pAllowClaimDaily, pAllowClaimMonthly;
    public int pDailyStreak, pMonthlyStreak;
    public string pLastDailyClaim, pLastMonthlyClaim;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        SettingStart();
    }
    void Start() { }
    void Update() { }

    #region Local Handle
    private void SettingStart()
    {
        deviceID = SystemInfo.deviceUniqueIdentifier;
        if (CheckFirstPlay() == true)
        {
            SetNewPlayer();
        }
        else if (CheckFirstPlay() == false)
        {
            LoadOldPlayer();
        }
        infor = GameObject.Find("PNL_PlayerInfo").GetComponent<PlayerInforSC>();
    }

    #endregion

    #region Player Data Handle
    private void SetNewPlayer()
    {
        Debug.Log("in new layer");
        string subStringID = deviceID.Substring(8, 12).ToString();
        string nameFistPlay = "Player" + subStringID;
        PlayerPrefs.SetInt("HasPlayed", 1);
        PlayerPrefs.SetString("PlayerName", nameFistPlay);

        PlayerPrefs.SetInt("Highscore", 0); //For total overview, leaderboard
        PlayerPrefs.SetInt("Totalscore", 0); //Actual player in-game currency
        PlayerPrefs.SetInt("TotalGems", 0); //Player's PIA currency
        PlayerPrefs.SetInt("PHighestLevel", 0);
        PlayerPrefs.SetInt("CurrentStoryLevel", 1);
        
        PlayerPrefs.SetInt("soundState", 1);
        PlayerPrefs.SetInt("sfxState", 1);

        PlayerPrefs.SetInt("CurWeaponID", 0); //index of weapon order in list. 0 is non
        PlayerPrefs.SetInt("CurAblility", 0); //index of ability order in list. 0 is non

        //Patrol Reward
        PlayerPrefs.SetInt("AllowClaimDaily", 0);
        PlayerPrefs.SetInt("AllowClaimMonthly", 0);
        PlayerPrefs.SetString("LastPatrolDailyTime", "");
        PlayerPrefs.SetString("LastPatrolMonthlyTime", "");
        PlayerPrefs.SetInt("PatrolDailyStreak", 0);
        PlayerPrefs.SetInt("PatrolMonthlyStreak", 0);

        Invoke("LoadOldPlayer", 3f); //De tam thoi
    }
    private void LoadOldPlayer()
    {
        pName = PlayerPrefs.GetString("PlayerName");
        pHighScore = PlayerPrefs.GetInt("Highscore");
        pHighLv = PlayerPrefs.GetInt("PHighestLevel");
        pStoryLvl = PlayerPrefs.GetInt("CurrentStoryLevel");
        pTotalScore = PlayerPrefs.GetInt("Totalscore");
        pGems = PlayerPrefs.GetInt("TotalGems");
        pTheme = PlayerPrefs.GetInt("soundState");
        pSFX = PlayerPrefs.GetInt("sfxState");
        pAbility = PlayerPrefs.GetInt("CurAblility");
        pWeapon = PlayerPrefs.GetInt("CurWeaponID");

        //Patrol Reward
        pLastDailyClaim = PlayerPrefs.GetString("LastPatrolDailyTime");
        pLastMonthlyClaim = PlayerPrefs.GetString("LastPatrolMonthlyTime");
        pAllowClaimDaily = PlayerPrefs.GetInt("AllowClaimDaily");
        pAllowClaimMonthly = PlayerPrefs.GetInt("AllowClaimMonthly");
        pDailyStreak = PlayerPrefs.GetInt("PatrolDailyStreak");
        pMonthlyStreak = PlayerPrefs.GetInt("PatrolMonthlyStreak");
    }
    public void DataDelete()
    {
        PlayerPrefs.DeleteAll();
        SetNewPlayer();
        infor.GetPlayerData();
    }
    public void UploadPlayerData()
    {

    }
    #endregion

    #region Data Update
    public void UpdatePName(string name)
    {
        PlayerPrefs.SetString("PlayerName", name);
        pName = PlayerPrefs.GetString("PlayerName");
    }
    public void UpdateHighScore(int highScore)
    {
        PlayerPrefs.SetInt("Highscore", highScore);
        pHighScore = PlayerPrefs.GetInt("Highscore");
    }
    public void UpdateHighLv(int highLv)
    {
        PlayerPrefs.SetInt("PHighestLevel", highLv);
        pHighLv = PlayerPrefs.GetInt("PHighestLevel");
    }
    public void UpdateStoryLv(int curStoryLv)
    {
        PlayerPrefs.SetInt("CurrentStoryLevel", curStoryLv);
        pStoryLvl = PlayerPrefs.GetInt("CurrentStoryLevel");
    }
    public void UpdateTotalScore(int currency)
    {
        PlayerPrefs.SetInt("Totalscore", currency);
        pTotalScore = PlayerPrefs.GetInt("Totalscore");
    }
    public void UpdateTotalGem(int gems)
    {
        PlayerPrefs.SetInt("TotalGems", gems);
        pGems = PlayerPrefs.GetInt("TotalGems");
    }
    public void UpdateSFXState(int sfxState)
    {
        PlayerPrefs.SetInt("sfxState", sfxState);
        pSFX = PlayerPrefs.GetInt("sfxState");
    }
    public void UpdateThemeState(int thameState)
    {
        PlayerPrefs.SetInt("soundState", thameState);
        pTheme = PlayerPrefs.GetInt("soundState");
    }
    public void UpdateAbility(int abilityOder)
    {
        PlayerPrefs.SetInt("CurAblility", abilityOder);
        pTotalScore = PlayerPrefs.GetInt("CurAblility");
    }
    public void UpdateWeapon(int weaponOder)
    {
        PlayerPrefs.SetInt("CurWeaponID", weaponOder);
        pTotalScore = PlayerPrefs.GetInt("CurWeaponID");
    }
    public void UpdatePatrolDailyReward(string lastPatrolDaily)
    {
        PlayerPrefs.SetString("LastPatrolDailyTime", lastPatrolDaily);
        pLastDailyClaim = PlayerPrefs.GetString("LastPatrolDailyTime");
    }
    public void UpdateAllowClaimDaily(int state)
    {
        PlayerPrefs.SetInt("AllowClaimDaily", state);
        pAllowClaimDaily = PlayerPrefs.GetInt("AllowClaimDaily");
    }
    public void UpdateStreak(int value)
    {
        PlayerPrefs.SetInt("PatrolDailyStreak", value);
        pDailyStreak = value;
    }
    #endregion

    #region Checking Zone
    private bool CheckFirstPlay()
    {
        print("FirstPlay: " + PlayerPrefs.GetInt("HasPlayed"));
        if (PlayerPrefs.GetInt("HasPlayed") == 1)
        {
            return isFirstPlay = false;
        }
        else return true;
    }
    #endregion
}
