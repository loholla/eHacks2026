using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;

public class QuickMatch : Minigame
{
    public Decks deck;
    private List<FlashCard> currentDeck;
    [SerializeField] private TextMeshProUGUI Question;
    [SerializeField] private TextMeshProUGUI Answer1;
    [SerializeField] private TextMeshProUGUI Answer2;
    [SerializeField] private TextMeshProUGUI Answer3;
    [SerializeField] private TextMeshProUGUI Answer4;

    private List<int> answerCardNums = new List<int>();
    private List<string> answerDefinitions = new List<string>();
    private int rightAnswer;

    protected override void Start()
    {
        base.Start();
        currentDeck = deck.flashcards;
        int deckSize = currentDeck.Count;
        for (int i = 0; i < 4; i++)
        {
            int temp = randomNum(deckSize, answerCardNums);
            answerCardNums.Add(temp);
        }

        for (int i = 0; i < answerCardNums.Count; i++)
        {
            int temp = answerCardNums[i];
            if (temp != -1)
            {
                answerDefinitions.Add(currentDeck[temp].word);
            }
            else
            {
                answerDefinitions.Add("");
            }
        }

        int correctAnswerNum = answerCardNums[Random.Range(0,answerCardNums.Count)];
        Question.text = currentDeck[correctAnswerNum].definition;
        string correctAnswer = currentDeck[correctAnswerNum].word;

        Answer1.text = answerDefinitions[0]; // Up
        Answer2.text = answerDefinitions[1]; // Right
        Answer3.text = answerDefinitions[2]; // Down
        Answer4.text = answerDefinitions[3]; // Left

        for (int i = 0; i < 4; i++)
        {
            if (answerDefinitions[i] == correctAnswer)
            {
                rightAnswer = i;
            }
        }
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

    private void HandleAction(string actionName)
    {
        if (actionName == "WalkUpD")
        {
            if (Answer1.text == answerDefinitions[rightAnswer])
            {
                WonGame();
            }
            else
            {
                LostGame();
            }
        } else if (actionName == "WalkLeftD")
        {
            if (Answer4.text == answerDefinitions[rightAnswer])
            {
                WonGame();
            }
            else
            {
                LostGame();
            }
        } else if (actionName == "WalkDownD")
        {
            if (Answer3.text == answerDefinitions[rightAnswer])
            {
                WonGame();
            }
            else
            {
                LostGame();
            }
        } else if (actionName == "WalkRightD")
        {
            if (Answer2.text == answerDefinitions[rightAnswer])
            {
                WonGame();
            }
            else
            {
                LostGame();
            }
        }
    }

    int randomNum(int deckSize, List<int> usedNums)
    {
        List<int> allNums = new List<int>();
        for (int i = 0; i < deckSize; i++)
        {
            if (!usedNums.Contains(i))
            {
                allNums.Add(i);
            }
        }

        if (allNums.Count == 0)
        {
            return -1;
        }

        int randIndex = Random.Range(0,allNums.Count);
        return allNums[randIndex];
    }
}
