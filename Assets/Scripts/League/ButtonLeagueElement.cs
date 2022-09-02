using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonLeagueElement : MonoBehaviour
{
    public Text txt_number_level;
    //public int number_level;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonLeague()
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }
        LeagueController.number_level_league = int.Parse(txt_number_level.text);
        switch (LeagueController.mode_league)
        {
            case 0:

                PlayerPrefs.SetInt(StringUtils.str_value_team_ai_league, LeagueController.lst_id_league_C[LeagueController.number_level_league - 1]);
                break;
            case 1:
                PlayerPrefs.SetInt(StringUtils.str_value_team_ai_league, LeagueController.lst_id_league_B[LeagueController.number_level_league - 1]);
                break;
            case 2:
                PlayerPrefs.SetInt(StringUtils.str_value_team_ai_league, LeagueController.lst_id_league_A[LeagueController.number_level_league - 1]);
                break;
            case 3:
                PlayerPrefs.SetInt(StringUtils.str_value_team_ai_league, LeagueController.lst_id_league_SUPER[LeagueController.number_level_league - 1]);
                break;
            case 4:
                PlayerPrefs.SetInt(StringUtils.str_value_team_ai_league, LeagueController.lst_id_league_LEGEN[LeagueController.number_level_league - 1]);
                break;
        }

        SceneManager.LoadScene("SetupStadium");
    }
}
