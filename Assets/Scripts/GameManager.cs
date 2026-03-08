using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Mini Game Information")]
    public List<string> minigameScenes;
    [SerializeField] private string currentMinigame;
    public float speedMultipler = 1f;

    [Header("Player Stats")]
    public int playerHealth = 3;
    public int playerScore = 0;

    //Flag so transition know you lost a heart
    public bool lostAHeartThisRound;

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
        SceneManager.LoadSceneAsync("TransitionScene", LoadSceneMode.Additive);

        
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

            if(speedMultipler < 2)
            {
                speedMultipler += .1f;
            }
        }
        else
        {
            DamagePlayer(1);
            //if (playerHealth == 0)
            //{
            //    //GameOver(); Transition Controller now handles this
            //    return;
            //}
        }

        if (!string.IsNullOrEmpty(currentMinigame))
        {
            await SceneManager.UnloadSceneAsync(currentMinigame);
            currentMinigame = "";
        }

        Debug.Log("Minigame ended");

        SceneManager.LoadScene("TransitionScene", LoadSceneMode.Additive);
    }

    public void GameOver()
    {
        // Go to game over and leader board scene
        SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
    }

    public void ResetMy()
    {
        playerHealth = 3;
        playerScore = 0;
        speedMultipler = 1f;
        Start();
    }

}