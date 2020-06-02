using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static GameSettings;
using static MathHelper;

public class GameController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questionDisplayBox;

    Clock clock;
    public delegate void GameOverAction(float finalTime);
    public GameOverAction gameOverAction;

    void Awake()
    {
        clock = GetComponent<Clock>();

        FindObjectOfType<InstructionsMenu>().GameStartAction += OnGameStart;
        FindObjectOfType<Keyboard>().SubmitAnswerAction += OnSubmitAnswer;
    }

    void OnGameStart()
    {
        enabled = true;
    }

    void Start()
    {
        numQuestionsAnswered = 0;
        numQuestionsCorrect = 0;

        GenerateQuestions(questionCount);

        switch (selectedGameMode)
        {
            case GameMode.Classic:
                clock.StartClock(0);
                break;
            case GameMode.Timed:
                clock.StartClock(timerDuration);
                break;
        }
    }

    //generate <amount> random questions based on enabled question types and question diffculty
    void GenerateQuestions(int amount)
    {
        //sort questionSettings such that all operations that are enabled are at the front of the dictionary
        var _questionSettings = questionSettings.OrderByDescending(i => i.Value);

        //find how many operations are enabled
        int numEnabledQuestionTypes = _questionSettings.Count(op => op.Value.enabled);

        for (int i = 0; i < amount; i++)
        {
            //generate a question, randomly based on one of the enabled operations
            QuestionType randQuestionType = _questionSettings.ElementAt(Random.Range(0, numEnabledQuestionTypes)).Key;
            string question = GenerateQuestion(randQuestionType);

            //append question to question display box
            questionDisplayBox.text += question + '\n';
        }
    }

    string GenerateQuestion(QuestionType questionType)
    {
        string output = "";
        int num1 = 0, num2 = 0;

        switch (questionType)
        {
            case QuestionType.Addition:
                num1 = GetRandomNumber(GetNumberRange(questionType, questionSettings[questionType].difficulty));
                num2 = GetRandomNumber(GetNumberRange(questionType, questionSettings[questionType].difficulty));

                output = $"{ConvertToString(num1)} + {ConvertToString(num2)} =";
                answers.Enqueue(num1 + num2);
                break;

            case QuestionType.Subtraction:
                num1 = GetRandomNumber(GetNumberRange(questionType, questionSettings[questionType].difficulty));
                num2 = GetRandomNumber((GetNumberRange(questionType, questionSettings[questionType].difficulty).min, num1));

                output = $"{ConvertToString(num1)} - {ConvertToString(num2)} =";
                answers.Enqueue(num1 - num2);
                break;

            case QuestionType.Multiplication:
                num1 = GetRandomNumber(GetNumberRange(questionType, questionSettings[questionType].difficulty + 1));
                num2 = GetRandomNumber(GetNumberRange(questionType, questionSettings[questionType].difficulty));

                output = $"{ConvertToString(num1)} x {ConvertToString(num2)} =";
                answers.Enqueue(num1 * num2);
                break;

            case QuestionType.Division:
                List<int> num1Factors = GetFactors(num1);
                while (num1Factors.Count <= 2)
                {
                    num1 = GetRandomNumber(GetNumberRange(questionType, questionSettings[questionType].difficulty));
                    num1Factors = GetFactors(num1);
                }

                num2 = GetRandomNumber(num1Factors);

                output = $"{ConvertToString(num1)} ÷ {ConvertToString(num2)} =";
                answers.Enqueue(num1 / num2);
                break;
        }

        return output;
    }

    (int min, int max) GetNumberRange(QuestionType questionType, int difficulty)
    {
        (int min, int max) result = (0, 0);

        switch (questionType)
        {
            case QuestionType.Addition:
            case QuestionType.Subtraction:
                result.min = TenToThePowerOf(difficulty);
                result.max = TenToThePowerOf(difficulty + 1);
                break;
            case QuestionType.Multiplication:
                result.min = TenToThePowerOf(difficulty / 2);
                result.max = TenToThePowerOf((difficulty / 2) + 1);
                break;
            case QuestionType.Division:
                result.min = TenToThePowerOf((difficulty % 3) + (difficulty / 3) + 1);
                result.max = TenToThePowerOf((difficulty % 3) + (difficulty / 3) + 2);
                break;
        }

        return result;
    }

    void OnSubmitAnswer(int playerInput)
    {
        int correctAnswer = answers.Dequeue();
        numQuestionsAnswered++;
        if (playerInput == correctAnswer)
        {
            numQuestionsCorrect++;
        }

        //check which game mode was selected
        if (selectedGameMode == GameMode.Classic)
        {
            if (answers.Count == 0)
            {
                gameOverAction?.Invoke(clock.StopClock());      //stop the clock if no questions remain
            }
        }
        else
        {
            GenerateQuestions(1);                               //for all other difficulties, continue generating questions
        }
    }
}