using System.Collections.Generic;
using UnityEngine;
using static MathHelper;

public static class GameSettings
{
    #region Game Data
    public enum GameMode
    {
        Classic,
        Timed
    }

    public enum QuestionType
    {
        Addition,
        Subtraction,
        Multiplication,
        Division
    }

    public static Dictionary<QuestionType, (bool enabled, int difficulty)> questionSettings = new Dictionary<QuestionType, (bool, int)>
    {
        [QuestionType.Addition] = (true, 0),
        [QuestionType.Subtraction] = (true, 0),
        [QuestionType.Multiplication] = (true, 0),
        [QuestionType.Division] = (true, 0)
    };

    public static Queue<int> answers = new Queue<int>();
    public static int numQuestionsAnswered, numQuestionsCorrect;

    static int TenToThePowerOf(int num)
    {
        return (int)Mathf.Pow(10, num);
    }
    
    static (int min, int max) GetNumberRange(QuestionType questionType, int difficulty)
    {
        (int min, int max) result = (0, 0);

        switch (questionType)
        {
            case QuestionType.Addition:
            case QuestionType.Subtraction:
                result.min = TenToThePowerOf(difficulty);
                result.max = TenToThePowerOf(difficulty + 1);
                break;
            case QuestionType.Multiplication:
                result.min = TenToThePowerOf(difficulty / 2);
                result.max = TenToThePowerOf((difficulty / 2) + 1);
                break;
            case QuestionType.Division:
                result.min = TenToThePowerOf((difficulty % 3) + (difficulty / 3) + 1);
                result.max = TenToThePowerOf((difficulty % 3) + (difficulty / 3) + 2);
                break;
        }

        return result;
    }

    static string FormatInteger(int i)
    {
        return i.ToString(IntDisplayFormat);
    }

    public static string GenerateQuestion(QuestionType questionType)
    {
        string output = "";
        int num1 = 0, num2 = 0;

        switch (questionType)
        {
            case QuestionType.Addition:
                num1 = GetRandomNumber(GetNumberRange(questionType, questionSettings[questionType].difficulty));
                num2 = GetRandomNumber(GetNumberRange(questionType, questionSettings[questionType].difficulty));

                output = $"{FormatInteger(num1)} + {FormatInteger(num2)} =";
                answers.Enqueue(num1 + num2);
                break;

            case QuestionType.Subtraction:
                num1 = GetRandomNumber(GetNumberRange(questionType, questionSettings[questionType].difficulty));
                num2 = GetRandomNumber((GetNumberRange(questionType, questionSettings[questionType].difficulty).min, num1));

                output = $"{FormatInteger(num1)} - {FormatInteger(num2)} =";
                answers.Enqueue(num1 - num2);
                break;

            case QuestionType.Multiplication:
                num1 = GetRandomNumber(GetNumberRange(questionType, questionSettings[questionType].difficulty + 1));
                num2 = GetRandomNumber(GetNumberRange(questionType, questionSettings[questionType].difficulty));

                output = $"{FormatInteger(num1)} × {FormatInteger(num2)} =";
                answers.Enqueue(num1 * num2);
                break;

            case QuestionType.Division:
                List<int> num1Factors = GetFactors(num1);
                while (num1Factors.Count <= 2)
                {
                    num1 = GetRandomNumber(GetNumberRange(questionType, questionSettings[questionType].difficulty));
                    num1Factors = GetFactors(num1);
                }

                num2 = GetRandomNumber(num1Factors);

                output = $"{FormatInteger(num1)} ÷ {FormatInteger(num2)} =";
                answers.Enqueue(num1 / num2);
                break;
        }

        return output;
    }
    #endregion

    #region Player Options
    public static GameMode selectedGameMode = GameMode.Classic;
    public static int questionCount = minQuestionCount;

    public static bool showClock = true;
    public static float timerDuration = minTimerDuration;

    #endregion

    #region Constants
    public const int minQuestionCount = 10, maxQuestionCount = 100;
    public const float minTimerDuration = 60f, maxTimerDuration = 300f;

    public const string IntDisplayFormat = "N0";
    public const string TimeDisplayFormat = "m':'ss'.'f";
    #endregion
}