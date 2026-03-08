using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WordScrambleManager : Minigame
{
    [SerializeField] private Decks deck;
    [SerializeField] private GameObject canvas;
    [SerializeField] private TextMeshProUGUI mainText;
    [SerializeField] private TextMeshProUGUI wordDisplay;

    private List<FlashCard> flashCards = new List<FlashCard>();

    private FlashCard mainCard;

    [SerializeField] private TextMeshProUGUI definition;
    [SerializeField] private Button buttonPrefab;

    [SerializeField] private int numberOfSegments;

    [SerializeField] float MinXPosition, MaxXPosition, MinYPosition, MaxYPosition;
    [SerializeField] float MinXNoZone, MaxXNoZone, MinYNoZone, MaxYNoZone;

    [SerializeField] List<Button> buttonList = new List<Button>();

    private int segmentLengths;
    private List<string> segments = new List<string>();

    private string reconstructedString;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        reconstructedString = "";

        wordDisplay.text = reconstructedString;

        flashCards = deck.flashcards;

        mainCard = flashCards[Random.Range(0, flashCards.Count)];

        definition.text = mainCard.definition;

        segmentLengths = (int)Mathf.Ceil(mainCard.word.Length / numberOfSegments);
        if (segmentLengths <= 0) segmentLengths = 1;

        for(int i = 0; i < mainCard.word.Length; i++)
        {
            if(i%segmentLengths == 0)
            {
                segments.Add(mainCard.word.Substring(i, segmentLengths));
                
            }
            
        }

        for (int i = segments.Count - 1; i >= 0; i--)
        {
            Button newButton = Instantiate(buttonPrefab, canvas.transform);
            string theSegment = segments[i];
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = theSegment;
            newButton.onClick.AddListener(() => OnButtonClick(theSegment, newButton.gameObject));
            do
            {
                newButton.transform.localPosition = new Vector3(Random.Range(MinXPosition, MaxXPosition), Random.Range(MinYPosition, MaxYPosition), newButton.transform.localPosition.z);
            } while ((newButton.transform.localPosition.x <= MaxXNoZone && newButton.transform.localPosition.x >= MinXNoZone) && (newButton.transform.localPosition.y <= MaxYNoZone && newButton.transform.localPosition.y >= MinYNoZone));
            buttonList.Add(newButton);
        }    
    
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }


    public void OnButtonClick(string segment, GameObject button)
    {

        Destroy(button);
        reconstructedString += segment;

        wordDisplay.text = reconstructedString;

        string wordUpTill = mainCard.word.Substring(0, reconstructedString.Length);

        if (wordUpTill != reconstructedString)
        {
            mainText.text = $"Inncorrect the word is:\n {mainCard.word}";
            mainText.transform.position = new Vector3(mainText.transform.position.x, mainText.transform.position.y, 2);
            for(int i = buttonList.Count - 1; i >= 0; i--)
            {
                Destroy(buttonList[i].gameObject);
            }
            LostGame();
        }

        if(reconstructedString == mainCard.word)
        {
            mainText.text = $"Good Job!";
            WonGame();
        }

        
    }

}
