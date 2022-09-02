using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectTeam : MonoBehaviour
{
    public static SelectTeam instance;
    public Sprite[] star;
    //public int[] Top_Star;
    public Sprite[] AI_Head;
    public Sprite[] sp_Stadiums;
    public Sprite[] sp_shoe, sp_Body;

    public Sprite[] sp_ball;
    public Sprite[] sp_Legend;
    public static List<int> lst_id_ai = new List<int>();
    public int[] kn_level_user;

    // Use this for initialization

    private void Awake()
    {
        Application.targetFrameRate = 60;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            //SetupInfoPlayer();
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
    private void Start()
    {
        lst_id_ai.Clear();
        for (int i = 1; i < AI_Head.Length + 1; i++)
        {
            lst_id_ai.Add(i);
        }
    }

}
