using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    #region Variables
    [Header("Mixer properties")]
    [SerializeField] private AudioMixer masterMixer;

    [Header("Clip properties")]
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource effectSource;

    //Audio parameters
    private const string MUSIC_KEY = "ENABLE_MUSIC";
    private const string EFFECT_KEY = "ENABLE_EFFECT";
    #endregion

    #region Properties
    public static AudioManager Instance { get; private set; }
    public bool MusicEnabled { get; set; }
    public bool EffectsEnabled { get; set; }
    #endregion

    #region Builts_In
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        GetAudioParameters();
    }

    private void OnDestroy()
    {
        SetAudioParameters();
    }
    #endregion

    #region Audio Methods
    /// <summary>
    /// Play an effect from the clips array 
    /// </summary>
    public void PlayEffectInPool(int index)
    {
        if (index < 0 || index >= clips.Length)
            return;

        effectSource.PlayOneShot(clips[index]);
    }

    /// <summary>
    /// Play an effect from a clip
    /// </summary>
    public void PlayClip(AudioClip clip)
    {
        effectSource.PlayOneShot(clip);
    }

    /// <summary>
    /// Apply audio parameters on the audio mixer values
    /// </summary>
    private void ApplyAudioParameters()
    {
        musicSource.mute = !MusicEnabled;
        effectSource.mute = !EffectsEnabled;
    }
    #endregion

    #region Audio Parameters
    /// <summary>
    /// Get playerPrefs for audio parameters
    /// </summary>
    public void GetAudioParameters()
    {
        //No parameters
        if (!PlayerPrefs.HasKey(MUSIC_KEY))
            PlayerPrefs.SetInt(MUSIC_KEY, 1);

        if (!PlayerPrefs.HasKey(EFFECT_KEY))
            PlayerPrefs.SetInt(EFFECT_KEY, 1);

        //Get parameters values
        MusicEnabled = ConvertPrefToBool(PlayerPrefs.GetInt(MUSIC_KEY));
        EffectsEnabled = ConvertPrefToBool(PlayerPrefs.GetInt(EFFECT_KEY));

        ApplyAudioParameters();
    }

    /// <summary>
    /// Save audio parameters with PlayerPrefs
    /// </summary>
    public void SetAudioParameters()
    {
        PlayerPrefs.SetInt(MUSIC_KEY, ConvertBoolToInt(MusicEnabled));
        PlayerPrefs.SetInt(EFFECT_KEY, ConvertBoolToInt(EffectsEnabled));

        ApplyAudioParameters();
    }

    /// <summary>
    /// Return a boolean from playerPref value
    /// </summary>
    private bool ConvertPrefToBool(int value)
    {
        return value >= 1;
    }

    /// <summary>
    /// Return an int from a boolean value
    /// </summary>
    private int ConvertBoolToInt(bool value)
    {
        //Debug.Log(value ? 1 : 0);
        return value ? 1 : 0;
    }
    #endregion
}
