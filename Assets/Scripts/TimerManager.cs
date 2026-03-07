using System;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private Slider timerBar;
    [SerializeField] private float StartingTime;

    private float speedMultiplier;
    private float currentTime;
    [SerializeField] private bool paused = false;
    [SerializeField] private Minigame minigame;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speedMultiplier = GameManager.Instance.speedMultipler;
        currentTime = StartingTime;
        minigame = GetComponentInParent<Minigame>();

        if (timerBar != null) timerBar.maxValue = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("Timer at: " + Mathf.RoundToInt(currentTime));
        if (paused) 
        {
            Debug.Log("Timer Paused");
            return;
        }

        timerBar.value = currentTime/StartingTime;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            TimeExpired();
        } 
        else if (currentTime < StartingTime * 0.25f)
        {
            //Call the you lose function
        }
    }
}
