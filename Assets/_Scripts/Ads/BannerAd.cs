using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAd : MonoBehaviour
{
    #region Variables
    [SerializeField] private string androidAdUnitId;
    [SerializeField] private string iosAdUnitId;
    [SerializeField] private bool loadBannerAtStart = true;

    private string _adUnitId;
    BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;
    #endregion

    #region Builts_In
    private void Start()
    {
#if UNITY_IOS
        _adUnitId = iosAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = androidAdUnitId;
#endif

        Advertisement.Banner.SetPosition(_bannerPosition);

        if (loadBannerAtStart)
            LoadBanner();
    }
    #endregion

    #region Load Banner Methods
    public void LoadBanner()
    {
        BannerLoadOptions options = new BannerLoadOptions()
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerLoadError
        };

        Advertisement.Banner.Load(_adUnitId, options);
    }

    private void OnBannerLoaded()
    {
        Debug.Log("Banner loaded");
        ShowBannerAd();
    }

    private void OnBannerLoadError(string error)
    {
        Debug.Log("Failed loading banner : " + error);
    }
    #endregion

    #region Show Banner Methods
    public void ShowBannerAd()
    {
        BannerOptions options = new BannerOptions()
        {
            showCallback = OnBannerShown,
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden
        };

        Advertisement.Banner.Show(_adUnitId, options);
    }

    public void HideBannerAd()
    {
        Advertisement.Banner.Hide();
    }

    private void OnBannerShown()
    {
        Debug.Log("Banner showed");
    }

    private void OnBannerClicked()
    {
        Debug.Log("Banner clicked");
    }

    private void OnBannerHidden()
    {
        Debug.Log("Banner hidden");
    }
    #endregion
}
