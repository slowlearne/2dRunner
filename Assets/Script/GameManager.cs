using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("PlayerController")]
    public PlayerController playerController;
    [Header("UIController")]
    public UIController uIController;
    [Header("SaveHighScore")]
    public SaveHighScore saveHighScore;
    [Header("GameManager")]
    public bool isGameOver;
    public AudioSource musicPlayer;
    public AudioClip bgSong;                              //background Song
    public Animator animator;

    private void Start()
    {
        musicPlayer = GetComponent<AudioSource>();

        // Assign the audio clip to the AudioSource
        musicPlayer.clip = bgSong;

        // Enable looping
        musicPlayer.loop = true;

        // Play the audio
        musicPlayer.Play();
    }
    public void gameOver()
    {
        isGameOver = true;
        musicPlayer.Stop();
        print("GameOver");
        playerController.playerAcceleration = 0f;            //to halt the movement of the player
        playerController.playerCurrentSpeed = 0f;
        stopAnimation();
        print("player acceleration after game over is " + playerController.playerAcceleration);
        print("player speed is " + playerController.playerCurrentSpeed);
        uIController.gameOverText();
        saveHighScore.updateScore();
        saveHighScore.UpdateHighScore();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);                 //reloads the game
    }

    public void stopAnimation()
    {
        animator.enabled = false;
    }
}
