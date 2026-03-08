using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverManager : MonoBehaviour
{
    private int finalScore;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI inputName, yourScore;
    [SerializeField] GameObject inputObject;

    [SerializeField] LeaderBoard leaderBoard;

    [SerializeField] TextMeshProUGUI nameLeaderBoardText;
    [SerializeField] TextMeshProUGUI scoreLeaderBoardText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        finalScore = GameManager.Instance.playerScore;
        scoreText.text = $"Final Score: {finalScore}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAgain()
    {
        GameManager.Instance.ResetMy();

        


        SceneManager.UnloadSceneAsync("GameOver");
    }

    public void SubmiteToLeaderBoard()
    {
        inputObject.gameObject.SetActive(false);
        yourScore.gameObject.SetActive(false);

        string playerName = inputName.text;

        string nameLeaderBoardOutput = "";
        string scoreLeaderBoardOutput = "";

        int length = 10;

        if(leaderBoard.scores.Count < 10)
        {
            length = leaderBoard.scores.Count + 1;
        }

        for (int i = 0; i < length; i++)
        {
            if(finalScore >= leaderBoard.scores[i])
            {
                
                   leaderBoard.scores.Insert(i,finalScore);
                   leaderBoard.names.Insert(i, playerName);
                break;
               
            }
        }

        for(int i = 0;i < length; i++)
        {
            nameLeaderBoardOutput += $"{leaderBoard.names[i]}\n";
            scoreLeaderBoardOutput += $"{leaderBoard.scores[i]}\n";
        }

        nameLeaderBoardText.gameObject.SetActive (true);
        scoreLeaderBoardText.gameObject.SetActive(true);

        nameLeaderBoardText.text = nameLeaderBoardOutput;
        scoreLeaderBoardText.text = scoreLeaderBoardOutput;

    }
}
