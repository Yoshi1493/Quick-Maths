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

    public static Dictionary<QuestionType, (bool enabled, int difficulty)> questionSettings = new Dictionary<QuestionType, (bool, int)>
    {
        [QuestionType.Addition] = (true, 0),
        [QuestionType.Subtraction] = (true, 0),
        [QuestionType.Multiplication] = (true, 0),
        [QuestionType.Division] = (true, 0)
    };

    public static Queue<int> answers = new Queue<int>();
    public static int numQuestionsAnswered, numQuestionsCorrect;
    #endregion

    #region Player Preferences
    public static GameMode selectedGameMode = GameMode.Classic;
    public static int questionCount = minQuestionCount;

    public static bool showClock = true;
    public static float timerDuration = minTimerDuration;

    #endregion

    #region Constants
    public const int minQuestionCount = 10;
    public const int maxQuestionCount = 100;

    public const float minTimerDuration = 60f;
    public const float maxTimerDuration = 300f;
    #endregion
}