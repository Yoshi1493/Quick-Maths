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
        FindObjectOfType<Game>().GameOverAction += OnGameOver;
    }

    void OnGameOver(float gameTime, int answerCount, int correctAnswerCount)
    {
        OpenMenu(thisMenu);

        if (playerSettings.selectedGameMode != GameMode.Challenge)
        {
            gameStats.text += $"{correctAnswerCount} / {answerCount} ({(correctAnswerCount * 100f / answerCount).ToString("F1")}%)\n";
        }
        else
        {
            gameStats.text += $"{correctAnswerCount}\n";
        }

        if (playerSettings.selectedGameMode != GameMode.Timed)
        {
            string finalTime = TimeSpan.FromSeconds(gameTime).ToString(TimeDisplayFormat);
            gameStats.text += $"Time: {finalTime}\n";
        }

        string avgTime = TimeSpan.FromSeconds(gameTime / answerCount).ToString(TimeDisplayFormat);
        gameStats.text += $"(avg. time: {avgTime})";

        Destroy(FindObjectOfType<PauseController>());
    }

    public void OnSelectRetry()
    {
        SceneManager.LoadScene("Game");
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            BackToMainMenu();
        }
    }
}