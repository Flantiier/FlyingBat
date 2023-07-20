using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    #region Variables
    [Header("Audio from pool")]
    [SerializeField] private bool playFromPool;
    [SerializeField] private int poolIndex;

    [Header("Audio clip override")]
    [SerializeField] private AudioClip clip;
    #endregion

    #region Methods
    public void PlayAudio()
    {
        if (playFromPool)
            AudioManager.Instance.PlayEffectInPool(poolIndex);
        else
            AudioManager.Instance.PlayClip(clip);
    }
    #endregion
}
