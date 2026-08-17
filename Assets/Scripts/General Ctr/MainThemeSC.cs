using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainThemeSC : Singleton<MainThemeSC>
{
    [SerializeField] AudioSource maintheme;
    [SerializeField] DataSC data;
    private int pMusic; //This variable handle communicate with PlayerPrefs
    private void Start()
    {
        pMusic = data.pTheme;
        CheckPlayerMusic();
    }
    private void CheckPlayerMusic()
    {
        //Decide on init
        if (pMusic == 0)
        {
            //Mute
            print("in pMusic = 0");
            MuteTheme();
        }
        else if (pMusic == 1)
        {
            print("in pMusic = 1");
            PlayTheme();
        }
    }
    public void UpdateMusic(bool isAllow)
    {
        if (isAllow == false)
        {
            MuteTheme();
            data.UpdateThemeState(0);
        }
        else if (isAllow == true)
        {
            PlayTheme();
            data.UpdateThemeState(1);
        }
    }
    public void PlayTheme()
    {
        maintheme.volume = 0.5f;
        maintheme.Play();
    }
    public void MuteTheme()
    {
        maintheme.volume = 0;
        maintheme.Stop();
    }
}
