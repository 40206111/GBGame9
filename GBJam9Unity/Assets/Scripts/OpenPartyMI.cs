using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPartyMI : MenuItemBase
{
    public override void PerformAction()
    {
        PartyControl.Instance.OpenPartyScreen();
    }
}
