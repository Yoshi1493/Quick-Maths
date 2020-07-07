using UnityEngine;

public class PauseController : MonoBehaviour
{
    bool isPaused;

    public delegate void PauseDelegate(bool state);
    public PauseDelegate PauseGameAction;

    void Awake()
    {
        PauseGameAction += SetPausedState;
        FindObjectOfType<Countdown>().StartGameAction += OnStartGame;
    }

    void OnStartGame()
    {
        enabled = true;
    }

#if UNITY_ANDROID
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            OnPausePressed();
        }
    }
#endif

    public void OnPausePressed()
    {
        PauseGameAction?.Invoke(!isPaused);
    }

    void SetPausedState(bool state)
    {
        isPaused = state;
    }
}