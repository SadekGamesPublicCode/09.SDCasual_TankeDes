using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingSC : MonoBehaviour
{
    int themeAllow, sfxAllow;
    [SerializeField] SoundSC soundSFX;
    [SerializeField] MainThemeSC soundMusic;
    [SerializeField] GeneralSC genCtrl;
    [SerializeField] DataSC data;
    [SerializeField] Image themeLoud, themeMute, sfxLoud, sfxMute;

    void Start()
    {
    }
    private void OnEnable()
    {
        if (data.pSFX == 0 && data.pTheme == 0)
        {
            //All disable
            themeMute.gameObject.SetActive(true);
            themeLoud.gameObject.SetActive(false);
            sfxMute.gameObject.SetActive(true);
            sfxLoud.gameObject.SetActive(false);
        }
        else if (data.pSFX == 0 && data.pTheme == 1)
        {
            themeMute.gameObject.SetActive(false);
            themeLoud.gameObject.SetActive(true);
            sfxMute.gameObject.SetActive(true);
            sfxLoud.gameObject.SetActive(false);
        }
        else if (data.pSFX == 1 && data.pTheme == 0)
        {
            themeMute.gameObject.SetActive(true);
            themeLoud.gameObject.SetActive(false);
            sfxMute.gameObject.SetActive(false);
            sfxLoud.gameObject.SetActive(true);
        }
        else if (data.pTheme == 1 && data.pSFX == 1)
        {
            //All lound
            themeMute.gameObject.SetActive(false);
            themeLoud.gameObject.SetActive(true);
            sfxMute.gameObject.SetActive(false);
            sfxLoud.gameObject.SetActive(true);
        }
    }
    public void OnChangeThemState()
    {
        if (themeAllow == 1)
        {
            themeAllow = 0;
            themeMute.gameObject.SetActive(true);
            themeLoud.gameObject.SetActive(false);
            soundMusic.UpdateMusic(false);
        }
        else if (themeAllow == 0)
        {
            themeAllow = 1;
            themeMute.gameObject.SetActive(false);
            themeLoud.gameObject.SetActive(true);
            soundMusic.UpdateMusic(true);
        }
    }
    public void OnChangeSFXState()
    {
        if (sfxAllow == 1)
        {
            sfxAllow = 0;
            //soundSFX.MuteSFX();
            sfxMute.gameObject.SetActive(true);
            sfxLoud.gameObject.SetActive(false);
        }
        else if (sfxAllow == 0)
        {
            sfxAllow = 1;
            //soundSFX.PlaySFX();
            sfxMute.gameObject.SetActive(false);
            sfxLoud.gameObject.SetActive(true);
        }
        data.UpdateSFXState(sfxAllow);
    }
    public void ExitGame() => Application.Quit();
}
