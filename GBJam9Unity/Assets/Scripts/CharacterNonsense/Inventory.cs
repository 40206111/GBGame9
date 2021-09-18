using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public EquiptmentLayout EquippedItems;
    public List<EquiptmentSlot> PocketItems;

    public List<EquiptmentSlot> GetAllItems()
    {
        List<EquiptmentSlot> outList = EquippedItems.GetAllEquipped();
        outList.AddRange(PocketItems);
        return outList;
    }

    public void AddItem(ItemDetails item)
    {
        EquiptmentSlot es = PocketItems.Find(x => x.Equiptment == null);
        es.Equiptment = item;
    }

    public void RemoveItem(ItemDetails item)
    {
        EquiptmentSlot es = PocketItems.Find(x => x.Equiptment == item);
        if (es != null)
        {
            es.Equiptment = null;
        }
    }

    public void EquipItem(ItemDetails item, eItemType slotType, int trinketSlot = 0)
    {
        EquiptmentSlot slot = EquippedItems.GetEquiptmentSlot(slotType, trinketSlot);
        if (slot.HasEquiptment)
        {
            AddItem(slot.Equiptment);
            slot.Equiptment = null;
        }
        slot.Equiptment = item;
        RemoveItem(item);
    }
}
