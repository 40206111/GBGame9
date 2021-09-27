using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenuManager : PopulatingMenuManager
{
    [SerializeField]
    protected Image ItemImage;
    [SerializeField]
    protected Sprite BackupSprite;

    public override void PopulateMenu(List<EquiptmentSlot> inDetails)
    {
        base.PopulateMenu(inDetails);
        UpdateInventoryVisuals();
    }

    protected override void ChangeSelectedMenuItem(int change, bool force = false)
    {
        int start = CurrentIndex;
        base.ChangeSelectedMenuItem(change, force);
        if (start != CurrentIndex)
        {
            UpdateInventoryVisuals();
        }
    }

    protected virtual void UpdateInventoryVisuals()
    {
        ItemMenuItem imi = (ItemMenuItem)MenuItems[CurrentIndex];
        Sprite sprite = imi.Slot.Equiptment.ItemImage;
        if (sprite == null)
        {
            sprite = BackupSprite;
        }
        ItemImage.sprite = sprite;
        DialogueBoxControl.Instance.PrintText(imi.Slot.Equiptment.ToString(), timePerChar: 0);
    }
}
