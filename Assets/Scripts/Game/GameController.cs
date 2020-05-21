using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using static GameSettings;

public class GameController : MonoBehaviour
{
    class Clock : MonoBehaviour
    {
        IEnumerator timekeeper;
        public float currentTime;
        bool isPaused;

        IEnumerator Increment()
        {
            while (!isPaused)
            {
                yield return new WaitForEndOfFrame();
                currentTime += Time.deltaTime;
            }
        }

        IEnumerator Decrement()
        {
            while (currentTime > 0 && !isPaused)
            {
                yield return new WaitForEndOfFrame();
                currentTime -= Time.deltaTime;
            }

            GetComponent<GameController>().gameOverAction?.Invoke(timerDuration);
        }

        public void StartClock()
        {
            timekeeper = currentTime == 0 ? Increment() : Decrement();
            StartCoroutine(timekeeper);
        }

        public float StopClock()
        {
            StopCoroutine(timekeeper);
            return currentTime;
        }

        public void SetPaused(bool state)
        {
            isPaused = state;
        }
    }

    [SerializeField] TextMeshProUGUI questionDisplayBox;

    Clock clock;
    public delegate void GameOverAction(float finalTime);
    public GameOverAction gameOverAction;

    void Awake()
    {
        FindObjectOfType<InstructionsMenu>().GameStartAction += OnGameStart;
        FindObjectOfType<Keyboard>().SubmitAnswerAction += OnSubmitAnswer;
    }

    void OnGameStart()
    {
        enabled = true;
    }

    void Start()
    {
        #region DEBUG
        if (selectedGameMode == GameMode.Classic) { print("questionCount: " + questionCount); }
        else { print("timerDuration: " + timerDuration); }
        #endregion

        numQuestionsAnswered = 0;
        numQuestionsCorrect = 0;

        GenerateQuestions(questionCount);

        clock = gameObject.AddComponent<Clock>();
        switch (selectedGameMode)
        {
            case GameMode.Classic:
                clock.currentTime = 0f;
                break;
            case GameMode.Timed:
                clock.currentTime = timerDuration;
                break;
        }
        clock.StartClock();
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