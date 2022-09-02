using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyLoginBonus : MonoBehaviour
{
    public Text txt_day, txt_numbercoin_reward;
    public GameObject obj_ok, obj_head;
    public Sprite spr_ok_daily;
    public Image img_coin;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ButtonGetPrize()
    {
        
        DailyController.instance.ButtonGetPrize();

    }
}
