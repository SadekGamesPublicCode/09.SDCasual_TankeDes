using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeSC : MonoBehaviour
{
    [HideInInspector] GeneralSC genControl;
    [HideInInspector] DataSC data;
    [HideInInspector] ShopSC shop;
    [HideInInspector] PatrolSC patrol;
    [HideInInspector] LeaderSC leader;
    [SerializeField] List<GameObject> cloudList = new List<GameObject>();
    [SerializeField] Text pNameTxt, overalScoreTxt,maxLevlTxt;
    public int totalScore, maxLvl;
    private void Awake()
    {
        genControl = GameObject.Find("GenGameControlMN").GetComponent<GeneralSC>();
        shop = GameObject.Find("PNL_Shop").GetComponent<ShopSC>();
        patrol = GameObject.Find("PNL_Patrol").GetComponent<PatrolSC>();
        leader = GameObject.Find("PNL_Leader").GetComponent<LeaderSC>();
        data = GameObject.Find("OBJ_Data").GetComponent<DataSC>();

        shop.gameObject.SetActive(false);
        patrol.gameObject.SetActive(false);
        leader.gameObject.SetActive(false);
    }
    void Start()
    {
        genControl.AssistGameplayElements(1);
        UpdateUI();
    }

    #region Scene redirect
    public void OnPlayChallenge() => genControl.ToChallengeScene(false);
    public void OnPlayArcade() => genControl.ToArcadeScene(false);
    #endregion

    public void UpdateUI()
    {
        pNameTxt.text = data.pName;
        totalScore = data.pTotalScore;
        maxLvl = data.pHighLv;

        maxLevlTxt.text = maxLvl.ToString();
        overalScoreTxt.text = totalScore.ToString();
    }

    #region Panel Controller
    public void OnSetting() => genControl.ShowSetting();
    public void OnInfor() => genControl.ShowPlayerInfo();
    #endregion
}
