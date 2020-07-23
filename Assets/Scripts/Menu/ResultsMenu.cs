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

    void OnGameOver(float gameTime, (int correct, int total) answerData)
    {
        OpenMenu(thisMenu);

        if (selectedGameMode != GameMode.Challenge)
        {
            gameStats.text += $"{answerData.correct} / {answerData.total} ({(answerData.correct * 100f / answerData.total).ToString("F1")}%)\n";
        }
        else
        {
            gameStats.text += $"{answerData.correct}\n";
        }

        if (selectedGameMode != GameMode.Timed)
        {
            string finalTime = TimeSpan.FromSeconds(gameTime).ToString(TimeDisplayFormat);
            gameStats.text += $"Time: {finalTime}\n";
        }

        string avgTime = TimeSpan.FromSeconds(gameTime / answerData.total).ToString(TimeDisplayFormat);
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