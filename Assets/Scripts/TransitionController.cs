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

    [SerializeField] private Animator animator;

    [SerializeField] private string oneHeartClip, twoHeartClip, threeHeartClip;

    void Start()
    {
        Debug.Log("Transition started");

        

        bool lostAHeart = GameManager.Instance.lostAHeartThisRound;

        switch (GameManager.Instance.playerHealth)
        {
            case 3:
                heartImage.sprite = threeHearts;
                Debug.Log(lostAHeart);
                if (lostAHeart)
                {
                    animator.Play(threeHeartClip);
                }
                break;
            case 2:
                heartImage.sprite = twoHearts;
                Debug.Log(lostAHeart);
                if (lostAHeart)
                {
                    animator.Play(twoHeartClip);
                }
                break;
            default:
                heartImage.sprite = oneHeart;
                if (lostAHeart)
                {
                    animator.Play(oneHeartClip);
                }
                break;
        }

       
        



        StartCoroutine(TransitionRoutine());
    }
    
    IEnumerator TransitionRoutine()
    {
        yield return new WaitForSeconds(delay);
        
        GameManager.Instance.StartNextMiniGame();

        SceneManager.UnloadSceneAsync("TransitionScene");
    }
}