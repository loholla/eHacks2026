using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TransitionController : MonoBehaviour
{
    [SerializeField] float delay = 3.5f;
    [SerializeField] Sprite threeHearts, twoHearts, oneHeart;
    [SerializeField] SpriteRenderer heartImage;
    [SerializeField] TextMeshProUGUI scoreText, miniGameName, miniGameTip;

    [SerializeField] private Animator animator;

    [SerializeField] private string staticOneHeart, staticTwoHearts, staticThreeHearts;
    [SerializeField] private string oneHeartClip, twoHeartClip, threeHeartClip;

    void Start()
    {
        Debug.Log("Transition started");

        switch (GameManager.Instance.currentMinigame)
        {
            case "ShootTheTarget":
                miniGameName.SetText("Shoot The Target!");
                miniGameTip.SetText("Left-click to select your answer!");
                break;
            case "Astroids":
                miniGameName.SetText("Asteroids!");
                miniGameTip.SetText("Use the A and D keys and Left-Click to fire!");
                break;
            case "Pitfall":
                miniGameName.SetText("Pitfall");
                miniGameTip.SetText("Use the WASD keys and get to a platform before time runs out!");
                break;
            case "WordScramble":
                miniGameName.SetText("Word Scramble!");
                miniGameTip.SetText("Use Left-Click to build a word!");
                break;
            case "CatchTheAnswer":
                miniGameName.SetText("Catch The Answer!");
                miniGameTip.SetText("Left-click to select your answer");
                break;
            case "QuickMatch":
                miniGameName.SetText("Quick Match!");
                miniGameTip.SetText("Use Left-Click to match word and definition together!");
                break;
        }

        scoreText.text = "Your Score: " + GameManager.Instance.playerScore.ToString();

        bool lostAHeart = GameManager.Instance.lostAHeartThisRound;

        switch (GameManager.Instance.playerHealth)
        {
            case 3:
                heartImage.sprite = threeHearts;

                animator.Play(staticThreeHearts);

               
                break;
            case 2:
                heartImage.sprite = twoHearts;


                animator.Play(staticTwoHearts);

                if (lostAHeart)
                {
                    animator.Play(oneHeartClip);
                }
                
                break;
            case 1:
                heartImage.sprite = oneHeart;

                animator.Play(staticOneHeart);

                if (lostAHeart)
                {
                    animator.Play(twoHeartClip);
                }
                
                break;
            default:
                if (lostAHeart)
                {
                    animator.Play(threeHeartClip);
                }
                break;
        }



        GameManager.Instance.lostAHeartThisRound = false;


        StartCoroutine(TransitionRoutine());
    }
    
    IEnumerator TransitionRoutine()
    {
        yield return new WaitForSeconds(delay);

        if (GameManager.Instance.playerHealth > 0)
        {
            SceneManager.LoadSceneAsync(GameManager.Instance.currentMinigame, LoadSceneMode.Additive);

            SceneManager.UnloadSceneAsync("TransitionScene");
        }
        else // player is dead
        {
            GameManager.Instance.GameOver();
        }
       
    }
}