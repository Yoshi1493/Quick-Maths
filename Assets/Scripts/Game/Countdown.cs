using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Countdown : MonoBehaviour
{
    [SerializeField] Image timerImage;
    [SerializeField] TextMeshProUGUI timerDisplay;

    const float CountdownTime = 3f;
    public event System.Action StartGameAction;

    void Awake()
    {
        FindObjectOfType<InstructionsMenu>().StartCountdownAction += OnStartCountdown;
    }

    void OnStartCountdown()
    {
        enabled = true;
    }

    IEnumerator Start()
    {
        timerImage.gameObject.SetActive(true);
        float timeRemaining = CountdownTime;

        while (timeRemaining > 0)
        {
            yield return new WaitForEndOfFrame();
            timeRemaining -= Time.deltaTime;

            //interpolate timer image's fill amount from 1 to 0 over CountdownTime seconds
            timerImage.fillAmount = timeRemaining / CountdownTime;

            //change timer display text to show timeRemaining (rounded up)
            timerDisplay.text = Mathf.CeilToInt(timeRemaining).ToString();
        }

        StartGameAction?.Invoke();
        gameObject.SetActive(false);
    }
}