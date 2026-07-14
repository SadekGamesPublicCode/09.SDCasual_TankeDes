using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingSC : MonoBehaviour
{
    int themeAllow, sfxAllow;
    private SoundSC soundSFX;
    private MainThemeSC soundMusic;
    [HideInInspector] GeneralSC genCtrl;
    [HideInInspector] DataSC data;
    [SerializeField] Image themeLoud, themeMute, sfxLoud, sfxMute;
    void Start()
    {
        data = GameObject.Find("OBJ_Data").GetComponent<DataSC>();
        soundSFX = GameObject.Find("OBJ_SoundSFX").GetComponent<SoundSC>();
        soundMusic = GameObject.Find("OBJ_SoundMusic").GetComponent<MainThemeSC>();
        themeAllow = PlayerPrefs.GetInt("soundState");
        sfxAllow = PlayerPrefs.GetInt("sfxState");
        genCtrl = GameObject.Find("GenGameControlMN").GetComponent<GeneralSC>();
    }
    public void CheckSound()
    {
        themeAllow = data.pTheme;
        sfxAllow = data.pSFX;
        switch (themeAllow)
        {
            case 0:
                themeMute.gameObject.SetActive(true);
                themeLoud.gameObject.SetActive(false);
                soundMusic.MuteTheme();
                break;
            case 1:
                themeMute.gameObject.SetActive(false);
                themeLoud.gameObject.SetActive(true);
                soundMusic.PlayTheme();
                break;
        }

        switch (sfxAllow)
        {
            case 0:
                sfxMute.gameObject.SetActive(true);
                sfxLoud.gameObject.SetActive(false);
                soundSFX.MuteSFX();
                break;
            case 1:
                sfxMute.gameObject.SetActive(false);
                sfxLoud.gameObject.SetActive(true);
                soundSFX.PlaySFX();
                break;
        }
    }
    public void OnChangeThemState()
    {
        if (themeAllow == 1)
        {
            themeAllow = 0;
            themeMute.gameObject.SetActive(true);
            themeLoud.gameObject.SetActive(false);
            soundMusic.MuteTheme();

        }
        else if (themeAllow == 0)
        {
            themeAllow = 1;
            themeMute.gameObject.SetActive(false);
            themeLoud.gameObject.SetActive(true);
            soundMusic.PlayTheme();
        }
        data.UpdateThemeState(themeAllow);
    }
    public void OnChangeSFXState()
    {
        if (sfxAllow == 1)
        {
            sfxAllow = 0;
            soundSFX.MuteSFX();
            sfxMute.gameObject.SetActive(true);
            sfxLoud.gameObject.SetActive(false);
        }
        else if (sfxAllow == 0)
        {
            sfxAllow = 1;
            soundSFX.PlaySFX();
            sfxMute.gameObject.SetActive(false);
            sfxLoud.gameObject.SetActive(true);
        }
        data.UpdateSFXState(sfxAllow);
    }
    public void ExitGame() => Application.Quit();
}
