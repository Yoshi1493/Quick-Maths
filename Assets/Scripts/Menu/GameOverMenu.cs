using System;
using UnityEngine;
using TMPro;
using static GameSettings;

public class GameOverMenu : Menu
{
    [SerializeField] TextMeshProUGUI gameStats;

    protected override void Awake()
    {
        base.Awake();
        FindObjectOfType<GameController>().gameOverAction += OnGameOver;
    }

    void OnGameOver(float gameTime)
    {
        OpenMenu(thisMenu);

        gameStats.text = $"Score: {numQuestionsCorrect} / {numQuestionsAnswered} ({(numQuestionsCorrect * 100f / numQuestionsAnswered).ToString("F1")}%)";

        if (selectedGameMode == GameMode.Classic)
        {
            string finalTime = TimeSpan.FromSeconds(gameTime).ToString(TimeDisplayFormat);
            gameStats.text += $"{'\n'}Time: {finalTime}";
        }

        string avgTime = TimeSpan.FromSeconds(gameTime / numQuestionsAnswered).ToString(TimeDisplayFormat);
        gameStats.text += $"{'\n'}Avg. time per question: {avgTime}";
    }
}