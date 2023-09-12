using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    #region Variables/Properties
    [SerializeField] private bool isTestingMode = true;

    [Header("Banner Ads")]
    [SerializeField] private bool showBannerAtStart = true;
    [SerializeField] private BannerPosition bannerPosition = BannerPosition.BOTTOM_CENTER;

    private UnityAdsIDs Ads;
    private const string IOS_GAME_ID = "5388772";
    private const string ANDROID_GAME_ID = "5388773";
    private Action OnRewardedAdSucces;
    #endregion

    #region Builts_In
    private void Awake()
    {
        Ads = new UnityAdsIDs(IOS_GAME_ID, ANDROID_GAME_ID);
        InitializeAds();
    }

    private void Start()
    {
        if (showBannerAtStart)
            LoadBanner();
    }
    #endregion

    #region Initialize Ads Methods
    private void InitializeAds()
    {
        if (!Advertisement.isInitialized && Advertisement.isSupported)
            Advertisement.Initialize(Ads.GameId, isTestingMode, this);
    }
    public void OnInitializationComplete() => Debug.Log("Ads successfully initialized!");
    public void OnInitializationFailed(UnityAdsInitializationError error, string message) => Debug.Log("Failed to initialize Ads : " + message);
    #endregion

    #region Banner Ads Methods
    //LOAD BANNER METHODS
    private void LoadBanner()
    {
        BannerLoadOptions options = new BannerLoadOptions()
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerLoadError
        };

        Advertisement.Banner.SetPosition(bannerPosition);
        Advertisement.Banner.Load(Ads.BannerAdId, options);
    }
    private void OnBannerLoaded() => ShowBanner();
    private void OnBannerLoadError(string error) => Debug.Log("Failed to load banner : " + error);


    //SHOW BANNER METHODS
    public void ShowBanner() => Advertisement.Banner.Show(Ads.BannerAdId);
    public void HideBanner() => Advertisement.Banner.Hide();
    #endregion

    #region Interstitial Ads Methods
    private string GetAdType(string placementId) => placementId == Ads.InterstitialAdId ? "Interstitial Ad" : "Rewarded Ad";

    /// <summary>
    /// Play a skippable ad
    /// </summary>
    [ContextMenu("Play Interstitial Ad")]
    public void PlaySkippableAd() => Advertisement.Load(Ads.InterstitialAdId, this);

    /// <summary>
    /// Play a non-skippable ad but it should reward the player
    /// </summary>
    [ContextMenu("Play Rewarded Ad")]
    public void PlayRewardedAd(Action onSucess)
    {
        OnRewardedAdSucces = onSucess;
        Advertisement.Load(Ads.RewardedAdId, this);
    }


    //LOAD ADS
    public void OnUnityAdsAdLoaded(string placementId) => Advertisement.Show(placementId == Ads.InterstitialAdId ? Ads.InterstitialAdId : Ads.RewardedAdId, this);
    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message) => Debug.Log("Failed to load ad : " + message);
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) => Debug.Log("Failed to show ad : " + message);


    //SHOW ADS
    public void OnUnityAdsShowStart(string placementId) => Debug.Log($"Ad showed. ({GetAdType(placementId)})");
    public void OnUnityAdsShowClick(string placementId) => Debug.Log($"Ad clicked. ({GetAdType(placementId)})");
    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log($"Ad completed. ({GetAdType(placementId)})");

        //Reward player
        if (placementId == Ads.RewardedAdId && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            Debug.Log("Player should be rewarded.");
            OnRewardedAdSucces.Invoke();
        }
    }
    #endregion
}

#region UnityAds IDs class
[System.Serializable]
public class UnityAdsIDs
{
    public string GameId { get; private set; }
    public string BannerAdId { get; private set; }
    public string InterstitialAdId { get; private set; }
    public string RewardedAdId { get; private set; }

    public UnityAdsIDs(string iosId, string androidId)
    {
#if UNITY_IOS
        GameId = iosId;
        BannerAdId = "Banner_iOS";
        InterstitialAdId= "Interstitial_iOS";
        RewardedAdId = "Rewarded_iOS";
#elif UNITY_ANDROID
        GameId = androidId;
        BannerAdId = "Banner_Android";
        InterstitialAdId = "Interstitial_Android";
        RewardedAdId = "Rewarded_Android";
#endif
    }
}
#endregion
