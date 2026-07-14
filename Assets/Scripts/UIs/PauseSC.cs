
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseSC : Singleton<PauseSC>
{
    [HideInInspector] GameplaySC gameCtr;
    [HideInInspector] ChallengeSC challengeCtr;
    [HideInInspector] GeneralSC genCtr;
    int gameMode;
    private void Start() => SettingStart();
    private void SettingStart()
    {
        genCtr = GameObject.Find("GenGameControlMN").GetComponent<GeneralSC>();
        CheckGameMode();
    }
    public void OnResume() => gameCtr.isEnablePlay = true;
    public void OnHome() => genCtr.ToHome();
    public void OnQuit() => Application.Quit(0);

    private void CheckGameMode()
    {
        gameMode = genCtr.gameMode;
        switch (gameMode)
        {
            case 1:
                //Arcade
                gameCtr = GameObject.Find("OBJ_ArcadeMN").GetComponent<GameplaySC>();
                break;
            case 2:
                //Challenge
                challengeCtr = GameObject.Find("OBJ_CHallengeMN").GetComponent<ChallengeSC>();
                break;
        }
    }
}
