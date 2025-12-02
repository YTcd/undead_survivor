using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player m_player;

    void Awake()
    {
        instance = this;
    }
}
