using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public PlayerController playerObj;
    public GameManager gameManager;
    public TMP_Text distanceText, showScoreText;
    GameObject gameover, reStart;
    public string scoreKey = "HighScore", highestScoreKey; // Key for saving the score
    public int score, distance,highestScore;
    public TMP_Text highScoreText;
    private void Awake()
    {
        distanceText =GameObject.Find("DistanceText").GetComponent<TMP_Text>();
        gameover = GameObject.Find("GameOver");
        reStart = GameObject.Find("Restart");
        gameover.SetActive(false);
        reStart.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isGameOver)
        {
            distance = Mathf.FloorToInt(playerObj.distance);
            print(distance);
            distanceText.text = distance.ToString();
        }
        
    }

    public void gameOverText()
    {
        gameover.SetActive(true);
        reStart.SetActive(true);
        /*SaveScore();*/
    }

    /*private void SaveScore()
    {
        // Save the score using PlayerPrefs
        PlayerPrefs.SetInt(scoreKey, distance);
        UpdateHighScore();
        PlayerPrefs.Save(); // Ensure the data is written to disk
    }

    public int GetSavedScore()
    {
        // Retrieve the saved score from PlayerPrefs
        return PlayerPrefs.GetInt(scoreKey, 0);
    }
  

    public void UpdateHighScore()
    {
        int currentHighScore = GetSavedScore();
        // If the current score is higher than the saved high score, update it
        if (distance > currentHighScore)
        {
            currentHighScore = distance;
            PlayerPrefs.SetInt(scoreKey, currentHighScore);
            PlayerPrefs.Save(); // Ensure the data is written to disk
            Debug.Log("New High Score: " + currentHighScore);
            highScoreText.text = "High score " + currentHighScore;
        }
    }
    public int GetHighScore()
    {
        // Retrieve the saved high score from PlayerPrefs
        return PlayerPrefs.GetInt(highestScoreKey, 0);
    }*/
}
