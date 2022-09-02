using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchiElement : MonoBehaviour
{
    public Image img_coin, img_achi, img_tab;
    public Text txt_index, txt_mission, txt_coin, txt_reward;
    public GameObject obj_getprize, obj_ok;
    public int id;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonGetPrizeAchi()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        
        PlayerPrefs.SetInt(StringUtils.str_is_achievement + id, 2);
        Debug.Log(id + "asdfsadf" + PlayerPrefs.GetInt(StringUtils.str_is_achievement + (int.Parse(txt_index.text.Substring(0, 1)) - 1), 0));
        obj_ok.SetActive(true);
        obj_getprize.SetActive(false);
        int achieved_1 = int.Parse(AchiManager.instance.txt_number_open_get_achi.text);
        int achieved_2 = int.Parse(AchiManager.instance.txt_number_achieved.text);
        achieved_1--;
        achieved_2++;
        AchiManager.instance.txt_number_achieved.text = achieved_2.ToString();
        AchiManager.instance.txt_number_open_get_achi.text = achieved_1.ToString();
        if (achieved_1 == 0)
        {
            AchiManager.instance.txt_number_open_get_achi.transform.parent.gameObject.SetActive(false);
        }
        StartCoroutine(Dailygift.instance.MoveCoin(img_coin.gameObject, 1, DailyController.instance.obj_coin_taget, 0.25f));
        StartCoroutine(Addcoin(1f));
        /*FirebaseAnalyticManager.instance.AnalyticsArchievement(id);*/
    }
    IEnumerator Addcoin(float t)
    {
        yield return new WaitForSeconds(t);
        int mn = PlayerPrefs.GetInt(StringUtils.str_money, 0);
        mn += int.Parse(txt_coin.text);
        PlayerPrefs.SetInt(StringUtils.str_money, mn);
    }
}
