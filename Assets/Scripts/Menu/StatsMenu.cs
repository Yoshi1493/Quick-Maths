using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsMenu : Menu
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
}