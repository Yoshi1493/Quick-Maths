using UnityEngine;

public class SettingsMenu : Menu
{
    public override void SwitchMenu(Canvas otherMenu)
    {
        FileHandler.SaveSettings();
        base.SwitchMenu(otherMenu);
    }
}