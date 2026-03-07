using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private Slider timerBar;
    [SerializeField] private float StartingTime;

    private float speedMultiplier;
    private float currentTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speedMultiplier = GameManager.Instance.speedMultipler;
        currentTime = StartingTime;
    }

    // Update is called once per frame
    void Update()
    {
        timerBar.value = currentTime/StartingTime;

        currentTime -= Time.deltaTime; //Add speed up multiplier here

        if(currentTime <= 0)
        {
            GameManager.Instance.EndMinigame(false, 0);
        }
    }
}
