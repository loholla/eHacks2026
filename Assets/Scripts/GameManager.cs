using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Mini Game Information")]
    public List<string> minigameScenes;
    [SerializeField] private string minigameSceneName;

    [Header("Player Stats")]
    public int playerHealth = 3;
    public int playerScore = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddScore(int amount)
    {
        playerScore += amount;
    }

    public void DamagePlayer(int amount)
    {
        playerHealth -= amount;

        if (playerHealth <= 0)
        {
            Debug.Log("Game Over");
        }
    }
}