using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSC : MonoBehaviour
{
    [SerializeField] GeneralSC genCtr;
    [SerializeField] Slider loadSlide;
    private float loadSpd1, loadSpd2;
    public float targetAlpha = 1f;
    void Start()
    {
        SetupStart();
        StartCoroutine(RunLoadGameLogo());
    }
    void SetupStart()
    {
        loadSlide.value = 0;
        loadSlide.maxValue = 1;
        loadSlide.minValue = loadSlide.value;
    }

    IEnumerator RunLoadGameLogo()
    {
        loadSpd2 = Random.Range(0.01f, 0.5f);
        if (loadSlide.value >= 1)
        {
            StopCoroutine(RunLoadGameLogo());
            SceneManager.LoadScene("01_MainScene");
        }
        yield return new WaitForSeconds(0.1f);
        loadSlide.value += loadSpd2 * Time.deltaTime * 10;
        StartCoroutine(RunLoadGameLogo());
    }
}
