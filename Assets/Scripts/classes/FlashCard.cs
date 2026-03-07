using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class FlashCard
{
    
    public string word;
    public string definition;


    public FlashCard(string word, string definition)
    {
        this.word = word;
        this.definition = definition;
    }

    public string GetWord()
    {
        return word;
    }

    public string GetDefinition()
    {
        return definition;
    }

}
