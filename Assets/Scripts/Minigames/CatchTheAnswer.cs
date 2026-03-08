using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CatchTheAnswer : Minigame
{
    public GameObject answerBlockPrefab;
    private float dropDelay = 1f;
    private float xPos;
    private int correctAnswerDroppedRecently = 0;
    [SerializeField] private GameObject player;
    [SerializeField] private float moveSpeed;
    private bool movingLeft = false;
    private bool movingRight = false;

    [SerializeField] public FlashCard prompt;
    public Decks Deck;
    private List<FlashCard> currentDeck;   

    int ans, counter;

    public TextMeshProUGUI question;

    protected override void Start()
    {
        base.Start();

        currentDeck = Deck.flashcards;

        ans = (int)Mathf.Abs(Random.Range(0f, currentDeck.Count - 1));
        counter = 0;
        
        foreach (var card in currentDeck)
        {
            if (counter == ans)
            {
                prompt = card;
                counter++;
            }
            else
            {
                counter++;
            }
        }

        currentDeck.RemoveAt(ans);

        question.SetText(prompt.definition);

        Invoke("DropBlockDelay", 0.5f);
    }
    protected override void Update()
    {
        base.Update();
        HandleMovement();

    }

    private void OnEnable()
    {
        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.OnActionPressed += HandleAction;
        }
    }

    private void OnDisable()
    {
        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.OnActionPressed -= HandleAction;
        }
    } 
    

    void HandleAction(string actionName)
    {
        switch (actionName)
        {
            case "WalkLeftD":
                Debug.Log("Moving Left");
                movingLeft = true;
                break;
            case "WalkLeftC":
                Debug.Log("Not Moving Left");
                movingLeft = false;
                break;
            case "WalkRightD":
                Debug.Log("Moving Right");
                movingRight = true;
                break;
            case "WalkRightC":
                Debug.Log("Not Moving Right");
                movingRight = false;
                break;
        }
    }

    void HandleMovement()
    {
        if (gameEnded)
        {
            return;
        }

        Vector3 moveDir = Vector3.zero;

        if (movingLeft && !movingRight)
        {
            moveDir.x -= Time.deltaTime * moveSpeed;
        }
        else if (movingRight && !movingLeft)
        {
            moveDir.x += Time.deltaTime * moveSpeed;
        }
        
        player.transform.position += moveDir;
    }

    void DropBlockDelay()
    {
        if (gameEnded) 
        {
            return;
        }

        dropDelay = Random.Range(0.33f, 0.75f);
        Invoke("DropBlock", dropDelay);
    }

    void DropBlock()
    {
        if (gameEnded) 
        {
            return;
        }

        xPos = Random.Range(-15f, 15f);
        Vector3 spawnPos = new Vector3(xPos, 18f, 0f);
        AnswerBlock ablock = Instantiate(answerBlockPrefab, spawnPos, Quaternion.identity, transform).GetComponent<AnswerBlock>();
        ablock.Init(this);

        ablock.GetComponent<MeshRenderer>().material.color = Color.black;

        ablock.isCorrect = Random.Range(0f, 1f) > 0.65f;

        if (ablock.isCorrect && correctAnswerDroppedRecently == 0)
        {
            ablock.answerText.SetText(prompt.word);
            correctAnswerDroppedRecently = 3;
        }
        else
        {
            ans = (int)Mathf.Abs(Random.Range(0f, currentDeck.Count - 1));
            ablock.answerText.SetText(currentDeck[ans].word);
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

