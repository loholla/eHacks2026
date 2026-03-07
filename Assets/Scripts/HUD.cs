using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;

    void Update()
    {
        scoreText.text = "Score: " + GameManager.Instance.playerScore;
        healthText.text = "Health: " + GameManager.Instance.playerHealth;
    }
}