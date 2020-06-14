using System;
using System.Collections;
using UnityEngine;
using static GameSettings;

public class Clock : MonoBehaviour
{
    public event Action<float> CountdownOverAction;

    IEnumerator clock;
    public float time { get; private set; }
    bool paused;

    IEnumerator CountUp()
    {
        while (!paused)
        {
            yield return new WaitForEndOfFrame();
            time += Time.deltaTime;
        }
    }

    IEnumerator CountDown()
    {
        while (!paused && time > 0)
        {
            yield return new WaitForEndOfFrame();
            time -= Time.deltaTime;
        }

        CountdownOverAction?.Invoke(playerSettings.timerDuration);
    }

    public void StartClock(float startTime)
    {
        clock = startTime == 0 ? CountUp() : CountDown();
        StartCoroutine(clock);
    }

    public void StopClock()
    {
        StopCoroutine(clock);
    }

    public void SetPaused(bool state)
    {
        paused = state;
    }
}