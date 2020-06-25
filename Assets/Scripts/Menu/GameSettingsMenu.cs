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

    [SerializeField] TextMeshProUGUI clockToggleDisplay;

    protected override void Awake()
    {
        base.Awake();
        InitUIObjects();
    }

    void InitUIObjects()
    {
        UpdateQuestionCountButtons();
        UpdateTimerLimitButtons();
        UpdateClockToggleDisplay();
    }

    void UpdateQuestionCountButtons()
    {
        questionCountButtons[0].interactable = playerSettings.questionCount > minQuestionCount;
        questionCountButtons[1].interactable = playerSettings.questionCount < maxQuestionCount;
        questionCountDisplay.text = playerSettings.questionCount.ToString();
    }

    void UpdateTimerLimitButtons()
    {
        timerDurationButtons[0].interactable = playerSettings.timeLimit > minTimeLimit;
        timerDurationButtons[1].interactable = playerSettings.timeLimit < maxTimeLimit;
        timerDurationDisplay.text = TimeSpan.FromSeconds(playerSettings.timeLimit).ToString(TimeDisplayFormat);
    }

    void UpdateClockToggleDisplay()
    {
        clockToggleDisplay.text = "Clock: " + (playerSettings.clockDisplayEnabled ? "on" : "off");
    }

    public void OnChangeQuestionCount(int changeAmount)
    {
        playerSettings.questionCount = Mathf.Clamp(playerSettings.questionCount + changeAmount, minQuestionCount, maxQuestionCount);
        UpdateQuestionCountButtons();
    }

    public void OnChangeTimeLimit(int changeAmount)
    {
        playerSettings.timeLimit = Mathf.Clamp(playerSettings.timeLimit + changeAmount, minTimeLimit, maxTimeLimit);
        UpdateTimerLimitButtons();
    }

    public void OnToggleClockDisplay()
    {
        playerSettings.clockDisplayEnabled = !playerSettings.clockDisplayEnabled;
        UpdateClockToggleDisplay();
    }

    public override void SwitchMenu(Canvas otherMenu)
    {
        FileHandler.SaveSettings();
        base.SwitchMenu(otherMenu);
    }
}