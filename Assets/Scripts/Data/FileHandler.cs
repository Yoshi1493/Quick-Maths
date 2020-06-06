using System;
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

        playerSettings.UpdateSettings(ps);
    }
}

[Serializable]
public class PlayerSettings
{
    public GameMode selectedGameMode = GameMode.Classic;
    public bool clockDisplayEnabled = false;
    public int questionCount = minQuestionCount;
    public float timerDuration = minTimerDuration;

    public PlayerSettings() { }

    public void UpdateSettings(PlayerSettings ps)
    {
        selectedGameMode = ps.selectedGameMode;
        clockDisplayEnabled = ps.clockDisplayEnabled;
        questionCount = ps.questionCount;
        timerDuration = ps.timerDuration;
    }
}