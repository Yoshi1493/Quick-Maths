using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour
{
    Button[] keyboardButtons;

    [SerializeField] TextMeshProUGUI answerDisplay;
    public event Action<int> SubmitAnswerAction;

    void Awake()
    {
        keyboardButtons = GetComponentsInChildren<Button>();

        FindObjectOfType<Countdown>().StartGameAction += OnStartGame;
    }

    //append <number> to the answer display
    public void OnInputNumber(int number)
    {
        answerDisplay.text += number.ToString();
    }

    //remove the latest inputted number from the answer display
    public void OnInputBackspace()
    {
        if (answerDisplay.text.Length > 0)
        {
            answerDisplay.text = answerDisplay.text.Remove(answerDisplay.text.Length - 1);
        }
    }

    //send answer to GameController, and clear the answer display
    public void OnSubmitInput()
    {
        //only do so if there is something in the display box
        if (answerDisplay.text.Length > 0)
        {
            int.TryParse(answerDisplay.text, out int answer);
            SubmitAnswerAction?.Invoke(answer);

            answerDisplay.text = string.Empty;
        }
    }

    //enable all keyboard buttons
    void OnStartGame()
    {
        foreach (var button in keyboardButtons)
        {
            button.interactable = true;
        }
    }
}