using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Mini Game Information")]
    public List<string> minigameScenes;
    public string currentMinigame;
    public float speedMultiplier = 1f;

    [Header("Player Stats")]
    public int playerHealth = 3;
    public int playerScore = 0;

    //Flag so transition know you lost a heart
    public bool lostAHeartThisRound;

    private Queue<int> previousScenes = new Queue<int>();

    void Awake()
    {
        bool instanceHasBeenMade = Instance != null;
        bool thisInstanceIsNotItself = Instance != this;

        if (instanceHasBeenMade && thisInstanceIsNotItself)
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
        lostAHeartThisRound = false;
        
        Invoke("StartNextMiniGame", 0.1f);
    }

    public void AddScore(int amount)
    {
        playerScore += amount;
    }

    public void DamagePlayer(int amount)
    {
        playerHealth -= amount;
        lostAHeartThisRound = true;

        if (playerHealth <= 0)
        {
            Debug.Log("Game Over");
        }
    }

    public async void StartNextMiniGame()
    {
        int index = Random.Range(0, minigameScenes.Count);

        while (previousScenes.Contains(index))
        {
            index = Random.Range(0, minigameScenes.Count);
        }

        previousScenes.Enqueue(index);
        if (previousScenes.Count > 3)
        {
            previousScenes.Dequeue();
        }

        currentMinigame = minigameScenes[index];
        
        Debug.Log("Loading minigame: " + currentMinigame);

        await SceneManager.LoadSceneAsync("TransitionScene", LoadSceneMode.Additive);
    }

    public async void EndMinigame(bool result, int score)
    {
        Debug.Log("EndMinigame() called");
        if (result)
        {
            AddScore(score);
            if (speedMultiplier <= 2.0f)
            {
                speedMultiplier += 0.1f;
            }
        }
        else
        {
            DamagePlayer(1);
        }

        if (!string.IsNullOrEmpty(currentMinigame))
        {
            await SceneManager.UnloadSceneAsync(currentMinigame);
            currentMinigame = "";
        }

        Debug.Log("Minigame ended");

        Invoke("StartNextMiniGame", 0.1f);
    }

    public void GameOver()
    {
        // Go to game over and leader board scene
        SceneManager.LoadScene("GameOver");
    }

}