using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
 
public class ChallengeSC : MonoBehaviour
{
    //Score from this game mode not contribue to data score/coin
    [HideInInspector] GeneralSC genctr;
    [HideInInspector] DataSC data;
    [SerializeField] Text pScoreChallenge, pSurviveChallenge;
    [SerializeField] Text objective01Txt, objective02Txt, objective03Txt, rewardTxt;
    [SerializeField] GameObject startPanel, winPanel;
    [SerializeField] GameObject spawnerGroup, ground, moutainBG, biomeBG;
    [SerializeField] DinoSC character;
    [Header("Spawner References")]
    [SerializeField] ChallengeSpawnerSC spawnerChallengeCtr;

    private int pHighestLv, numberOfObjective;
    private float spawnRate;
    private int challengeScore, challenSurviveTime; //variable for win condition check
    private string objective01, objective02, objective03;
    private bool scoreOccupied, surviveOccupied, killOccupied;
    private int tempScoreTarget, tempSurviveTarget, tempKillTarget, coinToReward, gemToReward;
    public bool isEnablePlay, isContinuePlay;

    [Header("Platform References")]
    [SerializeField] List<GameObject> platformL = new List<GameObject>();

    //VCam section
    public CinemachineVirtualCamera vcam;
    private Transform lookTarget;
    void Start()
    {
        Init();
        GenerateChallenge();
        StartCoroutine(GroundAnim()); //Control of ground, cloud and other relate object on scene;
        StartCoroutine(BiomeBGAnim());
        StartCoroutine(MoutainAnim());
    }
    void Update() 
    {    }

    private void Init()
    {
        character = Instantiate(character, new Vector3(0, 0, 0), Quaternion.identity);
        genctr = GameObject.Find("GenGameControlMN").GetComponent<GeneralSC>();
        data = GameObject.Find("OBJ_Data").GetComponent<DataSC>();
        spawnerChallengeCtr = GameObject.Find("SpawnerGroup").GetComponent<ChallengeSpawnerSC>();
        
        genctr.AssistGameplayElements(3);
        winPanel.gameObject.SetActive(false);
        GatheringData();
        objective01 = objective02 = objective03 = "";
        isEnablePlay = false;
        isContinuePlay = false;
        spawnRate = 0;
        tempKillTarget = tempScoreTarget = tempSurviveTarget = 0;
    }

    private void GatheringData()
    {
        pHighestLv = data.pHighLv;
    }
    private void GenerateChallenge()
    {
        DetermindChallenge();
        SelectReward();
        GenerateGameplay();
        StartCoroutine(WaitToUnShow());
    }
    private void DetermindChallenge()
    {
        DetermineAmountObjective();
        if(numberOfObjective == 1 || numberOfObjective == 0)
        {
            SelectFirstObjective();
            objective02Txt.gameObject.SetActive(false);
            objective03Txt.gameObject.SetActive(false);
        }
        else if(numberOfObjective == 2)
        {
            SelectFirstObjective();
            SelectSecondObjective();
            objective03Txt.gameObject.SetActive(false);
        }
        else if(numberOfObjective >= 3)
        {
            SelectFirstObjective();
            SelectSecondObjective();
            SelectThirdObjective();
        }
    }

