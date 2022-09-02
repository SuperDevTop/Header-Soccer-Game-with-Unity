using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loadding : MonoBehaviour
{
    public static int leftOrRight, topPlayer;
    public Text teamLeft, teamRight;
    public static int teamPlayer, teamAI, rotation_z;
    public Image img_HeadLeft, img_HeadRight, img_BodyLeft, img_BodyRight, img_ShoeLeft, img_ShoeRight;
    public GameObject img_BallRotation;
    public Text txt_stage;
    // Use this for initialization
    void Start()
    {

        leftOrRight = Random.Range(0, 2);
        int isMatchStage = PlayerPrefs.GetInt("isStage1", 1);

        if (Menu.mode == (int)Menu.MODE.WORLDCUP)
        {
            teamAI = PlayerPrefs.GetInt(StringUtils.value_team_ai_wc);
            teamPlayer = PlayerPrefs.GetInt(StringUtils.value_team_player_wc);
            PlayerPrefs.SetInt(StringUtils.id_player, teamPlayer);
        }
        else if (Menu.mode == (int)Menu.MODE.FRIENDMATCH)
        {
            teamAI = PlayerPrefs.GetInt(StringUtils.value_team_ai_friendmatch);
        }
        else if (Menu.mode == (int)Menu.MODE.LEAGUE)
        {
            teamAI = PlayerPrefs.GetInt(StringUtils.str_value_team_ai_league);
        }
        else if (Menu.mode == (int)Menu.MODE.TRAINING)
        {
            teamAI = PlayerPrefs.GetInt(StringUtils.value_team_ai_friendmatch);
            teamPlayer = PlayerPrefs.GetInt(StringUtils.value_team_player_friendmatch);
            PlayerPrefs.SetInt(StringUtils.id_player, teamPlayer);
        }
        else if (Menu.mode == (int)Menu.MODE.SIRVIVAL)
        {
            teamAI = SelectTeam.lst_id_ai[PlayerPrefs.GetInt(StringUtils.match_survival)];
            teamPlayer = PlayerPrefs.GetInt(StringUtils.value_team_player_survival);
            txt_stage.enabled = true;
            txt_stage.text = "Stage:  " + (PlayerPrefs.GetInt(StringUtils.match_survival) + 1).ToString();
        }
        else if (Menu.mode == (int)Menu.MODE.DAILY_CHALLENGE)
        {
            teamAI = PlayerPrefs.GetInt(StringUtils.str_nunber_daily_challenge, 1) - 1;
        }

        teamPlayer = PlayerPrefs.GetInt(StringUtils.id_player);
        Debug.Log("adskfjahdsfjkhasdfkj" + teamPlayer);
        img_ShoeLeft.sprite = SelectTeam.instance.sp_shoe[Random.Range(0, 3)];
        img_ShoeRight.sprite = SelectTeam.instance.sp_shoe[Random.Range(0, 3)];
        if (leftOrRight == 0)
        {

            if (Menu.mode == (int)Menu.MODE.WORLDCUP)
            {
                teamLeft.text = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.value_team_ai_wc) - 1].name.Substring(2);
                img_HeadLeft.sprite = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.value_team_ai_wc) - 1];
                img_BodyLeft.sprite = SelectTeam.instance.sp_Body[PlayerPrefs.GetInt(StringUtils.value_team_ai_wc) - 1];

                teamRight.text = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.value_team_player_wc) - 1].name.Substring(2);
                img_HeadRight.sprite = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.value_team_player_wc) - 1];
                img_BodyRight.sprite = SelectTeam.instance.sp_Body[PlayerPrefs.GetInt(StringUtils.value_team_player_wc) - 1];
            }
            else
            {
                teamLeft.text = SelectTeam.instance.AI_Head[teamAI - 1].name.Substring(2); ;
                img_HeadLeft.sprite = SelectTeam.instance.AI_Head[teamAI - 1];
                img_BodyLeft.sprite = SelectTeam.instance.sp_Body[teamAI - 1];

                teamRight.text = SelectTeam.instance.AI_Head[teamPlayer - 1].name.Substring(2);
                img_HeadRight.sprite = SelectTeam.instance.AI_Head[teamPlayer - 1];
                img_BodyRight.sprite = SelectTeam.instance.sp_Body[teamPlayer - 1];
            }

        }
        else
        {
            if (Menu.mode == (int)Menu.MODE.WORLDCUP)
            {
                teamRight.text = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.value_team_ai_wc) - 1].name.Substring(2);
                img_HeadRight.sprite = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.value_team_ai_wc) - 1];
                img_BodyRight.sprite = SelectTeam.instance.sp_Body[PlayerPrefs.GetInt(StringUtils.value_team_ai_wc) - 1];

                teamLeft.text = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.value_team_player_wc) - 1].name.Substring(2);
                img_HeadLeft.sprite = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.value_team_player_wc) - 1];
                img_BodyLeft.sprite = SelectTeam.instance.sp_Body[PlayerPrefs.GetInt(StringUtils.value_team_player_wc) - 1];
            }
            else
            {
                teamRight.text = SelectTeam.instance.AI_Head[teamAI - 1].name.Substring(2);
                img_HeadRight.sprite = SelectTeam.instance.AI_Head[teamAI - 1];
                img_BodyRight.sprite = SelectTeam.instance.sp_Body[teamAI - 1];

                teamLeft.text = SelectTeam.instance.AI_Head[teamPlayer - 1].name.Substring(2);
                img_HeadLeft.sprite = SelectTeam.instance.AI_Head[teamPlayer - 1];
                img_BodyLeft.sprite = SelectTeam.instance.sp_Body[teamPlayer - 1];
            }
        }
        StartCoroutine(waitLoadAsync());

    }

    // Update is called once per frame
    void Update()
    {
        rotation_z -= 5;
        img_BallRotation.transform.localRotation = Quaternion.Euler(0, 0, rotation_z);

    }
    IEnumerator LoadAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        while (!operation.isDone)
        {
            float progres = Mathf.Clamp01(operation.progress);
            yield return null;
        }
    }

    IEnumerator waitLoadAsync()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(LoadAsync());
    }
}
