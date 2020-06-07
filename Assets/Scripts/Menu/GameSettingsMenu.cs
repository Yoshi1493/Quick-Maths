using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static GameSettings;

public class GameSettingsMenu : Menu
{
    [SerializeField] Button[] questionCountButtons = new Button[2];
    [SerializeField] TextMeshProUGUI questionCountDisplay;

    [SerializeField] Button[] timerDurationButtons = new Button[2];
    [SerializeField] TextMeshProUGUI timerDurationDisplay;
    
    const string TimeDisplayFormat = "m':'ss";
    
    public void OnChangeQuestionCount(int changeAmount)
    {
        playerSettings.questionCount = Mathf.Clamp(playerSettings.questionCount + changeAmount, minQuestionCount, maxQuestionCount);

        questionCountButtons[0].interactable = playerSettings.questionCount > minQuestionCount;
        questionCountButtons[1].interactable = playerSettings.questionCount < maxQuestionCount;
        questionCountDisplay.text = playerSettings.questionCount.ToString();
    }

    public void OnChangeTimerDuration(int changeAmount)
    {
        playerSettings.timerDuration = Mathf.Clamp(playerSettings.timerDuration + changeAmount, minTimerDuration, maxTimerDuration);

        timerDurationButtons[0].interactable = playerSettings.timerDuration > minTimerDuration;
        timerDurationButtons[1].interactable = playerSettings.timerDuration < maxTimerDuration;
        timerDurationDisplay.text = TimeSpan.FromSeconds(playerSettings.timerDuration).ToString(TimeDisplayFormat);
    }

    public override void SwitchMenu(Canvas otherMenu)
    {
        FileHandler.SaveSettings();
        base.SwitchMenu(otherMenu);
    }
}