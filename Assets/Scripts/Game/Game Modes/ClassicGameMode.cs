using static GameSettings;

public class ClassicGameMode : GameController
{
    protected override void Awake()
    {
        base.Awake();

        if (playerSettings.selectedGameMode == GameMode.Classic)
        {
            FindObjectOfType<Countdown>().StartGameAction += OnStartGame;
        }
    }

    protected override void Start()
    {
        base.Start();
        clock.StartClock(0);
    }

    protected override void OnSubmitAnswer(int playerInput)
    {
        base.OnSubmitAnswer(playerInput);

        if (answerCount == playerSettings.questionCount)
        {
            clock.StopClock();
            OnGameOver(clock.time);
        }
    }
}