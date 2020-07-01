public static class GameSettings
{
    #region Game Data
    public enum GameMode
    {
        Classic,
        Timed,
        Challenge
    }

    public enum QuestionType
    {
        Addition,
        Subtraction,
        Multiplication,
        Division
    }
    #endregion

    #region Constants
    public const int minQuestionCount = 10;
    public const int maxQuestionCount = 100;

    public const float minTimeLimit = 60f;
    public const float maxTimeLimit = 300f;

    public const string TimeDisplayFormat = "m':'ss";
    #endregion

    #region Player Settings
    public static PlayerSettings playerSettings;
    #endregion
}