using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer: MonoBehaviour, IUnityAdsInitializationListener
{
    #region Variables
    [SerializeField] private string androidGameId;
    [SerializeField] private string iosGameId;
    [SerializeField] private bool isTestingMode = true;
    private string _gameId;
    #endregion

    #region Builts_In
    private void Awake()
    {
        InitializeAds();
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
