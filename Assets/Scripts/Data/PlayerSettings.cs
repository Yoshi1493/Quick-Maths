using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameSettings;

public class PlayerSettings
{
    public static GameMode selectedGameMode = GameMode.Classic;
    public static int questionCount = minQuestionCount;

    public static bool showClock = true;
    public static float timerDuration = minTimerDuration;
}