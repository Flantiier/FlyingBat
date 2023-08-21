using UnityEngine;
using UnityEngine.Advertisements;

public class RewardedAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
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
        Debug.Log("Rewarded loading");
        Advertisement.Load(_adUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        if (placementId.Equals(_adUnitId))
        {
            Debug.Log("Rewarded loaded");
            ShowAd();
        }
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log("Rewarded failed to load");
    }
    #endregion

    #region Show Ads Interface
    public void ShowAd()
    {
        Advertisement.Show(_adUnitId, this);
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("Rewarded clicked");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsCompletionState.COMPLETED))
            Debug.Log("Rewarded show complete");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("Rewarded show failure");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("Rewarded show start");
    }
    #endregion
}
