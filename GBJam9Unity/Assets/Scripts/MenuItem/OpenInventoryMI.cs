using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventoryMI : MenuItemBase
{
    public override bool PerformAction()
    {
        Debug.Log("This is where we open the inventory.");
        InventoryControl.Instance.ShowInventory();
        return true;
    }
}
