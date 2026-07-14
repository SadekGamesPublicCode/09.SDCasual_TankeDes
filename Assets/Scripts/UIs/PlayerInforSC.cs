using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInforSC : MonoBehaviour
{
    [HideInInspector] DataSC data;
    [SerializeField] Text pNameTxt, pHighScoreTxt, pHighLvTxt, pTotalScoreTxt, pGemTxt;
    private string deviceID, pName;
    public int pHighscore, pHighLv, pCurrency, pGem;
    // Start is called before the first frame update
    void Start()
    {
        data = GameObject.Find("OBJ_Data").GetComponent<DataSC>();
        deviceID = data.deviceID;
        GetPlayerData();
    }
    public void GetPlayerData()
    {
        //Call each time need update UI;
        pName = data.pName;
        pHighscore = data.pHighScore;
        pHighLv = data.pHighLv;
        pCurrency = data.pTotalScore;
        pGem = data.pGems;

        ShowPlayerData();
    }
    void ShowPlayerData()
    {
        pNameTxt.text = pName.ToString();
        pHighScoreTxt.text = pHighscore.ToString();
        pHighLvTxt.text = pHighLv.ToString();
        pTotalScoreTxt.text = pCurrency.ToString();
        pGemTxt.text = pGem.ToString();

    }
    public void ClearPlayerPrefs() => data.DataDelete();
}
