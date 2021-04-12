using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using static GameSettings;
using static MathHelper;

public class Game : MonoBehaviour
{
    protected struct Question
    {
        public readonly QuestionType questionType;
        public readonly (int num1, int num2) question;
        public readonly int answer;

        public Question(QuestionType _questionType, int _num1, int _num2)
        {
            questionType = _questionType;
            question.num1 = _num1;
            question.num2 = _num2;

            switch (_questionType)
            {
                case QuestionType.Addition:
                    answer = _num1 + _num2;
                    break;

                case QuestionType.Subtraction:
                    answer = _num1 - _num2;
                    break;

                case QuestionType.Multiplication:
                    answer = _num1 * _num2;
                    break;

                case QuestionType.Division:
                    answer = _num1 / _num2;
                    break;

                default:
                    answer = -1;
                    break;
            }
        }
    }

    [SerializeField] protected TextMeshProUGUI questionTextBox;

    protected Clock clock;

    protected Queue<Question> questions = new Queue<Question>();
    Question[] previousQuestions = new Question[2];
    (int correct, int total) answerData;

    public event System.Action<float, (int, int)> GameOverAction;

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
        string question;

        if (enabledQuestionTypes.Count() == 1)
        {
            QuestionType questionType = enabledQuestionTypes.First().Key;

            for (int i = 0; i < amount; i++)
            {
                question = GenerateQuestion(questionType, playerSettings.questionSettings[questionType].difficulty);
                questionTextBox.text += question + '\n';
            }
        }
        else
        {
            for (int i = 0; i < amount; i++)
            {
                QuestionType randQuestionType;

                do
                {
                    //generate a question, randomly based on one of the enabled operations
                    randQuestionType = enabledQuestionTypes.ElementAt(Random.Range(0, enabledQuestionTypes.Count())).Key;
                }
                //randomize again if the randQuestionType happens to match all the questionTypes in previousQuestions
                while (previousQuestions.Select(q => q.questionType).All(q => q == randQuestionType));

                question = GenerateQuestion(randQuestionType, playerSettings.questionSettings[randQuestionType].difficulty);
                questionTextBox.text += question + '\n';
            }
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
                break;

            case QuestionType.Subtraction:
                num1 = GetRandomNumber(GetNumberRange(questionType, difficulty));
                num2 = GetRandomNumber((Mathf.Min(GetNumberRange(questionType, difficulty).min, num1 / 2), num1 + 1));

                output = $"{ConvertToString(num1)} - {ConvertToString(num2)} =";
                break;

            case QuestionType.Multiplication:
                num1 = GetRandomNumber(GetNumberRange(questionType, difficulty + 1));
                num2 = GetRandomNumber(GetNumberRange(questionType, difficulty));

                output = $"{ConvertToString(num1)} x {ConvertToString(num2)} =";
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
                break;
        }

        Question q = new Question(questionType, num1, num2);
        questions.Enqueue(q);
        TrackPreviousQuestion(q);

        return output;
    }

    void TrackPreviousQuestion(Question q)
    {
        System.Array.Copy(previousQuestions, 0, previousQuestions, 1, previousQuestions.Length - 1);
        previousQuestions[0] = q;
    }

    (int min, int max) GetNumberRange(QuestionType questionType, int difficulty)
    {
        (int min, int max) result = (0, 0);

        switch (questionType)
        {
            case QuestionType.Addition:
            case QuestionType.Subtraction:
                result.min = Mathf.Max(TenToThePowerOf(difficulty), 2);
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
        int correctAnswer = questions.Dequeue().answer;

        if (playerInput == correctAnswer) { OnSubmitCorrectAnswer(); }
        else { OnSubmitIncorrectAnswer(); }
    }

    protected virtual void OnSubmitCorrectAnswer()
    {
        answerData.total++;
        answerData.correct++;
    }

    protected virtual void OnSubmitIncorrectAnswer()
    {
        answerData.total++;
    }

    protected void OnGameOver(float finalTime)
    {
        GameOverAction?.Invoke(finalTime, answerData);
    }
}