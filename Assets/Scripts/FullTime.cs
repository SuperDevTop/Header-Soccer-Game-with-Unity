using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class FullTime : MonoBehaviour
{
    public static FullTime instance;
    public Text text_resultGoal, text_AmoutMoneyMatch, text_TeamNameLeft, text_TeamNameRight, money;
    public Image img_Result, head_Left, head_Right, body_Left, body_Right, shoe_Left, shoe_Right;
    public Sprite[] sp_YouWin;
    public Sprite[] sp_YouDraw;
    public Sprite[] sp_YouLose;
    public int amountMoneyMatch;
    //public int dir_top;
    public int[,] ValueTeam = new int[4, 8];
    public int[] ScoreTeam = new int[46];
    public int[] random = new int[16];
    public int[,] randomPlayOff = new int[2, 8];
    public int[,] GoalsPlayOff = new int[2, 8];
    public int[,] ValueTeamPlayOff = new int[2, 8];

    public int[,] GoalsFirtOff = new int[2, 4];
    public int[,] ValueTeamFirtOff = new int[2, 4];

    public int[,] ValueTeamQuarterFinals = new int[2, 2];
    public int[,] GoalsQuarterFinals = new int[2, 2];
    public List<int> listWinR4 = new List<int>();
    public int isMatchStage;
    public GameObject GoalsLeft, GoalsRight, btnx2, obj_result;
    public SpriteRenderer stadium;
    public int[] indexPlayer = new int[46];
    public int teamPlayer;

    public GameObject obj_continue_survival, canloadvideo, obj_replay, cantloadvideo_x2;

    // Use this for initialization
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {

        if (PlayerPrefs.GetInt(GameConstants.MUSIC, 1) == 1)
        {
            SoundManager.Instance.musicMatch.Stop();
            SoundManager.Instance.musicBG.mute = true;
        }
        int showrate = PlayerPrefs.GetInt(StringUtils.str_show_rate, 0);
        showrate++;
        PlayerPrefs.SetInt(StringUtils.str_show_rate, showrate);
        if (PlayerPrefs.GetInt("rateOk", 0) == 0 && showrate % (3 + (int)(showrate / 3)) == 0)
        {
            menuRate.instance.panelRate.SetActive(true);
            obj_result.SetActive(false);
        }

        if (GameController.goalsConceded == 0)
        {
            int keep = PlayerPrefs.GetInt(StringUtils.str_keep_goal, 0);
            keep++;
            PlayerPrefs.SetInt(StringUtils.str_keep_goal, keep);
            if (keep == 1 && PlayerPrefs.GetInt(StringUtils.str_is_achievement + 21, 0) == 0)
            {
                PlayerPrefs.SetInt(StringUtils.str_is_achievement + 21, 1);
            }
            else if (keep == 5 && PlayerPrefs.GetInt(StringUtils.str_is_achievement + 22, 0) == 0)
            {
                PlayerPrefs.SetInt(StringUtils.str_is_achievement + 22, 1);
            }
            else if (keep == 10 && PlayerPrefs.GetInt(StringUtils.str_is_achievement + 23, 0) == 0)
            {
                PlayerPrefs.SetInt(StringUtils.str_is_achievement + 23, 1);
            }
            else if (keep == 15 && PlayerPrefs.GetInt(StringUtils.str_is_achievement + 24, 0) == 0)
            {
                PlayerPrefs.SetInt(StringUtils.str_is_achievement + 24, 1);
            }
        }


        if (GameController.goals >= 5)
        {
            if (PlayerPrefs.GetInt(StringUtils.str_is_achievement + 25, 0) == 0)
            {
                PlayerPrefs.SetInt(StringUtils.str_is_achievement + 25, 1);
            }
        }
        if (GameController.goals >= 10)
        {
            if (PlayerPrefs.GetInt(StringUtils.str_is_achievement + 26, 0) == 0)
            {
                PlayerPrefs.SetInt(StringUtils.str_is_achievement + 26, 1);
            }
        }
        if (GameController.goals >= 15)
        {
            if (PlayerPrefs.GetInt(StringUtils.str_is_achievement + 27, 0) == 0)
            {
                PlayerPrefs.SetInt(StringUtils.str_is_achievement + 27, 1);
            }
        }

        if (GameController.goals < GameController.goalsConceded)
        {

            if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
            {
                SoundManager.Instance.matchLost.Play();
            }
        }
        else if (GameController.goals == GameController.goalsConceded)
        {

            if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
            {
                SoundManager.Instance.matchWon.Play();
            }
        }
        else
        {

            if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
            {
                SoundManager.Instance.matchWon.Play();
            }
            int w_id = PlayerPrefs.GetInt(StringUtils.str_win_ai + (Loadding.teamAI - 1), 0);
            w_id++;
            PlayerPrefs.SetInt(StringUtils.str_win_ai + (Loadding.teamAI - 1), w_id);

            bool w_all = true;
            for (int i = 0; i < SelectTeam.instance.AI_Head.Length; i++)
            {
                int w_ai = PlayerPrefs.GetInt(StringUtils.str_win_ai + i, 0);
                if (w_ai < 1)
                {
                    w_all = false;
                    break;
                }
            }
            if (w_all == true && PlayerPrefs.GetInt(StringUtils.str_is_achievement + 34, 0) == 0)
            {
                PlayerPrefs.SetInt(StringUtils.str_is_achievement + 34, 1);
            }
            if (w_id == 1 && Loadding.teamAI == 29 && PlayerPrefs.GetInt(StringUtils.str_is_achievement + 28, 0) == 0)
            {
                PlayerPrefs.SetInt(StringUtils.str_is_achievement + 28, 1);
            }
            else if (w_id == 5 && Loadding.teamAI == 29 && PlayerPrefs.GetInt(StringUtils.str_is_achievement + 29, 0) == 0)
            {
                PlayerPrefs.SetInt(StringUtils.str_is_achievement + 29, 1);
            }
            else if (w_id == 1 && Loadding.teamAI == 30 && PlayerPrefs.GetInt(StringUtils.str_is_achievement + 30, 0) == 0)
            {
                PlayerPrefs.SetInt(StringUtils.str_is_achievement + 30, 1);
            }
            else if (w_id == 5 && Loadding.teamAI == 30 && PlayerPrefs.GetInt(StringUtils.str_is_achievement + 31, 0) == 0)
            {
                PlayerPrefs.SetInt(StringUtils.str_is_achievement + 31, 1);
            }
            else if (w_id == 1 && Loadding.teamAI == 45 && PlayerPrefs.GetInt(StringUtils.str_is_achievement + 32, 0) == 0)
            {
                PlayerPrefs.SetInt(StringUtils.str_is_achievement + 32, 1);
            }
            else if (w_id == 5 && Loadding.teamAI == 45 && PlayerPrefs.GetInt(StringUtils.str_is_achievement + 33, 0) == 0)
            {
                PlayerPrefs.SetInt(StringUtils.str_is_achievement + 33, 1);
            }
        }

        for (int i = 0; i < 46; i++)
        {
            indexPlayer[i] = PlayerPrefs.GetInt("index" + i, 0);
        }
        int index_Stadium = PlayerPrefs.GetInt("index_Stadium", 0);
        stadium.sprite = SelectTeam.instance.sp_Stadiums[index_Stadium];

        isMatchStage = PlayerPrefs.GetInt("isStage1", 1);
        if (Menu.mode == (int)Menu.MODE.WORLDCUP || Menu.mode == (int)Menu.MODE.DAILY_CHALLENGE)
        {
            obj_replay.SetActive(false);
        }
        else
        {
            if (Menu.mode != (int)Menu.MODE.LEAGUE)
                obj_replay.SetActive(true);
            else
            {
                if (GameController.goals > GameController.goalsConceded)
                {
                    obj_replay.SetActive(false);
                }
                else
                {
                    obj_replay.SetActive(true);
                }
            }
        }

        if (Menu.mode == (int)Menu.MODE.SIRVIVAL)
        {
            obj_result.SetActive(false);
            obj_continue_survival.SetActive(true);
        }
        if (Menu.mode == (int)Menu.MODE.WORLDCUP)
        {

            teamPlayer = PlayerPrefs.GetInt("wcPlayer");
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    GoalsPlayOff[i, j] = PlayerPrefs.GetInt("GoalsPlayOff" + "[" + i + "," + j + "]", 0);
                }

            }

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    ValueTeam[i, j] = PlayerPrefs.GetInt("valueTeam" + "[" + i + "," + j + "]");

                }
            }

            for (int i = 0; i < ScoreTeam.Length; i++)
            {

                ScoreTeam[i] = PlayerPrefs.GetInt("scoreTeam" + "[" + i + "]", 0);

            }
            SetupWinOrLoseWC();

        }

        if (Loadding.leftOrRight == 0)
        {
            text_resultGoal.text = GameController.goalsConceded + " - " + GameController.goals;

            if (Menu.mode == (int)Menu.MODE.WORLDCUP)
            {
                text_TeamNameLeft.text = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.value_team_ai_wc) - 1].name.Substring(2);
                head_Left.sprite = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.value_team_ai_wc) - 1];
                body_Left.sprite = SelectTeam.instance.sp_Body[PlayerPrefs.GetInt(StringUtils.value_team_ai_wc) - 1];

                text_TeamNameRight.text = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.value_team_player_wc) - 1].name.Substring(2);
                head_Right.sprite = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.value_team_player_wc) - 1];
                body_Right.sprite = SelectTeam.instance.sp_Body[PlayerPrefs.GetInt(StringUtils.value_team_player_wc) - 1];
            }
            else if (Menu.mode == (int)Menu.MODE.FRIENDMATCH)
            {
                text_TeamNameLeft.text = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.value_team_ai_friendmatch) - 1].name.Substring(2);
                head_Left.sprite = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.value_team_ai_friendmatch) - 1];
                body_Left.sprite = SelectTeam.instance.sp_Body[PlayerPrefs.GetInt(StringUtils.value_team_ai_friendmatch) - 1];

                text_TeamNameRight.text = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.id_player) - 1].name.Substring(2);
                head_Right.sprite = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.id_player) - 1];
                body_Right.sprite = SelectTeam.instance.sp_Body[PlayerPrefs.GetInt(StringUtils.id_player) - 1];
            }
            else if (Menu.mode == (int)Menu.MODE.LEAGUE)
            {
                text_TeamNameLeft.text = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.str_value_team_ai_league) - 1].name.Substring(2);
                head_Left.sprite = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.str_value_team_ai_league) - 1];
                body_Left.sprite = SelectTeam.instance.sp_Body[PlayerPrefs.GetInt(StringUtils.str_value_team_ai_league) - 1];

                text_TeamNameRight.text = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.id_player) - 1].name.Substring(2);
                head_Right.sprite = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.id_player) - 1];
                body_Right.sprite = SelectTeam.instance.sp_Body[PlayerPrefs.GetInt(StringUtils.id_player) - 1];
            }
            //else if (Menu.mode == (int)Menu.MODE.TRAINING)
            //{
            //    text_TeamNameLeft.text = SelectTeam.instance.nameTeam[PlayerPrefs.GetInt(StringUtils.value_team_ai_friendmatch) - 1];
            //    head_Left.sprite = SelectTeam.instance.flagTeam[PlayerPrefs.GetInt(StringUtils.value_team_ai_friendmatch) - 1];
            //    body_Left.sprite = SelectTeam.instance.sp_Body[PlayerPrefs.GetInt(StringUtils.value_team_ai_friendmatch) - 1];

            //    text_TeamNameRight.text = SelectTeam.instance.nameTeam[PlayerPrefs.GetInt(StringUtils.value_team_player_friendmatch) - 1];
            //    head_Right.sprite = SelectTeam.instance.flagTeam[PlayerPrefs.GetInt(StringUtils.value_team_player_friendmatch) - 1];
            //    body_Right.sprite = SelectTeam.instance.sp_Body[PlayerPrefs.GetInt(StringUtils.value_team_player_friendmatch) - 1];
            //}
            else if (Menu.mode == (int)Menu.MODE.SIRVIVAL)
            {
                text_TeamNameLeft.text = SelectTeam.instance.AI_Head
                    [SelectTeam.lst_id_ai[PlayerPrefs.GetInt(StringUtils.match_survival)] - 1].name.Substring(2);
                head_Left.sprite = SelectTeam.instance.AI_Head
                    [SelectTeam.lst_id_ai[PlayerPrefs.GetInt(StringUtils.match_survival)] - 1];
                body_Left.sprite = SelectTeam.instance.sp_Body[SelectTeam.lst_id_ai[PlayerPrefs.GetInt(StringUtils.match_survival)] - 1];

                text_TeamNameRight.text = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.id_player) - 1].name.Substring(2);
                head_Right.sprite = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.id_player) - 1];
                body_Right.sprite = SelectTeam.instance.sp_Body[PlayerPrefs.GetInt(StringUtils.id_player) - 1];
            }
            else if (Menu.mode == (int)Menu.MODE.DAILY_CHALLENGE)
            {
                text_TeamNameLeft.text = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.str_nunber_daily_challenge) - 2].name.Substring(2);
                head_Left.sprite = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.str_nunber_daily_challenge) - 2];
                body_Left.sprite = SelectTeam.instance.sp_Body[PlayerPrefs.GetInt(StringUtils.str_nunber_daily_challenge) - 2];

                text_TeamNameRight.text = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.id_player) - 1].name.Substring(2);
                head_Right.sprite = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.id_player) - 1];
                body_Right.sprite = SelectTeam.instance.sp_Body[PlayerPrefs.GetInt(StringUtils.id_player) - 1];
            }

        }
        else
        {
            text_resultGoal.text = GameController.goals + " - " + GameController.goalsConceded;

            if (Menu.mode == (int)Menu.MODE.WORLDCUP)
            {
                text_TeamNameLeft.text = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.value_team_player_wc) - 1].name.Substring(2);
                head_Left.sprite = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.value_team_player_wc) - 1];
                body_Left.sprite = SelectTeam.instance.sp_Body[PlayerPrefs.GetInt(StringUtils.value_team_player_wc) - 1];

                text_TeamNameRight.text = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.value_team_ai_wc) - 1].name.Substring(2);
                head_Right.sprite = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.value_team_ai_wc) - 1];
                body_Right.sprite = SelectTeam.instance.sp_Body[PlayerPrefs.GetInt(StringUtils.value_team_ai_wc) - 1];
            }
            else if (Menu.mode == (int)Menu.MODE.FRIENDMATCH)
            {
                text_TeamNameLeft.text = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.id_player) - 1].name.Substring(2);
                head_Left.sprite = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.id_player) - 1];
                body_Left.sprite = SelectTeam.instance.sp_Body[PlayerPrefs.GetInt(StringUtils.id_player) - 1];

                text_TeamNameRight.text = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.value_team_ai_friendmatch) - 1].name.Substring(2);
                head_Right.sprite = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.value_team_ai_friendmatch) - 1];
                body_Right.sprite = SelectTeam.instance.sp_Body[PlayerPrefs.GetInt(StringUtils.value_team_ai_friendmatch) - 1];
            }
            else if (Menu.mode == (int)Menu.MODE.LEAGUE)
            {
                text_TeamNameLeft.text = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.id_player) - 1].name.Substring(2);
                head_Left.sprite = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.id_player) - 1];
                body_Left.sprite = SelectTeam.instance.sp_Body[PlayerPrefs.GetInt(StringUtils.id_player) - 1];

                text_TeamNameRight.text = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.str_value_team_ai_league) - 1].name.Substring(2);
                head_Right.sprite = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.str_value_team_ai_league) - 1];
                body_Right.sprite = SelectTeam.instance.sp_Body[PlayerPrefs.GetInt(StringUtils.str_value_team_ai_league) - 1];
            }
            else if (Menu.mode == (int)Menu.MODE.TRAINING)
            {
                text_TeamNameLeft.text = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.value_team_player_friendmatch) - 1].name.Substring(2);
                head_Left.sprite = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.value_team_player_friendmatch) - 1];
                body_Right.sprite = SelectTeam.instance.sp_Body[PlayerPrefs.GetInt(StringUtils.value_team_player_friendmatch) - 1];

                text_TeamNameRight.text = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.value_team_ai_friendmatch) - 1].name.Substring(2);
                head_Right.sprite = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.value_team_ai_friendmatch) - 1];
                body_Right.sprite = SelectTeam.instance.sp_Body[PlayerPrefs.GetInt(StringUtils.value_team_ai_friendmatch) - 1];
            }
            else if (Menu.mode == (int)Menu.MODE.SIRVIVAL)
            {
                text_TeamNameLeft.text = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.id_player) - 1].name.Substring(2);
                head_Left.sprite = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.id_player) - 1];
                body_Left.sprite = SelectTeam.instance.sp_Body[PlayerPrefs.GetInt(StringUtils.id_player) - 1];

                text_TeamNameRight.text = SelectTeam.instance.AI_Head
                    [SelectTeam.lst_id_ai[PlayerPrefs.GetInt(StringUtils.match_survival)] - 1].name.Substring(2);
                head_Right.sprite = SelectTeam.instance.AI_Head
                    [SelectTeam.lst_id_ai[PlayerPrefs.GetInt(StringUtils.match_survival)] - 1];
                body_Right.sprite = SelectTeam.instance.sp_Body[SelectTeam.lst_id_ai[PlayerPrefs.GetInt(StringUtils.match_survival)] - 1];
            }
            else if (Menu.mode == (int)Menu.MODE.DAILY_CHALLENGE)
            {
                text_TeamNameLeft.text = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.id_player) - 1].name.Substring(2);
                head_Left.sprite = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.id_player) - 1];
                body_Left.sprite = SelectTeam.instance.sp_Body[PlayerPrefs.GetInt(StringUtils.id_player) - 1];

                text_TeamNameRight.text = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.str_nunber_daily_challenge) - 2].name.Substring(2);
                head_Right.sprite = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.str_nunber_daily_challenge) - 2];
                body_Right.sprite = SelectTeam.instance.sp_Body[PlayerPrefs.GetInt(StringUtils.str_nunber_daily_challenge) - 2];
            }
        }
        SetupScoreStage();

    }

    public void SetupWinOrLoseWC()
    {
        if (isMatchStage == 5)
        {
            ValueTeamPlayOff[0, 0] = ValueTeam[0, 0];
            ValueTeamPlayOff[1, 0] = ValueTeam[1, 1];

            ValueTeamPlayOff[0, 1] = ValueTeam[0, 2];
            ValueTeamPlayOff[1, 1] = ValueTeam[1, 3];

            ValueTeamPlayOff[0, 2] = ValueTeam[1, 0];
            ValueTeamPlayOff[1, 2] = ValueTeam[0, 1];

            ValueTeamPlayOff[0, 3] = ValueTeam[1, 2];
            ValueTeamPlayOff[1, 3] = ValueTeam[0, 3];

            ValueTeamPlayOff[0, 4] = ValueTeam[0, 4];
            ValueTeamPlayOff[1, 4] = ValueTeam[1, 5];

            ValueTeamPlayOff[0, 5] = ValueTeam[0, 6];
            ValueTeamPlayOff[1, 5] = ValueTeam[1, 7];

            ValueTeamPlayOff[0, 6] = ValueTeam[1, 4];
            ValueTeamPlayOff[1, 6] = ValueTeam[0, 5];

            ValueTeamPlayOff[0, 7] = ValueTeam[1, 6];
            ValueTeamPlayOff[1, 7] = ValueTeam[0, 7];


            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (ValueTeamPlayOff[i, j] == PlayerPrefs.GetInt("wcPlayer")
                       || ValueTeamPlayOff[i, j] == PlayerPrefs.GetInt("wcAI"))
                    {
                        if (ValueTeamPlayOff[0, j] == PlayerPrefs.GetInt("wcPlayer"))
                        {
                            GoalsPlayOff[0, j] = GameController.goals;
                            GoalsPlayOff[1, j] = GameController.goalsConceded;


                            PlayerPrefs.SetInt("GoalsPlayOff" + "[" + i + "," + j + "]", GoalsPlayOff[i, j]);
                        }
                        else if (ValueTeamPlayOff[1, j] == PlayerPrefs.GetInt("wcPlayer"))
                        {
                            GoalsPlayOff[1, j] = GameController.goals;
                            GoalsPlayOff[0, j] = GameController.goalsConceded;

                            PlayerPrefs.SetInt("GoalsPlayOff" + "[" + i + "," + j + "]", GoalsPlayOff[i, j]);
                        }
                    }
                }
            }
        }
        else if (isMatchStage == 6)
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    ValueTeamFirtOff[i, j] = PlayerPrefs.GetInt("ValueTeamFirtOff" + "[" + i + "," + j + "]", 1);
                }
            }
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (ValueTeamFirtOff[i, j] == PlayerPrefs.GetInt("wcPlayer") || ValueTeamFirtOff[i, j] == PlayerPrefs.GetInt("wcAI"))
                    {
                        if (ValueTeamFirtOff[0, j] == PlayerPrefs.GetInt("wcPlayer"))
                        {
                            GoalsFirtOff[0, j] = GameController.goals;
                            GoalsFirtOff[1, j] = GameController.goalsConceded;


                            PlayerPrefs.SetInt("GoalsFirtOff" + "[" + i + "," + j + "]", GoalsFirtOff[i, j]);
                        }
                        else if (ValueTeamFirtOff[1, j] == PlayerPrefs.GetInt("wcPlayer"))
                        {
                            GoalsFirtOff[1, j] = GameController.goals;
                            GoalsFirtOff[0, j] = GameController.goalsConceded;

                            PlayerPrefs.SetInt("GoalsFirtOff" + "[" + i + "," + j + "]", GoalsFirtOff[i, j]);
                        }
                    }
                }
            }
        }
        else if (isMatchStage == 7)
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    ValueTeamQuarterFinals[i, j] = PlayerPrefs.GetInt("ValueTeamQuarterFinals" + "[" + i + "," + j + "]", 1);
                }
            }
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (ValueTeamQuarterFinals[i, j] == PlayerPrefs.GetInt("wcPlayer") || ValueTeamQuarterFinals[i, j] == PlayerPrefs.GetInt("wcAI"))
                    {
                        if (ValueTeamQuarterFinals[0, j] == PlayerPrefs.GetInt("wcPlayer"))
                        {
                            GoalsQuarterFinals[0, j] = GameController.goals;
                            GoalsQuarterFinals[1, j] = GameController.goalsConceded;


                            PlayerPrefs.SetInt("GoalsQuarterFinals" + "[" + i + "," + j + "]", GoalsQuarterFinals[i, j]);
                        }
                        else if (ValueTeamQuarterFinals[1, j] == PlayerPrefs.GetInt("wcPlayer"))
                        {
                            GoalsQuarterFinals[1, j] = GameController.goals;
                            GoalsQuarterFinals[0, j] = GameController.goalsConceded;

                            PlayerPrefs.SetInt("GoalsQuarterFinals" + "[" + i + "," + j + "]", GoalsQuarterFinals[i, j]);
                        }
                    }
                }
            }
        }
        else if (isMatchStage == 8)
        {

            listWinR4.Add(PlayerPrefs.GetInt("listWinR40"));
            listWinR4.Add(PlayerPrefs.GetInt("listWinR41"));
            if (listWinR4[0] == PlayerPrefs.GetInt("wcPlayer"))
            {

                PlayerPrefs.SetInt("Number_Goals_CK0", GameController.goals);
                PlayerPrefs.SetInt("Number_Goals_CK1", GameController.goalsConceded);
            }
            else if (listWinR4[1] == PlayerPrefs.GetInt("wcPlayer"))
            {

                PlayerPrefs.SetInt("Number_Goals_CK0", GameController.goalsConceded);
                PlayerPrefs.SetInt("Number_Goals_CK1", GameController.goals);
            }
        }
    }




    public void SetupScoreStage()
    {

        if (GameController.goals > GameController.goalsConceded)
        {
            int win = PlayerPrefs.GetInt(StringUtils.win, 0);
            win++;
            PlayerPrefs.SetInt(StringUtils.win, win);
            if (win >= 1 && PlayerPrefs.GetInt(StringUtils.str_is_achievement + 5, 0) == 0)
            {
                PlayerPrefs.SetInt(StringUtils.str_is_achievement + 5, 1);
            }
            if (win >= 10 && PlayerPrefs.GetInt(StringUtils.str_is_achievement + 6, 0) == 0)
            {
                PlayerPrefs.SetInt(StringUtils.str_is_achievement + 6, 1);
            }
            if (win >= 50 && PlayerPrefs.GetInt(StringUtils.str_is_achievement + 7, 0) == 0)
            {
                PlayerPrefs.SetInt(StringUtils.str_is_achievement + 7, 1);
            }
            if (win >= 100 && PlayerPrefs.GetInt(StringUtils.str_is_achievement + 8, 0) == 0)
            {
                PlayerPrefs.SetInt(StringUtils.str_is_achievement + 8, 1);
            }


            int kn = PlayerPrefs.GetInt(StringUtils.kn_user, 0);
            kn += Random.Range(3, 6);
            PlayerPrefs.SetInt(StringUtils.kn_user, kn);

            if (Menu.mode == (int)Menu.MODE.DAILY_CHALLENGE)
            {
                amountMoneyMatch = 2 * DailyChallenge.reward_daily_challenge;
            }
            else
            {
                amountMoneyMatch = (Loadding.teamAI - Loadding.teamPlayer) * 30 + 300;

                if (amountMoneyMatch <= 300)
                {
                    amountMoneyMatch = 300;
                }
            }
            text_AmoutMoneyMatch.text = amountMoneyMatch.ToString();
            if (Menu.mode == (int)Menu.MODE.LEAGUE && LeagueController.mode_league == PlayerPrefs.GetInt(StringUtils.str_mode_league, 0))
            {
                if (LeagueController.number_level_league == PlayerPrefs.GetInt(StringUtils.str_level_open + LeagueController.mode_league, 1))
                {
                    int lv = PlayerPrefs.GetInt(StringUtils.str_level_open + LeagueController.mode_league, 1);
                    if (lv == 20)
                    {
                        lv++;
                        PlayerPrefs.SetInt(StringUtils.str_level_open + LeagueController.mode_league, lv);
                        int opmode = PlayerPrefs.GetInt(StringUtils.str_mode_league, 0);
                        opmode++;
                        PlayerPrefs.SetInt(StringUtils.str_mode_league, opmode);
                        if (opmode == 1 && PlayerPrefs.GetInt(StringUtils.str_is_achievement + 12, 0) == 0)
                        {
                            PlayerPrefs.SetInt(StringUtils.str_is_achievement + 12, 1);
                        }
                        else if (opmode == 2 && PlayerPrefs.GetInt(StringUtils.str_is_achievement + 13, 0) == 0)
                        {
                            PlayerPrefs.SetInt(StringUtils.str_is_achievement + 13, 1);
                        }
                        else if (opmode == 3 && PlayerPrefs.GetInt(StringUtils.str_is_achievement + 14, 0) == 0)
                        {
                            PlayerPrefs.SetInt(StringUtils.str_is_achievement + 14, 1);
                        }
                        else if (opmode == 4 && PlayerPrefs.GetInt(StringUtils.str_is_achievement + 15, 0) == 0)
                        {
                            PlayerPrefs.SetInt(StringUtils.str_is_achievement + 15, 1);
                        }
                        else if (opmode == 5 && PlayerPrefs.GetInt(StringUtils.str_is_achievement + 16, 0) == 0)
                        {
                            PlayerPrefs.SetInt(StringUtils.str_is_achievement + 16, 1);
                        }
                    }
                    else if (lv < 20)
                    {
                        lv++;
                        PlayerPrefs.SetInt(StringUtils.str_level_open + LeagueController.mode_league, lv);
                    }

                }

            }

            if (Menu.mode == (int)Menu.MODE.WORLDCUP && isMatchStage <= 4)
            {
                ScoreTeam[PlayerPrefs.GetInt("wcPlayer") - 1] += 3;
                PlayerPrefs.SetInt("scoreTeam" + "[" + (PlayerPrefs.GetInt("wcPlayer") - 1) + "]", ScoreTeam[PlayerPrefs.GetInt("wcPlayer") - 1]);
                ScoreTeam[PlayerPrefs.GetInt("wcAI") - 1] -= 3;
                PlayerPrefs.SetInt("scoreTeam" + "[" + (PlayerPrefs.GetInt("wcAI") - 1) + "]", ScoreTeam[PlayerPrefs.GetInt("wcAI") - 1]);
            }
            if (Menu.mode == (int)Menu.MODE.WORLDCUP && isMatchStage == 8)
            {

                PlayerPrefs.SetInt("champion", 1);
                if (PlayerPrefs.GetInt(StringUtils.str_is_achievement + 17, 0) == 0)
                {
                    PlayerPrefs.SetInt(StringUtils.str_is_achievement + 17, 1);
                }
            }
            int _rd = Random.Range(0, sp_YouWin.Length);
            img_Result.sprite = sp_YouWin[_rd];

            if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
            {
                SoundManager.Instance.matchWon.Play();
            }
        }
        else if (GameController.goals == GameController.goalsConceded)
        {
            int kn = PlayerPrefs.GetInt(StringUtils.kn_user, 0);
            kn += Random.Range(1, 2);
            PlayerPrefs.SetInt(StringUtils.kn_user, kn);
            int dr = PlayerPrefs.GetInt(StringUtils.draw, 0);
            dr++;
            PlayerPrefs.SetInt(StringUtils.draw, dr);

            if (Menu.mode == (int)Menu.MODE.DAILY_CHALLENGE)
            {
                amountMoneyMatch = DailyChallenge.reward_daily_challenge;
            }
            else
            {
                amountMoneyMatch = 150;
            }
            text_AmoutMoneyMatch.text = amountMoneyMatch.ToString();

            if (Menu.mode == (int)Menu.MODE.WORLDCUP && isMatchStage <= 4)
            {
                ScoreTeam[PlayerPrefs.GetInt("wcAI") - 1] -= 2;
                PlayerPrefs.SetInt("scoreTeam" + "[" + (PlayerPrefs.GetInt("wcAI") - 1) + "]", ScoreTeam[PlayerPrefs.GetInt("wcAI") - 1]);

                ScoreTeam[PlayerPrefs.GetInt("wcPlayer") - 1] += 1;
                PlayerPrefs.SetInt("scoreTeam" + "[" + (PlayerPrefs.GetInt("wcPlayer") - 1) + "]", ScoreTeam[PlayerPrefs.GetInt("wcPlayer") - 1]);
            }


            int _rd = Random.Range(0, sp_YouDraw.Length);
            img_Result.sprite = sp_YouDraw[_rd];

            if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
            {
                SoundManager.Instance.matchWon.Play();
            }

        }
        else
        {
            int ls = PlayerPrefs.GetInt(StringUtils.lose, 0);
            ls++;
            PlayerPrefs.SetInt(StringUtils.lose, ls);
            if (Menu.mode == (int)Menu.MODE.WORLDCUP && isMatchStage >= 5 && isMatchStage <= 8)
            {
                PlayerPrefs.SetInt("WinOrLose", 1);
            }
            if (Menu.mode != (int)Menu.MODE.SIRVIVAL)
            {
                btnx2.SetActive(false);
                amountMoneyMatch = 0;
            }

            text_AmoutMoneyMatch.text = amountMoneyMatch.ToString();
            int _rd = Random.Range(0, sp_YouLose.Length);
            img_Result.sprite = sp_YouLose[_rd];

            if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
            {
                SoundManager.Instance.matchLost.Play();
            }
        }
        int m1 = PlayerPrefs.GetInt(StringUtils.str_money);
        m1 += amountMoneyMatch;
        PlayerPrefs.SetInt(StringUtils.str_money, m1);

        if (PlayerPrefs.GetInt(StringUtils.kn_user, 0)
            >= SelectTeam.instance.kn_level_user[PlayerPrefs.GetInt(StringUtils.level_user, 1) - 1])
        {
            int lv = PlayerPrefs.GetInt(StringUtils.level_user, 1);
            int abc = (PlayerPrefs.GetInt(StringUtils.kn_user, 0) - SelectTeam.instance.kn_level_user[lv - 1]);
            PlayerPrefs.SetInt(StringUtils.kn_user, abc);
            lv++;
            PlayerPrefs.SetInt(StringUtils.level_user, lv);


        }
    }

    public void ListSortScore()
    {

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = j + 1; k < 4; k++)
                {

                    if (ScoreTeam[ValueTeam[j, i] - 1] < ScoreTeam[ValueTeam[k, i] - 1])
                    {
                        int temp2 = ValueTeam[j, i];
                        ValueTeam[j, i] = ValueTeam[k, i];
                        ValueTeam[k, i] = temp2;
                    }
                }
            }

        }
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                PlayerPrefs.SetInt("valueTeam" + "[" + i + "," + j + "]", ValueTeam[i, j]);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        money.text = PlayerPrefs.GetInt(StringUtils.str_money).ToString();
        if (menuRate.instance.panelRate.activeSelf == true)
        {
            money.transform.parent.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            money.transform.parent.GetChild(0).gameObject.SetActive(true);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene("Game");
        }
        if (Menu.mode == (int)Menu.MODE.WORLDCUP)
        {
            if (PlayerPrefs.GetInt("isStage1") <= 4)
            {
                ListSortScore();
                if (PlayerPrefs.GetInt("isStage1") == 4)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (ValueTeam[i, j] == PlayerPrefs.GetInt("wcPlayer") && i > 1)
                            {
                                PlayerPrefs.SetInt("WinOrLose", 1);
                            }

                        }
                    }

                }
            }

        }
    }


    public void ButtonHome()
    {

        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        AdsManager.mode_show_ful_ads = "Home";
        AdsManager.Instance.showFullAds();
    }
    public void ButtonReplay()
    {

        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        AdsManager.mode_show_ful_ads = "Replay";
        AdsManager.Instance.showFullAds();
    }
    public void ButtonContinious()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }

        AdsManager.mode_show_ful_ads = "Next";
        AdsManager.Instance.showFullAds();

    }
    public void ButtonYesContinueSurvival()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        AdsManager.mode_show_ful_reward = "ContinueSurvival";
        AdsManager.Instance.showRewardAdsVideo();

    }

    public void ButtonX2Money()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        AdsManager.mode_show_ful_reward = "X2Money";
        AdsManager.Instance.showRewardAdsVideo();

    }
    public void ButtonNoContinueSurvival()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }

        btnx2.SetActive(true);
        amountMoneyMatch = (PlayerPrefs.GetInt(StringUtils.match_survival) + 1) * 50 + 100;
        text_AmoutMoneyMatch.text = amountMoneyMatch.ToString();
        int m1 = PlayerPrefs.GetInt(StringUtils.str_money);
        m1 += amountMoneyMatch;
        PlayerPrefs.SetInt(StringUtils.str_money, m1);
        PlayerPrefs.SetInt(StringUtils.life_player_survival, 3);
        int valuePlayer = PlayerPrefs.GetInt(StringUtils.id_player, 1);
        SelectTeam.lst_id_ai.Remove(valuePlayer);
        PlayerPrefs.SetInt("teamAI", SelectTeam.lst_id_ai[0]);
        obj_continue_survival.SetActive(false);
        obj_result.SetActive(true);
        PlayerPrefs.SetInt(StringUtils.match_survival, 0);
        StringUtils.item_x2_survival = 3;
        StringUtils.item_ice_survival = 3;
    }
}
