using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    public Player m_player;
    public PoolManager pool;
    public float gameTime;
    public float EASY_MODE = 2 * 5f;
    public float maxGameTime = 60 * 5;

    [Header("# Player Info")]
    public int health;
    public int maxHealth = 100;
    public int level;
    public int kill;
    public int exp = 0;
    public int[] nexExp = { 3, 5, 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 };

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        gameTime += Time.deltaTime;
    }

    public void GetExp()
    {
        exp++;

        if (exp >= nexExp[level])
        {
            level++;
            exp = 0;
        }
    }
}
