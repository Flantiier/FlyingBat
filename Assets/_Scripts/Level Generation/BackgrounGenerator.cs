using UnityEngine;

public class BackgrounGenerator : MonoBehaviour
{
    #region Variables
    [Header("Background References")]
    [SerializeField] private Sprite backgroundSprite;
    [SerializeField] private float slidingSpeed = 1f;
    [SerializeField] private string layer = "Default";
    [SerializeField] private int priority = 0;

    private float _screenSize;
    #endregion

    #region Builts_In
    private void Awake()
    {
        GenerateSprites();
    }

    private void FixedUpdate()
    {
        BackgroundSliding();
    }
    #endregion

    #region Methods
    /// <summary>
    /// Determinate the amount of required backgrounds
    /// </summary>
    private void GenerateSprites()
    {
        _screenSize = Utilities.GetCameraDimensions().x;
        float spriteWidth = backgroundSprite.rect.width;
        int amount = Mathf.CeilToInt(_screenSize / spriteWidth) + 2;

        for (int i = 0; i < amount; i++)
        {
            GameObject instance = CreateNewBackground(transform);
            Vector2 position = new Vector2(i * _screenSize, 0f);
            instance.transform.position = position;
        }
    }

    /// <summary>
    /// Slideall the background to the left
    /// </summary>
    private void BackgroundSliding()
    {
        foreach (Transform background in transform)
        {
            background.position += Vector3.left * slidingSpeed * Time.fixedDeltaTime;

            if (background.position.x <= -_screenSize * 1.1f)
            {
                Transform lastElement = transform.GetChild(transform.childCount - 1);
                Vector2 position = new Vector3(lastElement.position.x + _screenSize, 0f);
                background.position = position;
                background.SetAsLastSibling();
            }
        }
    }

    /// <summary>
    /// Return an new instance of a gameobject with sprite renderer
    /// </summary>
    private GameObject CreateNewBackground(Transform parent)
    {
        GameObject instance = new GameObject("Background");
        instance.transform.SetParent(parent);
        //Add sprite renderer
        SpriteRenderer renderer = instance.AddComponent<SpriteRenderer>();
        renderer.sprite = backgroundSprite;
        renderer.sortingLayerID = SortingLayer.NameToID(layer);
        renderer.sortingOrder = priority;
        //Add scaler
        instance.AddComponent<SpriteScaler>();

        return instance;
    }
    #endregion
}