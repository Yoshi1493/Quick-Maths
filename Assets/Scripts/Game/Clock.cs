using System.Collections;
using UnityEngine;
using static PlayerSettings;

public class Clock : MonoBehaviour
{
    IEnumerator clock;
    float time;
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

        GetComponent<GameController>().gameOverAction?.Invoke(timerDuration);
    }

    public void StartClock(float startTime)
    {
        clock = startTime == 0 ? CountUp() : CountDown();
        StartCoroutine(clock);
    }

    public float StopClock()
    {
        StopCoroutine(clock);
        return time;
    }

    public void SetPaused(bool state)
    {
        paused = state;
    }
}