using static GameSettings;

public class ClassicGame : Game
{
    protected override void Awake()
    {
        if (selectedGameMode == GameMode.Classic)
        {
            base.Awake();
        }
    }

    void Start()
    {
        GenerateQuestions(playerSettings.questionCount);
        clock.StartClock(0);
    }

    protected override void OnSubmitAnswer(int playerInput)
    {
        base.OnSubmitAnswer(playerInput);

        if (answers.Count == 0)
        {
            clock.StopClock();
            OnGameOver(clock._currentTime);
        }
    }

}