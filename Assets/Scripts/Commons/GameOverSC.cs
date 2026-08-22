using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverSC : MonoBehaviour
{
    GameplaySC gameControl;
    GeneralSC genCtrl;
    ChallengeSC challengeCtr;
    void Start() { }
    public void AssitGameControl()
    {
        if(genCtrl == null) genCtrl = GameObject.Find("GenGameControlMN").GetComponent<GeneralSC>();

        if(genCtrl.gameMode == 1)
        {
            gameControl = GameObject.Find("OBJ_ArcadeMN").GetComponent<GameplaySC>();
        }
        else if(genCtrl.gameMode == 2)
        {
            challengeCtr = GameObject.Find("OBJ_CHallengeMN").GetComponent<ChallengeSC>();
        }
    }
    public void OnReplay() 
    {
        //Chance to load Instertiatie
        if (genCtrl.gameMode == 1)
        {
            genCtrl.ToArcadeScene(true);
            genCtrl.ShowgameOver(false);
        }else if(genCtrl.gameMode == 2)
        {
            genCtrl.ToChallengeScene(true);
            genCtrl.ShowgameOver(false);
        }

    }
    public void OnHome() 
    {
        //Chance to load Instertiatie
        genCtrl.ToHome();
    }
    public void OnResume()
    {
        //Load Reward to resume process
    }
}
