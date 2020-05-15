using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static GameSettings;

public class SettingsMenu : Menu
{
    [SerializeField] List<Transform> gameSettingObjects;
    Button[] questionCountButtons = new Button[2];
    Button[] timerDurationButtons = new Button[2];
    TextMeshProUGUI questionCountDisplay, timerDurationDisplay;

    [SerializeField] List<Transform> questionOptionObjects;
    List<Slider> difficultySliders = new List<Slider>(Enum.GetValues(typeof(QuestionType)).Length);
    List<Toggle> enableQuestionToggles = new List<Toggle>(Enum.GetValues(typeof(QuestionType)).Length);

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

        questionOptionObjects.ForEach(i =>
        {
            difficultySliders.Add(i.GetComponentInChildren<Slider>());
            enableQuestionToggles.Add(i.GetComponentInChildren<Toggle>());
        });
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

    public void OnToggleQuestionType(int questionType)
    {
        questionSettings[(QuestionType)questionType] = (enableQuestionToggles[questionType].isOn, questionSettings[(QuestionType)questionType].difficulty);

        print(questionType + (questionSettings[(QuestionType)questionType].enabled ? " enabled." : " disabled."));

        //disable all enable question toggles if there's only 1 toggle left on
        //(to avoid the player being able to disable all question types)
        enableQuestionToggles.ForEach(i =>
        {
            if (i.isOn) i.enabled = enableQuestionToggles.Count(j => j.isOn) > 1;
        });
    }

    public void OnChangeDifficulty(int questionType)
    {
        questionSettings[(QuestionType)questionType] = (questionSettings[(QuestionType)questionType].enabled, (int)difficultySliders[questionType].value);
    }

    public void OnChangeQuestionCount(int changeAmount)
    {
        questionCount = Mathf.Clamp(questionCount + changeAmount, minQuestionCount, maxQuestionCount);

        questionCountButtons[0].interactable = questionCount > minQuestionCount;
        questionCountButtons[1].interactable = questionCount < maxQuestionCount;
        questionCountDisplay.text = questionCount.ToString();
    }

    public void OnChangeTimerDuration(int changeAmount)
    {
        timerDuration = Mathf.Clamp(timerDuration + changeAmount, minTimerDuration, maxTimerDuration);

        timerDurationButtons[0].interactable = timerDuration > minTimerDuration;
        timerDurationButtons[1].interactable = timerDuration < maxTimerDuration;
        timerDurationDisplay.text = timerDuration + " sec.";
    }
}