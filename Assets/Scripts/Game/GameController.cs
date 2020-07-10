using UnityEngine;
using static GameSettings;

public class GameController : MonoBehaviour
{
    void Awake()
    {
        Game[] gameModes = transform.GetComponentsInChildren<Game>(true);

        for (int i = 0; i < gameModes.Length; i++)
        {
            gameModes[i].gameObject.SetActive(i == (int)playerSettings.selectedGameMode);
        }

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}