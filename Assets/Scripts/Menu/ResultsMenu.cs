using System;
using UnityEngine;
using TMPro;
using static GameSettings;

public class ResultsMenu : Menu
{
    [SerializeField] TextMeshProUGUI gameStats;
    public const string TimeDisplayFormat = "m':'ss'.'f";

    protected override void Awake()
    {
        base.Awake();
        FindObjectOfType<GameController>().gameOverAction += OnGameOver;
    }

    void OnGameOver(float gameTime)
    {
        OpenMenu(thisMenu);

        gameStats.text = $"Score: {numQuestionsCorrect} / {numQuestionsAnswered} ({(numQuestionsCorrect * 100f / numQuestionsAnswered).ToString("F1")}%){'\n'}";

        if (selectedGameMode == GameMode.Classic)
        {
            string finalTime = TimeSpan.FromSeconds(gameTime).ToString(TimeDisplayFormat);
            gameStats.text += $"Time: {finalTime} ";
        }

        string avgTime = TimeSpan.FromSeconds(gameTime / numQuestionsAnswered).ToString(TimeDisplayFormat);
        gameStats.text += $"(avg.: {avgTime})";
    }
}