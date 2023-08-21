using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    #region Variables
    [SerializeField] private string androidGameId;
    [SerializeField] private string iosGameId;
    [SerializeField] private bool isTestingMode = true;

    string _gameId;
    #endregion

    #region Buitls_In
    private void Awake()
    {
        InitializeAds();
    }
    #endregion

    #region Methods
    private void InitializeAds()
    {
#if UNITY_IOS
        _gameId = iosGameId;
#elif UNITY_ANDROID
        _gameId = androidGameId;
#elif UNITY_EDITOR
        _gameId = androidGameId; //for testing
#endif

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
