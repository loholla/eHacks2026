using Unity.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class AnswerBlock : MonoBehaviour
{
    public bool isCorrect;
    public static float destroyObjectBound = 1f;
    public float movespeed = 5f;
    private CatchTheAnswer cta;

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
                cta.WonGame();
            }
            else
            {
                cta.LostGame();
            }
        }
    }

    void Update()
    {
        Vector3 movedir = new Vector3(0f, movespeed * Time.deltaTime, 0f);
        transform.position -= movedir;

        if (transform.position.y < destroyObjectBound)
        {
            Destroy(gameObject);
        }
    }
}