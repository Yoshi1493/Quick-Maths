using UnityEngine;
using UnityEngine.SceneManagement;

public class AppInit : MonoBehaviour
{
    void Awake()
    {
        FileHandler.LoadSettings();
        SceneManager.LoadScene(1);
    }
}