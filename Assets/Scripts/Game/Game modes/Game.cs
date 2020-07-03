using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using static GameSettings;
using static MathHelper;

public class Game : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI questionDisplayBox;
    protected Queue<int> answers = new Queue<int>();

    protected Clock clock;

    public event System.Action<float, int, int> gameOverAction;

    int answerCount;
    protected int correctAnswerCount { get; private set; }


    protected virtual void Awake()
    {
        clock = transform.GetComponentInParent<Clock>();
        clock.CountdownOverAction += OnGameOver;

        FindObjectOfType<Countdown>().StartGameAction += OnStartGame;
        FindObjectOfType<Keyboard>().SubmitAnswerAction += OnSubmitAnswer;
    }

    void OnStartGame()
    {
        enabled = true;
    }

    //generate <amount> random questions based on enabled question types and question diffculty
    protected virtual void GenerateQuestions(int amount)
    {
        var enabledQuestionTypes = playerSettings.questionSettings.Where(i => i.Value.enabled);

        for (int i = 0; i < amount; i++)
        {
            //generate a question, randomly based on one of the enabled operations
            QuestionType randQuestionType = enabledQuestionTypes.ElementAt(Random.Range(0, enabledQuestionTypes.Count())).Key;
            string question = GenerateQuestion(randQuestionType, playerSettings.questionSettings[randQuestionType].difficulty);

            //append question to question display box
            questionDisplayBox.text += question + '\n';
        }
    }

    //return a random <questionType> question based on <difficulty>
    protected string GenerateQuestion(QuestionType questionType, int difficulty)
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
                num2 = GetRandomNumber((GetNumberRange(questionType, difficulty).min, num1 + 1));

                output = $"{ConvertToString(num1)} - {ConvertToString(num2)} =";
                answers.Enqueue(num1 - num2);
                break;

            case QuestionType.Multiplication:
                num1 = GetRandomNumber(GetNumberRange(questionType, difficulty + 1));
                num2 = GetRandomNumber(GetNumberRange(questionType, difficulty));

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
                result.min = TenToThePowerOf(difficulty / 2) + 1;
                result.max = TenToThePowerOf((difficulty / 2) + 1);
                break;
            case QuestionType.Division:
                result.min = TenToThePowerOf(difficulty / 2 + 1) * (difficulty % 2 * 4 + 1);
                result.max = TenToThePowerOf((difficulty + 1) / 2 + 1) * ((difficulty + 1) % 2 * 4 + 1);
                break;
        }

        return result;
    }

    protected virtual void OnSubmitAnswer(int playerInput)
    {
        int correctAnswer = answers.Dequeue();

        if (playerInput == correctAnswer) { OnSubmitCorrectAnswer(); }
        else { OnSubmitIncorrectAnswer(); }
    }

    protected virtual void OnSubmitCorrectAnswer()
    {
        answerCount++;
        correctAnswerCount++;
    }

    protected virtual void OnSubmitIncorrectAnswer()
    {
        answerCount++;
    }

    protected void OnGameOver(float finalTime)
    {
        gameOverAction?.Invoke(finalTime, answerCount, correctAnswerCount);
    }
}