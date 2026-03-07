using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using NUnit.Framework;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Mini Game Information")]
    public List<string> minigameScenes;
    [SerializeField] private string currentMinigame;
    public float speedMultipler;

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

    public async void StartNextMiniGame()
    {
        if (!string.IsNullOrEmpty(currentMinigame))
        {
            await SceneManager.UnloadSceneAsync(currentMinigame);
        }

        int index = Random.Range(0, minigameScenes.Count);
        currentMinigame = minigameScenes[index];

        await SceneManager.LoadSceneAsync(currentMinigame, LoadSceneMode.Additive);
    }

    public async void EndMinigame(bool result, int score)
    {
        if (result)
        {
            Instance.AddScore(score);
        }
        else
        {
            Instance.DamagePlayer(1);
            if (playerHealth == 0)
            {
                GameOver();
            }
        }

        await SceneManager.UnloadSceneAsync(currentMinigame);

        SceneManager.LoadScene("TransitionScene");
    }

    public async void GameOver()
    {
        // Go to game over and leader board scene
    }
}