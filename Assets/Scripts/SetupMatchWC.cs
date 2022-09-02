using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupMatchWC : MonoBehaviour
{
    public static SetupMatchWC instance;
    public int isMatchStage, wcPlayer, wcAI;
    public int[,] ValueTeam = new int[4, 8];
    public List<int> ValuePlayer1 = new List<int>();
    public List<int> ValuePlayer2 = new List<int>();
    public List<int> TopPlayer1 = new List<int>();
    public List<int> TopPlayer2 = new List<int>();
    public int[,] ValueTeamPlayOff = new int[2, 8];
    public int[,] ValueTeamFirtOff = new int[2, 4];
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
        isMatchStage = PlayerPrefs.GetInt("isStage1", 1);
        wcAI = PlayerPrefs.GetInt("wcAI", 1);

        if (Menu.mode == (int)Menu.MODE.WORLDCUP && PlayerPrefs.GetInt("isSelectTeamWC", 0) != 0)
        {
            SetupMatchStage();
            Debug.Log("wcPlayerwcPlayerwcPlayerwcPlayerwcPlayerwcPlayer    " + PlayerPrefs.GetInt("wcPlayer"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        wcPlayer = PlayerPrefs.GetInt("wcPlayer");
        //Debug.Log("wcPlayerwcPlayerwcPlayerwcPlayerwcPlayerwcPlayerAIIIIIIIIIII    " + PlayerPrefs.GetInt("wcAI"));
    }

    public void SetupMatchStage()
    {

        int[] ValueTop1 = new int[8];
        int[] ValueTop2 = { 23, 24, 25, 26, 27, 28, 29, 30 };
        int[] ValueTop3 = { 31, 32, 33, 34, 35, 36, 37, 38 };
        int[] ValueTop4 = { 39, 40, 41, 42, 43, 44, 45, 46 };
        List<int> listTop1 = new List<int>();
        List<int> listTop2 = new List<int>();
        List<int> listTop3 = new List<int>();
        List<int> listTop4 = new List<int>();

        for (int i = 0; i < ValueTop1.Length; i++)
        {
            ValueTop1[i] = PlayerPrefs.GetInt(StringUtils.str_list_top_1 + i);
        }

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                ValueTeam[i, j] = PlayerPrefs.GetInt("valueTeam" + "[" + i + "," + j + "]", 1);
            }

        }

        //Add top
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                for (int k = 0; k < 8; k++)
                {
                    if (ValueTeam[i, j] == ValueTop1[k])
                    {
                        listTop1.Add(1);
                    }
                    else if (ValueTeam[i, j] == ValueTop2[k])
                    {
                        listTop2.Add(2);
                    }
                    else if (ValueTeam[i, j] == ValueTop3[k])
                    {
                        listTop3.Add(3);
                    }
                    else listTop4.Add(4);
                }

            }

        }

        switch (isMatchStage)
        {

            case 1:
                // add top && value index

                for (int j = 0; j < 8; j++)
                {
                    TopPlayer1.Add(listTop1[j]);
                    TopPlayer2.Add(listTop2[j]);

                    for (int i = 0; i < 4; i++)
                    {
                        for (int k = 0; k < 8; k++)
                        {
                            if (ValueTeam[i, j] == ValueTop1[k])
                            {
                                ValuePlayer1.Add(ValueTeam[i, j]);
                            }
                            if (ValueTeam[i, j] == ValueTop2[k])
                            {
                                ValuePlayer2.Add(ValueTeam[i, j]);
                            }
                        }

                    }
                }

                for (int j = 0; j < 8; j++)
                {
                    TopPlayer1.Add(listTop3[j]);
                    TopPlayer2.Add(listTop4[j]);

                    for (int i = 0; i < 4; i++)
                    {
                        for (int k = 0; k < 8; k++)
                        {
                            if (ValueTeam[i, j] == ValueTop3[k])
                            {
                                ValuePlayer1.Add(ValueTeam[i, j]);
                            }
                            if (ValueTeam[i, j] == ValueTop4[k])
                            {
                                ValuePlayer2.Add(ValueTeam[i, j]);
                            }
                        }
                    }
                }

                for (int i = 0; i < ValuePlayer1.Count; i++)
                {

                    if (ValuePlayer1[i] == PlayerPrefs.GetInt("wcPlayer") || ValuePlayer2[i] == PlayerPrefs.GetInt("wcPlayer"))
                    {
                        if (ValuePlayer1[i] == PlayerPrefs.GetInt("wcPlayer"))
                        {
                            wcAI = ValuePlayer2[i];
                            PlayerPrefs.SetInt("wcAI", wcAI);

                        }
                        else if (ValuePlayer2[i] == PlayerPrefs.GetInt("wcPlayer"))
                        {
                            wcAI = ValuePlayer1[i];
                            PlayerPrefs.SetInt("wcAI", wcAI);
                        }

                        ValuePlayer1.RemoveAt(i);
                        ValuePlayer2.RemoveAt(i);
                        TopPlayer1.RemoveAt(i);
                        TopPlayer2.RemoveAt(i);
                    }
                }

                break;
            case 2:
                for (int j = 0; j < 8; j++)
                {
                    TopPlayer1.Add(listTop1[j]);
                    TopPlayer2.Add(listTop3[j]);

                    for (int i = 0; i < 4; i++)
                    {
                        for (int k = 0; k < 8; k++)
                        {
                            if (ValueTeam[i, j] == ValueTop1[k])
                            {
                                ValuePlayer1.Add(ValueTeam[i, j]);
                            }
                            if (ValueTeam[i, j] == ValueTop3[k])
                            {
                                ValuePlayer2.Add(ValueTeam[i, j]);
                            }
                        }

                    }
                }

                for (int j = 0; j < 8; j++)
                {
                    TopPlayer1.Add(listTop2[j]);
                    TopPlayer2.Add(listTop4[j]);

                    for (int i = 0; i < 4; i++)
                    {
                        for (int k = 0; k < 8; k++)
                        {
                            if (ValueTeam[i, j] == ValueTop2[k])
                            {
                                ValuePlayer1.Add(ValueTeam[i, j]);
                            }
                            if (ValueTeam[i, j] == ValueTop4[k])
                            {
                                ValuePlayer2.Add(ValueTeam[i, j]);
                            }
                        }
                    }
                }

                for (int i = 0; i < ValuePlayer1.Count; i++)
                {

                    if (ValuePlayer1[i] == PlayerPrefs.GetInt("wcPlayer") || ValuePlayer2[i] == PlayerPrefs.GetInt("wcPlayer"))
                    {
                        if (ValuePlayer1[i] == PlayerPrefs.GetInt("wcPlayer"))
                        {
                            wcAI = ValuePlayer2[i];
                            PlayerPrefs.SetInt("wcAI", wcAI);

                        }
                        else if (ValuePlayer2[i] == PlayerPrefs.GetInt("wcPlayer"))
                        {
                            wcAI = ValuePlayer1[i];
                            PlayerPrefs.SetInt("wcAI", wcAI);
                        }

                        ValuePlayer1.RemoveAt(i);
                        ValuePlayer2.RemoveAt(i);
                        TopPlayer1.RemoveAt(i);
                        TopPlayer2.RemoveAt(i);
                    }
                }

                break;
            case 3:
                for (int j = 0; j < 8; j++)
                {
                    TopPlayer1.Add(listTop1[j]);
                    TopPlayer2.Add(listTop4[j]);

                    for (int i = 0; i < 4; i++)
                    {
                        for (int k = 0; k < 8; k++)
                        {
                            if (ValueTeam[i, j] == ValueTop1[k])
                            {
                                ValuePlayer1.Add(ValueTeam[i, j]);
                            }
                            if (ValueTeam[i, j] == ValueTop4[k])
                            {
                                ValuePlayer2.Add(ValueTeam[i, j]);
                            }
                        }

                    }
                }

                for (int j = 0; j < 8; j++)
                {
                    TopPlayer1.Add(listTop2[j]);
                    TopPlayer2.Add(listTop3[j]);

                    for (int i = 0; i < 4; i++)
                    {
                        for (int k = 0; k < 8; k++)
                        {
                            if (ValueTeam[i, j] == ValueTop2[k])
                            {
                                ValuePlayer1.Add(ValueTeam[i, j]);
                            }
                            if (ValueTeam[i, j] == ValueTop3[k])
                            {
                                ValuePlayer2.Add(ValueTeam[i, j]);
                            }
                        }
                    }
                }

                for (int i = 0; i < ValuePlayer1.Count; i++)
                {

                    if (ValuePlayer1[i] == PlayerPrefs.GetInt("wcPlayer") || ValuePlayer2[i] == PlayerPrefs.GetInt("wcPlayer"))
                    {
                        if (ValuePlayer1[i] == PlayerPrefs.GetInt("wcPlayer"))
                        {
                            wcAI = ValuePlayer2[i];
                            PlayerPrefs.SetInt("wcAI", wcAI);

                        }
                        else if (ValuePlayer2[i] == PlayerPrefs.GetInt("wcPlayer"))
                        {
                            wcAI = ValuePlayer1[i];
                            PlayerPrefs.SetInt("wcAI", wcAI);
                        }

                        ValuePlayer1.RemoveAt(i);
                        ValuePlayer2.RemoveAt(i);
                        TopPlayer1.RemoveAt(i);
                        TopPlayer2.RemoveAt(i);
                    }
                }
                break;
            case 4:
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

                        if (ValueTeamPlayOff[0, j] == PlayerPrefs.GetInt("wcPlayer"))
                        {
                            wcAI = ValueTeamPlayOff[1, j];
                            PlayerPrefs.SetInt("wcAI", wcAI);
                        }
                        else if (ValueTeamPlayOff[1, j] == PlayerPrefs.GetInt("wcPlayer"))
                        {
                            wcAI = ValueTeamPlayOff[0, j];
                            PlayerPrefs.SetInt("wcAI", wcAI);

                        }

                    }
                }
                break;
            case 5:
                // setup in WCController
                break;

        }

    }


}
