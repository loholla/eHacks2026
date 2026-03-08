using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Minigame : MonoBehaviour
{
    [SerializeField] protected int baseScore = 3000;
    [SerializeField] protected int calculatedScore;
    [SerializeField] private int scoreLosePerSecound = 250;  
    
    public bool gameEnded = false;
    private TimerManager timer;

    protected virtual void Start()
    {
        calculatedScore = baseScore;
        timer = GetComponentInChildren<TimerManager>();
        Debug.Log("Starting Minigame");
    }

    protected virtual void Update()
    {
        if (gameEnded) return;
        calculatedScore -= (int)(Time.deltaTime * scoreLosePerSecound * GameManager.Instance.speedMultipler);
    }

    public void WonGame()
    {
        if (gameEnded) return;
        gameEnded = true;

        timer?.PauseTimer();

        Debug.Log("Won Minigame");

        StartCoroutine(EndAfterDelay(true, calculatedScore));
    }

    public void LostGame()
    {
        if (gameEnded) return;
        gameEnded = true;

        timer?.PauseTimer();

        Debug.Log("Lost Minigame");

        StartCoroutine(EndAfterDelay(false, 0));
    }

    IEnumerator EndAfterDelay(bool result, int score)
    {
        yield return new WaitForSeconds(1f);

        GameManager.Instance.EndMinigame(result, score);
    }

    public virtual void Timeout()
    {
        Debug.Log("Timer Expired");
        LostGame();
    }

}