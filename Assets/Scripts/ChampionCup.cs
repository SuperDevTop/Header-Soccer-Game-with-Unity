using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChampionCup : MonoBehaviour
{

    public Image[] head_AI, body_AI, shoe_AI;
    public List<int> listAIRound = new List<int>();
    public Text[] textScoreAI_1, textScoreAI_2, textScorePlayer_1, textScorePlayer_2, textTotalAI, textTotalPlayer, winOrLose, dauGach;
    public Text txt_Money;
    public Sprite[] sp_target;
    public Image[] img_target;
    public GameObject content;
    public GameObject[] listGameobjectAI, hidePlayer, textLose1;
    public Text txt_mission;
    public GameObject panelRenew, panelCup, gbo_Message, effect_Fireword;
    public static int int_SaveMe;


    // Use this for initialization
    void Start()
    {

        int_SaveMe = 1;
        //Menu.modeBack = 1;
        int _matchChampion = PlayerPrefs.GetInt("matchChampion", 0);
        for (int i = 0; i < shoe_AI.Length; i++)
        {
            shoe_AI[i].sprite = SelectTeam.instance.sp_shoe[Random.Range(0, 3)];
        }

        if (PlayerPrefs.GetInt("setupRoundChampionCup", 0) == 0)
        {
            SetupAIRound();
        }
        if (_matchChampion < 8)
        {
            for (int i = 0; i < head_AI.Length; i++)
            {

                head_AI[i].sprite = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt("listAIRound" + i) - 1];
                body_AI[i].sprite = SelectTeam.instance.sp_Body[PlayerPrefs.GetInt("listAIRound" + i) - 1];

                textScoreAI_1[i].text = PlayerPrefs.GetInt("listScoreAI_1" + i).ToString();
                textScorePlayer_1[i].text = PlayerPrefs.GetInt("listScorePlayer_1" + i).ToString();

                if (i < _matchChampion)
                {
                    textScoreAI_2[i].text = PlayerPrefs.GetInt("listScoreAI_2" + i).ToString();
                    textScorePlayer_2[i].text = PlayerPrefs.GetInt("listScorePlayer_2" + i).ToString();
                    textTotalAI[i].text = PlayerPrefs.GetInt("listScoreAI_1" + i) + PlayerPrefs.GetInt("listScoreAI_2" + i) + "";
                    textTotalPlayer[i].text = PlayerPrefs.GetInt("listScorePlayer_1" + i) + PlayerPrefs.GetInt("listScorePlayer_2" + i) + "";

                    if (PlayerPrefs.GetInt("listScoreAI_2" + i) > PlayerPrefs.GetInt("listScorePlayer_2" + i))
                    {
                        winOrLose[i].text = "Lose: ";
                    }
                    else if (PlayerPrefs.GetInt("listScoreAI_2" + i) == PlayerPrefs.GetInt("listScorePlayer_2" + i))
                    {
                        winOrLose[i].text = "Draw: ";
                    }
                    else
                    {
                        winOrLose[i].text = "Win: ";
                    }
                    textLose1[i].SetActive(true);

                    Debug.Log("thua ma  " + PlayerPrefs.GetInt("listScoreAI_1" + i));
                    if (PlayerPrefs.GetInt("listScoreAI_1" + i) > 0)
                    {
                        Debug.Log("thua ma");
                        textLose1[i].GetComponent<Text>().text = "Lose: ";
                    }
                    else if (PlayerPrefs.GetInt("listScoreAI_1" + i) == 0)
                    {
                        Debug.Log("hoaf ma");
                        textLose1[i].GetComponent<Text>().text = "Draw: ";
                    }
                    
                    hidePlayer[i].SetActive(false);
                    listGameobjectAI[i].SetActive(true);
                    textScoreAI_1[i].enabled = true;
                    textScorePlayer_1[i].enabled = true;
                    dauGach[i].text = "-";
                    if (PlayerPrefs.GetInt("listScoreAI_1" + i) + PlayerPrefs.GetInt("listScoreAI_2" + i)
                         > PlayerPrefs.GetInt("listScorePlayer_1" + i) + PlayerPrefs.GetInt("listScorePlayer_2" + i)
                       )
                    {
                        panelRenew.SetActive(true);

                    }
                    else
                    {
                        panelRenew.SetActive(false);
                    }
                }
                else
                {
                    if (i > _matchChampion)
                    {
                        hidePlayer[i].SetActive(true);
                        listGameobjectAI[i].SetActive(false);
                        textScoreAI_1[i].enabled = false;
                        textScorePlayer_1[i].enabled = false;
                        textLose1[i].SetActive(false);
                    }
                    else
                    {
                        hidePlayer[i].SetActive(false);
                        listGameobjectAI[i].SetActive(true);
                        textScoreAI_1[i].enabled = true;
                        textScorePlayer_1[i].enabled = true;
                        textLose1[i].SetActive(true);
                        if (PlayerPrefs.GetInt("listScoreAI_1" + i) > 0)
                        {
                            Debug.Log("thua ma");
                            textLose1[i].GetComponent<Text>().text = "Lose: ";
                        }
                        else if (PlayerPrefs.GetInt("listScoreAI_1" + i) == 0)
                        {
                            Debug.Log("hoaf ma");
                            textLose1[i].GetComponent<Text>().text = "Draw: ";
                        }
                    }

                    winOrLose[i].text = "";
                    textScoreAI_2[i].text = "";
                    textScorePlayer_2[i].text = "";
                    textTotalAI[i].text = "";
                    textTotalPlayer[i].text = "";
                    dauGach[i].text = "";
                }
            }
            PlayerPrefs.SetInt("teamAI", PlayerPrefs.GetInt("listAIRound" + _matchChampion));
            if (_matchChampion > 2)
            {
                content.GetComponent<RectTransform>().localPosition = new Vector2(-400 - (_matchChampion - 1) * 215, 0);
            }
        }
        else
        {
            for (int i = 0; i < head_AI.Length; i++)
            {

                if (PlayerPrefs.GetInt("listScoreAI_1" + i) + PlayerPrefs.GetInt("listScoreAI_2" + i)
                         < PlayerPrefs.GetInt("listScorePlayer_1" + i) + PlayerPrefs.GetInt("listScorePlayer_2" + i)
                       )
                {
                    panelCup.SetActive(true);
                    StartCoroutine(GetEffectFireword());

                }
            }
        }

        for (int i = 0; i < 9; i++)
        {
            if (i <= _matchChampion)
            {
                img_target[i].sprite = sp_target[1];
            }
            else
            {
                img_target[i].sprite = sp_target[0];
            }

        }


    }

    // Update is called once per frame
    void Update()
    {
        txt_Money.text = PlayerPrefs.GetInt("money").ToString();
    }

    IEnumerator GetEffectFireword()
    {
        yield return new WaitForSeconds(0.25f);
        Instantiate(effect_Fireword, new Vector2(0, 4), Quaternion.identity);

    }

    public void SetupAIRound()
    {
        List<int> _teamChampionCup = new List<int> { 29, 28, 26, 14, 31, 24, 5, 27, 9, 6, 25, 4, 1, 12, 11, 3 };
        listAIRound.Clear();
        for (int i = 0; i < head_AI.Length; i++)
        {
            int _index = Random.Range(0, _teamChampionCup.Count);
            listAIRound.Add(_teamChampionCup[_index]);
            _teamChampionCup.RemoveAt(_index);
        }

        for (int i = 0; i < listAIRound.Count; i++)
        {
            PlayerPrefs.SetInt("listAIRound" + i, listAIRound[i]);
            PlayerPrefs.SetInt("listScorePlayer_1" + i, 0);
            PlayerPrefs.SetInt("listScoreAI_1" + i, Random.Range(0, 3));
        }
        PlayerPrefs.SetInt("setupRoundChampionCup", 1);

    }

    public void ButtonBack()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        SceneManager.LoadScene("Menu");
    }

    public void ButtonStart()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        SceneManager.LoadScene("SetupStadium");
    }



    public void ButtonRenew()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        panelRenew.SetActive(false);
        PlayerPrefs.SetInt("setupRoundChampionCup", 0);
        PlayerPrefs.SetInt("matchChampion", 0);
        SceneManager.LoadScene("ChampionCup");
    }

    public void ButtonGetPrize()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }

    }

    
}