    #region Challenge Setup
    private void DetermineAmountObjective()
    {
        //Tell system how much objective need to show in game
        if (pHighestLv > 0 && pHighestLv <= 10) numberOfObjective = 1;
        else if (pHighestLv > 10 && pHighestLv <= 50) numberOfObjective = 2;
        else if (pHighestLv > 50) numberOfObjective = 3;
    }
    private void SelectFirstObjective()
    {
        int tempObjectiveOder = Random.Range(1, 4);
        if(tempObjectiveOder == 1)
        {
            scoreOccupied = true;
            if (pHighestLv <= 10) tempScoreTarget = Random.Range(20, 100);
            else if (pHighestLv > 10 && pHighestLv <= 50) tempScoreTarget = Random.Range(150, 300);
            else if (pHighestLv > 50) tempScoreTarget = Random.Range(300, 500);
            objective01 = "OBJECT: SCORE " + tempScoreTarget.ToString();
        }else if(tempObjectiveOder == 2) 
        {
            surviveOccupied = true;
            if (pHighestLv <= 10) tempSurviveTarget = Random.Range(20, 60);
            else if (pHighestLv > 10 && tempSurviveTarget <= 50) tempSurviveTarget = Random.Range(60, 180);
            else if (pHighestLv > 50) tempSurviveTarget = Random.Range(180, 300);
            objective01 = "OBJECTIVE: SURVIVE FOR " + tempSurviveTarget + " S";
        }
        else if(tempObjectiveOder == 3)
        {
            killOccupied = true;
            if (pHighestLv <= 10) tempKillTarget = Random.Range(20, 60);
            else if (pHighestLv > 10 && tempKillTarget <= 50) tempScoreTarget = Random.Range(60, 180);
            else if (pHighestLv > 50) tempKillTarget = Random.Range(180, 300);
            objective01 = "OBJECTIVE: KILL " + tempKillTarget + " MORPINOS";
        }
    }
    private void SelectSecondObjective()
    {
        int tempObjectiveOder = Random.Range(1, 4);
        switch (tempObjectiveOder)
        {
            case 1:
                if (scoreOccupied == false)
                {
                    if (pHighestLv <= 10) tempScoreTarget = Random.Range(20, 100);
                    else if (pHighestLv > 10 && pHighestLv <= 50) tempScoreTarget = Random.Range(150, 300);
                    else if (pHighestLv > 50) tempScoreTarget = Random.Range(300, 500);
                    objective02 = "OBJECT: SCORE " + tempScoreTarget.ToString();
                }
                else SelectSecondObjective();
                break;
            case 2:
                if (surviveOccupied == false)
                {
                    if (pHighestLv <= 10) tempSurviveTarget = Random.Range(20, 60);
                    else if (pHighestLv > 10 && tempSurviveTarget <= 50) tempSurviveTarget = Random.Range(60, 180);
                    else if (pHighestLv > 50) tempSurviveTarget = Random.Range(180, 300);
                    objective02 = "OBJECTIVE: SURVIVE FOR " + tempSurviveTarget + " S";
                }
                else SelectSecondObjective();
                break;
            case 3:
                if (killOccupied == false)
                {
                    killOccupied = true;
                    if (pHighestLv <= 10) tempKillTarget = Random.Range(20, 60);
                    else if (pHighestLv > 10 && tempKillTarget <= 50) tempScoreTarget = Random.Range(60, 180);
                    else if (pHighestLv > 50) tempKillTarget = Random.Range(180, 300);
                    objective02 = "OBJECTIVE: KILL " + tempKillTarget + " MORPINOS";
                }
                else SelectSecondObjective();
                break;
        }
    }
    private void SelectThirdObjective()
    {
        int tempObjectiveOder = Random.Range(1, 4);
        switch (tempObjectiveOder)
        {
            case 1:
                if (scoreOccupied == false)
                {
                    if (pHighestLv <= 10) tempScoreTarget = Random.Range(20, 100);
                    else if (pHighestLv > 10 && pHighestLv <= 50) tempScoreTarget = Random.Range(150, 300);
                    else if (pHighestLv > 50) tempScoreTarget = Random.Range(300, 500);
                    objective03 = "OBJECT: SCORE " + tempScoreTarget.ToString();
                }
                else SelectSecondObjective();
                break;
            case 2:
                if (surviveOccupied == false)
                {
                    if (pHighestLv <= 10) tempSurviveTarget = Random.Range(20, 60);
                    else if (pHighestLv > 10 && tempSurviveTarget <= 50) tempSurviveTarget = Random.Range(60, 180);
                    else if (pHighestLv > 50) tempSurviveTarget = Random.Range(180, 300);
                    objective03 = "OBJECTIVE: SURVIVE FOR " + tempSurviveTarget + " S";
                }
                else SelectSecondObjective();
                break;
            case 3:
                if (killOccupied == false)
                {
                    killOccupied = true;
                    if (pHighestLv <= 10) tempKillTarget = Random.Range(20, 60);
                    else if (pHighestLv > 10 && tempKillTarget <= 50) tempScoreTarget = Random.Range(60, 180);
                    else if (pHighestLv > 50) tempKillTarget = Random.Range(180, 300);
                    objective03 = "OBJECTIVE: KILL " + tempKillTarget + " MORPINOS";
                }
                else SelectSecondObjective();
                break;
        }
    }
    private void SelectReward()
    {
        int tempRewardOder = Random.Range(1, 4);
        if(tempRewardOder == 1)
        {
            //Case of reward Coin
            coinToReward = Random.Range(10, 50);
            gemToReward = 0;
            rewardTxt.text = "REWARD: " + coinToReward + " COIN ONCE WIN!";
        }
        else if(tempRewardOder == 2)
        {
            //Case of reward Free Gem
            gemToReward = Random.Range(1, 3);
            coinToReward = 0;
            rewardTxt.text = "REWARD: " + gemToReward + " GEM ONCE WIN!";
        }
        else if(tempRewardOder == 3)
        {
            coinToReward = Random.Range(10, 50);
            gemToReward = Random.Range(1, 3);
            rewardTxt.text = "REWARD: " + coinToReward + " COIN AND " +gemToReward + " GEM ONCE WIN!";
            //Case of Reward both Gem & Coin
        }


        if(!startPanel.activeSelf && isContinuePlay == true)
        {
            //Case of replay
            OnShowObjectivePanel(true);
        }
        else if(startPanel.activeSelf == true && isContinuePlay== false)
        {
            //Case of init play
            OnShowObjectivePanel(true);
        }
    }
    #endregion

