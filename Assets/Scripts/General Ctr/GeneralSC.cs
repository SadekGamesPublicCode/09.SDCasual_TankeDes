using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GeneralSC : Singleton<GeneralSC>
{
    sbyte overalScore; //player's total score, use as in-game currency and leaderboard in future
    sbyte ingameScore; //player ingame score, stack to overalScore once game over or back to menu, count by time alive
    [HideInInspector] DataSC data;
    [HideInInspector] PauseSC pnlPause;
    [HideInInspector] PlayerInforSC playerInfo;
    [HideInInspector] SettingSC pnlSetting;
    [HideInInspector] GameOverSC gameOverSC;
    [HideInInspector] GameplaySC gameCtr;
    [HideInInspector] SoundSC soundOBJ;
    [HideInInspector] MainThemeSC mainThemeOBJ;
    [HideInInspector] GameObject editNamePanel, noticPanel;
    [SerializeField] Text versionTxt;
    [SerializeField] InputField pNameToEdit;
    [HideInInspector] CreditSC credit;
    [HideInInspector] RatingSC ratePnl;
    [HideInInspector] HomeSC menuCtr;
    [HideInInspector] ChallengeSC challengeCtr;
    [HideInInspector] AdsMN adsMn;

    public bool isLoadByInit;
    public int deviceMode;
    public sbyte sceneActiveindex;
    public string today;
    public int gameMode;

    private int interAdsCount, rewardAdsCount, targetInterAdsCount, targetRewardAdsCount;
    void Start()
    {
        versionTxt.text = Application.version.ToString();
        Init();
        Invoke(nameof(AssistAdsMn), 10f);
        Invoke(nameof(ShowRatePnl), 600f);
    }
    private void Update() { }

    private void Init()
    {
        isLoadByInit = true;
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WebGLPlayer || Application.platform == RuntimePlatform.WindowsPlayer) { deviceMode = 1; }
        else if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.OSXPlayer) { deviceMode = 0; }
        data = GameObject.Find("OBJ_Data").GetComponent<DataSC>();
        pnlPause = GameObject.Find("PNL_Pause").GetComponent<PauseSC>();
        pnlSetting = GameObject.Find("PNL_Setting").GetComponent<SettingSC>();
        gameOverSC = GameObject.Find("PNL_GameOver").GetComponent<GameOverSC>();
        playerInfo = GameObject.Find("PNL_PlayerInfo").GetComponent<PlayerInforSC>();
        soundOBJ = GameObject.Find("OBJ_SoundSFX").GetComponent<SoundSC>();
        mainThemeOBJ = GameObject.Find("OBJ_SoundMusic").GetComponent<MainThemeSC>();
        credit = GameObject.Find("PNL_Credit").GetComponent<CreditSC>();
        ratePnl = GameObject.Find("PNL_Rating").GetComponent<RatingSC>();
        editNamePanel = GameObject.Find("PNL_EditName");
        noticPanel = GameObject.Find("PNL_NoticPnl");

        ingameScore = 0;
        today = DateTime.Today.Day.ToString();

        interAdsCount = 0;
        rewardAdsCount = 0;
        targetInterAdsCount = 3;
        targetRewardAdsCount = 2;

        DisableUIs();
    }
    public void AssistGameplayElements(int sceneOrder)
    {
        isLoadByInit = false; //Tell system that game already setuped for first start
        gameOverSC.AssitGameControl();
        switch (sceneOrder)
        {
            case 1:
                menuCtr = GameObject.Find("OBJ_Home").GetComponent<HomeSC>();
                break;
            case 2:
                //Arcade
                gameCtr = GameObject.Find("OBJ_ArcadeMN").GetComponent<GameplaySC>();
                break;
            case 3:
                //Challeneg
                challengeCtr = GameObject.Find("OBJ_CHallengeMN").GetComponent<ChallengeSC>();
                break;
            case 4:
                //Story
                break;
        }

    }
    private void AssistAdsMn()
    {
        adsMn = GameObject.Find("AdsMN").GetComponent<AdsMN>();
        if (adsMn == null) { print("adsMN null"); }
    }
    private void DisableUIs()
    {
        pnlPause.gameObject.SetActive(false);
        pnlSetting.gameObject.SetActive(false);
        gameOverSC.gameObject.SetActive(false);
        playerInfo.gameObject.SetActive(false);
        editNamePanel.gameObject.SetActive(false);
        credit.gameObject.SetActive(false);
        ratePnl.gameObject.SetActive(false);
        noticPanel.gameObject.SetActive(false);
    }
    public void ShowPause(bool show) => pnlPause.gameObject.SetActive(show);
    public void ShowSetting() => pnlSetting.gameObject.SetActive(true);
    public void ShowgameOver(bool show)
    {
        gameOverSC.gameObject.SetActive(show);
    }
    public void ShowPlayerInfo() => playerInfo.gameObject.SetActive(true);
    public void OnEditPlayerName()
    {
        string tempString = pNameToEdit.textComponent.text;
        data.UpdatePName(tempString);
        menuCtr.UpdateUI();
    }
    public bool isFirstPlay()
    {
        if (PlayerPrefs.GetInt("HasPlayed", 0) == 0) { return true; }
        else { return false; }
    }
    public void ToChallengeScene(bool isReplay) 
    {
        gameMode = 2;
        if (isReplay == true)
        {
            rewardAdsCount += 1;
            if (rewardAdsCount >= targetRewardAdsCount)
            {
                adsMn.ShowAds(2);
                SceneManager.LoadScene("02_Challenge");
                rewardAdsCount = 0;
            }
            else if (rewardAdsCount < targetRewardAdsCount)
            {
                SceneManager.LoadScene("02_Challenge");
            }
        }
        else if (isReplay == false)
        {
            gameMode = 1;
            if (isReplay == true)
            {
                rewardAdsCount += 1;
                if (rewardAdsCount >= targetRewardAdsCount)
                {
                    adsMn.ShowAds(2);
                    SceneManager.LoadScene("03_ArcadeScene");
                    rewardAdsCount = 0;
                }
                else if (rewardAdsCount < targetRewardAdsCount)
                {
                    SceneManager.LoadScene("03_ArcadeScene");
                }
            }
            else if (isReplay == false)
            {
                SceneManager.LoadScene("03_ArcadeScene");
            }
        }
    }
    public void ToArcadeScene(bool isReplay)
    {
        gameMode = 1;
        SceneManager.LoadScene("03_ArcadeScene");
    } 
    public void ToHome()
    {
        //update score
        //updtae high level
        gameMode = 0;
        interAdsCount += 1;
        //adsLoadChance = UnityEngine.Random.Range(0, 100);
        if (interAdsCount >= targetInterAdsCount)
        {
            adsMn.ShowAds(1);
            SceneManager.LoadScene("01_MainScene");
            interAdsCount = 0;
        }
        else if (interAdsCount < targetInterAdsCount)
        {
            SceneManager.LoadScene("01_MainScene");
        }

    }
    private void ShowRatePnl()
    {
        ratePnl.gameObject.SetActive(true);
    }
}
