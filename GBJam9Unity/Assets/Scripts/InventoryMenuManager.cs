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
        int start = CurrentIndex;
        base.ChangeSelectedMenuItem(change, force);
        if (start != CurrentIndex)
        {
            ItemMenuItem imi = (ItemMenuItem)MenuItems[CurrentIndex];
            ItemImage.sprite = imi.Slot.Equiptment.ItemImage;
            DialogueBoxControl.Instance.PrintText(imi.Slot.Equiptment.ToString(), timePerChar: 0);
        }
    }
}
