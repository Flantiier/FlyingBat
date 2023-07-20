using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    #region Variables
    [SerializeField] private bool showHigherScore = false;

	private TextMeshProUGUI _text;
    private GameManager _gameManager;
    #endregion

    #region Builts_In
    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        SetText();
    }
    #endregion

    #region Methods
    private void SetText()
    {
        int score;

        if (showHigherScore && PlayerPrefs.HasKey(_gameManager.ScoreKey))
            score = PlayerPrefs.GetInt(_gameManager.ScoreKey);
        else
            score = _gameManager.Score;

        _text.text = score.ToString();
    }
    #endregion
}
