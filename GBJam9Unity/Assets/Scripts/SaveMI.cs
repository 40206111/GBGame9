using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveMI : MenuItemBase
{
    public override bool PerformAction()
    {
        Debug.Log("This is where we save the game.");
        return true;
    }
}
