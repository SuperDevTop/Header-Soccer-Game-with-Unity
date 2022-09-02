using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePlayer : MonoBehaviour
{
    public static UpgradePlayer instance;
    public Text txt_speed, txt_size, txt_jump, txt_shoot;
    public Image img_speed, img_size, img_jump, img_shoot;
    public Button btn_upgrade;
    public int[] arr_money_upgrade;
    public Text txt_money_upgrade;

    public float[] arr_speed, arr_size, arr_jump, arr_shoot, arr_angle_shoot;
    public float speed_max, size_max, jump_max, shoot_max, angle_shoot_max;

    public int[] point_speed, point_size, point_jump, point_shoot;
    public int[] avg_point_star;
    public Sprite spr_maxlevel, spr_upgrade;
    public int[] price_buy_head;
    public GameObject obj_coin, obj_buy_player;
    public int id_buy;
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
        int money = PlayerPrefs.GetInt(StringUtils.str_money, 0);
        PlayerPrefs.SetInt(StringUtils.str_money, money);
        size_max = 1.25f;
        speed_max = 400;
        jump_max = 12f;
        angle_shoot_max = 70;
        shoot_max = 12.5f;

        GetPointPlayer();
        GetInfoPlayer();

        int id = PlayerPrefs.GetInt(StringUtils.id_player, 1);
        int lv_head = PlayerPrefs.GetInt(StringUtils.level_head + (id - 1), 1);

        if (lv_head >= 10)
        {
            btn_upgrade.interactable = false;
            btn_upgrade.GetComponent<Image>().sprite = spr_maxlevel;
            txt_money_upgrade.text = "Max Level";
            btn_upgrade.GetComponent<Animator>().enabled = false;
        }
        else
        {
            txt_money_upgrade.text = arr_money_upgrade[lv_head - 1].ToString();
        }
        img_speed.fillAmount = point_speed[id - 1] / 100f;
        img_shoot.fillAmount = point_shoot[id - 1] / 100f;
        img_jump.fillAmount = point_jump[id - 1] / 100f;

        txt_speed.text = "Speed: " + point_speed[id - 1] + "/100";
        txt_shoot.text = "Shoot: " + point_shoot[id - 1] + "/100";
        txt_jump.text = "Jump: " + point_jump[id - 1] + "/100";
        Menu.instance.txt_level_head.text = "Level: " + PlayerPrefs.GetInt(StringUtils.level_head + (id - 1), 1).ToString();
        Menu.instance.txt_power.text = "Power: " + (int)(PlayerPrefs.GetInt(StringUtils.str_point_speed + (id - 1))
            + PlayerPrefs.GetInt(StringUtils.str_point_jump + (id - 1))
            + PlayerPrefs.GetInt(StringUtils.str_point_shoot + (id - 1))) / 3 + "";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonBuyHead()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
       
    }
    public void ButtonUpgradePlayer()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        int money = PlayerPrefs.GetInt(StringUtils.str_money);
        int id = PlayerPrefs.GetInt(StringUtils.id_player);
        int lv_head = PlayerPrefs.GetInt(StringUtils.level_head + (id - 1), 1);

        if(btn_upgrade.GetComponent<Image>().sprite == spr_upgrade)
        {
            if (money < arr_money_upgrade[lv_head - 1])
            {
                Menu.instance.panelShop.SetActive(true);
                return;
            }
            if (lv_head >= 10)
            {
                int upgrade = PlayerPrefs.GetInt(StringUtils.str_upgrade_max, 0);
                upgrade++;
                PlayerPrefs.SetInt(StringUtils.str_upgrade_max, upgrade);
                if (upgrade == 1 && PlayerPrefs.GetInt(StringUtils.str_is_achievement + 9, 0) == 0)
                {
                    PlayerPrefs.SetInt(StringUtils.str_is_achievement + 9, 1);
                }
                else if (upgrade == 3 && PlayerPrefs.GetInt(StringUtils.str_is_achievement + 10, 0) == 0)
                {
                    PlayerPrefs.SetInt(StringUtils.str_is_achievement + 10, 1);
                }
                else if (upgrade == 10 && PlayerPrefs.GetInt(StringUtils.str_is_achievement + 11, 0) == 0)
                {
                    PlayerPrefs.SetInt(StringUtils.str_is_achievement + 11, 1);
                }
                btn_upgrade.interactable = false;
                btn_upgrade.GetComponent<Image>().sprite = spr_maxlevel;
                txt_money_upgrade.text = "Max Level";
                btn_upgrade.GetComponent<Animator>().enabled = false;
                return;
            }

            PlayerPrefs.SetInt(StringUtils.str_money, money - arr_money_upgrade[lv_head - 1]);
            int _sp = PlayerPrefs.GetInt(StringUtils.str_point_speed + (id - 1)) + Random.Range(1, 3);
            int _sh = PlayerPrefs.GetInt(StringUtils.str_point_shoot + (id - 1)) + Random.Range(1, 4);
            int _jump = PlayerPrefs.GetInt(StringUtils.str_point_jump + (id - 1)) + Random.Range(1, 3);

            PlayerPrefs.SetInt(StringUtils.str_point_speed + (id - 1), _sp);
            PlayerPrefs.SetInt(StringUtils.str_point_shoot + (id - 1), _sh);
            PlayerPrefs.SetInt(StringUtils.str_point_jump + (id - 1), _jump);

            //float _shot = PlayerPrefs.GetFloat(StringUtils.str_shoot + (id - 1));
            float _angshot = PlayerPrefs.GetFloat(StringUtils.str_angle_shoot + (id - 1));

            //PlayerPrefs.SetFloat(StringUtils.str_shoot + (id - 1), _shot);
            PlayerPrefs.SetFloat(StringUtils.str_angle_shoot + (id - 1), _angshot + 0.75f);

            img_speed.fillAmount = _sp / 100f;
            img_shoot.fillAmount = _sh / 100f;
            img_jump.fillAmount = _jump / 100f;

            txt_speed.text = "Speed: " + PlayerPrefs.GetInt(StringUtils.str_point_speed + (id - 1)) + "/100";
            txt_shoot.text = "Shoot: " + PlayerPrefs.GetInt(StringUtils.str_point_shoot + (id - 1)) + "/100";
            txt_jump.text = "Jump: " + PlayerPrefs.GetInt(StringUtils.str_point_jump + (id - 1)) + "/100";

            lv_head++;
            PlayerPrefs.SetInt(StringUtils.level_head + (id - 1), lv_head);
            txt_money_upgrade.text = arr_money_upgrade[lv_head - 1].ToString();

            Menu.instance.txt_level_head.text = "Level: " + PlayerPrefs.GetInt(StringUtils.level_head + (id - 1), 1).ToString();
            Menu.instance.txt_power.text = "Power: " + (int)(PlayerPrefs.GetInt(StringUtils.str_point_speed + (id - 1))
                + PlayerPrefs.GetInt(StringUtils.str_point_jump + (id - 1))
                + PlayerPrefs.GetInt(StringUtils.str_point_shoot + (id - 1))) / 3 + "";
        }
        else
        {
            int money2 = PlayerPrefs.GetInt(StringUtils.str_money);
            if (money2 < price_buy_head[id_buy - 1])
            {
                Menu.instance.panelShop.SetActive(true);
                return;
            }
            PlayerPrefs.SetInt(StringUtils.str_money, money2 - price_buy_head[id_buy - 1]);
            PlayerPrefs.SetInt(StringUtils.str_open_head + (id_buy - 1), id_buy);
            /*FirebaseAnalyticManager.instance.AnalyticsOpenCharacter(id_buy);*/
            obj_buy_player.GetComponent<ElementPlayer>().lock_player.SetActive(false);
            obj_buy_player.GetComponent<ElementPlayer>().img_head.color = Color.white;
            obj_buy_player.GetComponent<Image>().color = Color.white;
            obj_buy_player.GetComponent<ElementPlayer>().img_legend.color = Color.white;
            btn_upgrade.interactable = true;
            btn_upgrade.GetComponent<Image>().sprite = spr_upgrade;
            txt_money_upgrade.text = arr_money_upgrade[0].ToString();
            btn_upgrade.GetComponent<Animator>().enabled = true;
        }


    }
    void GetPointPlayer()
    {
        point_speed[0] = 10;
        point_shoot[0] = 8;
        point_jump[0] = 5;

        point_speed[1] = 9;
        point_shoot[1] = 8;
        point_jump[1] = 6;

        point_speed[2] = 11;
        point_shoot[2] = 7;
        point_jump[2] = 5;

        point_speed[3] = 6;
        point_shoot[3] = 12;
        point_jump[3] = 5;

        point_speed[4] = 7;
        point_shoot[4] = 8;
        point_jump[4] = 8;

        point_speed[5] = 12;
        point_shoot[5] = 13;
        point_jump[5] = 13;

        point_speed[6] = 16;
        point_shoot[6] = 10;
        point_jump[6] = 12;

        point_speed[7] = 12;
        point_shoot[7] = 11;
        point_jump[7] = 17;

        point_speed[8] = 15;
        point_shoot[8] = 14;
        point_jump[8] = 13;

        point_speed[9] = 15;
        point_shoot[9] = 15;
        point_jump[9] = 15;

        point_speed[10] = 18;
        point_shoot[10] = 16;
        point_jump[10] = 13;

        point_speed[11] = 15;
        point_shoot[11] = 18;
        point_jump[11] = 17;

        point_speed[12] = 19;
        point_shoot[12] = 15;
        point_jump[12] = 18;

        point_speed[13] = 18;
        point_shoot[13] = 18;
        point_jump[13] = 18;

        point_speed[14] = 15;
        point_shoot[14] = 20;
        point_jump[14] = 21;

        point_speed[15] = 21;
        point_shoot[15] = 20;
        point_jump[15] = 18;

        point_speed[16] = 23;
        point_shoot[16] = 21;
        point_jump[16] = 19;

        point_speed[17] = 22;
        point_shoot[17] = 23;
        point_jump[17] = 22;

        point_speed[18] = 23;
        point_shoot[18] = 25;
        point_jump[18] = 21;

        point_speed[19] = 26;
        point_shoot[19] = 27;
        point_jump[19] = 19;

        point_speed[20] = 26;
        point_shoot[20] = 25;
        point_jump[20] = 24;

        point_speed[21] = 23;
        point_shoot[21] = 27;
        point_jump[21] = 28;

        point_speed[22] = 27;
        point_shoot[22] = 27;
        point_jump[22] = 27;

        point_speed[23] = 27;
        point_shoot[23] = 31;
        point_jump[23] = 25;

        point_speed[24] = 31;
        point_shoot[24] = 29;
        point_jump[24] = 28;

        point_speed[25] = 33;
        point_shoot[25] = 31;
        point_jump[25] = 29;

        point_speed[26] = 33;
        point_shoot[26] = 33;
        point_jump[26] = 33;

        point_speed[27] = 35;
        point_shoot[27] = 36;
        point_jump[27] = 33;

        point_speed[28] = 39;
        point_shoot[28] = 40;
        point_jump[28] = 39;

        point_speed[29] = 38;
        point_shoot[29] = 41;
        point_jump[29] = 35;

        point_speed[30] = 43;
        point_shoot[30] = 40;
        point_jump[30] = 45;

        point_speed[31] = 42;
        point_shoot[31] = 46;
        point_jump[31] = 40;

        point_speed[32] = 44;
        point_shoot[32] = 43;
        point_jump[32] = 45;

        point_speed[33] = 47;
        point_shoot[33] = 41;
        point_jump[33] = 48;

        point_speed[34] = 44;
        point_shoot[34] = 42;
        point_jump[34] = 50;

        point_speed[35] = 46;
        point_shoot[35] = 50;
        point_jump[35] = 47;

        point_speed[36] = 48;
        point_shoot[36] = 49;
        point_jump[36] = 48;

        point_speed[37] = 53;
        point_shoot[37] = 50;
        point_jump[37] = 55;

        point_speed[38] = 53;
        point_shoot[38] = 60;
        point_jump[38] = 53;

        point_speed[39] = 55;
        point_shoot[39] = 57;
        point_jump[39] = 54;

        point_speed[40] = 58;
        point_shoot[40] = 60;
        point_jump[40] = 56;

        point_speed[41] = 57;
        point_shoot[41] = 59;
        point_jump[41] = 60;

        point_speed[42] = 60;
        point_shoot[42] = 58;
        point_jump[42] = 59;

        point_speed[43] = 65;
        point_shoot[43] = 60;
        point_jump[43] = 56;

        point_speed[44] = 68;
        point_shoot[44] = 71;
        point_jump[44] = 62;

        point_speed[45] = 72;
        point_shoot[45] = 70;
        point_jump[45] = 68;

        for (int i = 0; i < SelectTeam.instance.AI_Head.Length; i++)
        {
            int _sp = PlayerPrefs.GetInt(StringUtils.str_point_speed + i, point_speed[i]);
            int _sh = PlayerPrefs.GetInt(StringUtils.str_point_shoot + i, point_shoot[i]);
            int _jump = PlayerPrefs.GetInt(StringUtils.str_point_jump + i, point_jump[i]);

            PlayerPrefs.SetInt(StringUtils.str_point_speed + i, _sp);
            PlayerPrefs.SetInt(StringUtils.str_point_shoot + i, _sh);
            PlayerPrefs.SetInt(StringUtils.str_point_jump + i, _jump);

        }
    }

    void GetInfoPlayer()
    {
        arr_speed[0] = 312;
        arr_shoot[0] = 11.5f;
        arr_size[0] = 1.1f;
        arr_jump[0] = 9.12f;
        arr_angle_shoot[0] = 50;

        arr_speed[1] = 310;
        arr_shoot[1] = 11f;
        arr_size[1] = 1.12f;
        arr_jump[1] = 9.15f;
        arr_angle_shoot[1] = 50;

        arr_speed[2] = 315;
        arr_shoot[2] = 11f;
        arr_size[2] = 1.11f;
        arr_jump[2] = 9.1f;
        arr_angle_shoot[2] = 50;

        arr_speed[3] = 313;
        arr_shoot[3] = 11.1f;
        arr_size[3] = 1.1f;
        arr_jump[3] = 9.11f;
        arr_angle_shoot[3] = 50;

        arr_speed[4] = 316;
        arr_shoot[4] = 11.2f;
        arr_size[4] = 1.1f;
        arr_jump[4] = 9.14f;
        arr_angle_shoot[4] = 50;

        arr_speed[5] = 318;
        arr_shoot[5] = 11.5f;
        arr_size[5] = 1.14f;
        arr_jump[5] = 9.17f;
        arr_angle_shoot[5] = 50.25f;

        arr_speed[6] = 320;
        arr_shoot[6] = 11.75f;
        arr_size[6] = 1.1f;
        arr_jump[6] = 9.2f;
        arr_angle_shoot[6] = 51f;

        arr_speed[7] = 319;
        arr_shoot[7] = 11.78f;
        arr_size[7] = 1.16f;
        arr_jump[7] = 9.2f;
        arr_angle_shoot[7] = 51.25f;

        arr_speed[8] = 322;
        arr_shoot[8] = 11.8f;
        arr_size[8] = 1.11f;
        arr_jump[8] = 9.1f;
        arr_angle_shoot[8] = 51.5f;

        arr_speed[9] = 324;
        arr_shoot[9] = 11.9f;
        arr_size[9] = 1.14f;
        arr_jump[9] = 9.2f;
        arr_angle_shoot[9] = 51.75f;

        arr_speed[10] = 327;
        arr_shoot[10] = 11.95f;
        arr_size[10] = 1.15f;
        arr_jump[10] = 9.23f;
        arr_angle_shoot[10] = 52f;

        arr_speed[11] = 325;
        arr_shoot[11] = 11.7f;
        arr_size[11] = 1.17f;
        arr_jump[11] = 9.25f;
        arr_angle_shoot[11] = 52.25f;

        arr_speed[12] = 328;
        arr_shoot[12] = 11.6f;
        arr_size[12] = 1.175f;
        arr_jump[12] = 9.28f;
        arr_angle_shoot[12] = 52.5f;

        arr_speed[13] = 330;
        arr_shoot[13] = 11.5f;
        arr_size[13] = 1.18f;
        arr_jump[13] = 9.25f;
        arr_angle_shoot[13] = 52.75f;

        arr_speed[14] = 333;
        arr_shoot[14] = 11.3f;
        arr_size[14] = 1.185f;
        arr_jump[14] = 9.3f;
        arr_angle_shoot[14] = 53f;

        arr_speed[15] = 335;
        arr_shoot[15] = 11.2f;
        arr_size[15] = 1.187f;
        arr_jump[15] = 9.35f;
        arr_angle_shoot[15] = 53.25f;

        arr_speed[16] = 336;
        arr_shoot[16] = 11.1f;
        arr_size[16] = 1.188f;
        arr_jump[16] = 9.36f;
        arr_angle_shoot[16] = 53.5f;

        arr_speed[17] = 334;
        arr_shoot[17] = 11f;
        arr_size[17] = 1.187f;
        arr_jump[17] = 9.34f;
        arr_angle_shoot[17] = 53.75f;

        arr_speed[18] = 337;
        arr_shoot[18] = 10.8f;
        arr_size[18] = 1.185f;
        arr_jump[18] = 9.32f;
        arr_angle_shoot[18] = 54f;

        arr_speed[19] = 338;
        arr_shoot[19] = 10.78f;
        arr_size[19] = 1.186f;
        arr_jump[19] = 9.43f;
        arr_angle_shoot[19] = 54.25f;

        arr_speed[20] = 339;
        arr_shoot[20] = 10.8f;
        arr_size[20] = 1.185f;
        arr_jump[20] = 9.42f;
        arr_angle_shoot[20] = 54.5f;

        arr_speed[21] = 340;
        arr_shoot[21] = 10.82f;
        arr_size[21] = 1.188f;
        arr_jump[21] = 9.44f;
        arr_angle_shoot[21] = 54.75f;

        arr_speed[22] = 341;
        arr_shoot[22] = 10.81f;
        arr_size[22] = 1.1885f;
        arr_jump[22] = 9.45f;
        arr_angle_shoot[22] = 54.8f;

        arr_speed[23] = 341;
        arr_shoot[23] = 10.83f;
        arr_size[23] = 1.186f;
        arr_jump[23] = 9.44f;
        arr_angle_shoot[23] = 55f;

        arr_speed[24] = 341;
        arr_shoot[24] = 10.83f;
        arr_size[24] = 1.186f;
        arr_jump[24] = 9.44f;
        arr_angle_shoot[24] = 55f;

        arr_speed[25] = 342;
        arr_shoot[25] = 10.84f;
        arr_size[25] = 1.185f;
        arr_jump[25] = 9.45f;
        arr_angle_shoot[25] = 55.25f;

        arr_speed[26] = 343;
        arr_shoot[26] = 10.85f;
        arr_size[26] = 1.189f;
        arr_jump[26] = 9.48f;
        arr_angle_shoot[26] = 55.5f;

        arr_speed[27] = 345;
        arr_shoot[27] = 10.88f;
        arr_size[27] = 1.19f;
        arr_jump[27] = 9.52f;
        arr_angle_shoot[27] = 55.8f;

        arr_speed[28] = 346;
        arr_shoot[28] = 10.9f;
        arr_size[28] = 1.191f;
        arr_jump[28] = 9.55f;
        arr_angle_shoot[28] = 56f;

        arr_speed[29] = 346;
        arr_shoot[29] = 10.91f;
        arr_size[29] = 1.19f;
        arr_jump[29] = 9.48f;
        arr_angle_shoot[29] = 56.5f;

        arr_speed[30] = 348;
        arr_shoot[30] = 10.8f;
        arr_jump[30] = 9.51f;
        arr_angle_shoot[30] = 57f;

        arr_speed[31] = 346;
        arr_shoot[31] = 10.78f;
        arr_jump[31] = 9.5f;
        arr_angle_shoot[31] = 57.25f;

        arr_speed[32] = 347;
        arr_shoot[32] = 10.78f;
        arr_jump[32] = 9.52f;
        arr_angle_shoot[32] = 57.25f;

        arr_speed[33] = 348;
        arr_shoot[33] = 10.8f;
        arr_jump[33] = 9.53f;
        arr_angle_shoot[33] = 57.5f;

        arr_speed[34] = 348;
        arr_shoot[34] = 10.8f;
        arr_jump[34] = 9.53f;
        arr_angle_shoot[34] = 57.5f;

        arr_shoot[35] = 10.78f;
        arr_angle_shoot[35] = 57.75f;

        arr_shoot[36] = 10.77f;
        arr_angle_shoot[36] = 57.8f;

        arr_shoot[37] = 10.77f;
        arr_angle_shoot[37] = 58f;

        arr_shoot[38] = 10.77f;
        arr_angle_shoot[38] = 59f;

        arr_shoot[39] = 10.77f;
        arr_angle_shoot[39] = 58.5f;

        arr_shoot[40] = 10.75f;
        arr_angle_shoot[40] = 60f;

        arr_shoot[41] = 10.75f;
        arr_angle_shoot[41] = 60f;

        arr_shoot[42] = 10.78f;
        arr_angle_shoot[42] = 58.5f;

        arr_shoot[43] = 10.75f;
        arr_angle_shoot[43] = 59.5f;

        arr_shoot[44] = 10.75f;
        arr_angle_shoot[44] = 61.5f;

        arr_shoot[45] = 10.75f;
        arr_angle_shoot[45] = 62.5f;

        for (int i = 0; i < SelectTeam.instance.AI_Head.Length; i++)
        {
            arr_speed[i] = 300 + point_speed[i];
            arr_jump[i] = 9.05f + point_jump[i] / 100f;

            float _sp = PlayerPrefs.GetFloat(StringUtils.str_speed + i, arr_speed[i]);
            float _sh = PlayerPrefs.GetFloat(StringUtils.str_shoot + i, arr_shoot[i]);
            float _sz = PlayerPrefs.GetFloat(StringUtils.str_size + i, arr_size[i]);
            float _jump = PlayerPrefs.GetFloat(StringUtils.str_jump + i, arr_jump[i]);
            float _ang = PlayerPrefs.GetFloat(StringUtils.str_angle_shoot + i, arr_angle_shoot[i]);

            PlayerPrefs.SetFloat(StringUtils.str_speed + i, _sp);
            PlayerPrefs.SetFloat(StringUtils.str_shoot + i, _sh);
            PlayerPrefs.SetFloat(StringUtils.str_size + i, _sz);
            PlayerPrefs.SetFloat(StringUtils.str_jump + i, _jump);
            PlayerPrefs.SetFloat(StringUtils.str_angle_shoot + i, _ang);
        }

    }
}
