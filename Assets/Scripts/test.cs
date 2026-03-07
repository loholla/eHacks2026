using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private List<FlashCard> flashcards;
    public Decks currentDeck;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        flashcards = currentDeck.flashcards;


        for (int i = 0; i < flashcards.Count; i++)
        {
            Debug.Log(flashcards[i].GetWord());
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }
}
