///*using AudienceNetwork;*/
//using GoogleMobileAds.Api;
//using GoogleMobileAds.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public interface RewardListener
{
	void onRewarded();
	void onRewardFailed();
	void onRewardClosed();
}


public class AdsManager : MonoBehaviour
{
	private static AdsManager _instance;

	[Header("Bắt buộc")]
	public bool isTesting = true;

	public bool isHasFull = true;
	public bool isHasBanner = false;
	//public GoogleMobileAds.Api.AdPosition bannerPosition = GoogleMobileAds.Api.AdPosition.Top;
	public bool isHasReward = false;
	public bool isFBAudience = true;
	public bool isFbAdsLoaded = false;
	public bool isRewardFBLoaded = false;
	public bool isFacebookRewardvideo = true;
	public int timeBetweenShowAds = 40;


	[Header("Dành cho Admod ANDROID")]
	public string idAppAndroid;
	public string idBannerAndroid;
	public string idFullAndroid;
	public string idRewardAndroid;
	public string idOpenAppAndroid;

	[Header("Dành cho Facebook Android")]
	public string id_rewardvideo_FB;
	public string id_fullAds_FB;

	[Header("Dành cho IOS")]
	public string idAppIos;
	public string idFullAdmobIos;
	public string idRewardIos;
	public string idBannerIos;

	//private AppOpenAd adOpen;
	//private GoogleMobileAds.Api.InterstitialAd interstitial;
	//public RewardedAd rewardBasedVideo;
	//private BannerView bannerView;
	private DateTime lastTimeShowAds;

	/*private AudienceNetwork.InterstitialAd interstitialFb;*/
	//private BannerView bannerView_FB;
	//public AudienceNetwork.RewardedVideoAd rewardBasedVideoFB;

	private string idApp = "ca-app-pub-3940256099942544~3347511713";
	private string idBanner = "ca-app-pub-3940256099942544/6300978111";
	private string idFull = "ca-app-pub-3940256099942544/1033173712";
	private string idReward = "ca-app-pub-3940256099942544/5224354917";
	private string idOpenApp = "ca-app-pub-3940256099942544/3419835294";
	private bool isShowBanner = true;
	private bool isBannerLoaded = false;
	public int numberReward;
	public static string mode_show_ful_ads;
	public static string mode_show_ful_reward;
	private bool isShowingAdOpen = false;

	public static AdsManager Instance
	{
		get
		{
			return _instance;
		}
	}

