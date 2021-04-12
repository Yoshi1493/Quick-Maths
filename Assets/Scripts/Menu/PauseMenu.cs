using UnityEngine;

public class PauseMenu : Menu
{
    PauseController pauseController;
    [SerializeField] Canvas pausedMenu, unpausedMenu;

    protected override void Awake()
    {
        base.Awake();

        FindObjectOfType<Countdown>().StartGameAction += OnGameStart;

        pauseController = FindObjectOfType<PauseController>();
        pauseController.PauseGameAction += SetPausedState;
    }

    void OnGameStart()
    {
        OpenMenu(unpausedMenu);
    }

    void SetPausedState(bool isPaused)
    {
        if (isPaused)
        {
            unpausedMenu.GetComponent<Menu>().SwitchMenu(pausedMenu);
        }
        else
        {
            pausedMenu.GetComponent<Menu>().SwitchMenu(unpausedMenu);
        }
    }

    public void OnSelectResume()
    {
        pauseController.PauseGameAction?.Invoke(false);
    }

    public void OnSelectRetry()
    {
        LoadGame((int)GameSettings.selectedGameMode);
    }
}