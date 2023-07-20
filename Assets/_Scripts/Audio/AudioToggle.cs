using UnityEngine;

public class AudioToggle : ToggleButton
{
    #region Variables
    [Header("Audio Properties")]
    [SerializeField] private bool isMusicToggle;
    private AudioManager _audioManager;
    #endregion

    #region Builts_In
    private void Start()
    {
        _audioManager = AudioManager.Instance;
        bool toggle = isMusicToggle ? _audioManager.MusicEnabled : _audioManager.EffectsEnabled;
        OverrideToggleValue(toggle);
    }
    #endregion

    #region Inherited Methods
    public override void SwitchToggleValue()
    {
        base.SwitchToggleValue();

        if (isMusicToggle)
            _audioManager.MusicEnabled = _toggled;
        else
            _audioManager.EffectsEnabled = _toggled;

        _audioManager.SetAudioParameters();
    }
    #endregion
}