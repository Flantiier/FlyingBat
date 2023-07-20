using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PooledElement<T> where T : MonoBehaviour
{
    #region Variables
    public int basePoolSize = 5;
    public T poolPrefab;
    private List<T> pool;
    private Transform parent;
    #endregion

    #region Methods
    public void InitiatePool(Transform parent)
    {
        this.parent = parent;
        pool = new List<T>();

        for (int i = 0; i < basePoolSize; i++)
        {
            pool.Add(GameObject.Instantiate(poolPrefab, parent));
            pool[i].gameObject.SetActive(false);
        }
    }
    
    public T GetElement()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if(!pool[i].gameObject.activeInHierarchy)
            {
                pool[i].gameObject.SetActive(true);
                return pool[i];
            }
        }

        pool.Add(GameObject.Instantiate(poolPrefab, parent));
        return pool[pool.Count - 1];
    }

    public void DeleteElement(T element)
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if(pool[i] == element)
            {
                element.gameObject.SetActive(false);
                return;
            }
        }
    }
    #endregion
}