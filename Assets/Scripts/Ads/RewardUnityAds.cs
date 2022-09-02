using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class RewardUnityAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
	public static RewardUnityAds instance;
	[SerializeField] string _androidAdUnitId = "Rewarded_Android";
	[SerializeField] string _iOSAdUnitId = "Rewarded_iOS";
	public string _adUnitId = null;


	void Awake()
	{
		instance = this;
		// Get the Ad Unit ID for the current platform:
		_adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
			? _iOSAdUnitId
			: _androidAdUnitId;
	}

	// Load content to the Ad Unit:
	public void LoadAd()
	{
		// IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
		Debug.Log("Loading Ad: " + _adUnitId);
		Advertisement.Load(_adUnitId, this);
	}

	// If the ad successfully loads, add a listener to the button and enable it:
	public void OnUnityAdsAdLoaded(string adUnitId)
	{
		Debug.Log("Ad Loaded: " + adUnitId);

	}

	// Implement a method to execute when the user clicks the button.
	public void ShowAd()
	{
		// Then show the ad:
		Advertisement.Show(_adUnitId, this);
		FullUnityAds.isShowFull = false;
	}

	// Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
	public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
	{
		if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED) && FullUnityAds.isShowFull == false)
		{
			Debug.Log("Unity Ads Rewarded Ad Completed");
			// Grant a reward.
			AdsManager.Instance.RewardOK();
			// Load another ad:
			Advertisement.Load(_adUnitId, this);
		}
	}

	// Implement Load and Show Listener error callbacks:
	public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
	{
		Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
		// Use the error details to determine whether to try to load another ad.
	}

	public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
	{
		Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
		// Use the error details to determine whether to try to load another ad.
	}

	public void OnUnityAdsShowStart(string adUnitId) { }
	public void OnUnityAdsShowClick(string adUnitId) { }

	//void OnDestroy()
	//{
	//	// Clean up the button listeners:
	//	_showAdButton.onClick.RemoveAllListeners();
	//}
}