using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class DailyChallenge : MonoBehaviour
{
    public static DailyChallenge instance;
    public GameObject obj_daily_challenge, obj_btn_start, obj_coin, obj_coin_reward, obj_btn_cancel, obj_btn_challenge;
    public Text txt_messenger, txt_reward, txt_coin, txt_coin_placeabet;
    public static int reward_daily_challenge;
    public Image img_head, img_body, img_shoe;
    public Sprite spr_Start;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance = null)
        {
            instance = this;
        }
    }
    void Start()
    {
        string t_daily = PlayerPrefs.GetString(StringUtils.str_time_daily_challenge, DateTime.Now.ToString());
        PlayerPrefs.SetString(StringUtils.str_time_daily_challenge, t_daily);
        TimeSpan t_span = (DateTime.Now - Convert.ToDateTime(t_daily));
        if (Menu.isLoadding == true)
        {
            obj_daily_challenge.SetActive(false);
            return;
        }
        if (t_span.TotalDays < 1)
        {
            obj_daily_challenge.SetActive(false);
            return;
        }
        else
        {
            obj_daily_challenge.SetActive(true);
            obj_btn_challenge.SetActive(true);
        }
        int dl = PlayerPrefs.GetInt(StringUtils.str_nunber_daily_challenge, 1);
        if (dl > 46)
        {
            dl = 1;
        }
        PlayerPrefs.SetInt(StringUtils.str_nunber_daily_challenge, dl);
        reward_daily_challenge = 250 + (dl * 250);
        img_head.sprite = SelectTeam.instance.AI_Head[dl - 1];
        img_body.sprite = SelectTeam.instance.sp_Body[dl - 1];
        img_shoe.sprite = SelectTeam.instance.sp_shoe[UnityEngine.Random.Range(0, 3)];
        txt_messenger.text = "" + img_head.sprite.name.Substring(2) + " sends the challenge to you.";
        txt_coin_placeabet.text = 250 + (dl * 250) + "";
    }

    // Update is called once per frame
    void Update()
    {
        //txt_coin.text = PlayerPrefs.GetInt(StringUtils.str_money, 0).ToString();
    }

    public void ButtonCancel()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        obj_daily_challenge.SetActive(false);
    }

    public void ButtonStart()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        int money = PlayerPrefs.GetInt(StringUtils.str_money, 0);
        if (obj_btn_start.transform.GetChild(0).GetComponent<Image>().sprite.name == "btnPlaceabet")
        {
            if (money >= reward_daily_challenge)
            {
                txt_coin_placeabet.gameObject.SetActive(false);
                obj_btn_start.transform.GetChild(0).GetComponent<Image>().sprite = spr_Start;
                obj_btn_cancel.SetActive(false);
                int dl = PlayerPrefs.GetInt(StringUtils.str_nunber_daily_challenge, 1);
                dl++;
                PlayerPrefs.SetString(StringUtils.str_time_daily_challenge, DateTime.Now.ToString());
                PlayerPrefs.SetInt(StringUtils.str_nunber_daily_challenge, dl);
                StartCoroutine(Dailygift.instance.MoveCoin(DailyController.instance.obj_coin_taget, 1, obj_coin_reward, 0.25f));
                StartCoroutine(Addcoin(1f, reward_daily_challenge));
            }
            else
            {
                Menu.instance.panelShop.SetActive(true);
            }
        }
        else
        {
            Menu.mode = (int)Menu.MODE.DAILY_CHALLENGE;
            SceneManager.LoadScene("SetupStadium");
        }

    }

    IEnumerator Addcoin(float t, int money)
    {
        yield return new WaitForSeconds(t);
        int mn = PlayerPrefs.GetInt(StringUtils.str_money, 0);
        mn -= money;
        txt_reward.text = (reward_daily_challenge).ToString();
        PlayerPrefs.SetInt(StringUtils.str_money, mn);

    }
    public void ButtonDailyChallenge()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        obj_daily_challenge.SetActive(true);
    }

}
