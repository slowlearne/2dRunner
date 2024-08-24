using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveHighScore : MonoBehaviour
{
    public UIController uIController;
    private int score;
    public TMP_Text scoreText;
    public string highScoreKey = "HighScore"; // Key for saving the high score
    public string scoreKey = "Score";
    private void Start()
    {
        /*ClearAllData();*/                           // to clear saved data.
        score = GetHighScore();
        UpdateScoreText();
    }
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }


    public void updateScore()
    {
        score = uIController.distance;
        PlayerPrefs.SetInt(scoreKey, score);
        PlayerPrefs.Save();
    }
    public int getScore()
    {
       return PlayerPrefs.GetInt(scoreKey, 0);
    }
    public void UpdateHighScore()
    {
        int currentHighScore = GetHighScore();

        // If the current score is higher than the saved high score, update it
        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt(highScoreKey, score);
            PlayerPrefs.Save(); // Ensure the data is written to disk
            Debug.Log("New High Score: " + score);
        }
    }

    public int GetHighScore()
    {
        // Retrieve the saved high score from PlayerPrefs
        return PlayerPrefs.GetInt(highScoreKey, 0);
    }

    public void ClearAllData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("All PlayerPrefs data cleared.");
    }


}
