using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private RewardedAdPopUp adPopUp;

    [Header("Score Audio")]
    [SerializeField] private AudioClip scoreClip;

    private const string SCORE_KEY = "HIGHER_SCORE";
    private bool _playedRewardedAd = false;
    #endregion

    #region Events
    [Header("Events")]
    [SerializeField] private GameEvent startGameEvent;
    [SerializeField] private GameEvent backToMenuEvent;

    public event Action OnScoreModified;
    public event Action OnNewHigherScore;
    #endregion

    #region Properties
    public int Score { get; private set; }
    public string ScoreKey => SCORE_KEY;
    public static GameManager Instance { get; private set; }
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
        PlayerController.OnPlayerCollided += PlayerDeathListener;
    }

    private void OnDisable()
    {
        PlayerController.OnPlayerCollided -= PlayerDeathListener;
    }
    #endregion

    #region Methods
    /// <summary>
    /// Reset elements to display the menu
    /// </summary>
    private void ResetPlayerPosition()
    {
        player.position = Vector2.zero;
        player.GetComponent<PlayerController>().SetAnimationToDeath(false);
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

    #region Game Methods
    /// <summary>
    /// Select between ending the game or show an add to continue, subscribed to player collide event
    /// </summary>
    public void PlayerDeathListener()
    {
        int higherScore = !PlayerPrefs.HasKey(SCORE_KEY) ? 0 : PlayerPrefs.GetInt(SCORE_KEY);

        //Fairly high Score to trigger an add & already not played
        if (Score < higherScore/* && Score >= 5*/)
        {
            adPopUp.gameObject.SetActive(true);
            _playedRewardedAd = true;
            return;
        }

        //Already showed and end level
        GameOver();
        _playedRewardedAd = false;
    }

    /// <summary>
    /// Trigger an event that start the game
    /// </summary>
    public void StartGame()
    {
        SetScore(0);
        startGameEvent.Raise();
    }

    /// <summary>
    /// Trigger an event to going back to the menu
    /// </summary>
    public void GameOver()
    {
        //Save score and return to menu
        SaveScore();
        ResetPlayerPosition();
        backToMenuEvent.Raise();
    }

    /// <summary>
    /// Close the application
    /// </summary>
    public void QuitApplication()
    {
        Application.Quit();
    }
    #endregion

    #region Game Bonus
    public void SecondChance()
    {
        Debug.Log("Continue the current run!");
    }
    #endregion
}
