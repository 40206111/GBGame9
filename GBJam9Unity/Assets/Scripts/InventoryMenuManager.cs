using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenuManager : PopulatingMenuManager
{
    [SerializeField]
    protected Image ItemImage;


    protected override void ChangeSelectedMenuItem(int change, bool force = false)
    {
        base.ChangeSelectedMenuItem(change, force);
        ItemMenuItem imi = (ItemMenuItem)MenuItems[CurrentIndex];
        ItemImage.sprite = imi.Slot.Equiptment.ItemImage;
        DialogueBoxControl.Instance.PrintText(imi.Slot.ToString(), timePerChar: 0);
    }
}
