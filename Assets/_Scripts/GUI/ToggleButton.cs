using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ToggleButton : MonoBehaviour
{
    #region Variables/Properties
    [SerializeField] protected Sprite toggledImage;
    [SerializeField] protected Sprite untoggledImage;

    protected Image _image;
    protected bool _toggled;
    #endregion

    #region Builts_In
    protected virtual void Awake()
    {
        _image = GetComponent<Image>();
    }
    #endregion

    #region Methods
    /// <summary>
    /// Indicates if the button is toggled or not
    /// </summary>
    protected void OverrideToggleValue(bool value)
    {
        _toggled = value;
        SetSprite();
    }

    /// <summary>
    /// Swicth toggle value
    /// </summary>
    public virtual void SwitchToggleValue()
    {
        _toggled = !_toggled;
        SetSprite();
    }

    /// <summary>
    /// Set button sprite based on toggle state
    /// </summary>
    protected virtual void SetSprite()
    {
        Sprite sprite = _toggled ? toggledImage : untoggledImage;
        _image.sprite = sprite;
    }
    #endregion
}
