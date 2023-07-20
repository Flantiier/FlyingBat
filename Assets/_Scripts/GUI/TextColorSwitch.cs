using UnityEngine;
using TMPro;

public class TextColorSwitch : MonoBehaviour
{
    #region Variables
    [SerializeField] private float delay = 0.5f;
    [SerializeField] private Color[] colors;

    private TextMeshProUGUI _text;
    private int _index;
    private float _lastSwitch;
    #endregion

    #region Builts_In
    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        ResetColorSwitch();
    }

    private void FixedUpdate()
    {
        if (Time.time < _lastSwitch + delay)
            return;

        _lastSwitch = Time.time;
        _index = _index >= colors.Length - 1 ? 0 : _index + 1;
        SetColor(colors[_index]);
    }
    #endregion

    #region Methods
    /// <summary>
    /// Reset color index
    /// </summary>
    private void ResetColorSwitch()
    {
        _index = 0;
    }

    /// <summary>
    /// Set the color of the text
    /// </summary>
    private void SetColor(Color color)
    {
        _text.color = color;
    }
    #endregion
}
