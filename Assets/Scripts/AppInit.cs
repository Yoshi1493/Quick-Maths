using UnityEngine;
using UnityEngine.SceneManagement;

public class AppInit : MonoBehaviour
{
    void Awake()
    {
        GetPlayerPrefs();
        SceneManager.LoadScene(1);
    }

    void GetPlayerPrefs()
    {
        //to-do: load player settings from PlayerPrefs
    }
}