using static GameSettings;

public class TimedGame : Game
{
    protected override void Awake()
    {
        if (selectedGameMode == GameMode.Timed)
        {
            base.Awake();
        }
    }

    void Start()
    {
        GenerateQuestions(10);
        clock.StartClock(playerSettings.timeLimit);
    }

    protected override void OnSubmitAnswer(int playerInput)
    {
        base.OnSubmitAnswer(playerInput);
        GenerateQuestions(1);
    }
}