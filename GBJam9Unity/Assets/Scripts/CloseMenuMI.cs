using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseMenuMI : MenuItemBase
{
    public override void PerformAction()
    {
        GetComponentInParent<MenuItemManager>().gameObject.SetActive(false);
    }
}
