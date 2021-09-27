using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public EquiptmentLayout EquippedItems = new EquiptmentLayout();
    public List<EquiptmentSlot> PocketItems = new List<EquiptmentSlot>();

    public List<EquiptmentSlot> GetAllItems()
    {
        List<EquiptmentSlot> outList = EquippedItems.GetAllEquipped();
        outList.AddRange(GetPocketItems());
        return outList;
    }

    public List<EquiptmentSlot> GetPocketItems()
    {
        StripEmptySlots();
        return PocketItems;
    }

    public void StripEmptySlots()
    {
        for (int i = 0; i < PocketItems.Count; ++i)
        {
            if (!PocketItems[i].HasEquiptment)
            {
                PocketItems.RemoveAt(i);
                --i;
            }
        }
    }

    public void AddItem(ItemDetails item)
    {
        if (item.ItemType == eItemType.chestPiece)
        {
            PlayerData.MeleeChicken.EquippedItems.EquipItem(item, item.ItemType);
        }

        EquiptmentSlot es = PocketItems.Find(x => x.Equiptment == null);
        if (es != null)
        {
            es.Equiptment = item;
        }
        else
        {
            PocketItems.Add(new EquiptmentSlot());
            PocketItems[PocketItems.Count - 1].AddItem(item);
        }
    }

    public void AddItem(EquiptmentSlot slot)
    {
        if (slot.Equiptment.ItemType == eItemType.chestPiece)
        {
            PlayerData.MeleeChicken.EquippedItems.EquipItem(slot.Equiptment, slot.Equiptment.ItemType);
        }
        EquiptmentSlot currentSlot = PocketItems.Find(x => x.Equiptment == slot.Equiptment);

        if(currentSlot != null)
        {
            currentSlot.AddToCount(slot.Count);
        }
        else
        {
            PocketItems.Add(slot);
        }        
    }

    public void RemoveItem(ItemDetails item)
    {
        EquiptmentSlot es = PocketItems.Find(x => x.Equiptment == item);
        if (es != null)
        {
            es.Equiptment = null;
        }
    }
}
