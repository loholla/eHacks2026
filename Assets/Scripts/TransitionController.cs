using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TransitionController : MonoBehaviour
{
    [SerializeField] float delay = 2f;
    [SerializeField] Sprite threeHearts, twoHearts, oneHeart;
    [SerializeField] SpriteRenderer heartImage;
    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] private Animator animator;

    [SerializeField] private string staticOneHeart, staticTwoHearts, staticThreeHearts;
    [SerializeField] private string oneHeartClip, twoHeartClip, threeHeartClip;

    void Start()
    {
        Debug.Log("Transition started");

        scoreText.text = GameManager.Instance.playerScore.ToString();

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
            GameManager.Instance.StartNextMiniGame();

            SceneManager.UnloadSceneAsync("TransitionScene");
        }
        else // player is dead
        {
            GameManager.Instance.GameOver();
        }
       
    }
}