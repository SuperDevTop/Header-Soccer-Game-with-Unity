using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class SetupStatium : MonoBehaviour
{
	public static SetupStatium instance;

	public int teamAI, indexBall, amountMoney, _IDPlayer;
	public Text textMoney, txt_gems;
	public int index_Stadium;
	public GameObject usedBall, usedPlayer, usedStadium;
	public GameObject[] listBalls, listStadiums;
	public GameObject[] listBtnBuyBall, listBtnSelectBall, listBtnBuyStadium, listBtnSelectStadium;
	public GameObject panelShop;
	//public Image[] img_Head, img_Ball, img_Stadium;
	public GameObject obj_select_player, obj_select_ball, obj_start;
	public Image img_head, img_body, img_shoe, img_star;
	public Text txt_name_player;
	public Image btn_select;
	public static int id_head;
	public Sprite spr_select, spr_cancel;
	public Text txt_waiting;
	public GameObject obj_player, obj_btn_back;

	// Use this for initialization
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			DestroyImmediate(gameObject);
		}
	}

	void Start()
	{

		if (Menu.mode == (int)Menu.MODE.WORLDCUP)
		{
			teamAI = PlayerPrefs.GetInt(StringUtils.value_team_ai_wc);
			obj_select_player.SetActive(false);
			obj_select_ball.SetActive(true);
		}
		else if (Menu.mode == (int)Menu.MODE.FRIENDMATCH)
		{
			teamAI = PlayerPrefs.GetInt(StringUtils.value_team_ai_friendmatch);
			obj_select_player.SetActive(false);
			obj_select_ball.SetActive(true);
		}
		else if (Menu.mode == (int)Menu.MODE.SIRVIVAL)
		{
			teamAI = SelectTeam.lst_id_ai[PlayerPrefs.GetInt(StringUtils.match_survival)];
		}
		else if (Menu.mode == (int)Menu.MODE.DAILY_CHALLENGE)
		{
			teamAI = PlayerPrefs.GetInt(StringUtils.str_nunber_daily_challenge, 1);
			obj_btn_back.SetActive(false);
		}
		//amountMoney = PlayerPrefs.GetInt("money");
		StartCoroutine(SetupWaiting(txt_waiting));

	}

	// Update is called once per frame
	void Update()
	{
		for (int i = 0; i < listBtnSelectStadium.Length; i++)
		{

			if (i == PlayerPrefs.GetInt(StringUtils.id_stadium))
			{
				listBtnSelectStadium[i].SetActive(false);
				usedStadium.transform.position = listStadiums[PlayerPrefs.GetInt(StringUtils.id_stadium, 0)].transform.position;
			}
			else
			{
				listBtnSelectStadium[i].SetActive(true);
			}
		}

		for (int i = 0; i < listBtnSelectBall.Length; i++)
		{

			if (i == PlayerPrefs.GetInt(StringUtils.id_ball))
			{
				listBtnSelectBall[i].SetActive(false);
				usedBall.transform.position = listBalls[PlayerPrefs.GetInt(StringUtils.id_ball, 0)].transform.position;
			}
			else
			{
				listBtnSelectBall[i].SetActive(true);
			}
		}
	}

	IEnumerator SetupWaiting(Text txt)
	{
		while (txt.enabled == true)
		{
			yield return new WaitForSeconds(0.35f);
			txt.text = "Select below.";
			yield return new WaitForSeconds(0.35f);
			txt.text = "Select below..";
			yield return new WaitForSeconds(0.35f);
			txt.text = "Select below...";
		}
	}

	public void ButtonSelectPlayer()
	{
		if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
		{
			SoundManager.Instance.buttonClick.Play();
		}
		if (btn_select.sprite.name == "btnSelect")
		{
			PlayerPrefs.SetInt(StringUtils.id_player, id_head);
			btn_select.sprite = spr_cancel;
			btn_select.color = Color.white;
			obj_start.SetActive(true);
			btn_select.GetComponent<Animator>().enabled = false;

		}
		else
		{
			obj_start.SetActive(false);
			obj_player.SetActive(false);
			txt_name_player.text = "Select Player";
			txt_waiting.enabled = true;
			StartCoroutine(SetupWaiting(txt_waiting));
			btn_select.sprite = spr_select;
			btn_select.gameObject.SetActive(false);

		}
	}

	public void SelectBall(int _indexBall)
	{
		if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
		{
			SoundManager.Instance.buttonClick.Play();
		}

		PlayerPrefs.SetInt(StringUtils.id_ball, _indexBall);
	}

	public void ButtonNextSelectPlayer()
	{
		if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
		{
			SoundManager.Instance.buttonClick.Play();
		}
		if (Menu.mode == (int)Menu.MODE.SIRVIVAL)
		{
			obj_select_player.SetActive(false);
			obj_select_ball.SetActive(true);
			PlayerPrefs.SetInt(StringUtils.life_player_survival, 3);
			int valuePlayer = PlayerPrefs.GetInt(StringUtils.id_player, 1);
			SelectTeam.lst_id_ai.Remove(valuePlayer);
			PlayerPrefs.SetInt(StringUtils.match_survival, 0);
			PlayerPrefs.SetInt("teamAI", SelectTeam.lst_id_ai[0]);
			StringUtils.item_x2_survival = 3;
			StringUtils.item_ice_survival = 3;
		}
		else
		{
			obj_select_player.SetActive(false);
			obj_select_ball.SetActive(true);
			if (Menu.mode == (int)Menu.MODE.DAILY_CHALLENGE)
			{
				obj_btn_back.SetActive(true);
			}
		}


	}

	public void ButtonAddMoney(int _id)
	{

		if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
		{
			SoundManager.Instance.buttonClick.Play();
		}
		panelShop.SetActive(true);


	}

	public void ButtonExitShop()
	{

		if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
		{
			SoundManager.Instance.buttonClick.Play();
		}
		panelShop.SetActive(false);
	}


	public void ButtonStart()
	{
		if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
		{
			SoundManager.Instance.buttonClick.Play();
		}

        if (Menu.mode == (int)Menu.MODE.WORLDCUP)
        {
           /* FirebaseAnalyticManager.instance.AnalyticsWC();*/

        }
        else if (Menu.mode == (int)Menu.MODE.FRIENDMATCH)
        {
           /* FirebaseAnalyticManager.instance.AnalyticsFriendmatch();*/
        }
		else if (Menu.mode == (int)Menu.MODE.DAILY_CHALLENGE)
		{
			int dl = PlayerPrefs.GetInt(StringUtils.str_nunber_daily_challenge, 1);
			/*FirebaseAnalyticManager.instance.AnalyticsDailyChallenge(dl);*/
		}
		else if (Menu.mode == (int)Menu.MODE.LEAGUE)
        {
            int lv = LeagueController.number_level_league;
            switch (LeagueController.mode_league)
            {
                /*case 0:
                    FirebaseAnalyticManager.instance.AnalyticsLeague("AMATEURLEAGUE", lv);
                    break;
                case 1:
                    FirebaseAnalyticManager.instance.AnalyticsLeague("SEMIPROLEAGUE", lv);
                    break;
                case 2:
                    FirebaseAnalyticManager.instance.AnalyticsLeague("PROFESSIONALLEAGUE", lv);
                    break;
                case 3:
                    FirebaseAnalyticManager.instance.AnalyticsLeague("SUPERLEAGUE", lv);
                    break;
                case 4:
                    FirebaseAnalyticManager.instance.AnalyticsLeague("LEGENDARYLEAGUE", lv);
                    break;*/
            }

        }
        else if (Menu.mode == (int)Menu.MODE.SIRVIVAL)
        {
            /*FirebaseAnalyticManager.instance.AnalyticsSurvival(PlayerPrefs.GetInt(StringUtils.match_survival) + 1);*/

        }
        SceneManager.LoadScene("Loading");
	}
	public void ButtonHome()
	{
		if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
		{
			SoundManager.Instance.buttonClick.Play();
		}
		if (panelShop.activeSelf)
		{
			panelShop.SetActive(false);
		}
		else
		{
			SceneManager.LoadScene("Menu");

		}
	}



	public void ButtonSelectStadium(int _indexStadium)
	{
		if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
		{
			SoundManager.Instance.buttonClick.Play();
		}

		PlayerPrefs.SetInt(StringUtils.id_stadium, _indexStadium);
	}



	public void ButtonBack()
	{
		if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
		{
			SoundManager.Instance.buttonClick.Play();
		}
		if (panelShop.activeSelf)
		{
			panelShop.SetActive(false);
		}
		else
		{
			if (obj_select_player.activeSelf)
			{
				if (Menu.mode == (int)Menu.MODE.TRAINING)
				{
					SceneManager.LoadScene("Menu");
				}
				else if (Menu.mode == (int)Menu.MODE.WORLDCUP)
				{
					SceneManager.LoadScene("WorldCup");
				}
				else if (Menu.mode == (int)Menu.MODE.LEAGUE)
				{
					SceneManager.LoadScene("League");
				}
				else if (Menu.mode == (int)Menu.MODE.FRIENDMATCH)
				{
					SceneManager.LoadScene("FriendMatch");
				}
				else if (Menu.mode == (int)Menu.MODE.SIRVIVAL)
				{
					SceneManager.LoadScene("Menu");
				}

			}
			else
			{
				if (Menu.mode == (int)Menu.MODE.FRIENDMATCH)
				{
					SceneManager.LoadScene("FriendMatch");
				}
				else if (Menu.mode == (int)Menu.MODE.WORLDCUP)
				{
					SceneManager.LoadScene("WorldCup");
				}
				else
				{
					obj_select_player.SetActive(true);
					obj_select_ball.SetActive(false);
					if (Menu.mode == (int)Menu.MODE.DAILY_CHALLENGE)
					{
						obj_btn_back.SetActive(false);
					}
				}
			}
		}

	}

}
