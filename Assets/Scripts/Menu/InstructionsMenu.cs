using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static GameSettings;

public class InstructionsMenu : Menu
{
    [SerializeField] List<GameObject> instructions;

    [Space]

    [SerializeField] GameObject countdownTimer;
    Image timerImage;
    TextMeshProUGUI timerDisplay;

    IEnumerator countdown;
    const float CountdownTime = 3f;
    public event System.Action GameStartAction;

    protected override void Awake()
    {
        base.Awake();

        //display instructions for appropriate game mode
        for (int i = 0; i < instructions.Count; i++)
        {
            instructions[i].SetActive(i == (int)playerSettings.selectedGameMode);
        }

        timerImage = countdownTimer.GetComponent<Image>();
        timerDisplay = countdownTimer.GetComponentInChildren<TextMeshProUGUI>();
    }

    //start 3sec. countdown when player taps the screen
    public void StartCountdown()
    {
        //start coroutine only if it's not in progress already
        if (countdown == null)
        {
            instructions[(int)playerSettings.selectedGameMode].SetActive(false);       //hide instructions
            countdownTimer.SetActive(true);                             //display countdown timer

            countdown = Countdown();                                    //assign countdown
            StartCoroutine(countdown);                                  //start countdown
        }
    }

    IEnumerator Countdown()
    {
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

        //after CountdownTime seconds have passed, start the game and hide this menu
        GameStartAction?.Invoke();
        countdownTimer.SetActive(false);
        CloseMenu(thisMenu);
    }
}