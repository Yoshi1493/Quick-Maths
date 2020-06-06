using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using static GameSettings;

public class ResultsMenu : Menu
{
    [SerializeField] TextMeshProUGUI gameStats;
    const string TimeDisplayFormat = "m':'ss'.'f";

    protected override void Awake()
    {
        base.Awake();
        FindObjectOfType<GameController>().gameOverAction += OnGameOver;
    }

    void OnGameOver(float gameTime)
    {
        OpenMenu(thisMenu);

        gameStats.text = $"Score: {numCorrectAnswers} / {playerSettings.questionCount} ({(numCorrectAnswers * 100f / playerSettings.questionCount).ToString("F1")}%){'\n'}";

        if (playerSettings.selectedGameMode == GameMode.Classic)
        {
            string finalTime = TimeSpan.FromSeconds(gameTime).ToString(TimeDisplayFormat);
            gameStats.text += $"Time: {finalTime} ";
        }

        string avgTime = TimeSpan.FromSeconds(gameTime / playerSettings.questionCount).ToString(TimeDisplayFormat);
        gameStats.text += $"(avg.: {avgTime})";
    }

    public void OnSelectRetry()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnSelectBackToMainMenu()
    {
        SceneManager.LoadScene("Main");
    }
}