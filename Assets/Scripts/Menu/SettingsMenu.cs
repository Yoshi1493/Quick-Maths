using UnityEngine;

public class SettingsMenu : Menu
{
    [SerializeField] Canvas mainMenu;

#if UNITY_ANDROID
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            SwitchMenu(mainMenu);
        }
    }
#endif

    public override void SwitchMenu(Canvas otherMenu)
    {
        FileHandler.SaveSettings();
        base.SwitchMenu(otherMenu);
    }
}