using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static GameSettings;

public class DifficultySettingsMenu : Menu
{
    [SerializeField] Slider[] difficultySliders = new Slider[Enum.GetValues(typeof(QuestionType)).Length];
    [SerializeField] Toggle[] enableQuestionToggles = new Toggle[Enum.GetValues(typeof(QuestionType)).Length];

    public void OnToggleQuestionType(int questionType)
    {
        questionSettings[(QuestionType)questionType] = (enableQuestionToggles[questionType].isOn, questionSettings[(QuestionType)questionType].difficulty);

        //disable all enable question toggles if there's only 1 toggle left on
        //(to avoid the player being able to disable all question types)
        enableQuestionToggles.ToList().ForEach(i =>
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