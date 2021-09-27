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
        if (MenuItems.Count == 0)
        {
            DialogueBoxControl.Instance.PrintText("It's empty...");
        }
        else
        {
            UpdateInventoryVisuals();
        }
    }

    protected override void ChangeSelectedMenuItem(int change, bool force = false)
    {
        int start = CurrentIndex;
        base.ChangeSelectedMenuItem(change, force);
        if (CurrentIndex != start)
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

    public override void FeedbackResponse(eLittleFeedback feedback)
    {
        switch (feedback)
        {
            case eLittleFeedback.Wear:

                Debug.Log($"Need to wear {((ItemMenuItem)MenuItems[CurrentIndex]).Slot.Equiptment.Name}");
                //PlayerData.GetChickenData(PlayerData.ActiveChicken).EquippedItems.EquipItem()
                break;
            case eLittleFeedback.Data:
                Debug.Log($"Need to give info on {((ItemMenuItem)MenuItems[CurrentIndex]).Slot.Equiptment.Name}");
                break;
        }
    }
}
