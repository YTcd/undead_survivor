using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    public bool isLive = false;
    public Player m_player;
    public PoolManager pool;
    public float gameTime;
    public float EASY_MODE = 2 * 5f;
    public float maxGameTime = 60 * 5;
    public LevelUp uiLevelUp;

    [Header("# Player Info")]
    public int playerId;
    public float health;
    public float maxHealth = 100;
    public int level;
    public int kill;
    public int exp = 0;
    public int[] nexExp = { 3, 5, 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 };

    public Result uiResult;
    public GameObject enemyCleaner;

    void Awake()
    {
        instance = this;
    }

    public void GameStart(int id)
    {
        playerId = id;
        health = maxHealth;

        m_player.gameObject.SetActive(true);
        uiLevelUp.Select(playerId % 2);
        ResumeGame();
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        isLive = false;
        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        StopGame();
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }

    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()
    {
        isLive = false;
        enemyCleaner.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        StopGame();
    }

    void Update()
    {
        if (!isLive)
        {
            return;
        }
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
            GameVictory();
        }
    }

    public void GetExp()
    {
        if (!isLive)
        {
            return;
        }

        exp++;

        if (exp >= nexExp[Mathf.Min(level, nexExp.Length - 1)])
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    public void StopGame()
    {
        isLive = false;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        isLive = true;
        Time.timeScale = 1;
    }
}
