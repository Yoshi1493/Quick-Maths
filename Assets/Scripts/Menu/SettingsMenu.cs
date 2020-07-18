using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static GameSettings;

public class SettingsMenu : Menu
{
    [SerializeField] Canvas mainMenu;

    [SerializeField] Button[] questionCountButtons = new Button[2], timerDurationButtons = new Button[2];
    [SerializeField] TextMeshProUGUI questionCountDisplay, timerDurationDisplay;
    [SerializeField] Toggle clockToggle;

    [SerializeField] Toggle[] enableQuestionToggles = new Toggle[Enum.GetValues(typeof(QuestionType)).Length];
    [SerializeField] Slider[] difficultySliders = new Slider[Enum.GetValues(typeof(QuestionType)).Length];

    protected override void Awake()
    {
        base.Awake();
        InitUIObjects();
    }

    void InitUIObjects()
    {
        UpdateQuestionCountButtons();
        UpdateTimeLimitButtons();
        UpdateClockToggleDisplay();

        UpdateQuestionToggles();
        UpdateDifficultySliders();
    }

    void UpdateQuestionCountButtons()
    {
        questionCountButtons[0].interactable = playerSettings.questionCount > minQuestionCount;
        questionCountButtons[1].interactable = playerSettings.questionCount < maxQuestionCount;
        questionCountDisplay.text = playerSettings.questionCount.ToString();
    }

    void UpdateTimeLimitButtons()
    {
        timerDurationButtons[0].interactable = playerSettings.timeLimit > minTimeLimit;
        timerDurationButtons[1].interactable = playerSettings.timeLimit < maxTimeLimit;
        timerDurationDisplay.text = TimeSpan.FromSeconds(playerSettings.timeLimit).ToString(TimeDisplayFormat);
    }

    void UpdateClockToggleDisplay()
    {
        clockToggle.SetIsOnWithoutNotify(playerSettings.clockDisplayEnabled);
    }

    void UpdateQuestionToggles()
    {
        for (int i = 0; i < enableQuestionToggles.Length; i++)
        {
            enableQuestionToggles[i].SetIsOnWithoutNotify(playerSettings.questionSettings[(QuestionType)i].enabled);
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

    #region Button Functions

    public void OnChangeQuestionCount(int changeAmount)
    {
        playerSettings.questionCount = Mathf.Clamp(playerSettings.questionCount + changeAmount, minQuestionCount, maxQuestionCount);
        UpdateQuestionCountButtons();
    }

    public void OnChangeTimeLimit(int changeAmount)
    {
        playerSettings.timeLimit = Mathf.Clamp(playerSettings.timeLimit + changeAmount, minTimeLimit, maxTimeLimit);
        UpdateTimeLimitButtons();
    }

    public void OnToggleClockDisplay()
    {
        playerSettings.clockDisplayEnabled = !playerSettings.clockDisplayEnabled;
        UpdateClockToggleDisplay();
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

    #endregion

#if UNITY_ANDROID
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            SwitchMenu(mainMenu);
        }
    }
#endif

    public override void SwitchMenu(Canvas otherMenu)
    {
        FileHandler.SaveSettings();
        base.SwitchMenu(otherMenu);
    }

    public void ResetSettings()
    {
        FileHandler.ResetSettings();
        InitUIObjects();
    }
}