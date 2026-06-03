using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spwanPoint;
    public SpawnData[] spawnDatas;
    public float levelTime;

    int level;
    float timer;

    void Awake()
    {
        spwanPoint = GetComponentsInChildren<Transform>();
        timer = 0f;
        levelTime = GameManager.instance.maxGameTime / spawnDatas.Length;
    }

    void Update()
    {
        if (!GameManager.instance.isLive)
        {
            return;
        }

        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / levelTime), spawnDatas.Length - 1);

        if (timer > spawnDatas[level].spawnTime)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        GameObject Enemy = GameManager.instance.pool.Get(0);
        if (Enemy == null)
        {
            return;
        }
        Enemy.transform.position = spwanPoint[Random.Range(1, spwanPoint.Length)].position;
        Enemy.GetComponent<Enemy>().Init(spawnDatas[level]);
    }
}

[System.Serializable]
public class SpawnData
{
    public float spawnTime;
    public int enemyType;
    public int health;
    public float speed;
}
