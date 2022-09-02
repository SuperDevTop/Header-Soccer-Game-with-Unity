using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class BuyProductIDShop : MonoBehaviour
{
    public static BuyProductIDShop instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {

    }


    public void Buy(GameObject pack)
    {
        if (PlayerPrefs.GetInt(GameConstants.SOUND, 1) == 1)
        {
            SoundManager.Instance.buttonClick.Play();
        }

		if (pack.name == "Pack1")
		{
			CompleteProject.Purchaser.instance.BuyProductID("com.headsoccer.headball.footballgame.pack1");
		}

		if (pack.name == "Pack2")
		{
			CompleteProject.Purchaser.instance.BuyProductID("com.headsoccer.headball.footballgame.pack2");
		}

		if (pack.name == "Pack3")
		{
			CompleteProject.Purchaser.instance.BuyProductID("com.headsoccer.headball.footballgame.pack3");
		}

		if (pack.name == "Pack4")
		{
			CompleteProject.Purchaser.instance.BuyProductID("com.headsoccer.headball.footballgame.pack4");
		}
		if (pack.name == "Pack5")
		{
			CompleteProject.Purchaser.instance.BuyProductID("com.headsoccer.headball.footballgame.pack5");
		}
		if (pack.name == "Pack6")
		{
			CompleteProject.Purchaser.instance.BuyProductID("com.headsoccer.headball.footballgame.pack6");
		}
		if (pack.name == "PackSale1")
		{
			CompleteProject.Purchaser.instance.BuyProductID("com.headsoccer.headball.footballgame.pack7");
		}
		if (pack.name == "PackSale2")
		{
			CompleteProject.Purchaser.instance.BuyProductID("com.headsoccer.headball.footballgame.pack8");
		}
		if (pack.name == "PackSale3")
		{
			CompleteProject.Purchaser.instance.BuyProductID("com.headsoccer.headball.footballgame.pack9");
		}

	}

    private void Update()
    {
      
    }

   
}
