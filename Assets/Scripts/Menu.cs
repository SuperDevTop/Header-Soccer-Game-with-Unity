using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Networking;

public class Menu : MonoBehaviour
{
    public static Menu instance;
    public static int mode;
    public Text textMoney, txt_money_dailygift;
    public int disMatch, isMatchStage;
    public int[,] ValueTeam = new int[4, 8];
    public int[] ScoreTeam = new int[46];

    public int[,] ValueTeamPlayOff = new int[2, 8];
    public int[,] GoalsPlayOff = new int[2, 8];

    public int[,] GoalsFirtOff = new int[2, 4];
    public int[,] ValueTeamFirtOff = new int[2, 4];

    public int[,] ValueTeamQuarterFinals = new int[2, 2];
    public int[,] GoalsQuarterFinals = new int[2, 2];
    public Image img_Loadding;
    public GameObject panelLoadding, btnMenu, panelLoadScene, obj_daily_bonus, pn_achievement;
    public static bool isLoadding;
    public Image soundImg, musicImg;
    public Sprite muteSoundSprite, muteMusicSprite;
    public Sprite soundSprite, musicSprite;
    public int rdMusic;

    public Sprite[] sp_BGMenu;
    public SpriteRenderer img_bg;
    public GameObject backGround, money, buttonBackPlaytoMenu;
    public static int _upgradeLoadScene;
    public GameObject panelShop;
    public Image img_Reward;

    // training
    public static bool isTraining = false;
    public GameObject cussor_training, pn_black_training;
    public Button btn_wc, btn_friendmatch;
    public GameObject obj_training, group_btn_menu;

    public Image img_player, img_body, img_shoe, img_star;
    public Text name_player;
    public GameObject eff_player, eff_loadding, obj_daily_challenge;
    public float time_start_loading;
    public Text txt_win, txt_draw, txt_lose, txt_win_rate, txt_level_user, txt_power, txt_level_head;
    public Image img_level_user;

