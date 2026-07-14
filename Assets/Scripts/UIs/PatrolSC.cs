using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PatrolSC : MonoBehaviour
{
    public class PatrolRewardSC : MonoBehaviour
    {
        [HideInInspector] DataSC data;
        [HideInInspector] GeneralSC genCtr;
        [HideInInspector] HomeSC menuCtr;

        private const string LastPatrolTimeKey = "LastPatrolTime";
        private const string PatrolStreakKey = "PatrolStreak";
        private int rewardToGive = 10; // example reward, x2 for each time count
        private bool isAllowDailyClaim;
        private int streakDaily;
        private string lastCollectDay;

        [SerializeField] List<Button> rewardBtn = new List<Button>();

        [SerializeField] List<Button> dailyRewardBtn = new List<Button>();
        private void Awake()
        {
            genCtr = GameObject.Find("GenGameControlMN").GetComponent<GeneralSC>();
            data = GameObject.Find("OBJ_Data").GetComponent<DataSC>();
            menuCtr = GameObject.Find("OBJ_Home").GetComponent<HomeSC>();
            OnCheckDailyClaimOnInit();
        }
        void Start()
        {
            isAllowDailyClaim = false;
            streakDaily = data.pDailyStreak;
            lastCollectDay = "";
            rewardToGive = 0;
            ShowRewardDaily();
        }

        #region Handle Claim Daily
        void ShowRewardDaily()
        {
            print(data.pAllowClaimDaily);
            if (data.pLastDailyClaim == "")
            {
                //First day of play
                isAllowDailyClaim = true;
                for (int i = 0; i < rewardBtn.Count; i++)
                {
                    rewardBtn[i].GetComponent<Button>().interactable = false;
                }
                rewardBtn[0].GetComponent<Button>().interactable = true;
            }
            else
            {
                if (genCtr.today != data.pLastDailyClaim)
                {
                    //New day access + unclaimed
                    for (int i = 0; i < rewardBtn.Count; i++)
                    {
                        rewardBtn[streakDaily].GetComponent<Button>().interactable = false;
                    }

                    if (streakDaily >= 1 && streakDaily < 8)
                    {
                        //Lock previous day claim buttons
                        for (int i = 0; i < streakDaily; i++)
                        {
                            rewardBtn[i].GetComponent<Button>().interactable = false;
                        }

                        for (int j = streakDaily + 1; j > rewardBtn.Count; j++)
                        {
                            rewardBtn[j].GetComponent<Button>().interactable = false;
                        }
                        rewardBtn[streakDaily].GetComponent<Button>().interactable = true;

                        isAllowDailyClaim = false;
                    }
                }
                else if (genCtr.today == data.pLastDailyClaim)
                {
                    if (data.pAllowClaimDaily == 1)
                    {
                        //Same day access + claimed
                        isAllowDailyClaim = false;
                        for (int i = 0; i < rewardBtn.Count; i++)
                        {
                            rewardBtn[i].GetComponent<Button>().interactable = false;
                        }
                    }
                    else if (data.pAllowClaimDaily == 0)
                    {
                        //Same day, unclaimed
                        isAllowDailyClaim = true;
                        for (int i = 0; i < rewardBtn.Count; i++)
                        {
                            rewardBtn[i].GetComponent<Button>().interactable = false;
                        }
                        rewardBtn[streakDaily].GetComponent<Button>().interactable = true; //Enable only able-to-claim button
                    }
                }
            }
        }
        public void OnClaimDaily()
        {
            int tempFinalScoreToOverride;
            SelectRewardDaily();
            rewardBtn[streakDaily].GetComponent<Button>().interactable = false;
            lastCollectDay = DateTime.Today.Day.ToString();
            isAllowDailyClaim = false;
            tempFinalScoreToOverride = rewardToGive * streakDaily;
            streakDaily++;
            data.UpdateAllowClaimDaily(1);

            print("tempFinalScoreToOverride = " + tempFinalScoreToOverride);

            data.UpdateTotalScore(tempFinalScoreToOverride); // Update score
            data.UpdateStreak(streakDaily); //Update streak
            data.UpdatePatrolDailyReward(lastCollectDay); //Update last collect day
            ShowRewardDaily();
            menuCtr.UpdateUI();
        }
        private void SelectRewardDaily()
        {
            switch (streakDaily)
            {
                case 0:
                    rewardToGive = 10;
                    break;
                case 1:
                    rewardToGive = 20;
                    break;
                case 2:
                    rewardToGive = 40;
                    break;
                case 3:
                    rewardToGive = 80;
                    break;
                case 4:
                    rewardToGive = 160;
                    break;
                case 5:
                    rewardToGive = 320;
                    break;
                case 6:
                    rewardToGive = 640;
                    break;
                case 7:
                    rewardToGive = 1280;
                    break;
            }
        }
        #endregion

        private void OnCheckDailyClaimOnInit()
        {
            if (genCtr.today != data.pLastDailyClaim)
            {
                data.UpdateAllowClaimDaily(0);
                isAllowDailyClaim = true;
            }
            else
            {
                isAllowDailyClaim = false;
            }
        }
    }
}
