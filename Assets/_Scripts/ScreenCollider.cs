using UnityEngine;

public class ScreenCollider : MonoBehaviour
{
    #region Variables
    [SerializeField] private string layer;
    [SerializeField] private float height = 1f;

    private Vector2 _screenSize;
    #endregion

    #region Builts_In
    private void Awake()
    {
        _screenSize = Utilities.GetCameraDimensions();
        CreateColliders();
    }
    #endregion

    #region Methods
    private void CreateColliders()
    {
        BoxCollider2D topCol = NewCollider();
        BoxCollider2D bottomCol = NewCollider();

        float halfHeight = _screenSize.y / 2f;
        topCol.transform.position = new Vector3(0f, halfHeight - height / 2f);
        bottomCol.transform.position = new Vector3(0f, -halfHeight + height / 2f);
    }

    private BoxCollider2D NewCollider()
    {
        GameObject instance = new GameObject("Screen Collider");
        instance.layer = LayerMask.NameToLayer(layer);
        BoxCollider2D collider = instance.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(_screenSize.x, height);

        return collider;
    }
    #endregion
}
