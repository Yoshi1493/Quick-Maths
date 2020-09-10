using UnityEngine;
using UnityEngine.SceneManagement;

public class AppInit : MonoBehaviour
{
    void Awake()
    {
        ForceAspectRatio();

        FileHandler.LoadSettings();
        SceneManager.LoadScene(1);
    }

    void ForceAspectRatio()
    {
        Camera mainCam = Camera.main;
        Rect rect = mainCam.rect;

        float aspectRatio = 9f / 16f;
        float scaleHeight = (float)Screen.width / Screen.height / aspectRatio;

        if (scaleHeight < 1)
        {
            rect.width = 1;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1 - scaleHeight) / 2;
        }
        else
        {
            rect.width = 1 / scaleHeight;
            rect.height = 1;
            rect.x = (1 - 1 / scaleHeight) / 2;
            rect.y = 0;
        }

        mainCam.rect = rect;
    }
}