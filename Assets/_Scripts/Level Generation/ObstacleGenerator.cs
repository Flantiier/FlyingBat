using System.Collections;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    #region Variables
    [Header("GameProperties")]
    [SerializeField] private float waitBeforeGen = 1.5f;

    [Header("Obstacle Generation")]
    [SerializeField] private GameObject topObstacle;
    [SerializeField] private GameObject bottomObstacle;
    [SerializeField] private float gapSize = 1f;
    [SerializeField] private float generateDelay = 3f;
    [SerializeField] private float offset = 2f;
    [SerializeField] private float slidingSpeed = 1f;

    [Header("Pooling")]
    [SerializeField] private int obstacleAmount = 5;

    private float _halfScreenX;
    #endregion

    #region Builts_In
    private void Awake()
    {
        _halfScreenX = Utilities.GetCameraDimensions().x / 2f;
        GenerateObstaclePool();
    }

    private void FixedUpdate()
    {
        ObstacleSliding();
    }
    #endregion

    #region Game Methods
    /// <summary>
    /// Start the generation Coroutine
    /// </summary>
    public void StartGeneration()
    {
        StartCoroutine("WaitForGeneration");
    }

    /// <summary>
    /// Stop the generation Coroutine
    /// </summary>
    public void StopGeneration()
    {
        try{
            StopCoroutine(nameof(GenerateObstacle));
        }
        catch {
            Debug.Log("Coroutine not existing.");
        }

        //Hide all obstacles
        foreach (Transform child in transform)
        {
            if (!child.gameObject.activeInHierarchy)
                continue;

            child.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Wait a bit before generating at start of the game
    /// </summary>
    private IEnumerator WaitForGeneration()
    {
        yield return new WaitForSeconds(waitBeforeGen);
        StartCoroutine(nameof(GenerateObstacle));
    }
    #endregion

    #region Methods
    /// <summary>
    /// Get an obstacle and set its position
    /// </summary>
    private IEnumerator GenerateObstacle()
    {
        Transform obstacle = GetObstacle();
        Vector2 position = new Vector3(_halfScreenX * 1.5f, Random.Range(-offset, offset));
        obstacle.position = position;

        yield return new WaitForSeconds(generateDelay);
        StartCoroutine("GenerateObstacle");
    }

    /// <summary>
    /// Slide all the enabled obstacle to the left
    /// </summary>
    private void ObstacleSliding()
    {
        foreach (Transform child in transform)
        {
            if (!child.gameObject.activeInHierarchy)
                continue;

            child.position += Vector3.left * slidingSpeed * Time.fixedDeltaTime;

            if (child.position.x <= -_halfScreenX * 1.3f)
                child.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Generate a certain amount of obstacle at the beginning
    /// </summary>
    private void GenerateObstaclePool()
    {
        for (int i = 0; i < obstacleAmount; i++)
            CreateNewObstacle();
    }

    /// <summary>
    /// Get an obstacle from the pool
    /// </summary>
    private Transform GetObstacle()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeInHierarchy)
                continue;

            //Return the last disabled object
            child.gameObject.SetActive(true);
            return child;
        }

        //Return new instance
        return CreateNewObstacle();
    }

    /// <summary>
    /// Generate an parent with top and bottom obstacles
    /// </summary>
    private Transform CreateNewObstacle()
    {
        //Create both top and bottom obstacle
        Transform parent = new GameObject("Obstacle_instance").transform;
        Transform top = Instantiate(topObstacle, parent).transform;
        Transform bottom = Instantiate(bottomObstacle, parent).transform;
        //Set position based on properties
        float halfHeight = (top.GetComponent<SpriteRenderer>().sprite.rect.height / 2f) / 100;
        top.localPosition = new Vector2(0f, halfHeight + gapSize);
        bottom.localPosition = new Vector2(0f, -halfHeight - gapSize);

        //Create the score zone
        Transform scoreZone = new GameObject("Score_zone").transform;
        BoxCollider2D scoreBox = scoreZone.gameObject.AddComponent<BoxCollider2D>();
        scoreZone.gameObject.AddComponent<ScoreZone>();
        scoreZone.SetParent(parent);
        scoreZone.localPosition = new Vector2(0f, 0f);
        scoreBox.size = new Vector2(0.5f, gapSize);

        parent.SetParent(transform);
        parent.gameObject.SetActive(false);
        return parent;
    }
    #endregion
}