    // Use this for initialization
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        int abc = PlayerPrefs.GetInt(StringUtils.id_player, 1);
        PlayerPrefs.SetInt(StringUtils.id_player, abc);
    }

    void Start()
    {
        //FirebaseAnalyticManager.instance.AnalyticsWC();
        time_start_loading = 0;
        img_Loadding.fillAmount = 0;
        eff_loadding.SetActive(false);
        isTraining = false;
        int rd_music = UnityEngine.Random.Range(0, 2);
        if (rd_music == 0)
        {
            SoundManager.Instance.musicBG.clip = SoundManager.Instance.music1;
            SoundManager.Instance.musicBG.Play();
        }
        else
        {
            SoundManager.Instance.musicBG.clip = SoundManager.Instance.music2;
            SoundManager.Instance.musicBG.Play();
        }

        _upgradeLoadScene = 0;
        CheckMusicAndSound();


        if (isLoadding == false)
        {
            StartCoroutine(waitLoadScene());
        }
        else
        {

            if (PlayerPrefs.GetString(StringUtils.str_time_open_daily_bonus, "") == "")
            {
                btnMenu.SetActive(false);
                obj_daily_bonus.SetActive(true);
            }
            else
            {
                TimeSpan abc1 = (DateTime.Now.Date - Convert.ToDateTime(PlayerPrefs.GetString(StringUtils.str_time_open_daily_bonus)));
                if (abc1.Days >= 1)
                {
                    btnMenu.SetActive(false);
                    obj_daily_bonus.SetActive(true);
                }
                else
                {
                    btnMenu.SetActive(true);
                    obj_daily_bonus.SetActive(false);
                }
            }
            panelLoadScene.SetActive(false);
            img_bg.sprite = sp_BGMenu[2];
            eff_player.SetActive(true);
            panelLoadding.SetActive(false);

        }
        img_level_user.fillAmount = (float)(PlayerPrefs.GetInt(StringUtils.kn_user, 0)
            / (float)SelectTeam.instance.kn_level_user[PlayerPrefs.GetInt(StringUtils.level_user, 1) - 1]);

        int lv = PlayerPrefs.GetInt(StringUtils.level_user, 1);
        int abc = (PlayerPrefs.GetInt(StringUtils.kn_user, 0) - SelectTeam.instance.kn_level_user[lv - 1]);
        if (abc >= 0)
        {
            PlayerPrefs.SetInt(StringUtils.kn_user, abc);
            lv++;
            PlayerPrefs.SetInt(StringUtils.level_user, lv);

            img_level_user.fillAmount = (float)(PlayerPrefs.GetInt(StringUtils.kn_user, 0)
            / (float)SelectTeam.instance.kn_level_user[PlayerPrefs.GetInt(StringUtils.level_user, 1) - 1]);
        }
        txt_level_user.text = PlayerPrefs.GetInt(StringUtils.level_user, 1).ToString();
        txt_win.text = PlayerPrefs.GetInt(StringUtils.win, 0).ToString();
        txt_draw.text = PlayerPrefs.GetInt(StringUtils.draw, 0).ToString();
        txt_lose.text = PlayerPrefs.GetInt(StringUtils.lose, 0).ToString();
        if (PlayerPrefs.GetInt(StringUtils.win, 0) + PlayerPrefs.GetInt(StringUtils.draw, 0) + PlayerPrefs.GetInt(StringUtils.lose, 0) == 0)
        {
            txt_win_rate.text = " - ";
        }
        else
            txt_win_rate.text = (int)((float)(PlayerPrefs.GetInt(StringUtils.win, 0)
                / (float)(PlayerPrefs.GetInt(StringUtils.win, 0) + PlayerPrefs.GetInt(StringUtils.draw, 0) + PlayerPrefs.GetInt(StringUtils.lose, 0))) * 100) + "%";
        // player

        img_player.sprite = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.id_player) - 1];
        img_body.sprite = SelectTeam.instance.sp_Body[PlayerPrefs.GetInt(StringUtils.id_player) - 1];
        img_shoe.sprite = SelectTeam.instance.sp_shoe[UnityEngine.Random.Range(0, 3)];
        name_player.text = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.id_player) - 1].name.Substring(2);
        eff_player.transform.position = new Vector3(img_player.transform.position.x, img_player.transform.position.y, 0);
        int indexStar = int.Parse(SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.id_player) - 1].name.Substring(0, 1));
        img_star.sprite = SelectTeam.instance.star[indexStar];

        disMatch = PlayerPrefs.GetInt("disMatch", 0);
        isMatchStage = PlayerPrefs.GetInt("isStage1", 1);

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        if (PlayerPrefs.GetInt("disMatch") == 1)
        {
            SetupDisMatchWC();
            disMatch = 0;
            PlayerPrefs.SetInt("disMatch", disMatch);
            if (isMatchStage >= 5)
            {
                PlayerPrefs.SetInt("WinOrLose", 1);
            }
        }
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                ValueTeam[i, j] = PlayerPrefs.GetInt("valueTeam" + "[" + i + "," + j + "]", 1);

            }
        }
        for (int i = 0; i < ScoreTeam.Length; i++)
        {

            ScoreTeam[i] = PlayerPrefs.GetInt("scoreTeam" + "[" + i + "]", 0);

        }

        //if (PlayerPrefs.GetInt(StringUtils.str_number_training, 0) == 0)
        //{
        //    pn_black_training.SetActive(true);
        //    cussor_training.SetActive(true);
        //    btn_friendmatch.interactable = false;
        //    btn_wc.interactable = false;
        //}
        //else
        //{
        //    group_btn_menu.GetComponent<HorizontalLayoutGroup>().enabled = true;
        //    obj_training.SetActive(false);
        //}
    }
    IEnumerator waitLoadScene()
    {
        img_bg.sprite = sp_BGMenu[0];
        while (isLoadding == false)
        {
            yield return new WaitForSeconds(0.01f);
            if (time_start_loading <= 1)
            {
                time_start_loading++;
                eff_loadding.SetActive(false);
            }
            else
            {
                eff_loadding.SetActive(true);
                img_Loadding.fillAmount += 0.005f;
                eff_loadding.transform.localPosition = new Vector2(img_Loadding.fillAmount * img_Loadding.rectTransform.rect.width, 0);

                if (img_Loadding.fillAmount >= 1)
                {
                    isLoadding = true;
                    break;
                }
            }
        }

        yield return new WaitForSeconds(0.15f);
        eff_loadding.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        panelLoadScene.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        panelLoadScene.SetActive(false);
        //AdsManager.Instance.ShowAdIfAvailableOpenApp();
        if (PlayerPrefs.GetString(StringUtils.str_time_open_daily_bonus, "") == "")
        {
            btnMenu.SetActive(false);
            obj_daily_bonus.SetActive(true);
        }
        else
        {
            TimeSpan abc = (DateTime.Now - Convert.ToDateTime(PlayerPrefs.GetString(StringUtils.str_time_open_daily_bonus)));
            if (abc.TotalDays >= 1)
            {
                btnMenu.SetActive(false);
                obj_daily_bonus.SetActive(true);
            }
            else
            {
                btnMenu.SetActive(true);
                obj_daily_bonus.SetActive(false);
            }
        }

        panelLoadding.SetActive(false);
        img_bg.sprite = sp_BGMenu[2];
        eff_player.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        textMoney.text = PlayerPrefs.GetInt(StringUtils.str_money, 0).ToString();
        txt_money_dailygift.text = PlayerPrefs.GetInt(StringUtils.str_money, 0).ToString();

        if (Menu.mode == (int)Menu.MODE.WORLDCUP)
        {
            if (isMatchStage <= 4 && isMatchStage > 1)
            {
                ListSortScore();
                if (isMatchStage == 4)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (ValueTeam[i, j] == PlayerPrefs.GetInt("wcPlayer") && i > 1)
                            {
                                PlayerPrefs.SetInt("WinOrLose", 1);
                            }
                        }
                    }
                }
            }
        }
    }

    public void SetupDisMatchWC()
    {
        if (isMatchStage <= 4)
        {
            //ScoreTeam[PlayerPrefs.GetInt("wcAI") - 1] += 3;
            //PlayerPrefs.SetInt("scoreTeam" + "[" + (PlayerPrefs.GetInt("wcAI") - 1) + "]", ScoreTeam[PlayerPrefs.GetInt("wcAI") - 1]);

        }
        else if (isMatchStage == 5)
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    ValueTeamPlayOff[i, j] = PlayerPrefs.GetInt("ValueTeamPlayOff" + "[" + i + "," + j + "]", 0);
                    GoalsPlayOff[i, j] = PlayerPrefs.GetInt("GoalsPlayOff" + "[" + i + "," + j + "]", 0);
                }
            }

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (ValueTeamPlayOff[i, j] == PlayerPrefs.GetInt("wcPlayer")
                       || ValueTeamPlayOff[i, j] == PlayerPrefs.GetInt("wcAI"))
                    {
                        if (ValueTeamPlayOff[0, j] == PlayerPrefs.GetInt("wcPlayer"))
                        {
                            GoalsPlayOff[0, j] = 0;
                            GoalsPlayOff[1, j] = 3;


                            PlayerPrefs.SetInt("GoalsPlayOff" + "[" + i + "," + j + "]", GoalsPlayOff[i, j]);
                        }
                        else if (ValueTeamPlayOff[1, j] == PlayerPrefs.GetInt("wcPlayer"))
                        {
                            GoalsPlayOff[1, j] = 0;
                            GoalsPlayOff[0, j] = 3;

                            PlayerPrefs.SetInt("GoalsPlayOff" + "[" + i + "," + j + "]", GoalsPlayOff[i, j]);
                        }
                    }
                }
            }
        }
        else if (isMatchStage == 6)
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    ValueTeamFirtOff[i, j] = PlayerPrefs.GetInt("ValueTeamFirtOff" + "[" + i + "," + j + "]", 1);
                }
            }
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (ValueTeamFirtOff[i, j] == PlayerPrefs.GetInt("wcPlayer") || ValueTeamFirtOff[i, j] == PlayerPrefs.GetInt("wcAI"))
                    {
                        if (ValueTeamFirtOff[0, j] == PlayerPrefs.GetInt("wcPlayer"))
                        {
                            GoalsFirtOff[0, j] = 0;
                            GoalsFirtOff[1, j] = 3;


                            PlayerPrefs.SetInt("GoalsFirtOff" + "[" + i + "," + j + "]", GoalsFirtOff[i, j]);
                        }
                        else if (ValueTeamFirtOff[1, j] == PlayerPrefs.GetInt("wcPlayer"))
                        {
                            GoalsFirtOff[1, j] = 0;
                            GoalsFirtOff[0, j] = 3;

                            PlayerPrefs.SetInt("GoalsFirtOff" + "[" + i + "," + j + "]", GoalsFirtOff[i, j]);
                        }
                    }
                }
            }
        }
        if (isMatchStage == 7)
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    ValueTeamQuarterFinals[i, j] = PlayerPrefs.GetInt("ValueTeamQuarterFinals" + "[" + i + "," + j + "]", 1);
                }
            }
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (ValueTeamQuarterFinals[i, j] == PlayerPrefs.GetInt("wcPlayer") || ValueTeamQuarterFinals[i, j] == PlayerPrefs.GetInt("wcAI"))
                    {
                        if (ValueTeamQuarterFinals[0, j] == PlayerPrefs.GetInt("wcPlayer"))
                        {
                            GoalsQuarterFinals[0, j] = 0;
                            GoalsQuarterFinals[1, j] = 3;


                            PlayerPrefs.SetInt("GoalsQuarterFinals" + "[" + i + "," + j + "]", GoalsQuarterFinals[i, j]);
                        }
                        else if (ValueTeamQuarterFinals[1, j] == PlayerPrefs.GetInt("wcPlayer"))
                        {
                            GoalsQuarterFinals[1, j] = 0;
                            GoalsQuarterFinals[0, j] = 3;

                            PlayerPrefs.SetInt("GoalsQuarterFinals" + "[" + i + "," + j + "]", GoalsQuarterFinals[i, j]);
                        }
                    }
                }
            }
        }
    }

    public void ListSortScore()
    {

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = j + 1; k < 4; k++)
                {

                    if (ScoreTeam[ValueTeam[j, i] - 1] < ScoreTeam[ValueTeam[k, i] - 1])
                    {
                        int temp2 = ValueTeam[j, i];
                        ValueTeam[j, i] = ValueTeam[k, i];
                        ValueTeam[k, i] = temp2;
                    }
                }
            }

        }
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                PlayerPrefs.SetInt("valueTeam" + "[" + i + "," + j + "]", ValueTeam[i, j]);
            }

        }
    }


    public void ButtonFriendMatch()
    {
        if (obj_daily_challenge.activeSelf == true)
        {
            return;
        }
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }

        SceneManager.LoadScene("FriendMatch");
        mode = (int)MODE.FRIENDMATCH;
    }

    public void ButtonSurvival()
    {
        if (obj_daily_challenge.activeSelf == true)
        {
            return;
        }
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }

        SceneManager.LoadScene("SetupStadium");
        mode = (int)MODE.SIRVIVAL;
    }

    public void ButtonCampaignLeague()
    {
        if (obj_daily_challenge.activeSelf == true)
        {
            return;
        }
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        mode = (int)MODE.LEAGUE;
        SceneManager.LoadScene("League");

    }
    public void ButtonWorldCup()
    {
        if (obj_daily_challenge.activeSelf == true)
        {
            return;
        }
        SceneManager.LoadScene("WorldCup");
        mode = (int)MODE.WORLDCUP;
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
    }



    public void ButtonTraining()
    {
        SceneManager.LoadScene("Training");
        mode = (int)MODE.TRAINING;
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
    }

    public void ButtonAddMoney(int id)
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        SaleCharacter.instance.SetupSaleCharacters();
        panelShop.SetActive(true);

    }

    public void ButtonAchievement()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        //btnMenu.SetActive(false);
        money.GetComponent<RectTransform>().localPosition = new Vector2(money.transform.localPosition.x, money.transform.localPosition.y - 10);
        pn_achievement.SetActive(true);

    }

    public void ButtonCancelAchievement()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        //btnMenu.SetActive(true);
        money.GetComponent<RectTransform>().localPosition = new Vector2(money.transform.localPosition.x, money.transform.localPosition.y + 10);
        pn_achievement.SetActive(false);

    }

    public void ButtonExitShop()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        panelShop.SetActive(false);
    }
    public void ButtonShop()
    {

        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        //SaleCharacter.instance.SetupSaleCharacters();
        panelShop.SetActive(true);
    }

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
            SoundManager.Instance.musicBG.mute = true;
            musicImg.sprite = muteMusicSprite;
            PlayerPrefs.SetInt(GameConstants.MUSIC, 0);
        }
        else if (musicImg.sprite == muteMusicSprite)
        {
            SoundManager.Instance.musicBG.mute = false;
            musicImg.sprite = musicSprite;
            PlayerPrefs.SetInt(GameConstants.MUSIC, 1);
        }
    }


    private void CheckMusicAndSound()
    {
        SoundManager.Instance.matchLost.Stop();
        SoundManager.Instance.matchWon.Stop();
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            soundImg.sprite = soundSprite;


        }
        else
        {
            soundImg.sprite = muteSoundSprite;
            SoundManager.Instance.musicBG.mute = true;
        }
        if (PlayerPrefs.GetInt(GameConstants.MUSIC, 1) == 1)
        {
            musicImg.sprite = musicSprite;
            SoundManager.Instance.musicBG.mute = false;
        }
        else
        {
            musicImg.sprite = muteMusicSprite;
            SoundManager.Instance.musicBG.mute = true;
        }
    }

    public enum MODE
    {
        WORLDCUP, FRIENDMATCH, TRAINING, SIRVIVAL, LEAGUE, DAILY_CHALLENGE
    };
}

