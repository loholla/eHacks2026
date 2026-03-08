<<<<<<< HEAD
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEditor.Rendering;
using Unity.Collections;

public class PitFallController : Minigame
{
    public Decks deck;
    private List<FlashCard> currentDeck;
    [SerializeField] private TextMeshProUGUI Question;
    private List<int> answerCardNums = new List<int>();
    private List<string> answerDefinitions = new List<string>();
    private int questionNum;
    private int rightAnswer;
    [SerializeField] private TextMeshProUGUI Answer1;
    [SerializeField] private TextMeshProUGUI Answer2;
    [SerializeField] private TextMeshProUGUI Answer3;
    [SerializeField] private TextMeshProUGUI Answer4;
    [SerializeField] private TextMeshProUGUI Answer5;
    [SerializeField] private TextMeshProUGUI Answer6;
    [SerializeField] private TextMeshProUGUI Answer7;
    [SerializeField] private TextMeshProUGUI Answer8;

    [SerializeField] private GameObject answer1Floor;
    [SerializeField] private GameObject answer2Floor;
    [SerializeField] private GameObject answer3Floor;
    [SerializeField] private GameObject answer4Floor;
    [SerializeField] private GameObject answer5Floor;
    [SerializeField] private GameObject answer6Floor;
    [SerializeField] private GameObject answer7Floor;
    [SerializeField] private GameObject answer8Floor;
    [SerializeField] private GameObject ground;

    protected override void Start()
    {
        base.Start();
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

        int correctAnswerNum = answerCardNums[Random.Range(0,answerCardNums.Count)];
        Question.text = currentDeck[correctAnswerNum].definition;
        string correctAnswer = currentDeck[correctAnswerNum].word;
        Answer1.text = answerDefinitions[0];
        Answer2.text = answerDefinitions[1];
        Answer3.text = answerDefinitions[2];
        Answer4.text = answerDefinitions[3];
        Answer5.text = answerDefinitions[4];
        Answer6.text = answerDefinitions[5];
        Answer7.text = answerDefinitions[6];
        Answer8.text = answerDefinitions[7];

        for (int i = 0; i < 8; i++)
        {
            if (answerDefinitions[i] == correctAnswer)
            {
                rightAnswer = i;
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

    void removePlatforms()
    {
        List<GameObject> floors = new List<GameObject>() {answer1Floor, answer2Floor, answer3Floor, answer4Floor, answer5Floor, answer6Floor, answer7Floor, answer8Floor, ground};

        for (int i = 0; i < 9; i++)
        {
            if (i != rightAnswer)
            {
                Destroy(floors[i]);
            }    
        }
        Invoke("wincon", 5f);
    }

    void wincon()
    {
        if (winning)
        {
            WonGame();
        } else
        {
            LostGame();
        }
    }

    bool winning = true;
    void OnTriggerExit()
    {
        winning = false;
    }

    public override void Timeout()
    {
        Debug.Log("Timer Expired");
        removePlatforms();
    }
}
=======
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEditor.Rendering;
using Unity.Collections;

public class PitFallController : Minigame
{
    public Decks deck;
    private List<FlashCard> currentDeck;
    [SerializeField] private TextMeshProUGUI Question;
    private List<int> answerCardNums = new List<int>();
    private List<string> answerDefinitions = new List<string>();
    private int questionNum;
    private int rightAnswer;
    [SerializeField] private TextMeshProUGUI Answer1;
    [SerializeField] private TextMeshProUGUI Answer2;
    [SerializeField] private TextMeshProUGUI Answer3;
    [SerializeField] private TextMeshProUGUI Answer4;
    [SerializeField] private TextMeshProUGUI Answer5;
    [SerializeField] private TextMeshProUGUI Answer6;
    [SerializeField] private TextMeshProUGUI Answer7;
    [SerializeField] private TextMeshProUGUI Answer8;

    [SerializeField] private GameObject answer1Floor;
    [SerializeField] private GameObject answer2Floor;
    [SerializeField] private GameObject answer3Floor;
    [SerializeField] private GameObject answer4Floor;
    [SerializeField] private GameObject answer5Floor;
    [SerializeField] private GameObject answer6Floor;
    [SerializeField] private GameObject answer7Floor;
    [SerializeField] private GameObject answer8Floor;
    [SerializeField] private GameObject ground;

    protected override void Start()
    {
        base.Start();
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

        int correctAnswerNum = answerCardNums[Random.Range(0,answerCardNums.Count)];
        Question.text = currentDeck[correctAnswerNum].definition;
        string correctAnswer = currentDeck[correctAnswerNum].word;
        Answer1.text = answerDefinitions[0];
        Answer2.text = answerDefinitions[1];
        Answer3.text = answerDefinitions[2];
        Answer4.text = answerDefinitions[3];
        Answer5.text = answerDefinitions[4];
        Answer6.text = answerDefinitions[5];
        Answer7.text = answerDefinitions[6];
        Answer8.text = answerDefinitions[7];

        for (int i = 0; i < 8; i++)
        {
            if (answerDefinitions[i] == correctAnswer)
            {
                rightAnswer = i;
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

    void removePlatforms()
    {
        List<GameObject> floors = new List<GameObject>() {answer1Floor, answer2Floor, answer3Floor, answer4Floor, answer5Floor, answer6Floor, answer7Floor, answer8Floor, ground};

        for (int i = 0; i < 9; i++)
        {
            if (i != rightAnswer)
            {
                Destroy(floors[i]);
            }    
        }
        Invoke("wincon", 5f);
    }

    void wincon()
    {
        if (winning)
        {
            WonGame();
        } else
        {
            LostGame();
        }
    }

    bool winning = true;
    void OnTriggerExit()
    {
        winning = false;
    }

    public override void Timeout()
    {
        Debug.Log("Timer Expired");
        removePlatforms();
    }
}
>>>>>>> 5949865 (feat/completed Pitfall)
