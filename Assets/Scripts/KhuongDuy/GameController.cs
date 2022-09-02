using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

	public float timeKeepBall;
	public bool start_game, isMoveBoss;
	public int isMatchStage;
	public int[,] ValueTeam = new int[4, 8];
	public int[] ScoreTeam = new int[32];
	public int[] random = new int[16];
	public int[,] randomPlayOff = new int[2, 8];
	public int[,] GoalsPlayOff = new int[2, 8];
	public int[,] ValueTeamPlayOff = new int[2, 8];

	public int[,] GoalsFirtOff = new int[2, 4];
	public int[,] ValueTeamFirtOff = new int[2, 4];

	public int[,] ValueTeamQuarterFinals = new int[2, 2];
	public int[,] GoalsQuarterFinals = new int[2, 2];
	public int disMatch;
	public Text text_GoalsLeft, text_GoalsRight;
	public static int goals, goalsConceded;

	public static GameController Instance { get; private set; }
	public Button btnSetting;
	public GameObject
		targetLeft,
		targetRight,
		ball,
		playerComputer,
		player;

	[Space]
	public Character2DController playerController;
	public AIPlayer aiController;

	[Space]
	[Header("Starting position of the ball")]
	[Space]
	public Vector3 posCenter;

	public bool Scored { get; private set; }
	public bool EndMatch { get; private set; }

	public float matchTime;
	public GameObject panelGoldGoal;
	public bool isShowPanelGoldGoal;
	public GameObject img_timeKeepBall;
	public Sprite[] time123;

	public GameObject img_whistle;
	public GameObject[] obsPlay;
	public GameObject[] targetGoal;
	public GameObject stadium, panelSaveMe, panelShop, btn_watchvideo, cantLoadAds;
	public static int showAdsFullTime = 1;
	public List<Image> img_life_player = new List<Image>();
	public GameObject obj_stage_survival, obj_time;
	public Text txt_stage;
	public static int int_SaveMe;
	public List<int> lst_team_random = new List<int>();
	public GameObject item;

	private void Update()
	{

		if (timeKeepBall > 4)
		{
			//timeKeepBall = 3;

			img_timeKeepBall.SetActive(false);

		}
		else
		{
			timeKeepBall += Time.deltaTime;
		}

		if (timeKeepBall >= 0)
		{
			img_timeKeepBall.GetComponent<SpriteRenderer>().sprite = time123[0];
		}
		if (timeKeepBall >= 1)
		{
			img_timeKeepBall.GetComponent<SpriteRenderer>().sprite = time123[1];
		}
		if (timeKeepBall >= 2)
		{
			img_timeKeepBall.GetComponent<SpriteRenderer>().sprite = time123[2];
		}
		if (timeKeepBall >= 3)
		{
			img_timeKeepBall.GetComponent<SpriteRenderer>().sprite = time123[3];

		}

	}
	void Awake()
	{

		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance != null)
		{
			Destroy(this.gameObject);
		}
		int number = PlayerPrefs.GetInt(StringUtils.str_number_training, 0);
		isMatchStage = PlayerPrefs.GetInt("isStage1", 1);
		if (Menu.mode == (int)Menu.MODE.FRIENDMATCH && FriendMatchController.mode_friendmatch == "HARD")
		{
			CreatObs();
		}
		else if (Menu.mode == (int)Menu.MODE.WORLDCUP && WCController.str_difficul_wc == "HARD")
		{
			CreatObs();
		}
		else
		{
			GameObject _targetGoals = Instantiate(targetGoal[0], stadium.transform.position, Quaternion.identity) as GameObject;
			_targetGoals.transform.parent = stadium.transform;
			_targetGoals.transform.localScale = new Vector3(1f, 1f, 1);
		}
	}

	void Start()
	{
		int_SaveMe = 1;
		img_whistle.SetActive(false);
		isShowPanelGoldGoal = false;
		panelGoldGoal.SetActive(false);
		start_game = false;
		isMoveBoss = false;
		if (Time.timeScale == 0)
		{
			Time.timeScale = 1;
		}
		if (Menu.mode == (int)Menu.MODE.WORLDCUP)
		{
			//FirebaseAnalyticManager.instance.AnalyticsWC();
			isMatchStage = PlayerPrefs.GetInt("isStage1", 1);
			isMatchStage++;
			PlayerPrefs.SetInt("isStage1", isMatchStage);
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
			if (int_SaveMe == 1)
			{
				SetupWinOrLoseWC();
			}
		}
		else if (Menu.mode == (int)Menu.MODE.FRIENDMATCH)
		{
			//FirebaseAnalyticManager.instance.AnalyticsFriendmatch();
		}
		else if (Menu.mode == (int)Menu.MODE.LEAGUE)
		{
			int lv = LeagueController.number_level_league;
			//switch (LeagueController.mode_league)
			//{
			//    case 0:
			//        FirebaseAnalyticManager.instance.AnalyticsLeague("AMATEURLEAGUE",lv);
			//        break;
			//    case 1:
			//        FirebaseAnalyticManager.instance.AnalyticsLeague("SEMIPROLEAGUE", lv);
			//        break;
			//    case 2:
			//        FirebaseAnalyticManager.instance.AnalyticsLeague("PROFESSIONALLEAGUE", lv);
			//        break;
			//    case 3:
			//        FirebaseAnalyticManager.instance.AnalyticsLeague("SUPERLEAGUE", lv);
			//        break;
			//    case 4:
			//        FirebaseAnalyticManager.instance.AnalyticsLeague("LEGENDARYLEAGUE", lv);
			//        break;
			//}

		}
		else if (Menu.mode == (int)Menu.MODE.SIRVIVAL)
		{
            //FirebaseAnalyticManager.instance.AnalyticsSurvival(PlayerPrefs.GetInt(StringUtils.match_survival) + 1);
            AdsManager.mode_show_ful_ads = "Survival";
            AdsManager.Instance.showFullAds();
            obj_stage_survival.SetActive(true);
			obj_time.SetActive(false);
			txt_stage.text = (PlayerPrefs.GetInt(StringUtils.match_survival) + 1).ToString();
			for (int i = 0; i < img_life_player.Count; i++)
			{
				if (i >= PlayerPrefs.GetInt(StringUtils.life_player_survival, 0))
				{
					img_life_player[i].color = new Color(0, 0, 0, 0.6f);
				}
			}
		}

		disMatch = PlayerPrefs.GetInt("disMatch", 0);
		ball.SetActive(false);
		player.SetActive(false);
		playerComputer.SetActive(false);
		btnSetting.gameObject.SetActive(false);


		StartGame();
		StartCoroutine(waitStartGame());
		StartCoroutine(SetPlayerTrue());

	}

	IEnumerator waitStartGame()
	{
		if (StringUtils.item_ice_survival <= 0)
		{
			item.transform.GetChild(1).gameObject.SetActive(false);
		}
		if (StringUtils.item_x2_survival <= 0)
		{
			item.transform.GetChild(0).gameObject.SetActive(false);
		}
		yield return new WaitForSeconds(4f);
		start_game = true;
		yield return new WaitForSeconds(Random.Range(1f, 2f));
		isMoveBoss = true;
		while (true)
		{
			yield return new WaitForSeconds(0.08f);

			item.transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount += 0.02f;
			item.transform.GetChild(1).GetChild(0).GetComponent<Image>().fillAmount += 0.02f;

			if (item.transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount >= 1
				&& item.transform.GetChild(1).GetChild(0).GetComponent<Image>().fillAmount >= 1)
			{
				item.transform.GetChild(0).GetComponent<Image>().color = Color.white;
				item.transform.GetChild(1).GetComponent<Image>().color = Color.white;
				item.transform.GetChild(0).GetComponent<Animator>().enabled = true;
				item.transform.GetChild(1).GetComponent<Animator>().enabled = true;
				break;
			}
		}

		item.SetActive(true);
	}

	public void SetupPositionPlayer()
	{
		if (Loadding.leftOrRight == 0)
		{
			player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
			playerComputer.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

			if (Character2DController.instance.isIce == false)
				player.transform.position = new Vector2(7.5f, -2.5f);
			if (AIPlayer.instance.isIce == false)
				playerComputer.transform.position = new Vector2(-7.5f, -2.5f);

		}
		else
		{
			if (Character2DController.instance.isIce == false)
				player.transform.position = new Vector2(-7.5f, -2.5f);
			player.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
			if (AIPlayer.instance.isIce == false)
				playerComputer.transform.position = new Vector2(7.5f, -2.5f);
			playerComputer.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));

		}
	}

	public void StartGame()
	{
		Reset();
		goals = 0;
		goalsConceded = 0;
		if (Loadding.leftOrRight == 0)
		{
			text_GoalsRight.text = goals.ToString();
			text_GoalsLeft.text = goalsConceded.ToString();
		}
		else
		{
			text_GoalsRight.text = goalsConceded.ToString();
			text_GoalsLeft.text = goals.ToString();
		}
	}

	public void ButtonExitSaveMe()
	{

		if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
		{
			SoundManager.Instance.buttonClick.Play();
		}
		SceneManager.LoadScene("FullTime");
	}

	public void ButtonExitShop()
	{
		panelShop.SetActive(false);
		if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
		{
			SoundManager.Instance.buttonClick.Play();
		}
	}


	public void ButtonWatchAds()
	{
		if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
		{
			SoundManager.Instance.buttonClick.Play();
		}
		Time.timeScale = 1;

        //SelectTeam.numberReward = 8;
        AdsManager.Instance.showRewardAdsVideo();
    }

	private void Reset()
	{

		SetupPositionPlayer();
		EndMatch = Scored = false;
		timeKeepBall = 0f;
		goals = 0;
		goalsConceded = 0;

		ball.transform.position = posCenter;
		ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

	}
	IEnumerator waitLoadFullTime()
	{
		if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
		{
			SoundManager.Instance.refereeEndGame.Play();
		}
		img_whistle.SetActive(true);
		yield return new WaitForSeconds(2f);
		img_whistle.SetActive(false);
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene("FullTime");

	}
	IEnumerator SetPlayerTrue()
	{
		player.SetActive(true);
		playerComputer.SetActive(true);
		yield return new WaitForSeconds(3f);
		if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
		{
			SoundManager.Instance.refereeStartGame.Play();
		}
		yield return new WaitForSeconds(1f);

		ball.SetActive(true);
		btnSetting.gameObject.SetActive(true);

		StartCoroutine("RunMatchTime");
	}

	public void CreatObs()
	{

		int _rd1 = Random.Range(0, 8);
		int _rd2 = Random.Range(0, 2);

		if (_rd2 == 0)
		{
			GameObject _targetGoals = Instantiate(targetGoal[0], stadium.transform.position, Quaternion.identity) as GameObject;
			_targetGoals.transform.parent = stadium.transform;
			_targetGoals.transform.localScale = new Vector3(1f, 1f, 1);
		}
		else
		{
			if (Loadding.leftOrRight == 0)
			{
				GameObject _targetGoals = Instantiate(targetGoal[1], stadium.transform.position, Quaternion.identity) as GameObject;
				_targetGoals.transform.parent = stadium.transform;
				_targetGoals.transform.localScale = new Vector3(1f, 1f, 1);
			}
			else
			{
				GameObject _targetGoals = Instantiate(targetGoal[2], stadium.transform.position, Quaternion.identity) as GameObject;
				_targetGoals.transform.parent = stadium.transform;
				_targetGoals.transform.localScale = new Vector3(1f, 1f, 1);
			}
		}

		if (_rd1 < obsPlay.Length)
		{

			Instantiate(obsPlay[_rd1], new Vector3(0, 0.5f, 0), Quaternion.identity);
		}

	}
	public void ButtonYesGoldGoal()
	{
		Time.timeScale = 1;

		timeKeepBall = 0;
		img_timeKeepBall.SetActive(true);
		panelGoldGoal.SetActive(false);
		//textGoldGoal.SetActive(true);
		isShowPanelGoldGoal = true;
		StartCoroutine(WaitGoalGold());
	}
	IEnumerator WaitGoalGold()
	{
		yield return new WaitForSeconds(3f);
		if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
		{
			SoundManager.Instance.refereeStartGame.Play();
		}
		yield return new WaitForSeconds(1.0f);
		EndMatch = false;
		img_timeKeepBall.SetActive(false);
		ball.SetActive(true);
		player.SetActive(true);
		playerComputer.SetActive(true);
		ball.transform.position = posCenter;
		ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
		SetupPositionPlayer();
	}

	IEnumerator WaitRelive()
	{
		if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
		{
			SoundManager.Instance.refereeEndGame.Play();
		}
		img_whistle.SetActive(true);
		yield return new WaitForSeconds(2f);
		img_whistle.SetActive(false);
		yield return new WaitForSeconds(1f);
		panelSaveMe.SetActive(true);
		ball.SetActive(false);
		player.SetActive(false);
		playerComputer.SetActive(false);
		//Time.timeScale = 0;

	}
	public void GoldGoal()
	{
		ball.SetActive(false);
		player.SetActive(false);
		playerComputer.SetActive(false);
		panelGoldGoal.SetActive(true);
		AIPlayer.instance.shadow.transform.localScale = new Vector2(0, 0);
		Character2DController.instance.shadow.transform.localScale = new Vector2(0, 0);
		Time.timeScale = 0;
	}
	private IEnumerator RunMatchTime()
	{
		int lv_user = PlayerPrefs.GetInt(StringUtils.level_user, 1);
		var time = matchTime;
		while (true)
		{
			yield return new WaitForSeconds(1.0f);
			if (time > 0)
			{

				if (Menu.mode != (int)Menu.MODE.SIRVIVAL)
				{
					time--;
					AIPlayer.time = (int)time;
					if (time % 3 == 0)
					{
						if (time < 80)
						{
							if (Loadding.teamAI >= 40)
							{
								AIPlayer.instance.angle_shot = Random.Range(45, 60);
								AIPlayer.instance.rangeOfDefense = Random.Range(12f, 16f);
								AIPlayer.instance.checkHead.localPosition = new Vector2(-Random.Range(0.2f, 0.7f), 3);
							}
							else if (Loadding.teamAI >= 30)
							{

								AIPlayer.instance.angle_shot = Random.Range(35, 65);
								AIPlayer.instance.rangeOfDefense = Random.Range(9f, 14f);
								AIPlayer.instance.checkHead.localPosition = new Vector2(-Random.Range(0.15f, 0.86f), 3);
							}
							else if (Loadding.teamAI >= 20)
							{

								AIPlayer.instance.angle_shot = Random.Range(30, 65);
								AIPlayer.instance.rangeOfDefense = Random.Range(9f, 14f);
								AIPlayer.instance.checkHead.localPosition = new Vector2(-Random.Range(0f, 0.86f), 3);
							}
							else if (Loadding.teamAI >= 10)
							{
								if (lv_user <= 2)
								{
									AIPlayer.instance.angle_shot = Random.Range(30, 45);
									AIPlayer.instance.rangeOfDefense = Random.Range(8f, 13f);
									AIPlayer.instance.checkHead.localPosition = new Vector2(-Random.Range(-0.2f, 0.7f), 3);
								}
								else
								{
									AIPlayer.instance.angle_shot = Random.Range(30, 70);
									AIPlayer.instance.rangeOfDefense = Random.Range(8f, 13f);
									AIPlayer.instance.checkHead.localPosition = new Vector2(-Random.Range(-0.2f, 0.7f), 3);
								}

							}
							else if (Loadding.teamAI >= 1)
							{
								if (lv_user <= 2)
								{
									AIPlayer.instance.angle_shot = Random.Range(25, 35);
									AIPlayer.instance.rangeOfDefense = Random.Range(7f, 12f);
									AIPlayer.instance.checkHead.localPosition = new Vector2(-Random.Range(-0.3f, 0.6f), 3);
								}
								else
								{
									AIPlayer.instance.angle_shot = Random.Range(30, 75);
									AIPlayer.instance.rangeOfDefense = Random.Range(7f, 12f);
									AIPlayer.instance.checkHead.localPosition = new Vector2(-Random.Range(-0.3f, 0.6f), 3);
								}

							}
						}
						else
						{
							AIPlayer.instance.angle_shot = Random.Range(10, 15);
							AIPlayer.instance.rangeOfDefense = Random.Range(7f, 12f);
							AIPlayer.instance.checkHead.localPosition = new Vector2(-Random.Range(-0.3f, 0.6f), 3);
						}
					}
				}
				else
				{
					AIPlayer.time++;
					if (time % 3 == 0)
					{
						if (AIPlayer.time >= 10)
						{
							if (Loadding.teamAI >= 40)
							{
								AIPlayer.instance.angle_shot = Random.Range(45, 60);
								AIPlayer.instance.rangeOfDefense = Random.Range(12f, 16f);
								AIPlayer.instance.checkHead.localPosition = new Vector2(-Random.Range(0.2f, 0.7f), 3);
							}
							else if (Loadding.teamAI >= 30)
							{

								AIPlayer.instance.angle_shot = Random.Range(35, 65);
								AIPlayer.instance.rangeOfDefense = Random.Range(9f, 14f);
								AIPlayer.instance.checkHead.localPosition = new Vector2(-Random.Range(0.15f, 0.86f), 3);
							}
							else if (Loadding.teamAI >= 20)
							{

								AIPlayer.instance.angle_shot = Random.Range(30, 65);
								AIPlayer.instance.rangeOfDefense = Random.Range(9f, 14f);
								AIPlayer.instance.checkHead.localPosition = new Vector2(-Random.Range(0f, 0.86f), 3);
							}
							else if (Loadding.teamAI >= 10)
							{

								AIPlayer.instance.angle_shot = Random.Range(30, 70);
								AIPlayer.instance.rangeOfDefense = Random.Range(8f, 13f);
								AIPlayer.instance.checkHead.localPosition = new Vector2(-Random.Range(-0.2f, 0.7f), 3);
							}
							else if (Loadding.teamAI >= 1)
							{

								if (lv_user <= 2)
								{
									AIPlayer.instance.angle_shot = Random.Range(25, 35);
									AIPlayer.instance.rangeOfDefense = Random.Range(7f, 12f);
									AIPlayer.instance.checkHead.localPosition = new Vector2(-Random.Range(-0.3f, 0.6f), 3);
								}
								else
								{
									AIPlayer.instance.angle_shot = Random.Range(30, 75);
									AIPlayer.instance.rangeOfDefense = Random.Range(7f, 12f);
									AIPlayer.instance.checkHead.localPosition = new Vector2(-Random.Range(-0.3f, 0.6f), 3);
								}
							}
						}
						else
						{
							AIPlayer.instance.angle_shot = Random.Range(10, 15);
							AIPlayer.instance.rangeOfDefense = Random.Range(7f, 12f);
							AIPlayer.instance.checkHead.localPosition = new Vector2(-Random.Range(-0.3f, 0.6f), 3);
						}
					}
				}


			}

			if (time == 0)
			{
				UIManager.Instance.timeText.text = time + "";

				if (goals > goalsConceded)
				{

					StartCoroutine(waitLoadFullTime());
					break;
				}
				else if (goals < goalsConceded)
				{
					if (int_SaveMe > 0 && (Menu.mode == (int)Menu.MODE.WORLDCUP))
					{
						if (Menu.mode == (int)Menu.MODE.WORLDCUP)
						{
							if (isMatchStage > 4)
							{
								StartCoroutine(WaitRelive());
							}
							else
							{
								StartCoroutine(waitLoadFullTime());
							}
						}
						else
						{
							StartCoroutine(WaitRelive());
						}

						break;
					}
					else
					{
						StartCoroutine(waitLoadFullTime());
						break;
					}

				}
				else
				{

					if (Menu.mode == (int)Menu.MODE.WORLDCUP)
					{
						if (isMatchStage <= 4)
						{

							StartCoroutine(waitLoadFullTime());
							break;
						}
						else
						{
							if (isShowPanelGoldGoal == false)
							{
								if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
								{
									SoundManager.Instance.refereeStartGame.Play();
								}
								GoldGoal();
							}
							if (goals != goalsConceded)
							{
								ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
								StartCoroutine(waitLoadFullTime());
								break;
							}
						}
					}
					else
					{

						StartCoroutine(waitLoadFullTime());
						break;

					}
				}
			}
			if (Menu.mode == (int)Menu.MODE.SIRVIVAL)
			{
				UIManager.Instance.timeText.text = "---";
				if (goals >= 1)
				{
					//int win = PlayerPrefs.GetInt(StringUtils.win, 0);
					//win++;
					//PlayerPrefs.SetInt(StringUtils.win, win);

					int kn = PlayerPrefs.GetInt(StringUtils.kn_user, 0);
					kn += Random.Range(1, 3);
					PlayerPrefs.SetInt(StringUtils.kn_user, kn);

					if (PlayerPrefs.GetInt(StringUtils.kn_user, 0)
						>= SelectTeam.instance.kn_level_user[PlayerPrefs.GetInt(StringUtils.level_user, 1) - 1])
					{
						int lv = PlayerPrefs.GetInt(StringUtils.level_user, 1);
						int abc = (PlayerPrefs.GetInt(StringUtils.kn_user, 0) - SelectTeam.instance.kn_level_user[lv - 1]);
						PlayerPrefs.SetInt(StringUtils.kn_user, abc);
						lv++;
						PlayerPrefs.SetInt(StringUtils.level_user, lv);


					}

					Debug.Log("knnnnn" + PlayerPrefs.GetInt(StringUtils.kn_user, 0));
					StartCoroutine(NextSurvival());
					break;
				}
				else if (PlayerPrefs.GetInt(StringUtils.life_player_survival, 0) <= 0)
				{
					StartCoroutine(waitLoadFullTime());
					break;
				}
			}
			else
			{
				UIManager.Instance.timeText.text = time + "";
			}
		}

		if (goals < goalsConceded)
		{
			aiController.anim.SetBool(aiController.CelebrateHash, true);
			AIPlayer.instance.anim.SetBool(AIPlayer.instance.headShoot, false);

		}
		else if (goals == goalsConceded)
		{
			playerController.Anim.SetBool(playerController.CelebrateHash, true);
			aiController.anim.SetBool(aiController.CelebrateHash, true);
			AIPlayer.instance.anim.SetBool(AIPlayer.instance.headShoot, false);

		}
		else
		{
			playerController.Anim.SetBool(playerController.CelebrateHash, true);
		}

		EndMatch = true;
	}
	IEnumerator NextSurvival()
	{
		if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
		{
			SoundManager.Instance.refereeEndGame.Play();
		}
		btnSetting.gameObject.SetActive(false);
		img_whistle.SetActive(true);
		yield return new WaitForSeconds(2f);
		img_whistle.SetActive(false);
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene("Loading");

	}
	public void ScoredAgainst(bool netOfAIPlayer)
	{

		if (!EndMatch)
		{
			Scored = true;
			if (netOfAIPlayer)
			{
				goals++;
				playerController.Anim.SetBool(playerController.CelebrateHash, true);
				if (Menu.mode == (int)Menu.MODE.SIRVIVAL)
				{
					int _mat = PlayerPrefs.GetInt(StringUtils.match_survival);
					_mat++;
					PlayerPrefs.SetInt(StringUtils.match_survival, _mat);
					if (_mat == 5 && PlayerPrefs.GetInt(StringUtils.str_is_achievement + 18, 0) == 0)
					{
						PlayerPrefs.SetInt(StringUtils.str_is_achievement + 18, 1);
					}
					else if (_mat == 10 && PlayerPrefs.GetInt(StringUtils.str_is_achievement + 19, 0) == 0)
					{
						PlayerPrefs.SetInt(StringUtils.str_is_achievement + 19, 1);
					}
					else if (_mat == 25 && PlayerPrefs.GetInt(StringUtils.str_is_achievement + 20, 0) == 0)
					{
						PlayerPrefs.SetInt(StringUtils.str_is_achievement + 20, 1);
					}
				}


				int goal = PlayerPrefs.GetInt(StringUtils.str_goal, 0);
				goal++;
				PlayerPrefs.SetInt(StringUtils.str_goal, goal);
				if (goal == 10 && PlayerPrefs.GetInt(StringUtils.str_is_achievement + 0, 0) == 0)
				{
					PlayerPrefs.SetInt(StringUtils.str_is_achievement + 0, 1);
				}
				else if (goal == 20 && PlayerPrefs.GetInt(StringUtils.str_is_achievement + 1, 0) == 0)
				{
					PlayerPrefs.SetInt(StringUtils.str_is_achievement + 1, 1);
				}
				else if (goal == 50 && PlayerPrefs.GetInt(StringUtils.str_is_achievement + 2, 0) == 0)
				{
					PlayerPrefs.SetInt(StringUtils.str_is_achievement + 2, 1);
				}
				else if (goal == 100 && PlayerPrefs.GetInt(StringUtils.str_is_achievement + 3, 0) == 0)
				{
					PlayerPrefs.SetInt(StringUtils.str_is_achievement + 3, 1);
				}
				else if (goal == 500 && PlayerPrefs.GetInt(StringUtils.str_is_achievement + 4, 0) == 0)
				{
					PlayerPrefs.SetInt(StringUtils.str_is_achievement + 4, 1);
				}



			}
			else
			{
				goalsConceded++;
				aiController.anim.SetBool(aiController.CelebrateHash, true);
				int life = PlayerPrefs.GetInt(StringUtils.life_player_survival, 0);

				if (life > 0)
				{
					life--;
					PlayerPrefs.SetInt(StringUtils.life_player_survival, life);

					for (int i = 0; i < img_life_player.Count; i++)
					{
						if (i >= life)
						{
							img_life_player[i].color = new Color(0, 0, 0, 0.6f);
						}
					}
				}

			}

			if (Loadding.leftOrRight == 0)
			{
				text_GoalsRight.text = goals.ToString();
				text_GoalsLeft.text = goalsConceded.ToString();
			}
			else
			{
				text_GoalsRight.text = goalsConceded.ToString();
				text_GoalsLeft.text = goals.ToString();
			}
			StartCoroutine(ContinueMatch(netOfAIPlayer));
		}
	}


	private IEnumerator ContinueMatch(bool netOfAIPlayer)
	{
		AIPlayer.instance._velocity = 0;
		yield return new WaitForSeconds(1.5f);

		playerController.Anim.SetBool(playerController.CelebrateHash, false);
		aiController.anim.SetBool(aiController.CelebrateHash, false);
		//if (Character2DController.instance.isIce == true)
		//{
		//    Character2DController.instance.isIce = false;
		//    Character2DController.instance.Anim.enabled = true;
		//    Character2DController.instance.eff_ice_block.SetActive(false);

		//    if (Character2DController.instance.eff_explo_ice != null)
		//    {
		//        Character2DController.instance.eff_explo_ice.transform.SetParent(transform.parent);
		//        Character2DController.instance.eff_explo_ice.SetActive(true);
		//    }
		//}

		//if (AIPlayer.instance.isIce == true)
		//{
		//    AIPlayer.instance.isIce = false;
		//    AIPlayer.instance.anim.enabled = true;
		//    AIPlayer.instance.eff_ice_block.SetActive(false);

		//    if (AIPlayer.instance.eff_explo_ice != null)
		//    {
		//        AIPlayer.instance.eff_explo_ice.SetActive(true);
		//        AIPlayer.instance.eff_explo_ice.transform.SetParent(transform.parent);
		//    }
		//}
		yield return new WaitForSeconds(1.5f);
		if (!EndMatch)
		{
			SetupPositionPlayer();
			timeKeepBall = 0;

			ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

			if (netOfAIPlayer)
			{
				if (Loadding.leftOrRight == 0)
				{
					ball.transform.position = posCenter;
					ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-50 - 1.5f * matchTime, 50));
				}
				else
				{
					ball.transform.position = posCenter;
					ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(50 + 1.5f * matchTime, 50));
				}
			}
			else
			{
				if (Loadding.leftOrRight == 0)
				{
					ball.transform.position = posCenter;
					ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(50 + 1.5f * matchTime, 50));

				}
				else
				{
					ball.transform.position = posCenter;
					ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-50 - 1.5f * matchTime, 50));
				}
			}

			Scored = false;

			if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
			{
				SoundManager.Instance.refereeStartGame.Play();
			}
			yield return new WaitForSeconds(0.5f);
			AIPlayer.instance._velocity = 1f * AIPlayer.instance.a_velocity;

		}
	}

	public void RestartMatch()
	{
		Reset();
		playerController.Anim.SetBool(playerController.CelebrateHash, false);
		aiController.anim.SetBool(aiController.CelebrateHash, false);
		StartCoroutine("RunMatchTime");
	}

	public void Surrender()
	{
		StopCoroutine("RunMatchTime");
		goals = 0;
		goalsConceded = 3;
		if (Menu.mode == (int)Menu.MODE.SIRVIVAL)
		{
			PlayerPrefs.SetInt(StringUtils.life_player_survival, 0);
			for (int i = 0; i < img_life_player.Count; i++)
			{
				img_life_player[i].color = new Color(0, 0, 0, 0.6f);
			}
		}

		if (Loadding.leftOrRight == 0)
		{
			text_GoalsRight.text = goals.ToString();
			text_GoalsLeft.text = goalsConceded.ToString();
		}
		else
		{
			text_GoalsRight.text = goalsConceded.ToString();
			text_GoalsLeft.text = goals.ToString();
		}

		aiController.anim.SetBool(aiController.CelebrateHash, true);
		AIPlayer.instance.anim.SetBool(AIPlayer.instance.headShoot, false);


		Scored = false;
		EndMatch = true;
		img_whistle.SetActive(true);
		StartCoroutine(waitLoadFullTime());
	}

	private void OnApplicationPause(bool pause)
	{
		if (Menu.mode == (int)Menu.MODE.WORLDCUP)
		{
			disMatch = 1;
			PlayerPrefs.SetInt("disMatch", disMatch);
		}

	}
	private void OnApplicationFocus(bool focus)
	{
		if (Menu.mode == (int)Menu.MODE.WORLDCUP)
		{
			disMatch = 0;
			PlayerPrefs.SetInt("disMatch", disMatch);
		}

	}

	public void SetupWinOrLoseWC()
	{
		if (isMatchStage <= 4)
		{
			ScoreTeam[PlayerPrefs.GetInt("wcAI") - 1] += 3;
			PlayerPrefs.SetInt("scoreTeam" + "[" + (PlayerPrefs.GetInt("wcAI") - 1) + "]", ScoreTeam[PlayerPrefs.GetInt("wcAI") - 1]);
			for (int i = 0; i < 15; i++)
			{
				random[i] = Random.Range(0, 10);

				if (random[i] == 1 || random[i] == 0)
				{

					ScoreTeam[SetupMatchWC.instance.ValuePlayer1[i] - 1] += 1;
					ScoreTeam[SetupMatchWC.instance.ValuePlayer2[i] - 1] += 1;
					PlayerPrefs.SetInt("scoreTeam" + "[" + (SetupMatchWC.instance.ValuePlayer1[i] - 1) + "]", ScoreTeam[SetupMatchWC.instance.ValuePlayer1[i] - 1]);
					PlayerPrefs.SetInt("scoreTeam" + "[" + (SetupMatchWC.instance.ValuePlayer2[i] - 1) + "]", ScoreTeam[SetupMatchWC.instance.ValuePlayer2[i] - 1]);

				}

				else
				{
					if (SetupMatchWC.instance.TopPlayer1[i] >= SetupMatchWC.instance.TopPlayer2[i])
					{
						ScoreTeam[SetupMatchWC.instance.ValuePlayer1[i] - 1] += 3;
						PlayerPrefs.SetInt("scoreTeam" + "[" + (SetupMatchWC.instance.ValuePlayer1[i] - 1) + "]", ScoreTeam[SetupMatchWC.instance.ValuePlayer1[i] - 1]);
					}
					else
					{
						ScoreTeam[SetupMatchWC.instance.ValuePlayer2[i] - 1] += 3;
						PlayerPrefs.SetInt("scoreTeam" + "[" + (SetupMatchWC.instance.ValuePlayer2[i] - 1) + "]", ScoreTeam[SetupMatchWC.instance.ValuePlayer2[i] - 1]);
					}
				}
				Debug.Log("asfasfasdfsfasdfRandommmmmm  " + SetupMatchWC.instance.ValuePlayer2[i]);
			}
		}

		else if (isMatchStage == 5)
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

					if (ValueTeamPlayOff[i, j] != PlayerPrefs.GetInt("wcPlayer")
					   && ValueTeamPlayOff[i, j] != PlayerPrefs.GetInt("wcAI"))
					{
						if (int.Parse(SelectTeam.instance.AI_Head[ValueTeamPlayOff[0, j] - 1].name.Substring(0, 1))
							>= int.Parse(SelectTeam.instance.AI_Head[ValueTeamPlayOff[1, j] - 1].name.Substring(0, 1)))
						{
							GoalsPlayOff[0, j] = Random.Range(4, 9);
							GoalsPlayOff[1, j] = Random.Range(0, 5);
							PlayerPrefs.SetInt("GoalsPlayOff" + "[" + i + "," + j + "]", GoalsPlayOff[i, j]);
						}
						else
						{
							GoalsPlayOff[0, j] = Random.Range(0, 5);
							GoalsPlayOff[1, j] = Random.Range(4, 9);
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

					if (ValueTeamFirtOff[i, j] != PlayerPrefs.GetInt("wcPlayer") && ValueTeamFirtOff[i, j] != PlayerPrefs.GetInt("wcAI"))
					{
						if (int.Parse(SelectTeam.instance.AI_Head[ValueTeamFirtOff[0, j] - 1].name.Substring(0, 1))
							>= int.Parse(SelectTeam.instance.AI_Head[ValueTeamFirtOff[1, j] - 1].name.Substring(0, 1)))
						{
							GoalsFirtOff[0, j] = Random.Range(4, 9);
							GoalsFirtOff[1, j] = Random.Range(0, 5);
							PlayerPrefs.SetInt("GoalsFirtOff" + "[" + i + "," + j + "]", GoalsFirtOff[i, j]);
						}
						else
						{
							GoalsFirtOff[0, j] = Random.Range(0, 5);
							GoalsFirtOff[1, j] = Random.Range(4, 9);
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

					if (ValueTeamQuarterFinals[i, j] != PlayerPrefs.GetInt("wcPlayer") && ValueTeamQuarterFinals[i, j] != PlayerPrefs.GetInt("wcAI"))
					{
						if (int.Parse(SelectTeam.instance.AI_Head[ValueTeamQuarterFinals[0, j] - 1].name.Substring(0, 1)) 
							>= int.Parse(SelectTeam.instance.AI_Head[ValueTeamQuarterFinals[0, j] - 1].name.Substring(0, 1)))
						{
							GoalsQuarterFinals[0, j] = Random.Range(4, 9);
							GoalsQuarterFinals[1, j] = Random.Range(0, 5);
							PlayerPrefs.SetInt("GoalsQuarterFinals" + "[" + i + "," + j + "]", GoalsQuarterFinals[i, j]);
						}
						else
						{
							GoalsQuarterFinals[0, j] = Random.Range(0, 5);
							GoalsQuarterFinals[1, j] = Random.Range(4, 9);
							PlayerPrefs.SetInt("GoalsQuarterFinals" + "[" + i + "," + j + "]", GoalsQuarterFinals[i, j]);
						}
					}
				}
			}
		}

	}
}
