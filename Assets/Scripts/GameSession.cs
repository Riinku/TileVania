using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int playerScore = 0;
    [SerializeField] int gainLifeAfter = 10;        // after how many coins pick up to gain a life
    [SerializeField] Text livesText = null;
    [SerializeField] Text scoreText = null;



    private void Awake() 
    {        
        int numOfGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numOfGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        updatePlayerLives();
        updatePlayerScore();
    }


    public void AddToScore(int pointsToAdd)
    {
        playerScore += pointsToAdd;
        updatePlayerScore();

        if(playerScore >= gainLifeAfter)    
        {
            playerLives ++;
            updatePlayerLives();
            playerScore = 0;
            updatePlayerScore();
        }

    }

    public void HandlePlayerDeath()
    {
        if(playerLives > 1)                             // lose a life, update lives text and reload the current level
        {
            playerLives --;
            livesText.text = playerLives.ToString();
            ResetScore();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);       
        }
        else
        {
            ResetGameSession();
        }
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    private void updatePlayerLives()
    {
        livesText.text = playerLives.ToString();
    }

    private void updatePlayerScore()
    {
        scoreText.text = playerScore.ToString();
    }

    public void ResetScore()
    {
        playerScore = 0;
        updatePlayerScore();
    }

    public void DestroyGameSession()
    {
        Destroy(gameObject);
    }
}

