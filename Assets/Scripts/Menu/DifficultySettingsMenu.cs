using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static GameSettings;

public class DifficultySettingsMenu : Menu
{
    [SerializeField] Toggle[] enableQuestionToggles = new Toggle[Enum.GetValues(typeof(QuestionType)).Length];
    [SerializeField] Slider[] difficultySliders = new Slider[Enum.GetValues(typeof(QuestionType)).Length];

    protected override void Awake()
    {
        base.Awake();
        InitUIObjects();
    }

    void InitUIObjects()
    {
        UpdateQuestionToggles();
        UpdateDifficultySliders();
    }

    void UpdateQuestionToggles()
    {
        for (int i = 0; i < enableQuestionToggles.Length; i++)
        {
            enableQuestionToggles[i].isOn = playerSettings.questionSettings[(QuestionType)i].enabled;
        }

        //disable all enable question toggles if there's only 1 toggle left on
        //(to avoid the player being able to disable all question types)
        enableQuestionToggles.ToList().ForEach(i =>
        {
            if (i.isOn) { i.enabled = enableQuestionToggles.Count(j => j.isOn) > 1; }
        });
    }

    void UpdateDifficultySliders()
    {
        for (int i = 0; i < difficultySliders.Length; i++)
        {
            difficultySliders[i].value = playerSettings.questionSettings[(QuestionType)i].difficulty;
        }
    }

    public void OnToggleQuestionType(int questionType)
    {
        playerSettings.questionSettings[(QuestionType)questionType] = (enableQuestionToggles[questionType].isOn, playerSettings.questionSettings[(QuestionType)questionType].difficulty);
        UpdateQuestionToggles();
    }

    public void OnChangeDifficulty(int questionType)
    {
        playerSettings.questionSettings[(QuestionType)questionType] = (playerSettings.questionSettings[(QuestionType)questionType].enabled, (int)difficultySliders[questionType].value);
    }
}