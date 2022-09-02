using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuRate : MonoBehaviour
{
    public static menuRate instance;
    public int startGame = 0;
    public GameObject panelRate;
    public GameObject[] starYellow;

    // Use this for initialization
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        startGame = PlayerPrefs.GetInt("rateOk", 0);
        PlayerPrefs.SetInt("rateOk", startGame);
    }

    IEnumerator Rate()
    {
        yield return new WaitForSeconds(0.5f);
#if UNITY_ANDROID
        Application.OpenURL("market://details?id=com.headsoccer.headball.footballgame");
#elif UNITY_IOS
        Application.OpenURL("itms-apps://itunes.apple.com/WebObjects/MZStore.woa/wa/viewContentsUserReviews?id=1279147047&pageNumber=0&sortOrdering=2&type=Purple+Software&mt=8");
#endif

        panelRate.SetActive(false);
        FullTime.instance.obj_result.SetActive(true);
    }
    public void ButtonCancel()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }

        panelRate.SetActive(false);
        FullTime.instance.obj_result.SetActive(true);
    }

    public void ButtonOk()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }

        StartCoroutine(Rate());
    }

    IEnumerator disablePanel()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        yield return new WaitForSeconds(0.5f);
        panelRate.SetActive(false);
        FullTime.instance.obj_result.SetActive(true);
    }
    public void StarRate(int _id)
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        PlayerPrefs.SetInt("rateOk", -1);
        for (int i = 0; i < 5; i++)
        {
            if (i < _id)
                starYellow[i].SetActive(true);
            else
                starYellow[i].SetActive(false);
        }
        if (_id > 3)
        {

            StartCoroutine(Rate());
        }
        else
        {
            StartCoroutine(disablePanel());
        }
    }

}
