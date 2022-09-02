using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{

    public static UIManager Instance { get; private set; }
    public GameObject panelSetting;
    public SpriteRenderer img_Stadium;
    public Image soundImg, musicImg;
    public Sprite muteSoundSprite, muteMusicSprite;
    public Sprite soundSprite, musicSprite;
    public Text timeText;
    public static int showAdsSetting = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {

        int index_Stadium = PlayerPrefs.GetInt("index_Stadium", 0);
        CheckMusicAndSound();
        img_Stadium.sprite = SelectTeam.instance.sp_Stadiums[index_Stadium];
    }

    private void Update()
    {

    }



    private void CheckMusicAndSound()
    {
        SoundManager.Instance.musicBG.mute = true;

        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            soundImg.sprite = soundSprite;


        }
        else
        {
            soundImg.sprite = muteSoundSprite;
        }

        if (PlayerPrefs.GetInt(GameConstants.MUSIC, 1) == 1)
        {
            musicImg.sprite = musicSprite;
            SoundManager.Instance.musicMatch.Play();
        }
        else
        {
            musicImg.sprite = muteMusicSprite;
        }
    }

    public void ButtonResumeGame()
    {
        Time.timeScale = 1.0f;
        panelSetting.SetActive(false);
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
    }

    private void RestartMatch()
    {

        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        GameController.Instance.RestartMatch();
        Time.timeScale = 1.0f;
    }


    public void ButtonSetting()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        Time.timeScale = 0;
        AdsManager.mode_show_ful_ads = "Setting";
        AdsManager.Instance.showFullAds();

    }

    IEnumerator loadPanelSetting()
    {
        yield return new WaitForSeconds(1f);
        panelSetting.SetActive(true);
    }

    public void ButtonHome()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        SceneManager.LoadScene("Menu");
    }

    // Sound btn is clicked
    public void ButtonSound()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        if (soundImg.sprite == soundSprite)
        {

            soundImg.sprite = muteSoundSprite;
            PlayerPrefs.SetInt(GameConstants.SOUND, 0);


        }
        else if (soundImg.sprite == muteSoundSprite)
        {

            soundImg.sprite = soundSprite;
            PlayerPrefs.SetInt(GameConstants.SOUND, 1);

        }

    }

    public void ButtonMusic()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }

        if (musicImg.sprite == musicSprite)
        {
            SoundManager.Instance.musicMatch.Stop();
            musicImg.sprite = muteMusicSprite;
            PlayerPrefs.SetInt(GameConstants.MUSIC, 0);
        }
        else if (musicImg.sprite == muteMusicSprite)
        {
            SoundManager.Instance.musicMatch.Play();
            musicImg.sprite = musicSprite;
            PlayerPrefs.SetInt(GameConstants.MUSIC, 1);
        }
    }

    // Surrender btn is clicked
    public void Surrender()
    {
        GameController.Instance.Surrender();
        Time.timeScale = 1.0f;
        panelSetting.SetActive(false);
        GameController.Instance.btnSetting.gameObject.SetActive(true);
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
    }
    public void HomeTraining()
    {
        Time.timeScale = 0.0f;
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        AdsManager.Instance.showFullAds();
        SceneManager.LoadScene("Menu");
    }

    public void ExitTraining()
    {
        Time.timeScale = 1.0f;
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        SceneManager.LoadScene("Menu");
    }
}
