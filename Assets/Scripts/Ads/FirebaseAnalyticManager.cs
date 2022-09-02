/*using System;
using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using UnityEngine;

public class FirebaseAnalyticManager : MonoBehaviour
{
	public static FirebaseAnalyticManager instance;
	DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;
	protected bool firebaseInitialized = false;
	//FirebaseAnalytics firebase;
	// Start is called before the first frame update
	private void Awake()
	{
		if (instance == null)
		{

			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			DestroyImmediate(gameObject);
		}
	}
	void Start()
	{
		FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
		FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
		{
			dependencyStatus = task.Result;
			if (dependencyStatus == DependencyStatus.Available)
			{

				InitializeFirebase();
				FirebaseApp app = Firebase.FirebaseApp.DefaultInstance;
			}
			else
			{
				Debug.LogError(
				  "Could not resolve all Firebase dependencies: " + dependencyStatus);
				//Menu.instance.txt_win.text = "Could not resolve all Firebase dependencies: {0}";
			}
		});

	}

	// Update is called once per frame
	void Update()
	{

	}
	void InitializeFirebase()
	{
		Debug.Log("Enabling data collection.");
		FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
		Debug.Log("Set user properties.");
		firebaseInitialized = true;
	}

	public void AnalyticsSurvival(int stage)
	{
		FirebaseAnalytics.LogEvent(
			"SURVIVAL", "Survival", stage);
	}
	public void AnalyticsWC()
	{
		FirebaseAnalytics.LogEvent(
			"WORLDCUP", "WorldCup", 1);
	}
	public void AnalyticsArchievement(int number)
	{
		FirebaseAnalytics.LogEvent(
			"ARCHIEVEMENT", "Archievement", number);
	}
	public void AnalyticsOpenCharacter(int id)
	{
		FirebaseAnalytics.LogEvent(
			"CHARACTEROPEN", "Character", id);
	}
	public void AnalyticsPurchase(string id)
	{
		FirebaseAnalytics.LogEvent(
			"PURCHASE", "purchase", id);
	}
	public void AnalyticsDailyChallenge(int number)
	{
		FirebaseAnalytics.LogEvent(
			"DAILYCHALLENGE", "DailyChallenge", number);
	}
	public void AnalyticsFriendmatch()
	{
		Firebase.Analytics.FirebaseAnalytics.LogEvent(
			 "FRIENDMATCH", "Friendmatch", 1);
	}
	public void AnalyticsLeague(string mode, int level)
	{
		FirebaseAnalytics.LogEvent(
			mode, mode, level);
	}
	public void AnalyticsGroupstage()
	{
		FirebaseAnalytics.LogEvent(
			"WCGROUPSTAGE", "Groupstage", 1);
	}

	public void AnalyticsR16()
	{
		FirebaseAnalytics.LogEvent(
			"WCROUNDOF16", "r16", 1);
	}
	public void AnalyticsQuarterfinal()
	{
		FirebaseAnalytics.LogEvent(
			"WCQUATERFINAL", "Quarterfinal", 1);
	}
	public void AnalyticsSemifinal()
	{
		FirebaseAnalytics.LogEvent(
			"WCSEMIFINAL", "Semifinal", 1);
	}
	public void AnalyticsFinal()
	{
		FirebaseAnalytics.LogEvent(
			"WCFINAL", "Final", 1);
	}
	public void AnalyticsCupWC()
	{
		FirebaseAnalytics.LogEvent(
			"VODICHWC", "cupWC", 1);
	}
	public void AnalyticsShowFullAds()
	{
		FirebaseAnalytics.LogEvent(
			"SHOWFULLADS", "ShowFullAds", 1);
	}
	public void AnalyticsShowReward()
	{
		FirebaseAnalytics.LogEvent(
			"SHOWREWARDVIDEO", "ShowRewardVideo", 1);
	}
	public void AnalyticsStartNotify(int number)
	{
		FirebaseAnalytics.LogEvent(
			"NOTIFICATIONS", FirebaseAnalytics.ParameterLevel, number);
	}
	public void AnalyticsReward()
	{
		FirebaseAnalytics.LogEvent(
			"REWARD", "reward", 1);
	}

	public void AnalyticsStartIcon(int number)
	{
		FirebaseAnalytics.LogEvent(
			FirebaseAnalytics.EventAppOpen,
			//new Parameter(FirebaseAnalytics.ParameterLevel, number),
			new Parameter(FirebaseAnalytics.ParameterCharacter, "ICON:")
			);
	}


}
*/