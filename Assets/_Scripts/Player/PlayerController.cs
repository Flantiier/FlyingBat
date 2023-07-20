using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    #region Variables
    [Header("Movements")]
    [SerializeField] private float flyForce = 1f;
    [SerializeField] private float maxVelocity = 10f;

    [Header("Audio")]
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip deathClip;

    private Rigidbody2D _rb;
    private Animator _animator;
    private CircleCollider2D _collider;

    public static event Action OnPlayerCollided;
    #endregion

    #region Properties
    public bool Enabled { get; private set; }
    #endregion

    #region Builts_in
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<CircleCollider2D>();

        EnablePlayer(false);
    }

    private void FixedUpdate()
    {
        if (!Enabled)
            return;

        HandleMovements();
    }
    #endregion

    #region Game Methods
    /// <summary>
    /// Enable player inputs and rb physics
    /// </summary>
    public void EnablePlayer(bool enable)
    {
        Enabled = enable;
        _rb.simulated = enable;
        _collider.enabled = enable;
    }
    #endregion

    #region Movement Methods
    /// <summary>
    /// Handle input to move the player
    /// </summary>
    private void HandleMovements()
    {
        if (Input.touchCount >= 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                ImpulsePlayer();
                PlayAudio(jumpClip);
            }
        }

        ClampVelocity();
    }

    /// <summary>
    /// Impulse the player towards up
    /// </summary>
    public void ImpulsePlayer()
    {
        if (_rb.velocity.y < 0)
            _rb.velocity = Vector2.zero;

        _rb.AddForce(Vector2.up * flyForce, ForceMode2D.Impulse);
    }

    /// <summary>
    /// Clamping the velocity of the player
    /// </summary>
    private void ClampVelocity()
    {
        float clampedY = Mathf.Clamp(_rb.velocity.y, -maxVelocity, maxVelocity);
        _rb.velocity = new Vector2(0f, clampedY);
    }

    /// <summary>
    /// Player has collide with something
    /// </summary>
    public void HasCollided()
    {
        PlayAudio(deathClip);
        SetAnimationToDeath(true);
        EnablePlayer(false);
        _rb.velocity = Vector2.zero;
    }

    /// <summary>
    /// Play an audio clip
    /// </summary>
    private void PlayAudio(AudioClip clip)
    {
        AudioManager.Instance.PlayClip(clip);
    }
    #endregion

    #region Animations Methods
    /// <summary>
    /// An event at the end of the death animation
    /// </summary>
    public void DeathEvent()
    {
        OnPlayerCollided?.Invoke();
    }

    /// <summary>
    /// Set animation trigger
    /// </summary>
    public void SetAnimationToDeath(bool value)
    {
        if (value)
            _animator.SetTrigger("Death");
        else
            _animator.SetTrigger("Idle");
    }
    #endregion
}
