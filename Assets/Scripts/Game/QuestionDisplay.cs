using System.Collections;
using UnityEngine;
using TMPro;

public class QuestionDisplay : MonoBehaviour
{
    [SerializeField] RectTransform questionDisplayBox;
    [SerializeField] RectTransform answerDisplayBox;
    TextMeshProUGUI answerDisplay;

    void Awake()
    {
        FindObjectOfType<Keyboard>().SubmitAnswerAction += OnSubmitAnswer;
        FindObjectOfType<PauseController>().PauseGameAction += SetPausedState;

        answerDisplay = answerDisplayBox.GetComponent<TextMeshProUGUI>();
    }

    void OnSubmitAnswer(int answer)
    {
        answerDisplay.text += answer.ToString() + '\n';
        StartCoroutine(Scroll(questionDisplayBox));
        StartCoroutine(Scroll(answerDisplayBox));
    }

    //☢ WARNING ☢ MAGIC NUMBERS BELOW
    IEnumerator Scroll(RectTransform rt)
    {
        float startPos = rt.anchoredPosition.y;
        float endPos = rt.anchoredPosition.y + 172;

        float totalLerpTime = 0.1f;
        float currentLerpTime = 0;

        while (currentLerpTime < totalLerpTime)
        {
            yield return new WaitForEndOfFrame();
            currentLerpTime += Time.deltaTime;

            float yPos = Mathf.Lerp(startPos, endPos, currentLerpTime / totalLerpTime);
            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, yPos);
        }
    }
    
    void SetPausedState(bool state)
    {
        questionDisplayBox.gameObject.SetActive(!state);
        answerDisplayBox.gameObject.SetActive(!state);
    }
}