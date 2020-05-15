using UnityEngine;
using TMPro;

public class QuestionDisplay : MonoBehaviour
{
    RectTransform[] childObjects;
    const float scrollAmount = 104;     //change eventually

    void Awake()
    {
        childObjects = transform.GetComponentsInChildren<RectTransform>();
        TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();

        FindObjectOfType<Keyboard>().SubmitAnswerAction += OnSubmitAnswer;
    }

    void OnSubmitAnswer(int answer)
    {
        foreach (var child in childObjects)
        {
            child.anchoredPosition += Vector2.up * scrollAmount;
        }
    }
}