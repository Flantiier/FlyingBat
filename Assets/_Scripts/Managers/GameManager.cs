using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    [Header("Game properties")]
    [SerializeField] private Transform player;

    [Header("Score Audio")]
    [SerializeField] private AudioClip scoreClip; 

    private const string SCORE_KEY = "HIGHER_SCORE";
    #endregion

    #region Properties
    public int Score { get; private set; }
    public string ScoreKey => SCORE_KEY;
    public static GameManager Instance { get; private set; }
    #endregion

    #region Events
    [Header("Events")]
    [SerializeField] private GameEvent startGameEvent;
    [SerializeField] private GameEvent endGameEvent;

    public event Action OnScoreModified;
    public event Action OnNewHigherScore;
    #endregion

    #region Builts_In
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void OnEnable()
    {
        PlayerController.OnPlayerCollided += EndGame;
    }

    private void OnDisable()
    {
        PlayerController.OnPlayerCollided -= EndGame;
    }
    #endregion

    #region Methods
    /// <summary>
    /// Initialize game properties
    /// </summary>
    public void InitializeGame()
    {
        player.GetComponent<PlayerController>().ImpulsePlayer();
        SetScore(0);
    }

    /// <summary>
    /// Add 1 to the current score
    /// </summary>
    public void IncreaseScore()
    {
        int value = Score + 1;
        AudioManager.Instance.PlayClip(scoreClip);
        SetScore(value);
    }

    /// <summary>
    /// Modify score and call an event
    /// </summary>
    private void SetScore(int value)
    {
        Score = value;
        OnScoreModified?.Invoke();
    }

    /// <summary>
    /// Save the score if the current one is better
    /// </summary>
    private void SaveScore()
    {
        if (!PlayerPrefs.HasKey(SCORE_KEY))
        {
            PlayerPrefs.SetInt(SCORE_KEY, Score);
            OnNewHigherScore?.Invoke();
        }
        else
        {
            if (Score <= PlayerPrefs.GetInt(SCORE_KEY))
                return;

            PlayerPrefs.SetInt(SCORE_KEY, Score);
            OnNewHigherScore?.Invoke();
        }
    }

    [ContextMenu("Reset Saved Score")]
    private void ResetScore()
    {
        PlayerPrefs.DeleteKey(ScoreKey);
    }
    #endregion

    #region Menu Methods
    /// <summary>
    /// Reset elements to display the menu
    /// </summary>
    private void ResetToMenu()
    {
        player.position = Vector2.zero;
        player.GetComponent<PlayerController>().SetAnimationToDeath(false);
    }
    #endregion

    #region Game Methods
    /// <summary>
    /// Trigger start game event
    /// </summary>
    public void StartGame()
    {
        startGameEvent.Raise();
    }
    
    /// <summary>
    /// Trigger end game event
    /// </summary>
    public void EndGame()
    {
        SaveScore();
        ResetToMenu();
        endGameEvent.Raise();
    }

    /// <summary>
    /// Close the application
    /// </summary>
    public void QuitApplication()
    {
        Application.Quit();
    }
    #endregion
}
