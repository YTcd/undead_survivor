using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player m_player;
    public PoolManager pool;

    public float gameTime;
    public float EASY_MODE = 2 * 5f;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        gameTime += Time.deltaTime;
    }
}
