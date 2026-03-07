using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private Slider timerBar;
    [SerializeField] private float StartingTime;

    private float currentTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTime = StartingTime;
    }

    // Update is called once per frame
    void Update()
    {
        timerBar.value = currentTime/StartingTime;

        currentTime -= Time.deltaTime; //Add speed up multiplier here

        if(currentTime <= 0)
        {
            //Call the you lose function
        }
    }
}
