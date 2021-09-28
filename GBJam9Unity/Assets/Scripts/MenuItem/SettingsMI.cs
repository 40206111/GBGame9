using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMI : MenuItemBase
{
    public override bool PerformAction()
    {
        SettingsControl.Instance.ShowSettings();
        return true;
    }
}
