using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("PlayerController")]
    public PlayerController playerObj;
    [Header("GameManager")]
    public GameManager gameManager;
    [Header("UI")]
    public TMP_Text distanceText, showScoreText;
    GameObject gameover, reStart;
    public string scoreKey = "HighScore", highestScoreKey; // Key for saving the score
    public int score, distance, highestScore;
    public TMP_Text highScoreText;
    private void Awake()
    {
        distanceText = GameObject.Find("DistanceText").GetComponent<TMP_Text>();
        gameover = GameObject.Find("GameOver");
        reStart = GameObject.Find("Restart");
        gameover.SetActive(false);
        reStart.SetActive(false);
    }
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
    }
}
