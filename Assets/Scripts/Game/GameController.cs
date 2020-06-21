using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using static GameSettings;
using static MathHelper;

public class GameController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questionDisplayBox;
    Queue<int> answers = new Queue<int>();

    Clock clock;
    public delegate void GameOverAction(float finalTime);
    public GameOverAction gameOverAction;

    void Awake()
    {
        clock = GetComponent<Clock>();
        clock.CountdownOverAction += OnGameOver;

        FindObjectOfType<Countdown>().StartGameAction += OnStartGame;
        FindObjectOfType<Keyboard>().SubmitAnswerAction += OnSubmitAnswer;
    }

    void OnStartGame()
    {
        enabled = true;
    }

    void Start()
    {
        answerCount = 0;
        correctAnswerCount = 0;
        
        GenerateQuestions(playerSettings.questionCount);

        switch (playerSettings.selectedGameMode)
        {
            case GameMode.Classic:
                clock.StartClock(0);
                break;
            case GameMode.Timed:
                clock.StartClock(playerSettings.timerDuration);
                break;
        }
    }

    //generate <amount> random questions based on enabled question types and question diffculty
    void GenerateQuestions(int amount)
    {
        //sort questionSettings such that all operations that are enabled are at the front of the dictionary
        var _questionSettings = playerSettings.questionSettings.OrderByDescending(i => i.Value);

        //find how many operations are enabled
        int numEnabledQuestionTypes = _questionSettings.Count(op => op.Value.enabled);

        for (int i = 0; i < amount; i++)
        {
            //generate a question, randomly based on one of the enabled operations
            QuestionType randQuestionType = _questionSettings.ElementAt(Random.Range(0, numEnabledQuestionTypes)).Key;
            string question = GenerateQuestion(randQuestionType, playerSettings.questionSettings[randQuestionType].difficulty);

            //append question to question display box
            questionDisplayBox.text += question + '\n';
        }
    }

    //return 
    string GenerateQuestion(QuestionType questionType, int difficulty)
    {
        string output = "";
        int num1 = 0, num2 = 0;

        switch (questionType)
        {
            case QuestionType.Addition:
                num1 = GetRandomNumber(GetNumberRange(questionType, difficulty));
                num2 = GetRandomNumber(GetNumberRange(questionType, difficulty));

                output = $"{ConvertToString(num1)} + {ConvertToString(num2)} =";
                answers.Enqueue(num1 + num2);
                break;

            case QuestionType.Subtraction:
                num1 = GetRandomNumber(GetNumberRange(questionType, difficulty));
                num2 = GetRandomNumber((GetNumberRange(questionType, difficulty).min, num1));

                output = $"{ConvertToString(num1)} - {ConvertToString(num2)} =";
                answers.Enqueue(num1 - num2);
                break;

            case QuestionType.Multiplication:
                num1 = GetRandomNumber(GetNumberRange(questionType, difficulty + 1)) + 1;
                num2 = GetRandomNumber(GetNumberRange(questionType, difficulty)) + 1;

                output = $"{ConvertToString(num1)} x {ConvertToString(num2)} =";
                answers.Enqueue(num1 * num2);
                break;

            case QuestionType.Division:
                List<int> num1Factors = GetFactors(num1);
                while (num1Factors.Count <= 2)
                {
                    num1 = GetRandomNumber(GetNumberRange(questionType, difficulty));
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
                result.min = 10 * TenToThePowerOf(difficulty / 2) * (difficulty % 2 * 4 + 1);
                result.max = 10 * TenToThePowerOf((difficulty + 1) / 2) * ((difficulty + 1) % 2 * 4 + 1);
                break;
        }

        return result;
    }

    void OnSubmitAnswer(int playerInput)
    {
        answerCount++;

        int correctAnswer = answers.Dequeue();
        if (playerInput == correctAnswer)
        {
            correctAnswerCount++;
        }

        //check which game mode was selected
        if (playerSettings.selectedGameMode == GameMode.Classic)
        {
            if (answers.Count == 0)
            {
                clock.StopClock();
                OnGameOver(clock.time);
            }
        }
        else
        {
            GenerateQuestions(1);                               //for all other difficulties, continue generating questions
        }
    }

    void OnGameOver(float finalTime)
    {
        gameOverAction?.Invoke(finalTime);
    }
}