using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using static GameSettings;

public static class FileHandler
{
    const string fileName = "settings.json";
    static readonly string directoryPath = Path.Combine(UnityEngine.Application.persistentDataPath, "Settings");
    static readonly string filePath = Path.Combine(directoryPath, fileName);

    public static void SaveSettings()
    {
        FileStream file;

        Directory.CreateDirectory(directoryPath);
        if (File.Exists(filePath)) { file = File.OpenWrite(filePath); }
        else { file = File.Create(filePath); }

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, playerSettings);
        file.Close();

    }

    public static void LoadSettings()
    {
        FileStream file;

        if (File.Exists(filePath)) { file = File.OpenRead(filePath); }
        else return;

        BinaryFormatter bf = new BinaryFormatter();
        PlayerSettings ps = bf.Deserialize(file) as PlayerSettings;
        file.Close();

        if (playerSettings == null) { playerSettings = new PlayerSettings(); }
        playerSettings.UpdateSettings(ps);
    }
}

[Serializable]
public class PlayerSettings
{
    public Dictionary<QuestionType, (bool enabled, int difficulty)> questionSettings;
    public GameMode selectedGameMode;
    public bool clockDisplayEnabled;
    public int questionCount;
    public float timerDuration;

    public PlayerSettings()
    {
        questionSettings = new Dictionary<QuestionType, (bool enabled, int difficulty)>
        {
            [QuestionType.Addition] = (true, 0),
            [QuestionType.Subtraction] = (true, 0),
            [QuestionType.Multiplication] = (true, 0),
            [QuestionType.Division] = (true, 0)
        };

        selectedGameMode = GameMode.Classic;
        clockDisplayEnabled = false;
        questionCount = minQuestionCount;
        timerDuration = minTimerDuration;
    }

    public void UpdateSettings(PlayerSettings ps)
    {
        questionSettings = ps.questionSettings;
        selectedGameMode = ps.selectedGameMode;
        clockDisplayEnabled = ps.clockDisplayEnabled;
        questionCount = ps.questionCount;
        timerDuration = ps.timerDuration;
    }
}