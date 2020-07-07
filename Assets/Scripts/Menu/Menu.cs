using UnityEngine;
using UnityEngine.SceneManagement;
using static GameSettings;

public class Menu : MonoBehaviour
{
    protected Canvas thisMenu;

    protected virtual void Awake()
    {
        thisMenu = GetComponent<Canvas>();
    }

    protected void OpenMenu(Canvas menu)
    {
        menu.enabled = true;
        menu.GetComponent<Menu>().enabled = true;
    }

    protected void CloseMenu(Canvas menu)
    {
        menu.enabled = false;
        enabled = false;
    }

    public virtual void SwitchMenu(Canvas otherMenu)
    {
        OpenMenu(otherMenu);
        CloseMenu(thisMenu);
    }

    public void LoadGame(int gameMode)
    {
        playerSettings.selectedGameMode = (GameMode)gameMode;
        SceneManager.LoadScene("Game");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main");
    }
}