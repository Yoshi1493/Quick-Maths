using System.Collections.Generic;
using UnityEngine;
using static GameSettings;

public class InstructionsMenu : Menu
{
    [SerializeField] List<GameObject> instructions;

    public event System.Action StartCountdownAction;

    protected override void Awake()
    {
        base.Awake();

        //display instructions for appropriate game mode
        for (int i = 0; i < instructions.Count; i++)
        {
            instructions[i].SetActive(i == (int)playerSettings.selectedGameMode);
        }
    }
    
    public void StartCountdown()
    {
        //hide instructions
        instructions[(int)playerSettings.selectedGameMode].SetActive(false);

        StartCountdownAction?.Invoke();
        CloseMenu(thisMenu);
    }
}