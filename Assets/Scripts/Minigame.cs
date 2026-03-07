using System.Collections;
using UnityEngine;

public class Minigame : MonoBehaviour
{
    [SerializeField] protected int baseScore = 100;
    [SerializeField] protected int calculatedScore = 100;
    
    protected bool gameEnded = false;
    private TimerManager timer;

    void Start()
    {
        timer = GetComponentInChildren<TimerManager>();
        Debug.Log("Starting Minigame");
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