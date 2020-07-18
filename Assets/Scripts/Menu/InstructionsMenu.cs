using System.Collections.Generic;
using UnityEngine;
using static GameSettings;

public class InstructionsMenu : Menu
{
    public event System.Action StartCountdownAction;

    protected override void Awake()
    {
        base.Awake();
        SetInstructionsActive(true);
    }
    
    void SetInstructionsActive(bool active)
    {
        transform.GetChild((int)selectedGameMode).gameObject.SetActive(active);
    }

    public void StartCountdown()
    {
        SetInstructionsActive(false);

        StartCountdownAction?.Invoke();
        CloseMenu(thisMenu);
    }
}