//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Networking;
//using UnityEngine.UI;

//public class MyTeam : MonoBehaviour
//{
//    public static MyTeam instance;
//    public int id, amountMoney, id2;

//    public Image img_speed, img_jumb, img_shoot, img_Size;
//    public int[] indextPercentMaxShoot, indextPercentMaxSpeed, indextPercentMaxJump, indextPercentMaxSize;
//    public float shootMax, jumpMax, speedMax, sizeMax;
//    public Text textSpeedCurrent, textJumpcurrent, textShootCurrent, textSizeCurrent;

//    private void Awake()
//    {
//        if (instance == null)
//        {
//            instance = this;
//        }
//    }
//    // Use this for initialization
//    void Start()
//    {

//        id = PlayerPrefs.GetInt(StringUtils.id_player, 17);
//        id2 = PlayerPrefs.GetInt(StringUtils.id_player, 17);

//        amountMoney = PlayerPrefs.GetInt("money");

//        shootMax = 12f;
//        speedMax = 450f;
//        jumpMax = 11f;
//        sizeMax = 1.2f;

//        float shot = 1.3f * Mathf.Sqrt(SelectTeam.instance.x_dir_Shoot[id] * SelectTeam.instance.x_dir_Shoot[id] +
//            SelectTeam.instance.y_dir_Shoot[id] * SelectTeam.instance.y_dir_Shoot[id]);

//        img_jumb.fillAmount = SelectTeam.instance._jumpForce[id] / jumpMax;
//        img_speed.fillAmount = SelectTeam.instance._velocity[id] / speedMax;
//        img_shoot.fillAmount = shot / shootMax;
//        img_Size.fillAmount = SelectTeam.instance._size[id] / sizeMax;


//    }

//    // Update is called once per frame
//    void Update()
//    {
//        //int _id = PlayerPrefs.GetInt("IDPlayer", 17);
//        shootMax = 12f;
//        speedMax = 450f;
//        jumpMax = 11f;
//        sizeMax = 1.2f;

//        textSpeedCurrent.text = (int)(img_speed.fillAmount * 100) + "/" + 100;
//        textJumpcurrent.text = (int)(img_jumb.fillAmount * 100) + "/" + 100;
//        textShootCurrent.text = (int)(img_shoot.fillAmount * 100) + "/" + 100;
//        textSizeCurrent.text = (int)(img_Size.fillAmount * 100) + "/" + 100;


//    }


//}
