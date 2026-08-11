using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditSC : MonoBehaviour
{
    [HideInInspector] GeneralSC genCtr;
    void Start() { genCtr = GameObject.Find("GenGameControlMN").GetComponent<GeneralSC>(); }
    void Update() { }
    //public void OnCloseCredit() => genCtr.OnHideCredit();
    public void ToPrivaciPolicy() { Application.OpenURL("https://sadekgame.wordpress.com/2025/11/18/privacy-policy-existium-terrian-platformer/"); }
    public void ToTermUse() { Application.OpenURL("https://sadekgame.wordpress.com/2026/08/02/termuse-tank-destroyer-sdsoft-docs/"); }
    public void ToFB() { Application.OpenURL("https://www.facebook.com/sadeksoftVn"); }
    public void ToIG() { Application.OpenURL("https://www.instagram.com/sdsoftvn/"); }
    public void ToX() { Application.OpenURL("https://x.com/SadekGame15769"); }
    public void ToWebsite() { Application.OpenURL("https://play.google.com/store/apps/developer?id=Sadek+Games+Studio"); }
    public void ToYTB() { Application.OpenURL("https://www.youtube.com/@SadekGamesStudio"); }
    public void ToTikTok() { Application.OpenURL("https://www.tiktok.com/@sdsoft"); }
}
