using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Decks", menuName = "Scriptable Objects/Decks")]
public class Decks : ScriptableObject
{
    public List<FlashCard> flashcards;
    
}
