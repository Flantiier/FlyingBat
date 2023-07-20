using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    #region Variables
    [SerializeField] private LayerMask collisionMask;

    private PlayerController _player;
    #endregion

    #region Builts_In
    private void Awake()
    {
        _player = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collisionMask != (collisionMask | (1 << collision.gameObject.layer)))
            return;

        _player.HasCollided();
    }
    #endregion
}
