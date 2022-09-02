using UnityEngine;
using UnityEngine.Advertisements;

public class FullUnityAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
	public static FullUnityAds instance;
	[SerializeField] string _androidAdUnitId = "Interstitial_Android";
	[SerializeField] string _iOsAdUnitId = "Interstitial_iOS";
	public string _adUnitId;
	public static bool isShowFull = false;

	void Awake()
	{
		instance = this;
		// Get the Ad Unit ID for the current platform:
		_adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
			? _iOsAdUnitId
			: _androidAdUnitId;
	}

	// Load content to the Ad Unit:
	public void LoadAd()
	{
		// IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
		Debug.Log("Loading Ad: " + _adUnitId);
		Advertisement.Load(_adUnitId, this);
	}

	// Show the loaded content in the Ad Unit: 
	public void ShowAd()
	{
		// Note that if the ad content wasn't previously loaded, this method will fail
		Debug.Log("Showing Ad: " + _adUnitId);
		Advertisement.Show(_adUnitId, this);
		isShowFull = true;
	}

	// Implement Load Listener and Show Listener interface methods:  
	public void OnUnityAdsAdLoaded(string adUnitId)
	{
		// Optionally execute code if the Ad Unit successfully loads content.
	}

	public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
	{
		Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
		// Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
		Advertisement.Load(_adUnitId, this);
	}

	public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
	{
		Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
		// Optionally execute code if the Ad Unit fails to show, such as loading another ad.
		if (isShowFull == true)
		{
			if (Time.timeScale == 0)
			{
				Time.timeScale = 1;
			}
			AdsManager.Instance.ShowUIFullAds();
			Advertisement.Load(_adUnitId, this);
		}
	}

	public void OnUnityAdsShowStart(string adUnitId) { }
	public void OnUnityAdsShowClick(string adUnitId)
	{
		if (isShowFull == true)
		{
			if (Time.timeScale == 0)
			{
				Time.timeScale = 1;
			}
			AdsManager.Instance.ShowUIFullAds();
			Advertisement.Load(_adUnitId, this);
		}
	}
	public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
	{
		if(isShowFull == true)
		{
			if (Time.timeScale == 0)
			{
				Time.timeScale = 1;
			}
			AdsManager.Instance.ShowUIFullAds();
			Advertisement.Load(_adUnitId, this);
		}	
	}
}