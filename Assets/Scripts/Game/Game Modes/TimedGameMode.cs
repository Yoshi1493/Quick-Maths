using static GameSettings;

public class TimedGameMode : GameController
{
    protected override void Awake()
    {
        base.Awake();

        if (playerSettings.selectedGameMode == GameMode.Timed)
        {
            FindObjectOfType<Countdown>().StartGameAction += OnStartGame;
        }
    }

    protected override void Start()
    {
        base.Start();
        clock.StartClock(playerSettings.timeLimit);
    }

    protected override void OnSubmitAnswer(int playerInput)
    {
        base.OnSubmitAnswer(playerInput);
        GenerateQuestions(1);
    }
}