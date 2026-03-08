using UnityEngine;


public class CatchTheAnswer : Minigame
{
    public GameObject answerBlockPrefab;
    private float dropDelay = 1f;
    private float xPos;
    private int correctAnswerDroppedRecently = 0;
    


    protected override void Start()
    {
        base.Start();
        Invoke("DropBlockDelay", 0.5f);
    }

    void DropBlockDelay()
    {
        dropDelay = Random.Range(0.33f, 0.75f);
        Invoke("DropBlock", dropDelay);
    }

    void DropBlock()
    {
        xPos = Random.Range(-15f, 15f);
        Vector3 spawnPos = new Vector3(xPos, 20f, 0f);
        AnswerBlock ablock = Instantiate(answerBlockPrefab, spawnPos, Quaternion.identity, transform).GetComponent<AnswerBlock>();

        ablock.isCorrect = Random.Range(0f, 1f) > 0.75f;

        if (ablock.isCorrect && correctAnswerDroppedRecently == 0)
        {
            ablock.GetComponent<MeshRenderer>().material.color = Color.green;
            correctAnswerDroppedRecently = 5;
        }
        else
        {
            ablock.GetComponent<MeshRenderer>().material.color = Color.red;
        }

        if (correctAnswerDroppedRecently > 0)
        {
            correctAnswerDroppedRecently--;
        }
        
        Invoke("DropBlockDelay", Time.deltaTime);
    }
    
    public void OnAnswerBlockHit(AnswerBlock block)
    {
        if (gameEnded) return;

        if (block.isCorrect)
        {
            Debug.Log("Correct Answer!");
            WonGame();
        }
        else
        {
            Debug.Log("Wrong Answer");
            LostGame();
        }
    }

}

