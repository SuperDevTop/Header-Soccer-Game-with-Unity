using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ElementPlayer : MonoBehaviour
{
    public GameObject lock_player;
    public Image img_head, img_legend;
    public int id;
    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    
    public void ButtonSelectPlayer()
    {

        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }

        Scene sc = SceneManager.GetActiveScene();
        switch (sc.name)
        {
            case "WorldCup":
                if (lock_player.activeSelf == true)
                {
                    return;
                }
                if (WCController.instance.btn_select.gameObject.activeSelf == true &&
                    WCController.instance.btn_select.GetComponent<Image>().sprite.name == "btnCancel")
                {
                    return;
                }
                WCController.instance.txt_name_player.text = SelectTeam.instance.AI_Head[id - 1].name.Substring(2);
                WCController.instance.img_player.sprite = SelectTeam.instance.AI_Head[id - 1];
                WCController.instance.img_body.sprite = SelectTeam.instance.sp_Body[id - 1];
                WCController.instance.txt_select_below.enabled = false;
                WCController.instance.img_player.transform.parent.gameObject.SetActive(true);
                WCController.instance.btn_select.gameObject.SetActive(true);
                WCController.instance.btn_select.GetComponent<Image>().sprite = WCController.instance.spr_select;
                int indexStar4 = int.Parse(SelectTeam.instance.AI_Head[id - 1].name.Substring(0, 1));
                WCController.instance.img_star.sprite = SelectTeam.instance.star[indexStar4];

                PlayerPrefs.SetInt(StringUtils.value_team_player_wc, id);
                break;
            case "Menu":
                UpgradePlayer.instance.obj_buy_player = this.gameObject;
                Menu.instance.name_player.text = SelectTeam.instance.AI_Head[id - 1].name.Substring(2);
                Menu.instance.img_player.sprite = SelectTeam.instance.AI_Head[id - 1];
                int indexStar = int.Parse(SelectTeam.instance.AI_Head[id - 1].name.Substring(0, 1));
                Menu.instance.img_star.sprite = SelectTeam.instance.star[indexStar];
                Menu.instance.img_body.sprite = SelectTeam.instance.sp_Body[id - 1];

                UpgradePlayer.instance.img_speed.fillAmount = UpgradePlayer.instance.point_speed[id - 1] / 100f;
                UpgradePlayer.instance.img_shoot.fillAmount = UpgradePlayer.instance.point_shoot[id - 1] / 100f;
                UpgradePlayer.instance.img_jump.fillAmount = UpgradePlayer.instance.point_jump[id - 1] / 100f;

                UpgradePlayer.instance.txt_speed.text = "Speed: " + UpgradePlayer.instance.point_speed[id - 1] + "/100";
                UpgradePlayer.instance.txt_shoot.text = "Shoot: " + UpgradePlayer.instance.point_shoot[id - 1] + "/100";
                UpgradePlayer.instance.txt_jump.text = "Jump: " + UpgradePlayer.instance.point_jump[id - 1] + "/100";

                Menu.instance.txt_level_head.text = "Level: " + PlayerPrefs.GetInt(StringUtils.level_head + (id - 1), 1).ToString();
                Menu.instance.txt_power.text = "Power: " + (int)(PlayerPrefs.GetInt(StringUtils.str_point_speed + (id - 1))
                    + PlayerPrefs.GetInt(StringUtils.str_point_jump + (id - 1))
                    + PlayerPrefs.GetInt(StringUtils.str_point_shoot + (id - 1))) / 3 + "";
                if (lock_player.activeSelf == true)
                {
                    UpgradePlayer.instance.id_buy = id;
                    UpgradePlayer.instance.obj_coin.SetActive(true);
                    UpgradePlayer.instance.btn_upgrade.interactable = true;
                    UpgradePlayer.instance.btn_upgrade.GetComponent<Image>().sprite = UpgradePlayer.instance.spr_maxlevel;
                    UpgradePlayer.instance.txt_money_upgrade.text = UpgradePlayer.instance.price_buy_head[id -1].ToString();
                    UpgradePlayer.instance.btn_upgrade.GetComponent<Animator>().enabled = true;
                    return;
                }
                PlayerPrefs.SetInt(StringUtils.id_player, id);
                int lv_head = PlayerPrefs.GetInt(StringUtils.level_head + (id - 1), 1);
                if (lv_head >= 10)
                {
                    UpgradePlayer.instance.btn_upgrade.interactable = false;
                    UpgradePlayer.instance.btn_upgrade.GetComponent<Image>().sprite = UpgradePlayer.instance.spr_maxlevel;
                    UpgradePlayer.instance.txt_money_upgrade.text = "Max Level";
                    UpgradePlayer.instance.btn_upgrade.GetComponent<Animator>().enabled = false;
                }
                else
                {
                    UpgradePlayer.instance.btn_upgrade.interactable = true;
                    UpgradePlayer.instance.btn_upgrade.GetComponent<Image>().sprite = UpgradePlayer.instance.spr_upgrade;
                    UpgradePlayer.instance.txt_money_upgrade.text = UpgradePlayer.instance.arr_money_upgrade[lv_head - 1].ToString();
                    UpgradePlayer.instance.btn_upgrade.GetComponent<Animator>().enabled = true;
                }
                
                break;
            case "SetupStadium":
                if (lock_player.activeSelf == true)
                {
                    return;
                }
                if (SetupStatium.instance.btn_select.gameObject.activeSelf == true &&
                    SetupStatium.instance.btn_select.GetComponent<Image>().sprite.name == "btnCancel")
                {
                    return;
                }
                if (SetupStatium.instance.btn_select.gameObject.activeSelf == false && SetupStatium.instance.txt_waiting.enabled == true)
                {
                    SetupStatium.instance.btn_select.GetComponent<Animator>().enabled = true;
                    SetupStatium.instance.btn_select.gameObject.SetActive(true);
                    SetupStatium.instance.txt_waiting.enabled = false;
                    SetupStatium.instance.obj_player.SetActive(true);
                }
                SetupStatium.instance.txt_name_player.text = SelectTeam.instance.AI_Head[id - 1].name.Substring(2);
                SetupStatium.instance.img_head.sprite = SelectTeam.instance.AI_Head[id - 1];
                SetupStatium.instance.img_body.sprite = SelectTeam.instance.sp_Body[id - 1];
                int indexStar1 = int.Parse(SelectTeam.instance.AI_Head[id - 1].name.Substring(0, 1));
                SetupStatium.instance.img_star.sprite = SelectTeam.instance.star[indexStar1];
                if (Menu.mode == (int)Menu.MODE.SIRVIVAL || Menu.mode == (int)Menu.MODE.LEAGUE || Menu.mode == (int)Menu.MODE.DAILY_CHALLENGE)
                {
                    SetupStatium.id_head = id;
                }
                break;
            case "FriendMatch":
                if (lock_player.activeSelf == true)
                {
                    return;
                }
                FriendMatchController.id_head = id;

                if (FriendMatchController.instance.btn_select_player.activeSelf == false
                    && FriendMatchController.instance.txt_waiting_player.enabled == true)
                {
                    FriendMatchController.instance.btn_select_player.GetComponent<Animator>().enabled = true;
                    //FriendMatchController.instance.btn_select_player.GetComponent<Image>().sprite = FriendMatchController.instance.spr_select;
                    FriendMatchController.instance.btn_select_player.SetActive(true);
                    FriendMatchController.instance.txt_waiting_player.enabled = false;
                    FriendMatchController.instance.obj_player.SetActive(true);
                }
                if (FriendMatchController.instance.btn_select_com.activeSelf == false
                    && FriendMatchController.instance.txt_waiting_ai.enabled == true)
                {
                    FriendMatchController.instance.btn_select_com.GetComponent<Animator>().enabled = true;
                    FriendMatchController.instance.btn_select_com.SetActive(true);
                    //FriendMatchController.instance.btn_select_com.GetComponent<Image>().sprite = FriendMatchController.instance.spr_select;
                    FriendMatchController.instance.txt_waiting_ai.enabled = false;
                    FriendMatchController.instance.obj_com.SetActive(true);
                }

                if (FriendMatchController.instance.btn_select_player.GetComponent<Image>().sprite.name == "btnSelect"
                    && FriendMatchController.instance.btn_select_player.activeSelf)
                {
                    FriendMatchController.instance.img_head_player.sprite = SelectTeam.instance.AI_Head[id - 1];
                    FriendMatchController.instance.txt_name_player.text = SelectTeam.instance.AI_Head[id - 1].name.Substring(2);
                    int indexStar2 = int.Parse(SelectTeam.instance.AI_Head[id - 1].name.Substring(0, 1));
                    FriendMatchController.instance.img_star_player.sprite = SelectTeam.instance.star[indexStar2];
                    FriendMatchController.instance.img_body_player.sprite = SelectTeam.instance.sp_Body[id - 1];
                }
                else if (FriendMatchController.instance.btn_select_com.GetComponent<Image>().sprite.name == "btnSelect"
                    && FriendMatchController.instance.btn_select_com.activeSelf)
                {
                    FriendMatchController.instance.img_head_ai.sprite = SelectTeam.instance.AI_Head[id - 1];
                    FriendMatchController.instance.txt_name_ai.text = SelectTeam.instance.AI_Head[id - 1].name.Substring(2);
                    int indexStar3 = int.Parse(SelectTeam.instance.AI_Head[id - 1].name.Substring(0, 1));
                    FriendMatchController.instance.img_star_ai.sprite = SelectTeam.instance.star[indexStar3];
                    FriendMatchController.instance.img_body_ai.sprite = SelectTeam.instance.sp_Body[id - 1];
                }
                break;
        }

    }
}
