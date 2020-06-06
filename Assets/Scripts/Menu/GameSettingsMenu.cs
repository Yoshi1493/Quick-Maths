using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static GameSettings;

public class GameSettingsMenu : Menu
{
    [SerializeField] List<Transform> gameSettingObjects;
    Button[] questionCountButtons = new Button[2];
    Button[] timerDurationButtons = new Button[2];
    TextMeshProUGUI questionCountDisplay, timerDurationDisplay;
    
    const string TimeDisplayFormat = "m':'ss";

    protected override void Awake()
    {
        base.Awake();
        InitUIObjects();
    }

    void InitUIObjects()
    {
        questionCountButtons = gameSettingObjects[0].GetComponentsInChildren<Button>();
        questionCountDisplay = ActuallyGetComponentInChildren<TextMeshProUGUI>(gameSettingObjects[0]);

        timerDurationButtons = gameSettingObjects[1].GetComponentsInChildren<Button>();
        timerDurationDisplay = ActuallyGetComponentInChildren<TextMeshProUGUI>(gameSettingObjects[1]);
    }

    T ActuallyGetComponentInChildren<T>(Transform t) where T : Component
    {
        foreach (var item in t.GetComponentsInChildren<T>())
        {
            if (item.transform.parent == t)
            {
                return item;
            }
        }

        return null;
    }

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