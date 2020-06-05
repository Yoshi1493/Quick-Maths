using System.IO;
using UnityEngine;
using static GameSettings;

public class JsonFileHandler : MonoBehaviour
{
    PlayerSettings playerSettings;
    string filePath = Path.Combine(Application.persistentDataPath, "Settings", "settings.json");

    void Start()
    {
        playerSettings = new PlayerSettings();
    }

    public void SerializeData()
    {
        string jsonData = JsonUtility.ToJson(playerSettings);
        File.WriteAllText(filePath, jsonData);
    }

    public void DeserializeData()
    {
        string jsonData = File.ReadAllText(filePath);
        playerSettings = JsonUtility.FromJson<PlayerSettings>(jsonData);
    }

    [System.Serializable]
    public class PlayerSettings
    {
        public GameMode selectedGameMode;
        public bool clockDisplayEnabled;
        public int questionCount;
        public float timerDuration;

        /*
        public PlayerSettings(GameMode _selectedGameMode, bool _clockDisplayEnabled, int _questionCount, float _timerDuration)
        {
            (selectedGameMode, clockDisplayEnabled, questionCount, timerDuration) =
            (_selectedGameMode, _clockDisplayEnabled, _questionCount, _timerDuration);
        }
        */

        public PlayerSettings()
        {
            selectedGameMode = GameMode.Classic;
            clockDisplayEnabled = false;
            questionCount = minQuestionCount;
            timerDuration = minTimerDuration;
        }
    }
}