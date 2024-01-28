using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score;
    public TMP_Text scoreText;
    public TMP_Text winningScoreText;
    public TMP_Text losingScoreText;
    void Awake()
    {

        if (instance == null)
        {
            instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            // ResetGameManager();
            Destroy(gameObject);

        }
        score = 0;
        UpdateScore();
    }

    public void AddScore(int points)
    {
        score += points;

        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
        UpdateScore();
    }

    void UpdateScore()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    public void DisplayWinningScore()
    {
        if (winningScoreText != null)
        {
            winningScoreText.text = "Final Score: " + score.ToString();
        }

    }
    public void DisplayLosingScore()
    {
        if (losingScoreText != null)
        {
            losingScoreText.text = "Final Score: " + score.ToString();
        }
    }

    public void ResetGameManager()
    {
        score = 0;
        UpdateScore();
    }

}
