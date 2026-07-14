using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundSC : Singleton<SoundSC>
{
    [SerializeField] AudioSource theme;
    public bool isAllowSFX;
    private void Start() {    }
    public void PlaySFX() => isAllowSFX = true;
    public void MuteSFX() => isAllowSFX = false;
}
