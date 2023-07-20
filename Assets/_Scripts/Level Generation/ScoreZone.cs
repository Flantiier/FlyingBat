using UnityEngine;

public class ScoreZone : MonoBehaviour
{
    #region Variables
    private BoxCollider2D _collider;
    #endregion

    #region Builts_In
    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _collider.isTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out PlayerController player))
            return;

        if (!player.Enabled)
            return;

        GameManager.Instance.IncreaseScore();
    }
    #endregion
}
