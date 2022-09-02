using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FriendMatchController : MonoBehaviour
{
    // Start is called before the first frame update
    public static FriendMatchController instance;
    public Text txt_name_player, txt_name_ai;
    public Image img_head_ai, img_head_player, img_body_ai, img_body_player, img_shoe_ai, img_shoe_player, img_star_player, img_star_ai;
    public List<int> lst_id_team = new List<int>(){17, 19, 30, 2, 7, 32, 13, 20, 8, 22, 23, 10, 16, 15, 18, 21,
        29, 26, 14, 28, 24, 5, 31, 27, 9, 6, 25, 4, 1, 12, 11, 3};

    public GameObject btn_start, btn_select_player, btn_select_com;
    public Sprite spr_select, spr_cancel;
    public static int id_head;
    public static string mode_friendmatch;
    public GameObject obj_mode;
    public Text txt_mode, txt_waiting_player, txt_waiting_ai, txt_ready;
    public GameObject obj_player, obj_com;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Menu.isTraining = false;
        switch (Menu.mode)
        {
            case (int)Menu.MODE.FRIENDMATCH:

                btn_select_player.SetActive(false);
                btn_select_com.SetActive(false);
                btn_start.SetActive(false);
                mode_friendmatch = "NORMAL";
                id_head = PlayerPrefs.GetInt(StringUtils.id_player, 1);
                StartCoroutine(SetupWaiting(txt_waiting_player));
                txt_waiting_ai.enabled = false;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(btn_start.activeSelf == true)
        {
            txt_ready.text = "Ready to match";
        }
        else
        {
            txt_ready.text = "Select Difficulty";
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
    public void ButtonHome()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        SceneManager.LoadScene("Menu");

    }

    public void EventMode(string str_mode)
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        mode_friendmatch = str_mode;
        txt_mode.text = mode_friendmatch;
        obj_mode.SetActive(false);

    }
    public void ButtonModeFriendMatch(Text txt_mode)
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }

        if (obj_mode.activeSelf == true)
        {
            obj_mode.SetActive(false);
        }
        else
        {
            obj_mode.SetActive(true);
        }

    }
    public void ButtonSelectPlayer(int id)
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        if (btn_select_player.GetComponent<Image>().sprite.name == "btnSelect")
        {
            btn_select_player.GetComponent<Image>().sprite = spr_cancel;
            btn_select_player.GetComponent<Image>().color = Color.white;
            btn_select_player.GetComponent<Animator>().enabled = false;
            PlayerPrefs.SetInt(StringUtils.id_player, id_head);
            id_head = PlayerPrefs.GetInt(StringUtils.value_team_ai_friendmatch, 1);
            txt_waiting_ai.enabled = true;
            txt_name_ai.text = "Select Computer";
            txt_name_ai.enabled = true;
            StartCoroutine(SetupWaiting(txt_waiting_ai));
        }
        else
        {

            txt_waiting_ai.enabled = false;
            txt_name_ai.enabled = false;
            obj_com.SetActive(false);
            obj_player.SetActive(false);
            btn_select_player.SetActive(false);
            btn_select_com.SetActive(false);
            btn_start.SetActive(false);
            txt_name_player.text = "Select Player";
            txt_waiting_player.enabled = true;
            StartCoroutine(SetupWaiting(txt_waiting_player));
            btn_select_player.GetComponent<Image>().sprite = spr_select;
            btn_select_com.GetComponent<Image>().sprite = spr_select;
            id_head = PlayerPrefs.GetInt(StringUtils.id_player, 1);
        }
    }
    public void ButtonSelectCom(int id)
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        if (btn_select_com.GetComponent<Image>().sprite.name == "btnSelect")
        {
            btn_select_com.SetActive(true);
            btn_select_com.GetComponent<Image>().sprite = spr_cancel;
            btn_select_com.GetComponent<Image>().color = Color.white;
            btn_start.SetActive(true);
            btn_select_com.GetComponent<Animator>().enabled = false;
            PlayerPrefs.SetInt(StringUtils.value_team_ai_friendmatch, id_head);
        }
        else
        {
            txt_name_ai.text = "Select Computer";
            btn_select_com.SetActive(false);
            txt_waiting_ai.enabled = true;
            txt_name_ai.enabled = true;
            obj_com.SetActive(false);
            StartCoroutine(SetupWaiting(txt_waiting_ai));
            btn_select_com.GetComponent<Image>().sprite = spr_select;
            btn_start.SetActive(false);
        }
    }
   

    public void ButtonStart()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();


        }
        switch (Menu.mode)
        {
            case (int)Menu.MODE.FRIENDMATCH:
                PlayerPrefs.SetInt("teamAI", PlayerPrefs.GetInt("team_AI_friendMatch", 0));
                SceneManager.LoadScene("SetupStadium");
                break;
        }
    }

   
}
