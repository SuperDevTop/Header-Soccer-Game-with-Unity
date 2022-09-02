using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dailygift : MonoBehaviour
{
    public static Dailygift instance;
    public string MM, SS;
    public Button btn_dailygift;
    public Text txt_time_daily, txt_coin_reward;
    public Text txt_cantloadvideo;
    public Animator ani;
    public GameObject obj_add_coin;
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

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetString(StringUtils.str_time_daily, "") == "")
        {
            btn_dailygift.interactable = true;
            txt_time_daily.gameObject.SetActive(false);
            ani.enabled = true;
            txt_coin_reward.color = new Color(1f, 0.8f, 0.11f, 1f);
            txt_coin_reward.text = (350).ToString();
        }
        else
        {
            DailyGift();
        }

    }

    void DailyGift()
    {
        TimeSpan abc = (DateTime.Now - Convert.ToDateTime(PlayerPrefs.GetString(StringUtils.str_time_daily)));
        if (abc.TotalMinutes < 5)
        {
            btn_dailygift.interactable = false;
            txt_time_daily.gameObject.SetActive(true);
            ani.enabled = false;
            int _mm = 4 - abc.Minutes;
            int _ss = 59 - abc.Seconds;
            if (_mm < 10)
            {
                MM = "0" + _mm;
            }
            else
            {
                MM = _mm.ToString();
            }
            if (_ss < 10)
            {
                SS = "0" + _ss;
            }
            else
            {
                SS = _ss.ToString();
            }

            txt_time_daily.text = "00:" + MM + ":" + SS;
            txt_coin_reward.color = new Color(0.65f, 0.5f, 0f, 0.6f);
        }
        else
        {
            btn_dailygift.interactable = true;
            txt_time_daily.gameObject.SetActive(false);
            ani.enabled = true;
            txt_coin_reward.color = new Color(1f, 0.8f, 0.11f, 1f);
        }
        int dl = PlayerPrefs.GetInt(StringUtils.str_watchads_daily, 0);
        txt_coin_reward.text = (350 + dl * 10).ToString();
    }

    public void ButtonDailyGift()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        AdsManager.mode_show_ful_reward = "GetRewardAds";
        AdsManager.Instance.showRewardAdsVideo();
        //StartCoroutine(Dailygift.instance.MoveCoin(Dailygift.instance.btn_dailygift.gameObject, 1, DailyController.instance.obj_coin_taget, 0.05f));
    }

    public IEnumerator MoveCoin(GameObject obj_tranf, float t, GameObject obj_target, float t2)
    {
        yield return new WaitForSeconds(t2);
        Instantiate(DailyController.instance.eff_getcoin, obj_tranf.transform.position + new Vector3(0, 0.4f), Quaternion.identity);
        
        float t1 = 0;
        while (t1 >= 0)
        {
            yield return new WaitForSeconds(0.09f);
            t1 += 0.2f;
            if (t1 > 1)
            {
                break;
            }
            for (int i = 0; i < 2; i++)
            {
                yield return new WaitForSeconds(0.005f);
                GameObject obj = Instantiate(DailyController.instance.obj_coin_move,
                    obj_tranf.transform.position + new Vector3(UnityEngine.Random.Range(-0.2f, 0.2f), UnityEngine.Random.Range(0.1f, 0.5f)), Quaternion.identity) as GameObject;
                iTween.MoveTo(obj, obj_target.transform.position, 1 * t);
                iTween.ScaleTo(obj, new Vector2(0, 0), 2 * t);
                iTween.RotateFrom(obj, new Vector3(UnityEngine.Random.Range(10f, 80), UnityEngine.Random.Range(10f, 80), UnityEngine.Random.Range(10f, 80)), 0);
                iTween.RotateBy(obj, new Vector3(UnityEngine.Random.Range(10f, 80), UnityEngine.Random.Range(10f, 80), UnityEngine.Random.Range(10f, 80)), 0.5f * t);
                Destroy(obj, 1.8f);
                GameObject eff_add = Instantiate(obj_add_coin, obj_target.transform.position, Quaternion.identity) as GameObject;
                eff_add.GetComponent<ParticleSystem>().startDelay = t / 2.5f;
                if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
                {
                    SoundManager.Instance.coinEffect.PlayDelayed(t / 6f);
                }
            }

        }
    }
}
