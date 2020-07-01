using static GameSettings;

public class ChallengeGameMode : GameController
{
    const float TimeLimit = 60f;

    protected override void Awake()
    {
        base.Awake();

        if (playerSettings.selectedGameMode == GameMode.Challenge)
        {
            FindObjectOfType<Countdown>().StartGameAction += OnStartGame;
        }
    }

    protected override void Start()
    {
        base.Start();
        clock.StartClock(TimeLimit);
    }

    protected override void OnSubmitAnswer(int playerInput)
    {
        base.OnSubmitAnswer(playerInput);
        GenerateQuestions(1);
    }

    protected override void OnSubmitIncorrectAnswer()
    {
        base.OnSubmitIncorrectAnswer();

        clock.StopClock();
        OnGameOver(TimeLimit - clock.time);
    }
}