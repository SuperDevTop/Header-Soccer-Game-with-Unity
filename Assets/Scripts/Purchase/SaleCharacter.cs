using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaleCharacter : MonoBehaviour
{
    public static SaleCharacter instance;
    public Image img_head_sale_1, img_head_sale_2;
    public Text mess_1, mess_2;
    public GameObject obj_sale_1, obj_sale_2, obj_sale_3;
    public List<int> lst_random_character = new List<int>();
    int number_open;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        SetupSaleCharacters();
    }
    public void SetupSaleCharacters()
    {
        lst_random_character.Clear();
        number_open = 0;
        for (int i = 21; i < 46; i++)
        {
            lst_random_character.Add(i);
        }
        for (int i = 0; i < InsSelectPlayer.instance.lst_open_player.Count; i++)
        {
            for (int j = 0; j < lst_random_character.Count; j++)
            {
                if (InsSelectPlayer.instance.lst_open_player[i] == lst_random_character[j])
                {
                    lst_random_character.Remove(lst_random_character[j]);
                }
            }
            if (InsSelectPlayer.instance.lst_open_player[i] > 0)
            {
                number_open++;
            }
        }

        if (number_open == 46 || PlayerPrefs.GetInt(StringUtils.str_open_sale_3, 0) == 1)
        {
            obj_sale_1.SetActive(false);
            obj_sale_2.SetActive(false);
            obj_sale_3.SetActive(false);
            return;
        }


        if (PlayerPrefs.GetString(StringUtils.str_time_reset_sale, "") == "")
        {
            int rd1 = UnityEngine.Random.Range(0, lst_random_character.Count);
            int a1 = lst_random_character[rd1];
            lst_random_character.RemoveAt(rd1);
            int rd2 = UnityEngine.Random.Range(0, lst_random_character.Count);
            int a2 = lst_random_character[rd2];
            lst_random_character.RemoveAt(rd2);
            if (a1 < a2)
            {
                PlayerPrefs.SetInt(StringUtils.str_random_head_sale_1, a1);
                PlayerPrefs.SetInt(StringUtils.str_random_head_sale_2, a2);
            }
            else
            {
                PlayerPrefs.SetInt(StringUtils.str_random_head_sale_1, a2);
                PlayerPrefs.SetInt(StringUtils.str_random_head_sale_2, a1);
            }
            PlayerPrefs.SetString(StringUtils.str_time_reset_sale, DateTime.Now.ToString());
            obj_sale_1.SetActive(true);
            obj_sale_2.SetActive(true);
            obj_sale_3.SetActive(false);
        }
        else
        {
            TimeSpan abc1 = (DateTime.Now.Date - Convert.ToDateTime(PlayerPrefs.GetString(StringUtils.str_time_reset_sale)));
            if (abc1.Days >= 1)
            {
                if (lst_random_character.Count <= 0)
                {
                    obj_sale_1.SetActive(false);
                    obj_sale_2.SetActive(false);
                    obj_sale_3.SetActive(false);
                }
                else if (lst_random_character.Count == 1)
                {
                    obj_sale_1.SetActive(false);
                    obj_sale_2.SetActive(true);
                    obj_sale_3.SetActive(false);
                    int rd1 = UnityEngine.Random.Range(0, lst_random_character.Count);
                    PlayerPrefs.SetInt(StringUtils.str_random_head_sale_2, lst_random_character[rd1]);

                }
                else
                {
                    obj_sale_1.SetActive(true);
                    obj_sale_2.SetActive(true);
                    obj_sale_3.SetActive(false);
                    int rd1 = UnityEngine.Random.Range(0, lst_random_character.Count);
                    int a1 = lst_random_character[rd1];
                    lst_random_character.RemoveAt(rd1);
                    int rd2 = UnityEngine.Random.Range(0, lst_random_character.Count);
                    int a2 = lst_random_character[rd2];
                    lst_random_character.RemoveAt(rd2);
                    if (a1 < a2)
                    {
                        PlayerPrefs.SetInt(StringUtils.str_random_head_sale_1, a1);
                        PlayerPrefs.SetInt(StringUtils.str_random_head_sale_2, a2);
                    }
                    else
                    {
                        PlayerPrefs.SetInt(StringUtils.str_random_head_sale_1, a2);
                        PlayerPrefs.SetInt(StringUtils.str_random_head_sale_2, a1);
                    }
                }

                PlayerPrefs.SetString(StringUtils.str_time_reset_sale, DateTime.Now.ToString());
                PlayerPrefs.SetInt(StringUtils.str_open_sale_1, 0);
                PlayerPrefs.SetInt(StringUtils.str_open_sale_2, 0);
            }
            else
            {
                if (PlayerPrefs.GetInt(StringUtils.str_open_sale_1, 0) == 1)
                {
                    obj_sale_1.SetActive(false);
                }

                if (PlayerPrefs.GetInt(StringUtils.str_open_sale_2, 0) == 1)
                {
                    obj_sale_2.SetActive(false);
                }
                if (PlayerPrefs.GetInt(StringUtils.str_open_sale_3, 0) == 0 && number_open < 46)
                {
                    if (PlayerPrefs.GetInt(StringUtils.str_open_sale_1, 0) == 1 || PlayerPrefs.GetInt(StringUtils.str_open_sale_1, 0) == 1)
                        obj_sale_3.SetActive(true);
                    else
                    {
                        obj_sale_3.SetActive(false);
                    }
                }
                else
                {
                    obj_sale_3.SetActive(false);
                }
            }

        }
        int id1 = PlayerPrefs.GetInt(StringUtils.str_random_head_sale_1);
        int id2 = PlayerPrefs.GetInt(StringUtils.str_random_head_sale_2);
        img_head_sale_1.sprite = SelectTeam.instance.AI_Head[id1 - 1];
        img_head_sale_2.sprite = SelectTeam.instance.AI_Head[id2 - 1];
        mess_1.text = "+60.000 coins.\nUnlock " + SelectTeam.instance.AI_Head[id1 - 1].name.Substring(2);
        mess_2.text = "+180.000 coins.\nUnlock " + SelectTeam.instance.AI_Head[id2 - 1].name.Substring(2);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
