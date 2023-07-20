using UnityEngine;
using UnityEngine.UI;

public class ButtonSFX : MonoBehaviour
{
    #region Variables
    [SerializeField] private AudioClip clickClip;
    private Button _button;
    #endregion

    #region Builts_In
    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClickSFX);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClickSFX);
    }
    #endregion

    #region Methods
    private void OnClickSFX()
    {
        AudioManager.Instance.PlayClip(clickClip);
    }
    #endregion
}
