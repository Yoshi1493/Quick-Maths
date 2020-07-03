using System;
using System.Collections;
using UnityEngine;
using static GameSettings;

public class Clock : MonoBehaviour
{
    public event Action<float> CountdownOverAction;

    IEnumerator clock;
    public float _currentTime { get; private set; }
    public float _elapsedTime { get; private set; }

    IEnumerator CountUp()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            _elapsedTime += Time.deltaTime;
            _currentTime += Time.deltaTime;
        }
    }

    IEnumerator CountDown()
    {
        while (_currentTime > 0)
        {
            yield return new WaitForEndOfFrame();
            _elapsedTime += Time.deltaTime;
            _currentTime -= Time.deltaTime;
        }

        CountdownOverAction?.Invoke(playerSettings.timeLimit);
    }

    public void StartClock(float startTime)
    {
        _currentTime = startTime;
        clock = _currentTime == 0 ? CountUp() : CountDown();
        StartCoroutine(clock);
    }

    public void StopClock()
    {
        StopCoroutine(clock);
    }

    public void AddTime(float amount)
    {
        _currentTime += amount;
    }
}