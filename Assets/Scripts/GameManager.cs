using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] float delay = 1.5f;

    [Header("Mini Game Information")]
    public List<string> minigameScenes;
    [SerializeField] private string currentMinigame;
    public float speedMultipler = 1f;

    [Header("Player Stats")]
    public int playerHealth = 3;
    public int playerScore = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(transform.root.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(transform.root.gameObject);
    }

    void Start()
    {
        Debug.Log("Starting Game");
        SceneManager.LoadSceneAsync("TransitionScene", LoadSceneMode.Additive);
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
        int index = Random.Range(0, minigameScenes.Count);
        currentMinigame = minigameScenes[index];

        Debug.Log("Loading minigame: " + currentMinigame);

        await SceneManager.LoadSceneAsync(currentMinigame, LoadSceneMode.Additive);
    }

    public async void EndMinigame(bool result, int score)
    {
        Debug.Log("EndMinigame() called");
        if (result)
        {
            AddScore(score);
        }
        else
        {
            DamagePlayer(1);
            if (playerHealth == 0)
            {
                GameOver();
                return;
            }
        }

        if (!string.IsNullOrEmpty(currentMinigame))
        {
            await SceneManager.UnloadSceneAsync(currentMinigame);
            currentMinigame = "";
        }

        Debug.Log("Minigame ended");

        SceneManager.LoadScene("TransitionScene");
    }

    public async void GameOver()
    {
        // Go to game over and leader board scene
    }

}