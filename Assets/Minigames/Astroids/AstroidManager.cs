using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AstroidManager : Minigame
{
    [SerializeField] private GameObject astroidPrefab;
    [SerializeField] private int numberOfFakeAstroids, numberOfNormalAstroids;

    [SerializeField] private float xRange,yRange;
    [SerializeField] private float minSpeed, maxSpeed;

    [SerializeField] private GameObject spawnPoint;


    [SerializeField] private TextMeshProUGUI definitionText;

    [SerializeField] private Decks deck;
    private List<FlashCard> currentDeck = new List<FlashCard>();

    [SerializeField] float xBoundLower, xBoundUpper;

    private int indexOfMainCard;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        int randomIndex;

        currentDeck = deck.flashcards;

        indexOfMainCard = Random.Range(0, currentDeck.Count);

        definitionText.text = currentDeck[indexOfMainCard].definition;

        GameObject newAstroid = Instantiate(astroidPrefab, new Vector3(spawnPoint.transform.position.x + Random.Range(-xRange, xRange), spawnPoint.transform.position.y + Random.Range(-yRange, yRange), 0),Quaternion.identity, transform);
        newAstroid.GetComponent<astroid>().SetType(0, currentDeck[indexOfMainCard].word);

        for(int i = 0; i < numberOfFakeAstroids; i++)
        {
            newAstroid = Instantiate(astroidPrefab, new Vector3(spawnPoint.transform.position.x + Random.Range(-xRange, xRange), spawnPoint.transform.position.y + Random.Range(-yRange, yRange), 0), Quaternion.identity,transform);
            do
            {
                randomIndex = Random.Range(0, currentDeck.Count);
                

            } while (indexOfMainCard == randomIndex);

            newAstroid.GetComponent<astroid>().SetType(1, currentDeck[randomIndex].word);
        }

        for (int i = 0; i < numberOfNormalAstroids; i++)
        {
            newAstroid = Instantiate(astroidPrefab, new Vector3(spawnPoint.transform.position.x + Random.Range(-xRange, xRange), spawnPoint.transform.position.y + Random.Range(-yRange, yRange), 0), Quaternion.identity, transform);

            newAstroid.GetComponent<astroid>().SetType(2,"");
        }

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

       
    }
}
