using UnityEngine;
using static GameSettings;

public class ChallengeGame : Game
{
    int[] difficulties = new int[System.Enum.GetValues(typeof(QuestionType)).Length];

    const float TimeLimit = 60f;
    const float TimeRegen = 10f;

    const int DifficultyIncreaseRate = 5;
    const int MaxDifficultyLevel = 5;

    protected override void Awake()
    {
        if (playerSettings.selectedGameMode == GameMode.Challenge)
        {
            base.Awake();
        }
    }

    void Start()
    {
        GenerateQuestions(10);
        clock.StartClock(TimeLimit);
    }

    protected override void GenerateQuestions(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int randNum = Random.Range(0, System.Enum.GetValues(typeof(QuestionType)).Length);
            int difficulty = Mathf.Min(difficulties[randNum] / DifficultyIncreaseRate, MaxDifficultyLevel);
            string question = GenerateQuestion((QuestionType)randNum, difficulty);

            questionDisplayBox.text += question + '\n';
            difficulties[randNum]++;
        }
    }

    protected override void OnSubmitAnswer(int playerInput)
    {
        base.OnSubmitAnswer(playerInput);
        GenerateQuestions(1);
    }

    protected override void OnSubmitCorrectAnswer()
    {
        base.OnSubmitCorrectAnswer();
        clock.AddTime(TimeRegen);
    }

    protected override void OnSubmitIncorrectAnswer()
    {
        base.OnSubmitIncorrectAnswer();

        clock.StopClock();
        print(clock._elapsedTime);
        OnGameOver(clock._elapsedTime);
    }
}