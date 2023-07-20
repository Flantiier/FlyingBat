using TMPro;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    #region Variables
    [Header("Panels")]
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject menuPanel;

    [Header("Menu UI")]
    [SerializeField] private GameObject highScorePanel;
    [SerializeField] private GameObject currentScorePanel;
    [SerializeField] private GameObject newScorePanel;

    [Header("Game UI")]
    [SerializeField] private TextMeshProUGUI scoreText;

    private GameManager _gameManager;
    private AudioPlayer _audioPlayer;
    private bool _newBestScore = false;
    #endregion

    #region Builts_In
    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _audioPlayer = GetComponent<AudioPlayer>();
    }

    private void Start()
    {
        DisplayHighScoreAtStart();
    }

    private void OnEnable()
    {
        _gameManager.OnScoreModified += UpdateScore;
        _gameManager.OnNewHigherScore += NewHigherScore;
    }

    private void OnDisable()
    {
        _gameManager.OnScoreModified -= UpdateScore;
        _gameManager.OnNewHigherScore -= NewHigherScore;
    }
    #endregion

    #region Methods
    /// <summary>
    /// Enable menu panel else enable game panel
    /// </summary>
    public void EnableMenu(bool enable)
    {
        menuPanel.SetActive(enable);
        gamePanel.SetActive(!enable);
    }

    /// <summary>
    /// Update score text
    /// </summary>
    private void UpdateScore()
    {
        scoreText.text = _gameManager.Score.ToString();
    }

    private void DisplayHighScoreAtStart()
    {
        if (!PlayerPrefs.HasKey(_gameManager.ScoreKey))
        {
            highScorePanel.SetActive(false);
            newScorePanel.SetActive(false);
            currentScorePanel.SetActive(false);
            return;
        }

        highScorePanel.SetActive(true);
    }

    #region HighScore Methods
    /// <summary>
    /// Display and play an audio when a new high score is reached
    /// </summary>
    public void HandleNewHighScore()
    {
        DisplayHigherScore();
        PlayHighScoreAudio();
        _newBestScore = false;
    }

    /// <summary>
    /// Indicates is there's a new higher score
    /// </summary>
    private void NewHigherScore()
    {
        _newBestScore = true;
    }

    /// <summary>
    /// Show the higher score of the player
    /// </summary>
    private void DisplayHigherScore()
    {
        //Enable the right text
        highScorePanel.SetActive(!_newBestScore);
        newScorePanel.SetActive(_newBestScore);
        currentScorePanel.SetActive(!_newBestScore);
    }

    /// <summary>
    /// Play a clip if a new high score is reached
    /// </summary>
    private void PlayHighScoreAudio()
    {
        if (!_newBestScore)
            return;

        _audioPlayer.PlayAudio();
    }
    #endregion

    #endregion
}
