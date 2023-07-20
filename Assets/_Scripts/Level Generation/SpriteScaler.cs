using UnityEngine;

public class SpriteScaler : MonoBehaviour
{
    #region Variables
    [SerializeField] private bool scaleOnX = true;
    [SerializeField] private bool scaleOnY = true;

    private SpriteRenderer _spriteRenderer;
    #endregion

    #region Builts_In
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        ScaleObject();
    }
    #endregion

    #region Methods
    /// <summary>
    /// Scale the object based on screen size
    /// </summary>
    private void ScaleObject()
    {
        Vector2 screenSize = Utilities.GetCameraDimensions();
        Vector2 scale = new Vector2();
        scale.x = scaleOnX ? screenSize.x / _spriteRenderer.size.x : 1f;
        scale.y = scaleOnY ? screenSize.y / _spriteRenderer.size.y : 1f;

        transform.localScale = (Vector3)scale;
    }
    #endregion
}
