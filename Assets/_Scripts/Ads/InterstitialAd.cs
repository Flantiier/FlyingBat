using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    #region Variables
    [SerializeField] private string androidAdUnitId;
    [SerializeField] private string iosAdUnitId;

    private string _adUnitId;
    #endregion

    #region Builts_In
    private void Start()
    {
#if UNITY_IOS
        _adUnitId = iosAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = androidAdUnitId;
#endif

    }
    #endregion

    #region Load Ad Methods
    public void LoadAd()
    {
        Debug.Log("Interstitial loading");
        Advertisement.Load(_adUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Interstitial loaded");
        ShowAd();
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log("Interstitial failed to load");
    }
    #endregion

    #region Show Ads Interface
    public void ShowAd()
    {
        Advertisement.Show(_adUnitId, this);
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("Interstitial clicked");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("Interstitial show complete");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("Interstitial show failure");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("Interstitial show start");
    }
    #endregion
}