	void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			DestroyImmediate(gameObject);
		}
	}

	// Use this for initialization
	void Start()
	{
		timeBetweenShowAds = 40;
		if (!isTesting)
		{
#if UNITY_ANDROID
			idApp = idAppAndroid;
			idBanner = idBannerAndroid;
			idFull = idFullAndroid;
			idReward = idRewardAndroid;
			idOpenApp = idOpenAppAndroid;
#elif UNITY_IOS
        idApp = idAppIos;
        idBanner = idBannerIos;
        idFull = idFullAdmobIos;
        idReward = idRewardIos;
#endif
		}
		//MobileAds.Initialize(initStatus => { });
		lastTimeShowAds = DateTime.MinValue;
		//LoadAdOpenApp();

		//if (isHasFull)
		//	requestFullAds();

		//if (isHasBanner)
		//	requestBannerAmod();

		/*  if (isFBAudience == true)
		  {
			  requestFullFbAds();
			  if (isFacebookRewardvideo == true)
			  {
				  LoadRewardedVideoFB();
			  }
		  }*/

		//if (isHasReward == true)
		//{
		//	this.rewardBasedVideo = new RewardedAd(idReward);

		//	requestRewardAds();

		//}
	}

	// Ad open app
	//public void LoadAdOpenApp()
	//{
	//	AdRequest request = new AdRequest.Builder().Build();

	//	// Load an app open ad for portrait orientation
	//	AppOpenAd.LoadAd(idOpenApp, ScreenOrientation.Portrait, request, ((appOpenAd, error) =>
	//	{
	//		if (error != null)
	//		{
	//			// Handle the error.
	//			Debug.LogFormat("Failed to  load the ad. (reason: {0})", error.LoadAdError.GetMessage());
	//			return;
	//		}

	//		// App open ad is loaded.
	//		this.adOpen = appOpenAd;
	//	}));
	//}
	//public void ShowAdIfAvailableOpenApp()
	//{
	//	if (this.adOpen == null || isShowingAdOpen)
	//	{
	//		return;
	//	}

	//	this.adOpen.OnAdDidDismissFullScreenContent += HandleAdDidDismissFullScreenContent;
	//	this.adOpen.OnAdFailedToPresentFullScreenContent += HandleAdFailedToPresentFullScreenContent;
	//	this.adOpen.OnAdDidPresentFullScreenContent += HandleAdDidPresentFullScreenContent;
	//	this.adOpen.OnAdDidRecordImpression += HandleAdDidRecordImpression;
	//	this.adOpen.OnPaidEvent += HandlePaidEvent;

	//	this.adOpen.Show();
	//}

	//private void HandleAdDidDismissFullScreenContent(object sender, EventArgs args)
	//{
	//	Debug.Log("Closed app open ad");
	//	// Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
	//	adOpen = null;
	//	isShowingAdOpen = false;
	//	LoadAdOpenApp();
	//}

	//private void HandleAdFailedToPresentFullScreenContent(object sender, AdErrorEventArgs args)
	//{
	//	Debug.LogFormat("Failed to present the ad (reason: {0})", args.AdError.GetMessage());
	//	// Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
	//	adOpen = null;
	//	LoadAdOpenApp();
	//}

	//private void HandleAdDidPresentFullScreenContent(object sender, EventArgs args)
	//{
	//	Debug.Log("Displayed app open ad");
	//	isShowingAdOpen = true;
	//}

	//private void HandleAdDidRecordImpression(object sender, EventArgs args)
	//{
	//	Debug.Log("Recorded ad impression");
	//}

	//private void HandlePaidEvent(object sender, AdValueEventArgs args)
	//{
	//	Debug.LogFormat("Received paid event. (currency: {0}, value: {1}",
	//			args.AdValue.CurrencyCode, args.AdValue.Value);
	//}


	//------------------------------------------------------------------------//
	//
	private void requestBannerAmod()
	{

		//float widthInPixels =
		//Screen.safeArea.width > 0 ? Screen.safeArea.width : Screen.width;
		//int width = (int)(widthInPixels / MobileAds.Utils.GetDeviceScale());
		//MonoBehaviour.print("requesting width: " + width.ToString());
		//GoogleMobileAds.Api.AdSize adaptiveSize =
		//		GoogleMobileAds.Api.AdSize.SmartBanner;
		//bannerView = new BannerView(idBanner, adaptiveSize, bannerPosition);
		//bannerView.LoadAd(createAdRequest());

		//bannerView.OnAdLoaded += BannerView_OnAdLoaded;
	}

	private void BannerView_OnAdLoaded(object sender, EventArgs e)
	{
		//isBannerLoaded = true;
		//if (!isShowBanner)
		//    bannerView.Hide();
	}

	//private AdRequest createAdRequest()
	//{
	//    return new AdRequest.Builder()
	//            .Build();

	//}

	public void showBanner()
	{
		//isShowBanner = true;
		//if (bannerView != null && isBannerLoaded)
		//{
		//    bannerView.Show();
		//}
	}


	public void hideBanner()
	{
		//isShowBanner = false;
		//if (bannerView != null && isBannerLoaded)
		//{
		//    bannerView.Hide();
		//}
	}

	//private void requestFullAds()
	//{
	//	if (this.interstitial != null)
	//	{
	//		this.interstitial.OnAdClosed -= Interstitial_OnAdClosed;
	//		this.interstitial.Destroy();
	//	}

	//	this.interstitial = new GoogleMobileAds.Api.InterstitialAd(idFull);
	//	this.interstitial.OnAdClosed += Interstitial_OnAdClosed;
	//	// Initialize an InterstitialAd.
	//	//this.interstitial = new InterstitialAd(adUnitId);
	//	// Create an empty ad request.
	//	AdRequest request = new AdRequest.Builder().Build();
	//	// Load the interstitial with the request.
	//	this.interstitial.LoadAd(request);

	//	//interstitial.LoadAd(createAdRequest());
	//}

	//private void Interstitial_OnAdClosed(object sender, EventArgs e)
	//{
	//	ShowUIFullAds();
	//	requestFullAds();
	//}

	public void showFullAds()
	{
		if (lastTimeShowAds == null || PlayerPrefs.GetInt("haskAds", 0) == 1)
		{
			ShowUIFullAds();
			return;

		}

		DateTime current = DateTime.Now;
		TimeSpan between = current.Subtract(lastTimeShowAds);
		if (between.TotalSeconds <= timeBetweenShowAds)
		{
			ShowUIFullAds();
			return;

		}

		FullUnityAds.instance.ShowAd();
		lastTimeShowAds = DateTime.Now;
		//if (this.interstitial != null && this.interstitial.IsLoaded())
		//{
		//	//if (Menu.mode == (int)Menu.MODE.SIRVIVAL)
		//	//{
		//	//	Time.timeScale = 0;
		//	//}
		//	this.interstitial.Show();
		//	lastTimeShowAds = DateTime.Now;
		//}
		//else
		//{
		//	ShowUIFullAds();
		//	//requestFullAds();
		//	/*requestFullFbAds();*/
		//}
		//if (this.isFbAdsLoaded)
		//{
		//	if (Menu.mode == (int)Menu.MODE.SIRVIVAL)
		//	{
		//		Time.timeScale = 0;
		//	}
		//	/* this.interstitialFb.Show();*/
		//	this.isFbAdsLoaded = false;
		//	lastTimeShowAds = DateTime.Now;
		//	/*FirebaseAnalyticManager.instance.AnalyticsShowFullAds();*/
		//}
	}

	//private void requestRewardAds()
	//{
	//	//rewardBasedVideo = new RewardedAd(idReward);
	//	this.rewardBasedVideo.OnUserEarnedReward += HandleUserEarnedReward;
	//	this.rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
	//	this.rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
	//	AdRequest request = new AdRequest.Builder().Build();
	//	this.rewardBasedVideo.LoadAd(request);
	//	//rewardBasedVideo.LoadAd(createAdRequest(), idReward);
	//}

	public void showRewardAdsVideo()
	{

		RewardUnityAds.instance.ShowAd();

		//if (this.isRewardFBLoaded && rewardBasedVideoFB != null)
		//{
		//	//FBManager.instance.LogShowReward();

		//	this.rewardBasedVideoFB.Show();
		//	this.isRewardFBLoaded = false;
		//	*//*FirebaseAnalyticManager.instance.AnalyticsShowReward();*//*

		//}
		//if (this.rewardBasedVideo.IsLoaded())
		//{
		//	//UnityAds.instance.OnUnityAdsReady("rewardedVideo");
		//	//this.rewardBasedVideo.Show();
		//}
		//else
		//{
		//	Scene sc = SceneManager.GetActiveScene();
		//	if (sc.name == "FullTime")
		//	{
		//		if (mode_show_ful_reward == "ContinueSurvival")
		//		{
		//			FullTime.instance.canloadvideo.SetActive(true);
		//		}
		//		else if (mode_show_ful_reward == "X2Money")
		//		{
		//			FullTime.instance.cantloadvideo_x2.SetActive(true);
		//		}

		//	}
		//	else if (sc.name == "Menu")
		//	{
		//		if (mode_show_ful_reward == "GetPrizeDailyX2")
		//		{
		//			DailyController.instance.obj_cant_ads.SetActive(true);
		//		}
		//		else
		//		{
		//			Dailygift.instance.txt_cantloadvideo.gameObject.SetActive(true);
		//		}

		//	}
		//	StartCoroutine(waitHideTextCantloadAds());
		//	//requestRewardAds();
		//	/*LoadRewardedVideoFB();*/
		//}
	}

	IEnumerator waitHideTextCantloadAds()
	{
		yield return new WaitForSeconds(2f);
		Scene sc = SceneManager.GetActiveScene();
		if (sc.name == "Game")
		{
			GameController.Instance.cantLoadAds.SetActive(false);
		}
		else if (sc.name == "FullTime")
		{
			if (mode_show_ful_reward == "ContinueSurvival")
			{
				FullTime.instance.canloadvideo.SetActive(false);
			}
			else if (mode_show_ful_reward == "X2Money")
			{
				FullTime.instance.cantloadvideo_x2.SetActive(false);
			}
		}
		else if (sc.name == "Menu")
		{
			if (mode_show_ful_reward == "GetPrizeDailyX2")
			{
				DailyController.instance.obj_cant_ads.SetActive(false);
			}
			else
			{
				Dailygift.instance.txt_cantloadvideo.gameObject.SetActive(false);
			}
		}
	}

	//private void HandleUserEarnedReward(object sender, Reward e)
	//{
	//	RewardOK();
	//}

	public void RewardOK()
	{
		Debug.Log("OKKKKKKKKKKKKK Reward");
		Scene sc = SceneManager.GetActiveScene();
		if (sc.name == "Game")
		{
			Time.timeScale = 1;
			GameController.int_SaveMe--;
			GameController.Instance.panelSaveMe.SetActive(false);

			switch (Menu.mode)
			{
				case (int)Menu.MODE.WORLDCUP:
					int isMatchStage = PlayerPrefs.GetInt("isStage1", 1);
					isMatchStage--;
					PlayerPrefs.SetInt("isStage1", isMatchStage);
					break;

			}
			SceneManager.LoadScene("Game");
		}
		else if (sc.name == "FullTime")
		{
			if (mode_show_ful_reward == "ContinueSurvival")
			{
				int life = PlayerPrefs.GetInt(StringUtils.life_player_survival, 0);
				life += 2;
				PlayerPrefs.SetInt(StringUtils.life_player_survival, life);
				SceneManager.LoadScene("Game");
			}
			else if (mode_show_ful_reward == "X2Money")
			{
				int m1 = PlayerPrefs.GetInt(StringUtils.str_money);
				m1 += FullTime.instance.amountMoneyMatch;
				PlayerPrefs.SetInt(StringUtils.str_money, m1);
				FullTime.instance.text_AmoutMoneyMatch.text = (2 * FullTime.instance.amountMoneyMatch) + "";
				FullTime.instance.btnx2.SetActive(false);
			}

		}
		else if (sc.name == "Menu")
		{
			if (mode_show_ful_reward == "GetRewardAds")
			{
				int dl = PlayerPrefs.GetInt(StringUtils.str_watchads_daily, 0);
				PlayerPrefs.SetString(StringUtils.str_time_daily, DateTime.Now.ToString());
				StartCoroutine(Dailygift.instance.MoveCoin(Dailygift.instance.btn_dailygift.gameObject, 1, DailyController.instance.obj_coin_taget, 1f));
				StartCoroutine(Addcoin(2f));
				dl++;
				PlayerPrefs.SetInt(StringUtils.str_watchads_daily, dl);
			}
			if (mode_show_ful_reward == "GetPrizeDailyX2")
			{
				DailyController.instance.GetPrizeX2();
			}
		}
	}

	IEnumerator Addcoin(float t)
	{
		int dl = PlayerPrefs.GetInt(StringUtils.str_watchads_daily, 0);
		yield return new WaitForSeconds(t);
		int mn = PlayerPrefs.GetInt(StringUtils.str_money, 0);
		mn += (350 + dl * 10);
		PlayerPrefs.SetInt(StringUtils.str_money, mn);
	}


	//private void HandleRewardBasedVideoClosed(object sender, EventArgs args)
	//{
	//	requestRewardAds();
	//}

	//private void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	//{
	//	//txt_full.text = " reward: Loi to";
	//}

	private bool isDevMode()
	{
		AndroidJavaClass clsUnity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject objActivity = clsUnity.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaObject objResolver = objActivity.Call<AndroidJavaObject>("getContentResolver");
		AndroidJavaClass clsSecure = new AndroidJavaClass("android.provider.Settings$Secure");
		int is_dev = clsSecure.CallStatic<int>("getInt", objResolver, "development_settings_enabled", 0);

		if (is_dev == 0)
			return false;
		else
			return true;
	}

	private bool isInstallFromGooglePlay()
	{
		if ("com.android.vending".Equals(Application.installerName))
			return true;
		else
			return false;
	}

	private bool isEnableAds()
	{
		if (isTesting)
			return true;

		if (isDevMode() && isInstallFromGooglePlay())
			return true;
		else
			return false;
	}


	//void OnDestroy()
	//{
	//    Debug.Log("InterstitialAdTest was destroyed!");
	//    if (interstitial != null)
	//        interstitial.Destroy();
	//    if (bannerView != null)
	//        bannerView.Destroy();

	//}

	public void ShowUIFullAds()
	{
		Scene _sceneCurrent = SceneManager.GetActiveScene();
		if (_sceneCurrent.name == "Game")
		{
			if (mode_show_ful_ads == "Setting")
			{
				UIManager.Instance.panelSetting.SetActive(true);
			}
			else if (mode_show_ful_ads == "Survival")
			{
				Time.timeScale = 1;
			}
		}
		else if (_sceneCurrent.name == "Training")
		{
			UIManager.Instance.panelSetting.SetActive(true);
		}
		else if (_sceneCurrent.name == "FullTime")
		{
			switch (mode_show_ful_ads)
			{
				case "Home":
					SceneManager.LoadScene("Menu");
					break;
				case "Replay":
					SceneManager.LoadScene("Game");
					break;
				case "Next":
					if (Menu.isTraining == false)
					{
						if (Menu.mode == (int)Menu.MODE.TRAINING)
						{
							SceneManager.LoadScene("Training");
						}
						else if (Menu.mode == (int)Menu.MODE.WORLDCUP)
						{
							SceneManager.LoadScene("WorldCup");
						}
						else if (Menu.mode == (int)Menu.MODE.LEAGUE)
						{
							SceneManager.LoadScene("League");
						}
						else if (Menu.mode == (int)Menu.MODE.FRIENDMATCH)
						{
							SceneManager.LoadScene("FriendMatch");
						}
						else if (Menu.mode == (int)Menu.MODE.SIRVIVAL)
						{
							SceneManager.LoadScene("SetupStadium");
						}
						else if (Menu.mode == (int)Menu.MODE.DAILY_CHALLENGE)
						{
							SceneManager.LoadScene("Menu");
						}

					}
					else
					{
						SceneManager.LoadScene("Menu");
					}
					break;
			}
		}
	}

	/* private void requestFullFbAds()
	 {
		 interstitialFb = new AudienceNetwork.InterstitialAd(id_fullAds_FB);

		 interstitialFb.Register(this.gameObject);
		 // Set delegates to get notified on changes or when the user interacts with the ad.
		 interstitialFb.InterstitialAdDidLoad = (delegate ()
		 {
			 Debug.Log("Interstitial ad loaded.");
			 this.isFbAdsLoaded = true;
		 });
		 interstitialFb.InterstitialAdDidFailWithError = (delegate (string error)
		 {
			 Debug.Log("Interstitial ad failed to load with error: " + error);
		 });
		 interstitialFb.InterstitialAdDidClose = (delegate ()
		 {
			 Debug.Log("Interstitial ad close.");
			 ShowUIFullAds();
			 this.isFbAdsLoaded = false;
		 });

		 // Initiate the request to load the ad.
		 interstitialFb.LoadAd();
	 }*/
	/*    public void showInterstitialFB()
		{
			if (this.isFbAdsLoaded)
			{
				this.interstitialFb.Show();
				this.isFbAdsLoaded = false;
			}
			else
			{
				requestFullFbAds();
			}
		}*/

	/* public void LoadRewardedVideoFB()
	 {
		 // Create the rewarded video unit with a placement ID (generate your own on the Facebook app settings).
		 // Use different ID for each ad placement in your app.
		 this.rewardBasedVideoFB = new RewardedVideoAd(id_rewardvideo_FB);

		 this.rewardBasedVideoFB.Register(this.gameObject);

		 // Set delegates to get notified on changes or when the user interacts with the ad.
		 this.rewardBasedVideoFB.RewardedVideoAdDidLoad = (delegate ()
		 {
			 Debug.Log("RewardedVideo ad loaded.");
			 this.isRewardFBLoaded = true;
		 });
		 this.rewardBasedVideoFB.RewardedVideoAdDidFailWithError = (delegate (string error)
		 {
			 Debug.Log("RewardedVideo ad failed to load with error: " + error);
		 });
		 this.rewardBasedVideoFB.RewardedVideoAdWillLogImpression = (delegate ()
		 {
			 Debug.Log("RewardedVideo ad logged impression.");
		 });
		 this.rewardBasedVideoFB.RewardedVideoAdDidClick = (delegate ()
		 {
			 Debug.Log("RewardedVideo ad clicked.");
		 });

		 this.rewardBasedVideoFB.RewardedVideoAdComplete = (delegate ()
		 {
			 RewardOK();

			 Debug.Log("RewardedVideo ad complete.");

			 LoadRewardedVideoFB();

		 });

		 this.rewardBasedVideoFB.RewardedVideoAdDidClose = (delegate ()
		 {
			 Debug.Log("Rewarded video ad did close.");
			 if (this.rewardBasedVideoFB != null)
			 {
				 this.rewardBasedVideoFB.Dispose();
			 }
			 LoadRewardedVideoFB();
		 });

		 // Initiate the request to load the ad.
		 this.rewardBasedVideoFB.LoadAd();
	 }*/

	/* public void ShowRewardedVideoFB()
	 {
		 if (this.isRewardFBLoaded && rewardBasedVideoFB != null)
		 {
			 this.rewardBasedVideoFB.Show();
			 this.isRewardFBLoaded = false;
		 }
		 else
		 {
			 //AdsManager.Instance.requestRewardBasedVideo();
			 LoadRewardedVideoFB();
			 Debug.Log("Ad not loaded. Click load to request an ad.");

		 }
	 }*/
}
