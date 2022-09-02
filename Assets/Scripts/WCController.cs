using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class WCController : MonoBehaviour
{
	public static WCController instance;
	public int wcPlayer;
	public Text textMoney;
	//public Sprite avatarFB;

	public GameObject panelStage;
	public int isSelectTeamWC;

	[Header("Setting Group")]

	public int[,] ValueTeam = new int[4, 8];
	public int[] ScoreTeam = new int[46];
	public Text[] Name_A, Name_B, Name_C, Name_D, Name_E, Name_F, Name_G, Name_H;
	public Text[] Score_A, Score_B, Score_C, Score_D, Score_E, Score_F, Score_G, Score_H;
	[Header("Setting Knoct - Out")]

	public int[,] ValueTeamPlayOff = new int[2, 8];
	public int[,] GoalsPlayOff = new int[2, 8];
	public Text[] Name_0, Name_1, Name_2, Name_3, Name_4, Name_5, Name_6, Name_7;
	public Text[] Goals_0, Goals_1, Goals_2, Goals_3, Goals_4, Goals_5, Goals_6, Goals_7;
	public Text[] NameWinPlayOff;
	public Text[] GoalsWinPlayOff;
	public List<int> listWinR16 = new List<int>();
	[Header("Round Of 8")]

	public Text[] Goals_FirtOff_0, Goals_FirtOff_1, Goals_FirtOff_2, Goals_FirtOff_3;
	public int[,] GoalsFirtOff = new int[2, 4];
	public int[,] ValueTeamFirtOff = new int[2, 4];
	public List<int> listWinR8 = new List<int>();

	[Header("Quarter Final")]
	public Text[] Goals_TK_0, Goals_TK_1, Goals_TK_2, Goals_TK_3, Goals_Win_TK, Goals_Bk_1;
	public Text[] Name_TK_0, Name_TK_1, Name_TK_2, Name_TK_3, Name_Win_TK, Name_BK_1;
	public int[,] ValueTeamQuarterFinals = new int[2, 2];
	public int[,] GoalsQuarterFinals = new int[2, 2];
	public List<int> listWinR4 = new List<int>();

	[Header("Cup")]
	public Text nameLeft, nameRight;

	public static int[] haskWin = new int[16];
	public GameObject panelLoseWC, panelPlayOff, panelQuarterFinal, panelCup;
	public int winOrLose;
	public Button reset;
	public GameObject effectChampion, panelGetPrizeCup, panelReset, pn_selecteam;

	public Image img_player, img_star, img_body;
	public Text txt_select_below, txt_name_player, txt_difficul;
	public static string str_difficul_wc = "NORMAL";
	public Sprite spr_select, spr_cancel;
	public Button btn_select;
	public GameObject obj_diff, btn_start;
	public List<int> list_top_1 = new List<int>();
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
		Menu.isTraining = false;
		str_difficul_wc = PlayerPrefs.GetString(StringUtils.str_mode_wc, "NORMAL");
		txt_difficul.text = str_difficul_wc;
		if (PlayerPrefs.GetInt(GameConstants.MUSIC, 1) == 1)
		{
			SoundManager.Instance.musicBG.mute = false;
			SoundManager.Instance.matchLost.Stop();
			SoundManager.Instance.matchWon.Stop();
		}

		GetValueTeam();
		wcPlayer = PlayerPrefs.GetInt(StringUtils.value_team_player_wc, 1);
		for (int i = 0; i < 2; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				GoalsPlayOff[i, j] = PlayerPrefs.GetInt("GoalsPlayOff" + "[" + i + "," + j + "]", 0);
			}

		}
		isSelectTeamWC = PlayerPrefs.GetInt("isSelectTeamWC", 0);
		winOrLose = PlayerPrefs.GetInt("WinOrLose", 0);
		int isMatchStage = PlayerPrefs.GetInt("isStage1", 1);

		if (isSelectTeamWC == 0)
		{
			reset.gameObject.SetActive(false);
			panelGetPrizeCup.SetActive(false);
			panelPlayOff.SetActive(false);
			panelQuarterFinal.SetActive(false);
			panelCup.SetActive(false);
			panelStage.SetActive(false);
			pn_selecteam.SetActive(true);
			StartCoroutine(SetupWaiting(txt_select_below));
		}
		else
		{
			reset.gameObject.SetActive(true);
			pn_selecteam.SetActive(false);
			if (isMatchStage < 4)
			{
				//FBManager.instance.logEventGroupStage();

				panelGetPrizeCup.SetActive(false);
				panelPlayOff.SetActive(false);
				panelQuarterFinal.SetActive(false);
				panelCup.SetActive(false);
				panelStage.SetActive(true);
                /*FirebaseAnalyticManager.instance.AnalyticsGroupstage();*/
            }

			else if (isMatchStage >= 4)
			{
				SetupUIPlayOff();

				panelLoseWC.SetActive(false);
				panelStage.SetActive(false);
				if (isMatchStage == 4)
				{
					//FBManager.instance.logEventRoundOf16();

					panelPlayOff.SetActive(true);
                    /*FirebaseAnalyticManager.instance.AnalyticsR16();*/
                }
				else if (isMatchStage == 5)
				{
					//FBManager.instance.logEventQuarterFinals();

					panelPlayOff.SetActive(true);
					WinPlayOff();
					GetValueTeamRoundOf8();
                   /* FirebaseAnalyticManager.instance.AnalyticsQuarterfinal();*/
                }
				else if (isMatchStage == 6)
				{
					//FBManager.instance.logEventSemifinals();

					WinPlayOff();
					panelQuarterFinal.SetActive(true);
					panelPlayOff.SetActive(false);
					GetValueTeamRoundOf8();
					SetUITeamR8_FirtOff();
					SetupWinR8();
                    /*FirebaseAnalyticManager.instance.AnalyticsSemifinal();*/
                }
				else if (isMatchStage == 7)
				{
					//FBManager.instance.logEventFinal();

					WinPlayOff();
					if (winOrLose == 0)
					{
						panelQuarterFinal.SetActive(false);
						panelCup.SetActive(true);
					}
					else
					{
						panelQuarterFinal.SetActive(true);
						panelCup.SetActive(false);
					}

					GetValueTeamRoundOf8();
					SetUITeamR8_FirtOff();
					SetupWinR8();
					SetupBK();
                    /*FirebaseAnalyticManager.instance.AnalyticsFinal();*/
                }
				else if (isMatchStage == 8)
				{
					if (PlayerPrefs.GetInt("champion") == 1)
					{
						StartCoroutine(GetEffectChampion());
						panelGetPrizeCup.SetActive(true);
						int index_Stadium = PlayerPrefs.GetInt("index_Stadium", 0);
						int id = PlayerPrefs.GetInt("IDPlayer", 0);
						int randomShoePlayer = Random.Range(0, 3);
						reset.interactable = false;
                        /*FirebaseAnalyticManager.instance.AnalyticsCupWC();*/

                    }
					else
					{
						panelLoseWC.SetActive(true);
						panelPlayOff.SetActive(false);
						panelQuarterFinal.SetActive(false);
						panelCup.SetActive(false);
						panelStage.SetActive(false);
					}

				}

				else if (isMatchStage > 8)
				{
					panelLoseWC.SetActive(true);
					panelPlayOff.SetActive(false);
					panelQuarterFinal.SetActive(false);
					panelCup.SetActive(false);
					panelStage.SetActive(false);
				}
			}

		}


		PlayerPrefs.SetInt("wcPlayer", wcPlayer);
		textMoney.text = PlayerPrefs.GetInt("money").ToString();

		Debug.Log(PlayerPrefs.GetInt("wcPlayer"));

	}

	// Update is called once per frame
	void Update()
	{
		wcPlayer = PlayerPrefs.GetInt("wcPlayer");
		if (isSelectTeamWC == 1)
		{
			reset.gameObject.SetActive(true);
			LoadColorTeamPlayer();
		}
		//textMoney.text = PlayerPrefs.GetInt("money").ToString();

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
	public void ButtonCantSelectPlayer()
	{
		if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
		{
			SoundManager.Instance.buttonClick.Play();
		}
		if (btn_select.GetComponent<Image>().sprite.name == "btnCancel")
		{

			txt_select_below.enabled = true;
			img_player.transform.parent.gameObject.SetActive(false);
			btn_select.gameObject.SetActive(false);
			btn_start.gameObject.SetActive(false);
			txt_name_player.text = "Select Player";
			btn_select.GetComponent<Animator>().enabled = true;
			StartCoroutine(SetupWaiting(txt_select_below));
		}
		else
		{
			btn_select.GetComponent<Image>().sprite = spr_cancel;
			btn_start.gameObject.SetActive(true);
			btn_select.GetComponent<Animator>().enabled = false;
		}
	}
	public void ButtonMode()
	{
		if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
		{
			SoundManager.Instance.buttonClick.Play();
		}
		if (obj_diff.activeSelf == false)
		{
			obj_diff.SetActive(true);
		}
		else
		{
			obj_diff.SetActive(false);
		}
	}
	public void SelectDiffcult(string mode)
	{
		obj_diff.SetActive(false);
		str_difficul_wc = mode;
		txt_difficul.text = mode.ToString();
		PlayerPrefs.SetString(StringUtils.str_mode_wc, mode);
	}

	public void ButtonNextSelectTeam()
	{
		if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
		{
			SoundManager.Instance.buttonClick.Play();
		}
		reset.gameObject.SetActive(true);
		panelGetPrizeCup.SetActive(false);
		panelPlayOff.SetActive(false);
		panelQuarterFinal.SetActive(false);
		panelCup.SetActive(false);

		pn_selecteam.SetActive(false);
		PlayerPrefs.SetInt("isSelectTeamWC", 1);

		InstanceValueTeam();
		LoadColorTeamPlayer();
		SetupMatchWC.instance.SetupMatchStage();
		panelStage.SetActive(true);


	}

	IEnumerator GetEffectChampion()
	{
		yield return new WaitForSeconds(0.25f);
		Instantiate(effectChampion, new Vector2(0, 4), Quaternion.identity);

	}



	public void ButtonBack()
	{
		if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
		{
			SoundManager.Instance.buttonClick.Play();
		}
		SceneManager.LoadScene("Menu");
	}
	public void ButtonBackPlayOff()
	{
		if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
		{
			SoundManager.Instance.buttonClick.Play();
		}
		panelStage.SetActive(true);
		panelPlayOff.SetActive(false);
		panelQuarterFinal.SetActive(false);
	}

	public void ButtonReset()
	{
		if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
		{
			SoundManager.Instance.buttonClick.Play();
		}
		panelReset.SetActive(true);
	}

	public void ButtonNoReset()
	{
		if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
		{
			SoundManager.Instance.buttonClick.Play();
		}
		panelReset.SetActive(false);
	}

	public void ButtonYes()
	{
		if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
		{
			SoundManager.Instance.buttonClick.Play();
		}
		PlayerPrefs.SetInt("isStage1", 1);
		PlayerPrefs.SetInt("WinOrLose", 0);
		PlayerPrefs.SetInt("isSelectTeamWC", 0);
		PlayerPrefs.SetInt(StringUtils.value_team_player_wc, 1);
		for (int i = 0; i < ScoreTeam.Length; i++)
		{
			PlayerPrefs.SetInt("scoreTeam" + "[" + i + "]", 0);
		}
		SceneManager.LoadScene("WorldCup");
	}





	public void ButtonNextStage()
	{
		if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
		{
			SoundManager.Instance.buttonClick.Play();
		}
		if (PlayerPrefs.GetInt("isStage1") < 4)
		{
			if (winOrLose == 0)
			{
				SceneManager.LoadScene("SetupStadium");
			}
			else
			{
				panelLoseWC.SetActive(true);
				panelStage.SetActive(false);
				panelPlayOff.SetActive(false);
			}
		}
		else
		{
			panelPlayOff.SetActive(true);
			panelStage.SetActive(false);
		}

	}

	public void ButtonNextPlayOff()
	{
		if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
		{
			SoundManager.Instance.buttonClick.Play();
		}
		if (PlayerPrefs.GetInt("isStage1") < 6)
		{
			if (winOrLose == 0)
			{
				SceneManager.LoadScene("SetupStadium");
			}
			else
			{
				panelLoseWC.SetActive(true);
				panelStage.SetActive(false);
				panelPlayOff.SetActive(false);
			}

		}
		else
		{
			SetUITeamR8_FirtOff();
			panelPlayOff.SetActive(false);
			panelQuarterFinal.SetActive(true);
		}




	}
	public void ButtonBackQuarterFinal()
	{
		if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
		{
			SoundManager.Instance.buttonClick.Play();
		}
		panelPlayOff.SetActive(true);
		panelQuarterFinal.SetActive(false);
	}

	public void ButtonNextQuarterFinal()
	{
		if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
		{
			SoundManager.Instance.buttonClick.Play();
		}
		if (PlayerPrefs.GetInt("isStage1") < 7)
		{
			if (winOrLose == 0)
			{
				SceneManager.LoadScene("SetupStadium");
			}
			else
			{
				panelLoseWC.SetActive(true);
				panelStage.SetActive(false);
				panelPlayOff.SetActive(false);
				panelQuarterFinal.SetActive(false);
			}
		}
		else
		{
			if (winOrLose == 0)
			{
				panelQuarterFinal.SetActive(false);
				panelCup.SetActive(true);
			}
			else
			{
				panelLoseWC.SetActive(true);
				panelStage.SetActive(false);
				panelPlayOff.SetActive(false);
				panelQuarterFinal.SetActive(false);
			}

		}
	}


	public void ButtonNextCup()
	{
		if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
		{
			SoundManager.Instance.buttonClick.Play();
		}
		if (winOrLose == 0)
		{
			SceneManager.LoadScene("SetupStadium");
		}
	}
	public void ButtonBackCup()
	{
		if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
		{
			SoundManager.Instance.buttonClick.Play();
		}
		panelCup.SetActive(false);
		panelQuarterFinal.SetActive(true);
	}
	public void LoadColorTeamPlayer()
	{

		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				if (ValueTeam[i, j] == wcPlayer)
				{
					switch (j)
					{
						case 0:

							Name_A[i].text = SelectTeam.instance.AI_Head[wcPlayer - 1].name.Substring(2);

							Name_A[i].color = Color.green;
							Score_A[i].color = Color.green;
							break;
						case 1:
							Name_B[i].text = SelectTeam.instance.AI_Head[wcPlayer - 1].name.Substring(2);
							Name_B[i].color = Color.green;
							Score_B[i].color = Color.green;
							break;
						case 2:
							Name_C[i].text = SelectTeam.instance.AI_Head[wcPlayer - 1].name.Substring(2);
							Name_C[i].color = Color.green;
							Score_C[i].color = Color.green;
							break;
						case 3:
							Name_D[i].text = SelectTeam.instance.AI_Head[wcPlayer - 1].name.Substring(2);
							Name_D[i].color = Color.green;
							Score_D[i].color = Color.green;
							break;
						case 4:
							Name_E[i].text = SelectTeam.instance.AI_Head[wcPlayer - 1].name.Substring(2);
							Name_E[i].color = Color.green;
							Score_E[i].color = Color.green;
							break;
						case 5:
							Name_F[i].text = SelectTeam.instance.AI_Head[wcPlayer - 1].name.Substring(2);
							Name_F[i].color = Color.green;
							Score_F[i].color = Color.green;
							break;
						case 6:
							Name_G[i].text = SelectTeam.instance.AI_Head[wcPlayer - 1].name.Substring(2);
							Name_G[i].color = Color.green;
							Score_G[i].color = Color.green;
							break;
						case 7:
							Name_H[i].text = SelectTeam.instance.AI_Head[wcPlayer - 1].name.Substring(2);
							Name_H[i].color = Color.green;
							Score_H[i].color = Color.green;
							break;
					}
				}

			}

		}
	}


	public void GetValueTeam()
	{
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				ValueTeam[i, j] = PlayerPrefs.GetInt("valueTeam" + "[" + i + "," + j + "]", 1);
			}

		}

		for (int i = 0; i < ScoreTeam.Length; i++)
		{

			ScoreTeam[i] = PlayerPrefs.GetInt("scoreTeam" + "[" + i + "]", 0);

		}

		for (int i = 0; i < 4; i++)
		{
			//ListSortScore();


			Name_A[i].text = SelectTeam.instance.AI_Head[ValueTeam[i, 0] - 1].name.Substring(2);
			Name_B[i].text = SelectTeam.instance.AI_Head[ValueTeam[i, 1] - 1].name.Substring(2);
			Name_C[i].text = SelectTeam.instance.AI_Head[ValueTeam[i, 2] - 1].name.Substring(2);
			Name_D[i].text = SelectTeam.instance.AI_Head[ValueTeam[i, 3] - 1].name.Substring(2);
			Name_E[i].text = SelectTeam.instance.AI_Head[ValueTeam[i, 4] - 1].name.Substring(2);
			Name_F[i].text = SelectTeam.instance.AI_Head[ValueTeam[i, 5] - 1].name.Substring(2);
			Name_G[i].text = SelectTeam.instance.AI_Head[ValueTeam[i, 6] - 1].name.Substring(2);
			Name_H[i].text = SelectTeam.instance.AI_Head[ValueTeam[i, 7] - 1].name.Substring(2);

			Score_A[i].text = ScoreTeam[ValueTeam[i, 0] - 1].ToString();
			Score_B[i].text = ScoreTeam[ValueTeam[i, 1] - 1].ToString();
			Score_C[i].text = ScoreTeam[ValueTeam[i, 2] - 1].ToString();
			Score_D[i].text = ScoreTeam[ValueTeam[i, 3] - 1].ToString();
			Score_E[i].text = ScoreTeam[ValueTeam[i, 4] - 1].ToString();
			Score_F[i].text = ScoreTeam[ValueTeam[i, 5] - 1].ToString();
			Score_G[i].text = ScoreTeam[ValueTeam[i, 6] - 1].ToString();
			Score_H[i].text = ScoreTeam[ValueTeam[i, 7] - 1].ToString();

		}
	}

	public void InstanceValueTeam()
	{

		int[] top1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22 };
		int[] top2 = { 23, 24, 25, 26, 27, 28, 29, 30 };
		int[] top3 = { 31, 32, 33, 34, 35, 36, 37, 38 };
		int[] top4 = { 39, 40, 41, 42, 43, 44, 45, 46 };


		List<int> list1 = new List<int>();
		List<int> list2 = new List<int>();
		List<int> list3 = new List<int>();
		List<int> list4 = new List<int>();
		list_top_1.Clear();
		for (int i = 0; i < top1.Length; i++)
		{
			list_top_1.Add(top1[i]);
		}

		while (list_top_1.Count > 8)
		{
			int a = Random.Range(0, list_top_1.Count);
			if (list_top_1[a] != wcPlayer)
			{
				list_top_1.RemoveAt(a);
			}
		}
		for (int i = 0; i < list_top_1.Count; i++)
		{
			PlayerPrefs.SetInt(StringUtils.str_list_top_1 + i, list_top_1[i]);
		}
		//Debug.Log("list_top_1.Count =" + list_top_1.Count);
		for (int i = 0; i < 8; i++)
		{
			list1.Add(list_top_1[i]);
			list2.Add(top2[i]);
			list3.Add(top3[i]);
			list4.Add(top4[i]);
		}

		for (int j = 0; j < 8; j++)
		{
			int temp1 = 0, temp2 = 0, temp3 = 0, temp4 = 0;
			temp1 = Random.Range(0, list1.Count);
			temp2 = Random.Range(0, list2.Count);
			temp3 = Random.Range(0, list3.Count);
			temp4 = Random.Range(0, list4.Count);

			ValueTeam[0, j] = list1[temp1];
			list1.RemoveAt(temp1);

			ValueTeam[1, j] = list2[temp2];
			list2.RemoveAt(temp2);

			ValueTeam[2, j] = list3[temp3];
			list3.RemoveAt(temp3);

			ValueTeam[3, j] = list4[temp4];
			list4.RemoveAt(temp4);
		}

		//for (int i = 0; i < 4; i++)
		//{
		//    Debug.Log("asdfsdfas11111111111111111111111111111111        " + ValueTeam[i, 0]);

		//}

		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				PlayerPrefs.SetInt("valueTeam" + "[" + i + "," + j + "]", ValueTeam[i, j]);
			}

		}
		for (int i = 0; i < ScoreTeam.Length; i++)
		{

			PlayerPrefs.SetInt("scoreTeam" + "[" + i + "]", 0);

		}

		for (int i = 0; i < 4; i++)
		{
			//ListSortScore();


			Name_A[i].text = SelectTeam.instance.AI_Head[ValueTeam[i, 0] - 1].name.Substring(2);
			Name_B[i].text = SelectTeam.instance.AI_Head[ValueTeam[i, 1] - 1].name.Substring(2);
			Name_C[i].text = SelectTeam.instance.AI_Head[ValueTeam[i, 2] - 1].name.Substring(2);
			Name_D[i].text = SelectTeam.instance.AI_Head[ValueTeam[i, 3] - 1].name.Substring(2);
			Name_E[i].text = SelectTeam.instance.AI_Head[ValueTeam[i, 4] - 1].name.Substring(2);
			Name_F[i].text = SelectTeam.instance.AI_Head[ValueTeam[i, 5] - 1].name.Substring(2);
			Name_G[i].text = SelectTeam.instance.AI_Head[ValueTeam[i, 6] - 1].name.Substring(2);
			Name_H[i].text = SelectTeam.instance.AI_Head[ValueTeam[i, 7] - 1].name.Substring(2);

			Score_A[i].text = ScoreTeam[ValueTeam[i, 0] - 1].ToString();
			Score_B[i].text = ScoreTeam[ValueTeam[i, 1] - 1].ToString();
			Score_C[i].text = ScoreTeam[ValueTeam[i, 2] - 1].ToString();
			Score_D[i].text = ScoreTeam[ValueTeam[i, 3] - 1].ToString();
			Score_E[i].text = ScoreTeam[ValueTeam[i, 4] - 1].ToString();
			Score_F[i].text = ScoreTeam[ValueTeam[i, 5] - 1].ToString();
			Score_G[i].text = ScoreTeam[ValueTeam[i, 6] - 1].ToString();
			Score_H[i].text = ScoreTeam[ValueTeam[i, 7] - 1].ToString();
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
	public void WinPlayOff()
	{

		for (int j = 0; j < 8; j++)
		{

			if (GoalsPlayOff[0, j] >= GoalsPlayOff[1, j])
			{
				NameWinPlayOff[j].text = SelectTeam.instance.AI_Head[ValueTeamPlayOff[0, j] - 1].name.Substring(2);

				listWinR16.Add(ValueTeamPlayOff[0, j]);
			}
			else
			{
				NameWinPlayOff[j].text = SelectTeam.instance.AI_Head[ValueTeamPlayOff[1, j] - 1].name.Substring(2);
				listWinR16.Add(ValueTeamPlayOff[1, j]);
			}

			if (ValueTeamPlayOff[0, j] == PlayerPrefs.GetInt("wcPlayer") || ValueTeamPlayOff[1, j] == PlayerPrefs.GetInt("wcPlayer"))
			{
				if ((PlayerPrefs.GetInt("WinOrLose") == 0 && PlayerPrefs.GetInt("isStage1", 1) < 6) || PlayerPrefs.GetInt("isStage1", 1) >= 6)
				{
					NameWinPlayOff[j].text = SelectTeam.instance.AI_Head[wcPlayer - 1].name.Substring(2);

					NameWinPlayOff[j].color = Color.green;
					GoalsWinPlayOff[j].color = Color.green;
				}

			}
		}



	}

	public void SetupUIPlayOff()
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
				PlayerPrefs.SetInt("ValueTeamPlayOff" + "[" + i + "," + j + "]", ValueTeamPlayOff[i, j]);
			}
		}
		for (int i = 0; i < 2; i++)
		{

			Name_0[i].text = SelectTeam.instance.AI_Head[ValueTeamPlayOff[i, 0] - 1].name.Substring(2);
			Name_1[i].text = SelectTeam.instance.AI_Head[ValueTeamPlayOff[i, 1] - 1].name.Substring(2);
			Name_2[i].text = SelectTeam.instance.AI_Head[ValueTeamPlayOff[i, 2] - 1].name.Substring(2);
			Name_3[i].text = SelectTeam.instance.AI_Head[ValueTeamPlayOff[i, 3] - 1].name.Substring(2);
			Name_4[i].text = SelectTeam.instance.AI_Head[ValueTeamPlayOff[i, 4] - 1].name.Substring(2);
			Name_5[i].text = SelectTeam.instance.AI_Head[ValueTeamPlayOff[i, 5] - 1].name.Substring(2);
			Name_6[i].text = SelectTeam.instance.AI_Head[ValueTeamPlayOff[i, 6] - 1].name.Substring(2);
			Name_7[i].text = SelectTeam.instance.AI_Head[ValueTeamPlayOff[i, 7] - 1].name.Substring(2);
		}
		for (int i = 0; i < 2; i++)
		{
			for (int j = 0; j < 8; j++)
			{


				if (ValueTeamPlayOff[i, j] == PlayerPrefs.GetInt("wcPlayer"))
				{
					switch (j)
					{
						case 0:
							Name_0[i].text = SelectTeam.instance.AI_Head[wcPlayer - 1].name.Substring(2);

							Name_0[i].color = Color.green;
							Goals_0[i].color = Color.green;
							break;
						case 1:
							Name_1[i].text = SelectTeam.instance.AI_Head[wcPlayer - 1].name.Substring(2);

							Name_1[i].color = Color.green;
							Goals_1[i].color = Color.green;
							break;
						case 2:
							Name_2[i].text = SelectTeam.instance.AI_Head[wcPlayer - 1].name.Substring(2);

							Name_2[i].color = Color.green;
							Goals_2[i].color = Color.green;
							break;
						case 3:
							Name_3[i].text = SelectTeam.instance.AI_Head[wcPlayer - 1].name.Substring(2);

							Name_3[i].color = Color.green;
							Goals_3[i].color = Color.green;
							break;
						case 4:
							Name_4[i].text = SelectTeam.instance.AI_Head[wcPlayer - 1].name.Substring(2);

							Name_4[i].color = Color.green;
							Goals_4[i].color = Color.green;
							break;
						case 5:
							Name_5[i].text = SelectTeam.instance.AI_Head[wcPlayer - 1].name.Substring(2);

							Name_5[i].color = Color.green;
							Goals_5[i].color = Color.green;
							break;
						case 6:
							Name_6[i].text = SelectTeam.instance.AI_Head[wcPlayer - 1].name.Substring(2);

							Name_6[i].color = Color.green;
							Goals_6[i].color = Color.green;
							break;
						case 7:
							Name_7[i].text = SelectTeam.instance.AI_Head[wcPlayer - 1].name.Substring(2);

							Name_7[i].color = Color.green;
							Goals_7[i].color = Color.green;
							break;
					}

				}

			}
		}


		if (PlayerPrefs.GetInt("isStage1") >= 5)
		{
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					GoalsPlayOff[i, j] = PlayerPrefs.GetInt("GoalsPlayOff" + "[" + i + "," + j + "]", 0);
				}

			}
			for (int i = 0; i < 2; i++)
			{

				Goals_0[i].text = GoalsPlayOff[i, 0].ToString();
				Goals_1[i].text = GoalsPlayOff[i, 1].ToString();
				Goals_2[i].text = GoalsPlayOff[i, 2].ToString();
				Goals_3[i].text = GoalsPlayOff[i, 3].ToString();
				Goals_4[i].text = GoalsPlayOff[i, 4].ToString();
				Goals_5[i].text = GoalsPlayOff[i, 5].ToString();
				Goals_6[i].text = GoalsPlayOff[i, 6].ToString();
				Goals_7[i].text = GoalsPlayOff[i, 7].ToString();


			}
		}

	}


	// Set up Round Of 8

	public void GetValueTeamRoundOf8()
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
				ValueTeamFirtOff[0, 0] = listWinR16[0];
				ValueTeamFirtOff[1, 0] = listWinR16[1];
				ValueTeamFirtOff[0, 1] = listWinR16[2];
				ValueTeamFirtOff[1, 1] = listWinR16[3];
				ValueTeamFirtOff[0, 2] = listWinR16[4];
				ValueTeamFirtOff[1, 2] = listWinR16[5];
				ValueTeamFirtOff[0, 3] = listWinR16[6];
				ValueTeamFirtOff[1, 3] = listWinR16[7];
				PlayerPrefs.SetInt("ValueTeamFirtOff" + "[" + i + "," + j + "]", ValueTeamFirtOff[i, j]);
			}

		}
		for (int j = 0; j < 4; j++)
		{
			if (ValueTeamFirtOff[0, j] == PlayerPrefs.GetInt("wcPlayer"))
			{
				int wcAI = ValueTeamFirtOff[1, j];
				PlayerPrefs.SetInt("wcAI", wcAI);
			}
			else if (ValueTeamFirtOff[1, j] == PlayerPrefs.GetInt("wcPlayer"))
			{
				int wcAI = ValueTeamFirtOff[0, j];
				PlayerPrefs.SetInt("wcAI", wcAI);

			}
		}


	}

	public void SetUITeamR8_FirtOff()
	{
		for (int i = 0; i < 2; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				GoalsFirtOff[i, j] = PlayerPrefs.GetInt("GoalsFirtOff" + "[" + i + "," + j + "]", 0);
				ValueTeamFirtOff[i, j] = PlayerPrefs.GetInt("ValueTeamFirtOff" + "[" + i + "," + j + "]", 1);

				Name_TK_0[i].text = SelectTeam.instance.AI_Head[ValueTeamFirtOff[i, 0] - 1].name.Substring(2);
				Name_TK_1[i].text = SelectTeam.instance.AI_Head[ValueTeamFirtOff[i, 1] - 1].name.Substring(2);
				Name_TK_2[i].text = SelectTeam.instance.AI_Head[ValueTeamFirtOff[i, 2] - 1].name.Substring(2);
				Name_TK_3[i].text = SelectTeam.instance.AI_Head[ValueTeamFirtOff[i, 3] - 1].name.Substring(2);


			}

		}

		for (int i = 0; i < 2; i++)
		{
			for (int j = 0; j < 4; j++)
			{

				if (ValueTeamFirtOff[i, j] == PlayerPrefs.GetInt("wcPlayer"))
				{
					switch (j)
					{
						case 0:
							Name_TK_0[i].text = SelectTeam.instance.AI_Head[wcPlayer - 1].name.Substring(2);

							Name_TK_0[i].color = Color.green;
							Goals_TK_0[i].color = Color.green;
							break;
						case 1:
							Name_TK_1[i].text = SelectTeam.instance.AI_Head[wcPlayer - 1].name.Substring(2);
							Name_TK_1[i].color = Color.green;
							Goals_TK_1[i].color = Color.green;
							break;
						case 2:
							Name_TK_2[i].text = SelectTeam.instance.AI_Head[wcPlayer - 1].name.Substring(2);
							Name_TK_2[i].color = Color.green;
							Goals_TK_2[i].color = Color.green;
							break;
						case 3:
							Name_TK_3[i].text = SelectTeam.instance.AI_Head[wcPlayer - 1].name.Substring(2);
							Name_TK_3[i].color = Color.green;
							Goals_TK_3[i].color = Color.green;
							break;

					}
				}

			}

		}


		for (int i = 0; i < 2; i++)
		{

			Goals_FirtOff_0[i].text = GoalsFirtOff[i, 0].ToString();
			Goals_FirtOff_1[i].text = GoalsFirtOff[i, 1].ToString();
			Goals_FirtOff_2[i].text = GoalsFirtOff[i, 2].ToString();
			Goals_FirtOff_3[i].text = GoalsFirtOff[i, 3].ToString();

			Goals_TK_0[i].text = GoalsFirtOff[i, 0].ToString();
			Goals_TK_1[i].text = GoalsFirtOff[i, 1].ToString();
			Goals_TK_2[i].text = GoalsFirtOff[i, 2].ToString();
			Goals_TK_3[i].text = GoalsFirtOff[i, 3].ToString();

		}

	}

	public void SetupWinR8()
	{
		for (int i = 0; i < 2; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				GoalsFirtOff[i, j] = PlayerPrefs.GetInt("GoalsFirtOff" + "[" + i + "," + j + "]", 0);
			}
		}
		for (int j = 0; j < 4; j++)
		{
			if (GoalsFirtOff[0, j] >= GoalsFirtOff[1, j])
			{
				listWinR8.Add(ValueTeamFirtOff[0, j]);
			}
			else
			{
				listWinR8.Add(ValueTeamFirtOff[1, j]);
			}
		}
		ValueTeamQuarterFinals[0, 0] = listWinR8[0];
		ValueTeamQuarterFinals[1, 0] = listWinR8[1];
		ValueTeamQuarterFinals[0, 1] = listWinR8[2];
		ValueTeamQuarterFinals[1, 1] = listWinR8[3];

		for (int i = 0; i < 2; i++)
		{
			for (int j = 0; j < 2; j++)
			{

				PlayerPrefs.SetInt("ValueTeamQuarterFinals" + "[" + i + "," + j + "]", ValueTeamQuarterFinals[i, j]);

				if (ValueTeamQuarterFinals[0, j] == PlayerPrefs.GetInt("wcPlayer"))
				{
					int wcAI = ValueTeamQuarterFinals[1, j];
					PlayerPrefs.SetInt("wcAI", wcAI);
				}
				else if (ValueTeamQuarterFinals[1, j] == PlayerPrefs.GetInt("wcPlayer"))
				{
					int wcAI = ValueTeamQuarterFinals[0, j];
					PlayerPrefs.SetInt("wcAI", wcAI);

				}

			}
		}

		for (int i = 0; i < 4; i++)
		{
			Name_Win_TK[i].text = SelectTeam.instance.AI_Head[listWinR8[i] - 1].name.Substring(2);

			if (listWinR8[i] == PlayerPrefs.GetInt("wcPlayer"))
			{
				Name_Win_TK[i].text = SelectTeam.instance.AI_Head[wcPlayer - 1].name.Substring(2);

				Name_Win_TK[i].color = Color.green;
				Goals_Win_TK[i].color = Color.green;

			}
		}
	}

	public void SetupBK()
	{
		for (int i = 0; i < 2; i++)
		{
			for (int j = 0; j < 2; j++)
			{
				GoalsQuarterFinals[i, j] = PlayerPrefs.GetInt("GoalsQuarterFinals" + "[" + i + "," + j + "]", 0);
				ValueTeamQuarterFinals[i, j] = PlayerPrefs.GetInt("ValueTeamQuarterFinals" + "[" + i + "," + j + "]", 1);
				Goals_Win_TK[0].text = GoalsQuarterFinals[0, 0].ToString();
				Goals_Win_TK[1].text = GoalsQuarterFinals[1, 0].ToString();
				Goals_Win_TK[2].text = GoalsQuarterFinals[0, 1].ToString();
				Goals_Win_TK[3].text = GoalsQuarterFinals[1, 1].ToString();

			}
		}

		for (int j = 0; j < 2; j++)
		{
			if (GoalsQuarterFinals[0, j] >= GoalsQuarterFinals[1, j])
			{
				listWinR4.Add(ValueTeamQuarterFinals[0, j]);
			}
			else
			{
				listWinR4.Add(ValueTeamQuarterFinals[1, j]);
			}
		}

		if (listWinR4[0] == PlayerPrefs.GetInt("wcPlayer"))
		{
			int wcAI = listWinR4[1];
			PlayerPrefs.SetInt("wcAI", wcAI);

			nameLeft.text = SelectTeam.instance.AI_Head[wcPlayer - 1].name.Substring(2);
			nameRight.text = SelectTeam.instance.AI_Head[listWinR4[1] - 1].name.Substring(2);
			nameLeft.color = Color.green;
		}
		else if (listWinR4[1] == PlayerPrefs.GetInt("wcPlayer"))
		{
			int wcAI = listWinR4[0];
			PlayerPrefs.SetInt("wcAI", wcAI);
			nameLeft.text = SelectTeam.instance.AI_Head[listWinR4[0] - 1].name.Substring(2);
			nameRight.text = SelectTeam.instance.AI_Head[wcPlayer - 1].name.Substring(2);
			nameRight.color = Color.green;
		}

		for (int i = 0; i < 2; i++)
		{

			PlayerPrefs.SetInt("listWinR4" + i, listWinR4[i]);
		}
	}

}