    #region Objective Panel
    private void OnShowObjectivePanel(bool isShow)
    {
        if(isShow == false)
        {
            objective01 = objective02 = objective03 = "";
            objective01Txt.text = objective01;
            objective02Txt.text = objective02;
            objective03Txt.text = objective03;

            isEnablePlay = true;
            StartToSpawnEnemies();
            print("in Disable Panel & enablePlay = true");
        }
        else if(isShow == true)
        {
            objective01Txt.text = objective01;
            objective02Txt.text = objective02;
            objective03Txt.text = objective03;
            isEnablePlay = false;
        }
        startPanel.gameObject.SetActive(isShow);
    }
    private IEnumerator WaitToUnShow()
    {
        yield return new WaitForSeconds(5f);
        OnShowObjectivePanel(false);
    }
    #endregion

    #region Gameplay Control
    private void TransformSpawnerGroup()
    {
        float tempY = 5;
        spawnerGroup.gameObject.transform.position = new Vector3(5, tempY, 0);
    }
    private void GenerateGameplay()
    {
        DetermineSpawnerNumber();

        TransformSpawnerGroup();
        spawnerChallengeCtr.SetGamemode();
        lookTarget = GameObject.Find("OBJ_Dino_Step(Clone)").GetComponent<Transform>().transform;
        SetIngamePlayerStat();
        StartToSpawnEnemies();
        InvokeRepeating(nameof(AddChallengeScore), 1f, 1f);
        InvokeRepeating(nameof(AddChallenegSurviveTime), 1f, 1f);
        vcam.Follow = lookTarget;
    }
    private void DetermineSpawnerNumber()
    {
        if (pHighestLv <= 30) spawnRate = Random.Range(2f, 5f);
        else if(pHighestLv > 30 && pHighestLv <= 100) spawnRate = Random.Range(1f, 2.5f);
        else if(pHighestLv > 100) spawnRate = Random.Range(0.75f, 1.25f);
    }
    private void SetIngamePlayerStat()
    {
        challengeScore = 0;
        challenSurviveTime = 0;
        pScoreChallenge.text = challengeScore.ToString();
        pSurviveChallenge.text = challenSurviveTime.ToString() + "s";
    }
    #endregion

    private void AddChallengeScore()
    {
        if(isEnablePlay == true)
        {
            challengeScore += 1;
            if (scoreOccupied == true)
            {
                if (challengeScore >= tempScoreTarget)
                {
                    OnWinChallenge();
                }
            }
            else if (killOccupied)
            {
                if (challengeScore >= tempKillTarget)
                {
                    OnWinChallenge();
                }
            }
            else if (scoreOccupied == true && killOccupied == true)
            {
                if (challengeScore >= tempScoreTarget && challengeScore >= tempKillTarget)
                {
                    OnWinChallenge();
                }
            }
        }
        pScoreChallenge.text = challengeScore.ToString();
    }
    private void AddChallenegSurviveTime()
    {
        if(isEnablePlay == true)
        {
            challenSurviveTime += 1;
            if (surviveOccupied == true)
            {
                if (challenSurviveTime >= tempSurviveTarget)
                {
                    OnWinChallenge();
                }
            }
        }
        pSurviveChallenge.text = challenSurviveTime.ToString() + "s";
    }
    private void OnWinChallenge()
    {
        isEnablePlay = false;
        spawnerChallengeCtr.PauseGame(isEnablePlay);
        winPanel.gameObject.SetActive(true);

        if(coinToReward != 0 && gemToReward == 0)
        {
            int tempScore;
            tempScore = coinToReward + data.pTotalScore;
            data.UpdateTotalScore(tempScore);
        }else if(coinToReward == 0 && gemToReward != 0)
        {
            int tempScore;
            tempScore = gemToReward + data.pGems;
            data.UpdateTotalGem(tempScore);
        }else if(coinToReward != 0 && gemToReward != 0)
        {
            int tempCoin, tempGems;
            tempCoin = coinToReward + data.pTotalScore;
            tempGems = gemToReward + data.pGems;
            data.UpdateTotalScore(tempCoin);
            data.UpdateTotalGem(tempGems);
        }

    }
    public void OnToHome()
    {
        genctr.ToHome();
    }
    public void OnNextChallenge()
    {
        isContinuePlay = true;
        spawnerChallengeCtr.ResumeGame(isEnablePlay);
    }
    private void StartToSpawnEnemies()
    {
        spawnerChallengeCtr.StartGameOnInit();
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

    public void OnGameLose()
    {
        isEnablePlay = false;
        lookTarget = null;
        vcam.Follow = null;
        spawnerChallengeCtr.StopGameplay();
        genctr.ShowgameOver(true);
        CancelInvoke(nameof(AddChallengeScore));
        CancelInvoke(nameof(AddChallenegSurviveTime));
    }
}
