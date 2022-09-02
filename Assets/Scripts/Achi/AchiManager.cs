using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchiManager : MonoBehaviour
{
    public static AchiManager instance;
    public GameObject prf_achi, content_achi_scroll;
    public string[] str_mission;
    public int[] number_reward;
    public int[] lst_is_getAchi = new int[35];
    public Sprite spr_tab_gray, spr_tab_while, spr_achi_gray, spr_achi_while, spr_getprize_gray, spr_getprize_while;
    public Text txt_number_achieved, txt_number_open_get_achi;

    // Start is called before the first frame update
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        lst_is_getAchi = new int[35];
        int achieved_1 = 0;
        int achieved_2 = 0;
        for (int i = 0; i < 35; i++)
        {
            lst_is_getAchi[i] = PlayerPrefs.GetInt(StringUtils.str_is_achievement + i, 0);
            //lst_is_getAchi[i] = PlayerPrefs.GetInt(StringUtils.str_is_achievement + i, 0);
            //PlayerPrefs.SetInt(StringUtils.str_is_achievement + i, lst_is_getAchi[i]);
            if (lst_is_getAchi[i] == 1)
            {
                achieved_1++;
            }
            else if (lst_is_getAchi[i] == 2)
            {
                achieved_2++;
            }

        }
       
        txt_number_achieved.text = achieved_2.ToString();
        txt_number_open_get_achi.text = achieved_1.ToString();
        if (achieved_1 == 0)
        {
            txt_number_open_get_achi.transform.parent.gameObject.SetActive(false);
        }
        for (int i = 0; i < 35; i++)
        {
            GameObject obj = Instantiate(prf_achi, content_achi_scroll.transform);
            obj.GetComponent<AchiElement>().txt_index.text = (i + 1) + ". ";
            obj.GetComponent<AchiElement>().txt_mission.text = str_mission[i];
            obj.GetComponent<AchiElement>().txt_coin.text = number_reward[i].ToString();
            obj.GetComponent<AchiElement>().id = i;

            if (PlayerPrefs.GetInt(StringUtils.str_is_achievement + i, 0) == 0)
            {
                obj.GetComponent<AchiElement>().txt_index.color = Color.gray;
                obj.GetComponent<AchiElement>().txt_mission.color = Color.gray;
                obj.GetComponent<AchiElement>().txt_coin.color = Color.gray;
                obj.GetComponent<AchiElement>().txt_reward.color = Color.gray;
                obj.GetComponent<AchiElement>().img_coin.color = Color.gray;
                obj.GetComponent<AchiElement>().img_tab.sprite = spr_tab_gray;
                obj.GetComponent<AchiElement>().img_achi.sprite = spr_achi_gray;
                obj.GetComponent<AchiElement>().obj_getprize.GetComponent<Image>().sprite = spr_getprize_gray;
                obj.GetComponent<AchiElement>().obj_getprize.GetComponent<Animator>().enabled = false;
                obj.GetComponent<AchiElement>().obj_getprize.GetComponent<Button>().interactable = false;
            }
            else if (PlayerPrefs.GetInt(StringUtils.str_is_achievement + i, 0) == 1)
            {
                obj.GetComponent<AchiElement>().txt_index.color = Color.white;
                obj.GetComponent<AchiElement>().txt_mission.color = Color.white;
                obj.GetComponent<AchiElement>().txt_coin.color = Color.white;
                obj.GetComponent<AchiElement>().txt_reward.color = Color.white;
                obj.GetComponent<AchiElement>().img_coin.color = Color.white;
                obj.GetComponent<AchiElement>().img_tab.sprite = spr_tab_while;
                obj.GetComponent<AchiElement>().img_achi.sprite = spr_achi_while;
                obj.GetComponent<AchiElement>().obj_getprize.GetComponent<Image>().sprite = spr_getprize_while;
                obj.GetComponent<AchiElement>().obj_getprize.GetComponent<Animator>().enabled = true;
                obj.GetComponent<AchiElement>().obj_getprize.GetComponent<Button>().interactable = true;
            }
            else if (PlayerPrefs.GetInt(StringUtils.str_is_achievement + i, 0) == 2)
            {
                obj.GetComponent<AchiElement>().txt_index.color = Color.white;
                obj.GetComponent<AchiElement>().txt_mission.color = Color.white;
                obj.GetComponent<AchiElement>().txt_coin.color = Color.white;
                obj.GetComponent<AchiElement>().txt_reward.color = Color.white;
                obj.GetComponent<AchiElement>().img_coin.color = Color.white;
                obj.GetComponent<AchiElement>().img_tab.sprite = spr_tab_while;
                obj.GetComponent<AchiElement>().img_achi.sprite = spr_achi_while;
                obj.GetComponent<AchiElement>().obj_getprize.GetComponent<Image>().sprite = spr_getprize_while;
                obj.GetComponent<AchiElement>().obj_getprize.SetActive(false);
                obj.GetComponent<AchiElement>().obj_ok.SetActive(true);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
