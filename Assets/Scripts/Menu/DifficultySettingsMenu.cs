using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static GameSettings;

public class DifficultySettingsMenu : Menu
{
    [SerializeField] List<Transform> questionOptionObjects;
    List<Slider> difficultySliders = new List<Slider>(Enum.GetValues(typeof(QuestionType)).Length);
    List<Toggle> enableQuestionToggles = new List<Toggle>(Enum.GetValues(typeof(QuestionType)).Length);

    protected override void Awake()
    {
        base.Awake();

        questionOptionObjects.ForEach(i =>
        {
            difficultySliders.Add(i.GetComponentInChildren<Slider>());
            enableQuestionToggles.Add(i.GetComponentInChildren<Toggle>());
        });
    }

    public void OnToggleQuestionType(int questionType)
    {
        questionSettings[(QuestionType)questionType] = (enableQuestionToggles[questionType].isOn, questionSettings[(QuestionType)questionType].difficulty);

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

    public override void SwitchMenu(Canvas otherMenu)
    {
        FileHandler.SaveSettings();
        base.SwitchMenu(otherMenu);
    }
}