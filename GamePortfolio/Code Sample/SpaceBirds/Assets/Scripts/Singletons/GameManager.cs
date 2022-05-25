using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        ResetGame();
        if (instance != null) Destroy(gameObject);
        else instance = this;
    }

    private int score;

    public void AddScore(int scoreToAdd) 
    { 
        score += scoreToAdd;
        UIPresenter.instance.SetScoreText(score);
    }

    public void SetGameOver()
    {
        UIPresenter.instance.SetGameOverText();
        UIPresenter.instance.SetRestartHintText();
        gameObject.AddComponent<GameOver>();
    }
    private void ResetGame()
    {
        Time.timeScale = 1;
    }
}
