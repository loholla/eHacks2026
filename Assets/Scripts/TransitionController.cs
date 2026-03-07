using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TransitionController : MonoBehaviour
{
    [SerializeField] float delay = 2f;

    void Start()
    {
        Debug.Log("Transition started");
        StartCoroutine(TransitionRoutine());
    }
    
    IEnumerator TransitionRoutine()
    {
        yield return new WaitForSeconds(delay);
        
        GameManager.Instance.StartNextMiniGame();

        SceneManager.UnloadSceneAsync("TransitionScene");
    }
}