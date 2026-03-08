using System;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private Slider timerBar;
    [SerializeField] private float StartingTime;

    private float speedMultiplier;
    private float currentTime;
    [SerializeField] private bool timerIsPaused = false;
    [SerializeField] private Minigame minigame;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speedMultiplier = GameManager.Instance.speedMultiplier;
        currentTime = StartingTime;
        minigame = GetComponentInParent<Minigame>();

        if (timerBar != null) 
        {
            timerBar.maxValue = 1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("Timer at: " + Mathf.RoundToInt(currentTime));
        if (timerIsPaused) 
        {
            Debug.Log("Timer Paused");
            return;
        }

        timerBar.value = currentTime / StartingTime;
        currentTime -= Time.deltaTime * speedMultiplier;

        if (currentTime <= 0f)
        {
            TimeExpired();
        } 
    }

    void TimeExpired()
    {
        enabled = false;

        if (minigame != null) 
        {
            minigame.Timeout();
        }
    }

    public void PauseTimer()
    {
        enabled = false;
    }
}
