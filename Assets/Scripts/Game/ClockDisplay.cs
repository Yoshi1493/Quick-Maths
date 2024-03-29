﻿using System;
using UnityEngine;
using TMPro;
using static GameSettings;

public class ClockDisplay : MonoBehaviour
{
    [SerializeField] Clock clock;
    TextMeshProUGUI timeDisplay;
    const string MonospaceTag = "<mspace=24>";

    void Awake()
    {
        timeDisplay = GetComponent<TextMeshProUGUI>();

        if (playerSettings.clockDisplayEnabled)
        {
            gameObject.SetActive(true);
            FindObjectOfType<Countdown>().StartGameAction += OnStartGame;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    void OnStartGame()
    {
        enabled = true;
    }

    void Update()
    {
        timeDisplay.text = MonospaceTag + TimeSpan.FromSeconds(selectedGameMode == GameMode.Classic ? Mathf.Floor(clock._currentTime) : Mathf.Ceil(clock._currentTime)).ToString(TimeDisplayFormat);
    }
}