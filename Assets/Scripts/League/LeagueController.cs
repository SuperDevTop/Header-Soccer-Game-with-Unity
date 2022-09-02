using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeagueController : MonoBehaviour
{
    public static int number_level_league;
    public static int mode_league;
    public GameObject obj_level, content;
    public Text txt_name_league;
    public int level_open;
    public static List<int> lst_id_league_C = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    public static List<int> lst_id_league_B = new List<int>() { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
    public static List<int> lst_id_league_A = new List<int>() { 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };
    public static List<int> lst_id_league_SUPER = new List<int>() { 31, 32, 33, 34, 35, 36, 37, 38, 31, 32, 33, 34, 35, 36, 37, 38 };
    public static List<int> lst_id_league_LEGEN = new List<int>() { 39, 40, 41, 42, 43, 44, 45, 46, 39, 40, 41, 42, 43, 44, 45, 46 };
    public List<Button> lst_btn_league = new List<Button>();
    public Sprite spr_level_lock, spr_level_unlock, spr_level_complete;
    // Start is called before the first frame update
    void Start()
    {
        mode_league = PlayerPrefs.GetInt(StringUtils.str_mode_league, 0);

        for (int i = 0; i < lst_btn_league.Count; i++)
        {
            if (i <= mode_league)
            {
                lst_btn_league[i].interactable = true;
            }
            else
            {
                lst_btn_league[i].interactable = false;
                lst_btn_league[i].transform.GetChild(0).gameObject.SetActive(true);
                lst_btn_league[i].GetComponent<Image>().color = new Color(0.25f,0.25f,0.25f,0.8f);
            }
        }
        PlayerPrefs.SetInt(StringUtils.str_mode_league, mode_league);
        switch (mode_league)
        {
            case 0:
                txt_name_league.text = "AMATEUR LEAGUE";
                GrenarateLevelLeague(20, mode_league);
                break;
            case 1:
                txt_name_league.text = "SEMI-PRO LEAGUE";
                GrenarateLevelLeague(20, mode_league);
                break;
            case 2:
                txt_name_league.text = "PROFESSIONAL LEAGUE";
                GrenarateLevelLeague(20, mode_league);
                break;
            case 3:
                txt_name_league.text = "SUPER LEAGUE";
                GrenarateLevelLeague(16, mode_league);
                break;
            case 4:
                txt_name_league.text = "LEGENDARY LEAGUE";
                GrenarateLevelLeague(16, mode_league);
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    void GrenarateLevelLeague(int total_number_level, int mode)
    {
        GameObject[] arr = GameObject.FindGameObjectsWithTag("ButtonSelectLeague");
        if (arr.Length > 0)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Destroy(arr[i]);
            }
        }
        level_open = PlayerPrefs.GetInt(StringUtils.str_level_open + mode, 1);
        PlayerPrefs.SetInt(StringUtils.str_level_open + mode, level_open);
        for (int i = 1; i < total_number_level + 1; i++)
        {
            GameObject lv = Instantiate(obj_level, content.transform);
            lv.GetComponent<ButtonLeagueElement>().txt_number_level.text = (i).ToString();
            if (i == level_open)
            {
                lv.GetComponent<Button>().interactable = true;
                lv.GetComponent<Image>().sprite = spr_level_unlock;
            }
            else if (i < level_open)
            {
                lv.GetComponent<Button>().interactable = true;
                lv.GetComponent<Image>().sprite = spr_level_complete;
            }
            else
            {
                lv.GetComponent<Button>().interactable = false;
                lv.transform.GetChild(0).GetComponent<Text>().color = Color.gray;
                lv.GetComponent<Image>().sprite = spr_level_lock;
            }
        }
    }

    public void ButtonBackLeague()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        SceneManager.LoadScene("Menu");
    }

    public void ButtonModeLeague(int md)
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        if(mode_league != md)
        {
            mode_league = md;
            switch (md)
            {
                case 0:
                    txt_name_league.text = "AMATEUR LEAGUE";
                    GrenarateLevelLeague(20, md);
                    break;
                case 1:
                    txt_name_league.text = "SEMI-PRO LEAGUE";
                    GrenarateLevelLeague(20, md);
                    break;
                case 2:
                    txt_name_league.text = "PROFESSIONAL LEAGUE";
                    GrenarateLevelLeague(20, md);
                    break;
                case 3:
                    txt_name_league.text = "SUPER LEAGUE";
                    GrenarateLevelLeague(16, md);
                    break;
                case 4:
                    txt_name_league.text = "LEGENDARY LEAGUE";
                    GrenarateLevelLeague(16, md);
                    break;
            }
        }
    }
}
