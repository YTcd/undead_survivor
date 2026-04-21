using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] enemyPrefab = new GameObject[5];
    List<GameObject>[] pool;
    
    void Awake()
    {
        pool = new List<GameObject>[enemyPrefab.Length];
        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = new List<GameObject>();
        }
    }

    public GameObject GetEnemy(int index)
    {
        GameObject select = null;

        foreach(GameObject item in pool[index])
        {
            if (item.activeSelf == false)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (select == null && pool[index].Count < 10)
        {
            select = Instantiate(enemyPrefab[index], transform);
            pool[index].Add(select);
        }

        return select;
    }
}
