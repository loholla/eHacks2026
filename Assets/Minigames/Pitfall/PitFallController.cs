using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEditor.Rendering;

public class PitFallController : MonoBehaviour
{
    public Decks deck;
    private List<FlashCard> currentDeck;
    [SerializeField] private TextMeshProUGUI Question;
    private List<int> answerCardNums = new List<int>();
    private List<string> answerDefinitions = new List<string>();
    private int questionNum;
    [SerializeField] private TextMeshProUGUI Answer1;
    [SerializeField] private TextMeshProUGUI Answer2;
    [SerializeField] private TextMeshProUGUI Answer3;
    [SerializeField] private TextMeshProUGUI Answer4;
    [SerializeField] private TextMeshProUGUI Answer5;
    [SerializeField] private TextMeshProUGUI Answer6;
    [SerializeField] private TextMeshProUGUI Answer7;
    [SerializeField] private TextMeshProUGUI Answer8;

    void Start()
    {
        currentDeck = deck.flashcards;
        int deckSize = currentDeck.Count;
        for (int i = 0; i < 8; i++)
        {
            int temp = randomNum(deckSize, answerCardNums);
            answerCardNums.Add(temp);
        }

        for (int i = 0; i < answerCardNums.Count; i++)
        {
            int temp = answerCardNums[i];
            if (temp != -1){
                answerDefinitions.Add(currentDeck[temp].word);
            } else
            {
                answerDefinitions.Add("");
            }
        }

        Question.text = currentDeck[answerCardNums[Random.Range(0,answerCardNums.Count)]].definition;
        Answer1.text = answerDefinitions[0];
        Answer2.text = answerDefinitions[1];
        Answer3.text = answerDefinitions[2];
        Answer4.text = answerDefinitions[3];
        Answer5.text = answerDefinitions[4];
        Answer6.text = answerDefinitions[5];
        Answer7.text = answerDefinitions[6];
        Answer8.text = answerDefinitions[7];
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
