using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetHead_Shoe : MonoBehaviour
{
    public static GetHead_Shoe instance;
    public GameObject shoePlayer, flagPlayer;
    public GameObject shoeAI, flagAI;
    public Text nameTeamPlayer, nameTeamAI;
    public SpriteRenderer headPlayer, headAI, bodyPlayer, bodyAI;
    public int[] indexPlayer = new int[46];
    public int teamPlayer, teamAI, randomShoePlayer;
    public Text txt_nameTeam_left_survival, txt_nameTeam_right_survival;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Use this for initialization
    void Start()
    {
        int id = PlayerPrefs.GetInt(StringUtils.id_player, 1);

        if (Menu.mode == (int)Menu.MODE.WORLDCUP)
        {
            teamAI = PlayerPrefs.GetInt(StringUtils.value_team_ai_wc);
            teamPlayer = PlayerPrefs.GetInt(StringUtils.value_team_player_wc);
        }
        else if (Menu.mode == (int)Menu.MODE.FRIENDMATCH)
        {
            teamAI = PlayerPrefs.GetInt(StringUtils.value_team_ai_friendmatch);
            teamPlayer = PlayerPrefs.GetInt(StringUtils.id_player);
        }
        else if (Menu.mode == (int)Menu.MODE.LEAGUE)
        {
            teamAI = PlayerPrefs.GetInt(StringUtils.str_value_team_ai_league);
            teamPlayer = PlayerPrefs.GetInt(StringUtils.id_player);
        }
        else if (Menu.mode == (int)Menu.MODE.SIRVIVAL)
        {
            teamAI = SelectTeam.lst_id_ai[PlayerPrefs.GetInt(StringUtils.match_survival)];
            teamPlayer = PlayerPrefs.GetInt(StringUtils.id_player);
        }
        else if (Menu.mode == (int)Menu.MODE.DAILY_CHALLENGE)
        {
            teamAI = PlayerPrefs.GetInt(StringUtils.str_nunber_daily_challenge, 1) - 1;
            teamPlayer = PlayerPrefs.GetInt(StringUtils.id_player);
        }

        randomShoePlayer = Random.Range(0, 3);
        int randomShoeAI = Random.Range(0, 3);
        shoePlayer.GetComponent<SpriteRenderer>().sprite = SelectTeam.instance.sp_shoe[randomShoePlayer];
        if (randomShoeAI == randomShoePlayer)
        {
            if (randomShoeAI == 0)
            {
                shoeAI.GetComponent<SpriteRenderer>().sprite = SelectTeam.instance.sp_shoe[randomShoeAI + 1];
            }
            else
            {
                shoeAI.GetComponent<SpriteRenderer>().sprite = SelectTeam.instance.sp_shoe[randomShoeAI - 1];
            }
        }
        else
        {
            shoeAI.GetComponent<SpriteRenderer>().sprite = SelectTeam.instance.sp_shoe[randomShoeAI];
        }

        GetUI();

        headPlayer.sprite = SelectTeam.instance.AI_Head[id - 1];
        bodyPlayer.sprite = SelectTeam.instance.sp_Body[id - 1];
        headAI.sprite = SelectTeam.instance.AI_Head[teamAI - 1];
        bodyAI.sprite = SelectTeam.instance.sp_Body[teamAI - 1];
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void GetUI()
    {
        int valuePlayer = 1;
        int valueAI = 1;
        if (Menu.mode == (int)Menu.MODE.WORLDCUP)
        {
            valuePlayer = PlayerPrefs.GetInt(StringUtils.value_team_player_wc, 1);
            valueAI = PlayerPrefs.GetInt(StringUtils.value_team_ai_wc, 1);

        }
        else if (Menu.mode == (int)Menu.MODE.FRIENDMATCH)
        {
            valuePlayer = PlayerPrefs.GetInt(StringUtils.id_player, 1);
            valueAI = PlayerPrefs.GetInt(StringUtils.value_team_ai_friendmatch, 1);

        }
        else if (Menu.mode == (int)Menu.MODE.LEAGUE)
        {
            valuePlayer = PlayerPrefs.GetInt(StringUtils.id_player, 1);
            valueAI = PlayerPrefs.GetInt(StringUtils.str_value_team_ai_league, 1);

        }
        else if (Menu.mode == (int)Menu.MODE.TRAINING)
        {
            valuePlayer = PlayerPrefs.GetInt(StringUtils.value_team_player_friendmatch, 1);
            valueAI = PlayerPrefs.GetInt(StringUtils.value_team_ai_friendmatch, 1);

        }
        else if (Menu.mode == (int)Menu.MODE.SIRVIVAL)
        {
            valueAI = SelectTeam.lst_id_ai[PlayerPrefs.GetInt(StringUtils.match_survival)];
            valuePlayer = PlayerPrefs.GetInt(StringUtils.id_player);
        }
        else if (Menu.mode == (int)Menu.MODE.DAILY_CHALLENGE)
        {
            valueAI = PlayerPrefs.GetInt(StringUtils.str_nunber_daily_challenge, 1) - 1;
            valuePlayer = PlayerPrefs.GetInt(StringUtils.id_player);
        }
        if (Loadding.leftOrRight == 0)
        {
            if (Menu.mode == (int)Menu.MODE.SIRVIVAL)
            {
                txt_nameTeam_left_survival.text = SelectTeam.instance.AI_Head[valueAI - 1].name.Substring(2);
                txt_nameTeam_right_survival.text = SelectTeam.instance.AI_Head[valuePlayer - 1].name.Substring(2);
            }
            else
            {
                nameTeamAI.text = SelectTeam.instance.AI_Head[valueAI - 1].name.Substring(2);
                nameTeamPlayer.text = SelectTeam.instance.AI_Head[valuePlayer - 1].name.Substring(2);
            }
        }
        else
        {
            if (Menu.mode == (int)Menu.MODE.SIRVIVAL)
            {
                txt_nameTeam_right_survival.text = SelectTeam.instance.AI_Head[valueAI - 1].name.Substring(2);
                txt_nameTeam_left_survival.text = SelectTeam.instance.AI_Head[valuePlayer - 1].name.Substring(2);
            }
            else
            {
                nameTeamAI.text = SelectTeam.instance.AI_Head[valuePlayer - 1].name.Substring(2);
                nameTeamPlayer.text = SelectTeam.instance.AI_Head[valueAI - 1].name.Substring(2);
            }
        }
    }

}
