using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPartyMI : MenuItemBase
{
    public override bool PerformAction()
    {
        PartyControl.Instance.OpenPartyScreen();
        return true;
    }
}
