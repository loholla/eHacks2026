using TMPro;
using UnityEngine;

public class AnswerBlock : MonoBehaviour
{
    public bool isCorrect;
    public static float destroyObjectBound = 1f;
    public float movespeed = 5f;
    private CatchTheAnswer cta;
    public TextMeshProUGUI answerText;
    public void Init(CatchTheAnswer game)
    {
        cta = game;
    }

    private void OnTriggerEnter(Collider coll)
    {
        Debug.Log("Made Contact with collider: " + coll);
        if (coll.CompareTag("Player"))
        {
            if (isCorrect)
            {
                cta.question.SetText("CORRECT!");
                cta.WonGame();
            }
            else
            {

                cta.question.SetText("INCORRECT!");
                cta.LostGame();
            }
        }
    }

    void Update()
    {
        if (cta.gameEnded)
        {
            return;
        }
        
        Vector3 movedir = new Vector3(0f, movespeed * Time.deltaTime, 0f);
        transform.position -= movedir;

        if (transform.position.y < destroyObjectBound)
        {
            Destroy(gameObject);
        }
    }
}