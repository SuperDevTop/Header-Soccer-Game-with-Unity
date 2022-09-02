using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InsSelectPlayer : MonoBehaviour
{
    public static InsSelectPlayer instance;
    public GameObject content, btn_player;
    public List<int> lst_open_player = new List<int>();
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        //else
        //{
        //    DestroyImmediate(gameObject);
        //}
    }
    void Start()
    {
        lst_open_player.Clear();
        for (int i = 0; i < SelectTeam.instance.AI_Head.Length; i++)
        {
            if (i < 5)
            {
                PlayerPrefs.SetInt(StringUtils.str_open_head + i, i + 1);
            }
            else
            {
                int abc = PlayerPrefs.GetInt(StringUtils.str_open_head + i, 0);
                PlayerPrefs.SetInt(StringUtils.str_open_head + i, abc);
            }
            lst_open_player.Add(PlayerPrefs.GetInt(StringUtils.str_open_head + i, 0));
        }

        for (int i = 0; i < SelectTeam.instance.AI_Head.Length; i++)
        {
            GameObject _player = Instantiate(btn_player, content.transform);
            _player.GetComponent<ElementPlayer>().img_head.sprite = SelectTeam.instance.AI_Head[i];
            _player.GetComponent<ElementPlayer>().id = i + 1;

            for (int j = 0; j < lst_open_player.Count; j++)
            {
                if(i + 1 == lst_open_player[j])
                {
                    _player.GetComponent<ElementPlayer>().lock_player.SetActive(false);
                    _player.GetComponent<ElementPlayer>().img_head.color = Color.white;
                    _player.GetComponent<Image>().color = Color.white;
                    _player.GetComponent<ElementPlayer>().img_legend.color = Color.white;
                    break;
                }
            }

            if (i < 10)
            {
                _player.GetComponent<ElementPlayer>().img_legend.sprite = SelectTeam.instance.sp_Legend[0];
            }
            else if (i < 20)
            {
                _player.GetComponent<ElementPlayer>().img_legend.sprite = SelectTeam.instance.sp_Legend[1];
            }
            else if (i < 30)
            {
                _player.GetComponent<ElementPlayer>().img_legend.sprite = SelectTeam.instance.sp_Legend[2];
            }
            else if (i < 38)
            {
                _player.GetComponent<ElementPlayer>().img_legend.sprite = SelectTeam.instance.sp_Legend[3];
            }
            else if (i < 46)
            {
                _player.GetComponent<ElementPlayer>().img_legend.sprite = SelectTeam.instance.sp_Legend[4];
            }
        }

        
        Scene sc = SceneManager.GetActiveScene();
        switch (sc.name)
        {
            case "Menu":
                Debug.Log("alsdfjsdf : " + PlayerPrefs.GetInt(StringUtils.id_player, 1));
                Menu.instance.name_player.text = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.id_player, 1) - 1].name.Substring(2);
                Menu.instance.img_player.sprite = SelectTeam.instance.AI_Head[PlayerPrefs.GetInt(StringUtils.id_player, 1) - 1];

                break;
            case "SetupStadium":
                SetupStatium.instance.txt_name_player.text = "Select Player";
                break;
            case "FriendMatch":
                FriendMatchController.instance.txt_name_player.text = "Select Player";
                FriendMatchController.instance.txt_name_ai.text = "Select Computer";
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {

    }


}
