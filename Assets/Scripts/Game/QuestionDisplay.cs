using System.Collections;
using UnityEngine;
using TMPro;

public class QuestionDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questionDisplayBox;
    [SerializeField] TextMeshProUGUI answerDisplayBox;

    void Awake()
    {
        FindObjectOfType<Keyboard>().SubmitAnswerAction += OnSubmitAnswer;
    }

    void OnSubmitAnswer(int answer)
    {
        //trim first line of questionDisplayBox
        int newLineIndex = questionDisplayBox.text.IndexOf('\n');
        string s = questionDisplayBox.text.Substring(newLineIndex + 1);

        questionDisplayBox.text = s;
    }
}