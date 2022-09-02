using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DailyController : MonoBehaviour
{
    public static DailyController instance;
    public GameObject cont, prf_dailyreward, eff_getcoin, obj_btn_getnow, obj_coin_move, obj_coin_taget, obj_coin_taget_2, obj_getx2;
    public Sprite spr_head;
    int number_daily;
    int number_coint_reward;
    public List<int> lst_open_daily_6 = new List<int>() { 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
    public List<int> lst_open_daily_13 = new List<int>() { 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46 };
    public Image img_head, img_star, img_legend;
    public GameObject obj_open_head, card_head;
    public Text txt_name, txt_power;
    public GameObject btn_getprize, btn_getprize_2, raylight_1, raylight_2, obj_cant_ads;
    float targetRotation;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        number_daily = PlayerPrefs.GetInt(StringUtils.str_number_daily, 0);
        lst_open_daily_6 = new List<int>() { 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
        lst_open_daily_13 = new List<int>() { 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46 };
        for (int i = 0; i < InsSelectPlayer.instance.lst_open_player.Count; i++)
        {
            for (int j = 0; j < lst_open_daily_6.Count; j++)
            {
                if (InsSelectPlayer.instance.lst_open_player[i] == lst_open_daily_6[j])
                {
                    lst_open_daily_6.Remove(lst_open_daily_6[j]);
                }
            }
        }
        for (int i = 0; i < InsSelectPlayer.instance.lst_open_player.Count; i++)
        {
            for (int j = 0; j < lst_open_daily_13.Count; j++)
            {
                if (InsSelectPlayer.instance.lst_open_player[i] == lst_open_daily_13[j])
                {
                    lst_open_daily_13.Remove(lst_open_daily_13[j]);
                }
            }
        }
        if (number_daily == 6 || number_daily == 13)
        {
            obj_getx2.SetActive(false);
        }
        for (int i = 0; i < 14; i++)
        {
            GameObject dl = Instantiate(prf_dailyreward, cont.transform);

            dl.GetComponent<DailyLoginBonus>().txt_numbercoin_reward.text = 900 + (i + 1) * 100 + "";
            if (i < number_daily)
            {
                dl.GetComponent<DailyLoginBonus>().txt_day.text = "Day " + (i + 1);
                dl.GetComponent<DailyLoginBonus>().txt_day.color = Color.white;
                dl.GetComponent<DailyLoginBonus>().txt_numbercoin_reward.enabled = false;
                dl.GetComponent<DailyLoginBonus>().GetComponent<Image>().sprite = dl.GetComponent<DailyLoginBonus>().spr_ok_daily;
                dl.GetComponent<DailyLoginBonus>().obj_ok.SetActive(true);
                dl.GetComponent<DailyLoginBonus>().img_coin.color = Color.white;
                dl.GetComponent<Button>().interactable = false;

                if (i == 6 || i == 13)
                {
                    dl.GetComponent<DailyLoginBonus>().obj_head.transform.GetChild(0).gameObject.SetActive(false);
                    dl.GetComponent<DailyLoginBonus>().obj_head.GetComponent<Image>().color = Color.white;
                    if (i == 6)
                    {
                        dl.GetComponent<DailyLoginBonus>().obj_head.GetComponent<Image>().sprite
                           = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.str_id_head_open_daily_6) - 1];
                    }
                    if (i == 13)
                    {
                        dl.GetComponent<DailyLoginBonus>().obj_head.GetComponent<Image>().sprite
                           = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.str_id_head_open_daily_13) - 1];
                    }
                    dl.GetComponent<DailyLoginBonus>().obj_head.SetActive(true);
                    dl.GetComponent<DailyLoginBonus>().img_coin.enabled = false;
                    dl.GetComponent<DailyLoginBonus>().txt_numbercoin_reward.enabled = false;
                }
            }
            else if (i == number_daily)
            {
                number_coint_reward = 900 + (i + 1) * 100;
                obj_btn_getnow = dl.GetComponent<DailyLoginBonus>().gameObject;
                dl.GetComponent<DailyLoginBonus>().txt_day.text = "Claim now ";
                dl.GetComponent<DailyLoginBonus>().txt_day.color = Color.green;
                dl.GetComponent<DailyLoginBonus>().obj_ok.SetActive(false);
                dl.GetComponent<DailyLoginBonus>().txt_numbercoin_reward.color = Color.yellow;
                dl.GetComponent<DailyLoginBonus>().GetComponent<Image>().sprite = dl.GetComponent<DailyLoginBonus>().spr_ok_daily;
                dl.GetComponent<DailyLoginBonus>().img_coin.color = Color.white;
                dl.GetComponent<Button>().interactable = true;
                if (i == 6 && lst_open_daily_6.Count > 0)
                {
                    dl.GetComponent<DailyLoginBonus>().obj_head.transform.GetChild(0).gameObject.SetActive(true);
                    dl.GetComponent<DailyLoginBonus>().obj_head.SetActive(true);
                    dl.GetComponent<DailyLoginBonus>().img_coin.enabled = false;
                    dl.GetComponent<DailyLoginBonus>().txt_numbercoin_reward.enabled = false;
                }
                if (i == 13 && lst_open_daily_13.Count > 0)
                {
                    dl.GetComponent<DailyLoginBonus>().obj_head.transform.GetChild(0).gameObject.SetActive(true);
                    dl.GetComponent<DailyLoginBonus>().obj_head.SetActive(true);
                    dl.GetComponent<DailyLoginBonus>().img_coin.enabled = false;
                    dl.GetComponent<DailyLoginBonus>().txt_numbercoin_reward.enabled = false;
                }
            }
            else
            {
                dl.GetComponent<DailyLoginBonus>().txt_day.text = "Day " + (i + 1);
                dl.GetComponent<DailyLoginBonus>().obj_ok.SetActive(false);
                dl.GetComponent<Button>().interactable = false;
                if (i == 6 && lst_open_daily_6.Count > 0)
                {
                    dl.GetComponent<DailyLoginBonus>().obj_head.SetActive(true);
                    dl.GetComponent<DailyLoginBonus>().img_coin.enabled = false;
                    dl.GetComponent<DailyLoginBonus>().txt_numbercoin_reward.enabled = false;
                }
                if (i == 13 && lst_open_daily_13.Count > 0)
                {
                    dl.GetComponent<DailyLoginBonus>().obj_head.SetActive(true);
                    dl.GetComponent<DailyLoginBonus>().img_coin.enabled = false;
                    dl.GetComponent<DailyLoginBonus>().txt_numbercoin_reward.enabled = false;
                }
            }
            //if (i == 6 || i == 13)
            //{
            //    dl.GetComponent<DailyLoginBonus>().obj_head.SetActive(true);
            //    dl.GetComponent<DailyLoginBonus>().img_coin.enabled = false;
            //    dl.GetComponent<DailyLoginBonus>().txt_numbercoin_reward.enabled = false;
            //}

        }
    }

    // Update is called once per frame
    void Update()
    {
        targetRotation += 1;
        raylight_1.transform.rotation = Quaternion.Euler(0, 0, -1 * targetRotation);
        raylight_2.transform.rotation = Quaternion.Euler(0, 0, 0.5f * targetRotation);
    }

    public void ButtonGetPrizeX2()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        AdsManager.mode_show_ful_reward = "GetPrizeDailyX2";
        AdsManager.Instance.showRewardAdsVideo();
    }

    public void ButtonGetPrize()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        PlayerPrefs.SetString(StringUtils.str_time_open_daily_bonus, DateTime.Now.Date.ToString());
        btn_getprize.SetActive(false);
        btn_getprize_2.SetActive(false);
        obj_btn_getnow.GetComponent<DailyLoginBonus>().txt_day.text = "Day " + (number_daily + 1);
        obj_btn_getnow.GetComponent<DailyLoginBonus>().txt_day.color = Color.white;
        obj_btn_getnow.GetComponent<DailyLoginBonus>().txt_numbercoin_reward.enabled = false;
        obj_btn_getnow.GetComponent<DailyLoginBonus>().GetComponent<Image>().sprite = obj_btn_getnow.GetComponent<DailyLoginBonus>().spr_ok_daily;
        obj_btn_getnow.GetComponent<DailyLoginBonus>().obj_ok.SetActive(true);
        obj_btn_getnow.GetComponent<DailyLoginBonus>().img_coin.color = Color.white;
        obj_btn_getnow.GetComponent<Button>().interactable = false;
        lst_open_daily_6 = new List<int>() { 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
        lst_open_daily_13 = new List<int>() { 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46 };
        if (number_daily != 6 && number_daily != 13)
        {

            StartCoroutine(Dailygift.instance.MoveCoin(obj_btn_getnow, 2.1f, obj_coin_taget_2, 0.05f));
            StartCoroutine(Addcoin(1.2f, number_coint_reward));
            StartCoroutine(HidePanel());
        }
        else if (number_daily == 6)
        {

            for (int i = 0; i < InsSelectPlayer.instance.lst_open_player.Count; i++)
            {
                for (int j = 0; j < lst_open_daily_6.Count; j++)
                {
                    if (InsSelectPlayer.instance.lst_open_player[i] == lst_open_daily_6[j])
                    {
                        lst_open_daily_6.Remove(lst_open_daily_6[j]);
                    }
                }
            }
            if (lst_open_daily_6.Count > 0)
            {
                int a = UnityEngine.Random.Range(0, lst_open_daily_6.Count);
                PlayerPrefs.SetInt(StringUtils.str_open_head + (lst_open_daily_6[a] - 1), lst_open_daily_6[a]);
                
                PlayerPrefs.SetInt(StringUtils.str_id_head_open_daily_6, lst_open_daily_6[a]);
                obj_btn_getnow.GetComponent<DailyLoginBonus>().obj_head.GetComponent<Image>().sprite
                   = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.str_id_head_open_daily_6) - 1];
                /*FirebaseAnalyticManager.instance.AnalyticsOpenCharacter(lst_open_daily_6[a]);*/
            }
            else
            {
                StartCoroutine(Dailygift.instance.MoveCoin(obj_btn_getnow, 2.1f, obj_coin_taget_2, 0.05f));
                StartCoroutine(Addcoin(1.2f, number_coint_reward));
                StartCoroutine(HidePanel());
            }
            //SceneManager.LoadScene("Menu");
        }
        else if (number_daily == 13)
        {
            for (int i = 0; i < InsSelectPlayer.instance.lst_open_player.Count; i++)
            {
                for (int j = 0; j < lst_open_daily_13.Count; j++)
                {
                    if (InsSelectPlayer.instance.lst_open_player[i] == lst_open_daily_13[j])
                    {
                        lst_open_daily_13.Remove(lst_open_daily_13[j]);
                    }
                }
            }
            if (lst_open_daily_13.Count > 0)
            {
                int a = UnityEngine.Random.Range(0, lst_open_daily_13.Count);
                PlayerPrefs.SetInt(StringUtils.str_open_head + (lst_open_daily_13[a] - 1), lst_open_daily_13[a]);
                
                PlayerPrefs.SetInt(StringUtils.str_id_head_open_daily_13, lst_open_daily_13[a]);
                obj_btn_getnow.GetComponent<DailyLoginBonus>().obj_head.GetComponent<Image>().sprite
                   = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.str_id_head_open_daily_13) - 1];
                /*FirebaseAnalyticManager.instance.AnalyticsOpenCharacter(lst_open_daily_13[a]);*/
            }
            else
            {
                StartCoroutine(Dailygift.instance.MoveCoin(obj_btn_getnow, 2.1f, obj_coin_taget_2, 0.05f));
                StartCoroutine(Addcoin(1.2f, number_coint_reward));
                StartCoroutine(HidePanel());
            }

            //SceneManager.LoadScene("Menu");
        }
        if (number_daily == 6 && lst_open_daily_6.Count > 0)
        {
            obj_btn_getnow.GetComponent<DailyLoginBonus>().obj_head.transform.GetChild(0).gameObject.SetActive(false);
            obj_btn_getnow.GetComponent<DailyLoginBonus>().obj_head.GetComponent<Image>().color = Color.white;
            StartCoroutine(MoveCardPlayer());
        }
        if (number_daily == 13 && lst_open_daily_13.Count > 0)
        {
            obj_btn_getnow.GetComponent<DailyLoginBonus>().obj_head.transform.GetChild(0).gameObject.SetActive(false);
            obj_btn_getnow.GetComponent<DailyLoginBonus>().obj_head.GetComponent<Image>().color = Color.white;
            StartCoroutine(MoveCardPlayer());
        }
        number_daily = PlayerPrefs.GetInt(StringUtils.str_number_daily, 0);
        number_daily++;
        PlayerPrefs.SetInt(StringUtils.str_number_daily, number_daily);
        if (number_daily == 14)
        {
            PlayerPrefs.SetInt(StringUtils.str_number_daily, 0);
        }

    }

    public void GetPrizeX2()
    {

        PlayerPrefs.SetString(StringUtils.str_time_open_daily_bonus, DateTime.Now.Date.ToString());
        obj_btn_getnow.GetComponent<DailyLoginBonus>().txt_day.text = "Day " + (number_daily + 1);
        obj_btn_getnow.GetComponent<DailyLoginBonus>().txt_day.color = Color.white;
        obj_btn_getnow.GetComponent<DailyLoginBonus>().txt_numbercoin_reward.enabled = false;
        obj_btn_getnow.GetComponent<DailyLoginBonus>().GetComponent<Image>().sprite = obj_btn_getnow.GetComponent<DailyLoginBonus>().spr_ok_daily;
        obj_btn_getnow.GetComponent<DailyLoginBonus>().obj_ok.SetActive(true);
        obj_btn_getnow.GetComponent<DailyLoginBonus>().img_coin.color = Color.white;
        obj_btn_getnow.GetComponent<Button>().interactable = false;
        btn_getprize.SetActive(false);
        btn_getprize_2.SetActive(false);
        lst_open_daily_6 = new List<int>() { 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
        lst_open_daily_13 = new List<int>() { 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46 };
        if (number_daily == 6 || number_daily == 13)
        {
            obj_btn_getnow.GetComponent<DailyLoginBonus>().obj_head.transform.GetChild(0).gameObject.SetActive(false);
            obj_btn_getnow.GetComponent<DailyLoginBonus>().obj_head.GetComponent<Image>().color = Color.white;

        }
        if (number_daily != 6 && number_daily != 13)
        {
            StartCoroutine(Dailygift.instance.MoveCoin(obj_btn_getnow, 2.1f, obj_coin_taget_2, 0.05f));
            StartCoroutine(Addcoin(1.2f, number_coint_reward * 2));
        }
        else if (number_daily == 6)
        {
            for (int i = 0; i < InsSelectPlayer.instance.lst_open_player.Count; i++)
            {
                for (int j = 0; j < lst_open_daily_6.Count; j++)
                {
                    if (InsSelectPlayer.instance.lst_open_player[i] == lst_open_daily_6[j])
                    {
                        lst_open_daily_6.Remove(lst_open_daily_6[j]);
                    }
                }
            }
            if (lst_open_daily_6.Count > 0)
            {
                int a = UnityEngine.Random.Range(0, lst_open_daily_6.Count);
                PlayerPrefs.SetInt(StringUtils.str_open_head + (lst_open_daily_6[a] - 1), lst_open_daily_6[a]);
                PlayerPrefs.SetInt(StringUtils.str_id_head_open_daily_6, lst_open_daily_6[a]);
                /*FirebaseAnalyticManager.instance.AnalyticsOpenCharacter(lst_open_daily_6[a]);*/
            }
            else
            {
                StartCoroutine(Dailygift.instance.MoveCoin(obj_btn_getnow, 2.1f, obj_coin_taget_2, 0.05f));
                StartCoroutine(Addcoin(1.2f, number_coint_reward));
                StartCoroutine(HidePanel());
            }
            //SceneManager.LoadScene("Menu");
        }
        else if (number_daily == 13)
        {
            for (int i = 0; i < InsSelectPlayer.instance.lst_open_player.Count; i++)
            {
                for (int j = 0; j < lst_open_daily_13.Count; j++)
                {
                    if (InsSelectPlayer.instance.lst_open_player[i] == lst_open_daily_13[j])
                    {
                        lst_open_daily_13.Remove(lst_open_daily_13[j]);
                    }
                }
            }
            if (lst_open_daily_13.Count > 0)
            {
                int a = UnityEngine.Random.Range(0, lst_open_daily_13.Count);
                PlayerPrefs.SetInt(StringUtils.str_open_head + (lst_open_daily_13[a] - 1), lst_open_daily_13[a]);
                PlayerPrefs.SetInt(StringUtils.str_id_head_open_daily_13, lst_open_daily_13[a]);
                /*FirebaseAnalyticManager.instance.AnalyticsOpenCharacter(lst_open_daily_13[a]);*/
            }
            else
            {
                StartCoroutine(Dailygift.instance.MoveCoin(obj_btn_getnow, 2.1f, obj_coin_taget_2, 0.05f));
                StartCoroutine(Addcoin(1.2f, number_coint_reward));
                StartCoroutine(HidePanel());
            }
            //SceneManager.LoadScene("Menu");
        }
        number_daily = PlayerPrefs.GetInt(StringUtils.str_number_daily, 0);
        number_daily++;
        PlayerPrefs.SetInt(StringUtils.str_number_daily, number_daily);
        if (number_daily == 14)
        {
            PlayerPrefs.SetInt(StringUtils.str_number_daily, 0);
        }

        StartCoroutine(HidePanel());
    }

    IEnumerator HidePanel()
    {
        yield return new WaitForSeconds(2.75f);
        Menu.instance.obj_daily_bonus.SetActive(false);
        Menu.instance.btnMenu.SetActive(true);
    }

    IEnumerator MoveCardPlayer()
    {
        Menu.instance.obj_daily_bonus.transform.GetChild(0).gameObject.SetActive(false);
        if (number_daily == 6)
        {
            img_head.sprite
                = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.str_id_head_open_daily_6) - 1];
            txt_name.text = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.str_id_head_open_daily_6) - 1].name.Substring(2);
            int ind_star = int.Parse(SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.str_id_head_open_daily_6) - 1].name.Substring(0, 1));
            img_star.sprite = SelectTeam.instance.star[ind_star];
            txt_power.text = "Power: " + (int)(PlayerPrefs.GetInt(StringUtils.str_point_speed + (PlayerPrefs.GetInt(StringUtils.str_id_head_open_daily_6) - 1))
            + PlayerPrefs.GetInt(StringUtils.str_point_jump + (PlayerPrefs.GetInt(StringUtils.str_id_head_open_daily_6) - 1))
            + PlayerPrefs.GetInt(StringUtils.str_point_shoot + (PlayerPrefs.GetInt(StringUtils.str_id_head_open_daily_6) - 1))) / 3 + "";
        }
        else if (number_daily == 13)
        {
            img_head.sprite
               = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.str_id_head_open_daily_13) - 1];
            txt_name.text = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.str_id_head_open_daily_13) - 1].name.Substring(2);
            int ind_star = int.Parse(SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.str_id_head_open_daily_13) - 1].name.Substring(0, 1));
            img_star.sprite = SelectTeam.instance.star[ind_star];
            txt_power.text = "Power: " + (int)(PlayerPrefs.GetInt(StringUtils.str_point_speed + (PlayerPrefs.GetInt(StringUtils.str_id_head_open_daily_13) - 1))
            + PlayerPrefs.GetInt(StringUtils.str_point_jump + (PlayerPrefs.GetInt(StringUtils.str_id_head_open_daily_13) - 1))
            + PlayerPrefs.GetInt(StringUtils.str_point_shoot + (PlayerPrefs.GetInt(StringUtils.str_id_head_open_daily_13) - 1))) / 3 + "";
        }
        obj_open_head.SetActive(true);

        card_head.transform.localScale = new Vector2(1, 1);
        iTween.MoveFrom(card_head, obj_btn_getnow.transform.position, 1.1f);
        iTween.ScaleFrom(card_head, new Vector3(0.01f, 0.01f, 0.01f), 1.5f);
        iTween.RotateFrom(card_head, new Vector3(60, 60, 60), 1.5f);
        yield return new WaitForSeconds(0.1f);
        obj_open_head.GetComponent<Image>().color = new Color(0, 0, 0, 0.85f);
        yield return new WaitForSeconds(0.4f);
        iTween.ScaleTo(obj_open_head.transform.GetChild(0).GetChild(0).gameObject, new Vector3(1, 1, 1), 1.5f);
        iTween.ScaleTo(obj_open_head.transform.GetChild(0).GetChild(2).gameObject, new Vector3(1, 1, 1), 0.5f);

    }

    IEnumerator Addcoin(float t, int coin)
    {
        yield return new WaitForSeconds(t);
        int mn = PlayerPrefs.GetInt(StringUtils.str_money, 0);
        mn += coin;
        PlayerPrefs.SetInt(StringUtils.str_money, mn);
    }

    public void ButtonOK()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        //obj_open_head.SetActive(false);
        //Menu.instance.obj_daily_bonus.SetActive(false);
        //Menu.instance.btnMenu.SetActive(true);
        SceneManager.LoadScene("Menu");
    }
}
