using System.Collections.Generic;

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

    public static int numCorrectAnswers;
    #endregion

    #region Constants
    public const int minQuestionCount = 10;
    public const int maxQuestionCount = 100;

    public const float minTimerDuration = 60f;
    public const float maxTimerDuration = 300f;
    #endregion

    #region Player Settings
    public static PlayerSettings playerSettings;
    #endregion
}