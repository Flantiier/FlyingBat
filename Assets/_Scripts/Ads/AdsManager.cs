using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener
{
    #region Variables
    [SerializeField] private string androidGameId;
    [SerializeField] private string iosGameId;
    [SerializeField] private bool isTestingMode = true;
    private string _gameId;

    public static AdsManager Instance { get; private set; }
    public BannerAd Banner { get; private set; }
    public InterstitialAd Interstitial { get; private set; }
    public RewardedAd Rewarded { get; private set; }
    #endregion

    #region Buitls_In
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        InitializeAds();
        GetAdsScripts();
    }

    private void Start()
    {
        Banner.LoadBanner();
    }
    #endregion

    #region Methods
    private void InitializeAds()
    {
        if (Application.isEditor)
            _gameId = androidGameId;
        else
            _gameId = Application.platform == RuntimePlatform.Android ? androidGameId : iosGameId;

        if (!Advertisement.isInitialized && Advertisement.isSupported)
            Advertisement.Initialize(_gameId, isTestingMode, this);
    }

    private void GetAdsScripts()
    {
        Banner = GetComponent<BannerAd>();
        Interstitial = GetComponent<InterstitialAd>();
        Rewarded = GetComponent<RewardedAd>();
    }
    #endregion

    #region Interfaces Implementation
    public void OnInitializationComplete()
    {
        Debug.Log("Ads Initialize Complete");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log("Ads Initialize failed");
    }
    #endregion
}
